/// Author:					
/// Created:				2007-02-13
/// Last Modified:		    2017-10-06
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web;
using mojoPortal.Web.UI;
using mojoPortal.Web.Framework;
using mojoPortal.Business;
using mojoPortal.Business.Commerce;
using mojoPortal.Business.WebHelpers.PaymentGateway;
using WebStore.Business;
using Resources;
using WebStore.Helpers;
//using GCheckout.Checkout;
//using GCheckout.Util;

namespace WebStore.UI
{

    public partial class CartPage : NonCmsBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        private Store store = null;
        private CommerceConfiguration commerceConfig = null;
        private SiteUser siteUser = null;
        private Cart cart;
        protected CultureInfo currencyCulture = CultureInfo.CurrentCulture;
        private bool canCheckoutWithoutAuthentication = false;
        //private Module module = null;
        private Hashtable moduleSettings = null;
        private WebStoreConfiguration config = new WebStoreConfiguration();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUtils.SslIsAvailable()) { SiteUtils.ForceSsl(); }

            LoadParams();
            if (!UserCanViewPage(moduleId, Store.FeatureGuid))
            {
                SiteUtils.RedirectToAccessDeniedPage(this);
                return;
            }
            LoadSettings();

            if ((store == null) || (store.IsClosed))
            {
                WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
                return;
            }
            
            PopulateLabels();
            ShowCart();

            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsWebStoreSection", "store");
        }

        private void ShowCart()
        {
            if (Page.IsPostBack) { return; }

            if (store != null && cart != null && cart.CartOffers.Count != 0)
            {
				bool showTotal = false;

				litSubTotal.Text = String.Format(displaySettings.CartSubTotalFormat, WebStoreResources.CartSubTotalLabel, cart.SubTotal.ToString("c", currencyCulture));

				if (cart.Discount > 0)
				{
					showTotal = true;
					decimal discount = -cart.Discount;
					litDiscount.Text = String.Format(displaySettings.CartDiscountTotalFormat, WebStoreResources.CartDiscountTotalLabel, discount.ToString("c", currencyCulture));
				}
				
				if (cart.ShippingTotal > 0)
				{
					showTotal = true;
					litShippingTotal.Text = String.Format(displaySettings.CartShippingTotalFormat, WebStoreResources.CartShippingTotalLabel, cart.ShippingTotal.ToString("c", currencyCulture));
				}

				if (showTotal)
				{
					decimal totalWithoutTax = cart.SubTotal - cart.Discount + cart.ShippingTotal;
					litTotal.Text = String.Format(displaySettings.CartTotalFormat, WebStoreResources.CartTotalLabel, totalWithoutTax.ToString("c", currencyCulture));
				}
            }
			else
			{
				pnlCartItems.Visible = false;
				//pnlSubTotal.Visible = false;
				//pnlDiscountAmount.Visible = false;
				//pnlTotal.Visible = false;
				pnlDiscountCode.Visible = false;
				litEmptyCart.Text = String.Format(displaySettings.EmptyCartFormat, WebStoreResources.EmptyCartMessage, SiteUtils.GetCurrentPageUrl(), WebStoreResources.EmptyCartGoShopping);
				litKeepShopping.Text = String.Format(displaySettings.StartShoppingLinkFormat, SiteUtils.GetCurrentPageUrl(), WebStoreResources.StartShopping);
				pnlCheckoutActions.OutsideTopMarkup = displaySettings.CartCheckoutActionsEmptyCartOutsideTopMarkup;
				pnlCheckoutActions.OutsideBottomMarkup = displaySettings.CartCheckoutActionsEmptyCartOutsideBottomMarkup;
			}
        }

        void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            if(StoreHelper.ApplyDiscount(store, cart, txtDiscountCode.Text, out errorMessage))
            {
                WebUtils.SetupRedirect(this, Request.RawUrl);
                return;
            }
            lblDiscountError.Text = errorMessage;
        }

        private void SetupPayPalStandardForm()
        {
            if (cart == null) { return; }
            //if (siteUser == null) { return; }

            if (cart.UserGuid == Guid.Empty)
            {
                if (siteUser != null)
                {
                    cart.UserGuid = siteUser.UserGuid;
                    cart.Save();
                }
            }

            //if ((cart.CartOffers.Count == 0)||(cart.OrderTotal == 0))
            //{
            //    litOr.Visible = false;
            //    btnPayPal.Visible = false;
            //    return; 
            //}

            PayPalLog payPalLog = StoreHelper.EnsurePayPalStandardCheckoutLog(cart, store, SiteRoot, pageId, moduleId);
            if (payPalLog == null) return;

            litPayPalFormVariables.Text = StoreHelper.GetCartUploadFormFields(
                payPalLog.RowGuid,
                cart,
                store,
                commerceConfig);

            btnPayPal.PostBackUrl = commerceConfig.PayPalStandardUrl;
        }

        void btnPayPal_Click(object sender, ImageClickEventArgs e)
        {
            // with paypal standard we set the postbackurl on the button
            // so this code is never executed
            if (commerceConfig.PayPalUsePayPalStandard)
            {
                DoPayPalStandardCheckout();
            }
            else
            {
                DoPayPalExpressCeckout();
            }
        }
		
        private void DoPayPalExpressCeckout()
        {
            PayPalExpressGateway gateway
                = new PayPalExpressGateway(
                    commerceConfig.PayPalAPIUsername,
                    commerceConfig.PayPalAPIPassword,
                    commerceConfig.PayPalAPISignature,
                    commerceConfig.PayPalStandardEmailAddress);

            gateway.UseTestMode = commerceConfig.PaymentGatewayUseTestMode;

            gateway.MerchantCartId = cart.CartGuid.ToString();
            gateway.ChargeTotal = cart.OrderTotal;

            string siteRoot = SiteUtils.GetNavigationSiteRoot();
            gateway.ReturnUrl = siteRoot + "/Services/PayPalReturnHandler.ashx";
            gateway.CancelUrl = siteRoot + Request.RawUrl;
            //Currency currency = new Currency(store.DefaultCurrencyId);
            //gateway.CurrencyCode = currency.Code;
            gateway.CurrencyCode = siteSettings.GetCurrency().Code;
            gateway.OrderDescription = store.Name + " " + WebStoreResources.OrderHeading;

            gateway.BuyerEmail = cart.OrderInfo.CustomerEmail;
            gateway.ShipToFirstName = cart.OrderInfo.DeliveryFirstName;
            gateway.ShipToLastName = cart.OrderInfo.DeliveryLastName;
            gateway.ShipToAddress = cart.OrderInfo.DeliveryAddress1;
            gateway.ShipToAddress2 = cart.OrderInfo.DeliveryAddress2;
            gateway.ShipToCity = cart.OrderInfo.DeliveryCity;
            gateway.ShipToState = cart.OrderInfo.DeliveryState;
            gateway.ShipToCountry = cart.OrderInfo.DeliveryCountry;
            gateway.ShipToPostalCode = cart.OrderInfo.DeliveryPostalCode;
            gateway.ShipToPhone = cart.OrderInfo.CustomerTelephoneDay;

            // this tells paypal to use the shipping address we pass in
            // rather than what the customer has on file
            // when we implement shippable products we'll do shipping calculations before
            // sending the user to paypal
            //gateway.OverrideShippingAddress = true;

            //commented out the above, we want user to be able to populate shipping info from their paypal account

            bool executed = gateway.CallSetExpressCheckout();
            if (executed)
            {
                //TODO: log the raw response
                if (gateway.PayPalExpressUrl.Length > 0)
                {
                    // record the gateway.PayPalToken
                    PayPalLog payPalLog = new PayPalLog();
                    payPalLog.RawResponse = gateway.RawResponse;
                    payPalLog.ProviderName = "WebStorePayPalHandler";
                    payPalLog.ReturnUrl = siteRoot + Request.RawUrl;
                    payPalLog.Token = HttpUtility.UrlDecode(gateway.PayPalToken);
                    payPalLog.RequestType = "SetExpressCheckout";
                    
                    cart.SerializeCartOffers();
                    payPalLog.SerializedObject = SerializationHelper.SerializeToString(cart);

                    payPalLog.CartGuid = cart.CartGuid;
                    payPalLog.SiteGuid = store.SiteGuid;
                    payPalLog.StoreGuid = store.Guid;
                    payPalLog.UserGuid = cart.UserGuid;

                    payPalLog.Save();

                    Response.Redirect(gateway.PayPalExpressUrl);
                }
            }
        }

        /// <summary>
        /// This is really a fallback method, it will not be executed in normal use because we are setting the postbackurl of the button
        /// to post directly to paypal, so this method should not fire unless somehow the page is manipulated to postback to itself.
        /// In this case, we just consolidate the cart into a buy now button.
        /// </summary>
        private void DoPayPalStandardCheckout()
        {
            PayPalLog payPalLog = new PayPalLog();

            payPalLog.ProviderName = "WebStorePayPalHandler";
            payPalLog.PDTProviderName = "WebStorePayPalPDTHandlerProvider";
            payPalLog.IPNProviderName = "WebStorePayPalIPNHandlerProvider";
            payPalLog.ReturnUrl = SiteRoot +
                                "/WebStore/OrderDetail.aspx?pageid="
                                + pageId.ToInvariantString()
                                + "&mid=" + moduleId.ToInvariantString()
                                + "&orderid=" + cart.CartGuid.ToString();

            payPalLog.RequestType = "StandardCheckout";

            cart.SerializeCartOffers();
            payPalLog.SerializedObject = SerializationHelper.SerializeToString(cart);
            payPalLog.CartGuid = cart.CartGuid;
            payPalLog.SiteGuid = store.SiteGuid;
            payPalLog.StoreGuid = store.Guid;
            payPalLog.UserGuid = cart.UserGuid;
            payPalLog.CartTotal = cart.OrderTotal;
            payPalLog.CurrencyCode = siteSettings.GetCurrency().Code;

            payPalLog.Save();

            string payPalStandardUrl = StoreHelper.GetBuyNowUrl(
                payPalLog.RowGuid,
                cart,
                store,
                commerceConfig);


            WebUtils.SetupRedirect(this, payPalStandardUrl);

        }

		private void PopulateLabels()
		{
			Control c = Master.FindControl("Breadcrumbs");
			if (c != null)
			{
				BreadcrumbsControl crumbs = (BreadcrumbsControl)c;
				crumbs.ForceShowBreadcrumbs = true;

			}
			Title = SiteUtils.FormatPageTitle(siteSettings, CurrentPage.PageName + " - " + WebStoreResources.CartHeader);
			heading.Text = WebStoreResources.CartHeader;

			string confirmOrderUrl = $"{SiteRoot}/WebStore/ConfirmOrder.aspx?pageid={pageId.ToInvariantString()}&mid={moduleId.ToInvariantString()}";

			litConfirmOrder.Text = String.Format(displaySettings.ConfirmOrderLinkFormat, confirmOrderUrl, WebStoreResources.ProceedToCheckout);

			litKeepShopping.Text = String.Format(displaySettings.ContinueShoppingLinkFormat, SiteUtils.GetCurrentPageUrl(), WebStoreResources.CartKeepShoppingLink);

			pnlDiscountCode.CssClass = displaySettings.CartCheckoutDiscountDivCssClass;


			btnApplyDiscount.Text = WebStoreResources.ApplyDiscountButton;

			lblDiscountError.Text = string.Empty;

			lnkLogin.OverrideText = WebStoreResources.LoginToCheckout;
			lnkLogin.CssClass = displaySettings.LoginToCheckoutCssClass;

			pnlCheckoutActions.CssClass = displaySettings.CartCheckoutActionsDivClass;


			pnlCheckoutLinks.CssClass = displaySettings.CartCheckoutLinksDivCssClass;
			pnlCheckoutLinks.OutsideTopMarkup = displaySettings.CartCheckoutLinksDivOutsideTopMarkup;
			pnlCheckoutLinks.OutsideBottomMarkup = displaySettings.CartCheckoutLinksDivOutsideBottomMarkup;

			if (!Request.IsAuthenticated && !canCheckoutWithoutAuthentication)
			{
				pnlCheckoutLinks.CssClass += $" {displaySettings.CartCheckoutLinksDivAnonymousExtraCssClass}";
				pnlCheckoutLinks.OutsideTopMarkup = displaySettings.CartCheckoutLinksDivAnonymousOutsideTopMarkup;
				pnlCheckoutLinks.OutsideBottomMarkup = displaySettings.CartCheckoutLinksDivAnonymousOutsideBottomMarkup;
			}

			pnlPayPal.CssClass = displaySettings.CartPayPalDivCssClass;
			pnlPayPal.OutsideTopMarkup = displaySettings.CartPayPalDivOutsideTopMarkup;
			pnlPayPal.OutsideBottomMarkup = displaySettings.CartPayPalDivOutsideBottomMarkup;
		}

        private void LoadParams()
        {            
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", true, moduleId);
        }

        private void ConfigureCheckoutButtons()
        {
            bool shouldShowPayPal = ShouldShowPayPal();

            pnlPayPal.Visible = shouldShowPayPal;

            if((shouldShowPayPal)&&(commerceConfig.PayPalUsePayPalStandard))
            {
                SetupPayPalStandardForm();
            }
        }
		
        private void LoadSettings()
        {
            AddClassToBody($"webstore webstorecart {displaySettings.AdditionalBodyClass}");

            SiteUtils.AddNoIndexMeta(Page);

            commerceConfig = SiteUtils.GetCommerceConfig();
            currencyCulture = ResourceHelper.GetCurrencyCulture(siteSettings.GetCurrency().Code);

            moduleSettings = ModuleSettings.GetModuleSettings(moduleId);
            if (moduleSettings != null)
            {
                config = new WebStoreConfiguration(moduleSettings);
            }

            litCartFooter.Text = config.CartPageFooter;

            if (Request.IsAuthenticated)
            {
                siteUser = SiteUtils.GetCurrentSiteUser();
            }

            store = StoreHelper.GetStore();
            if (store == null) { return; }
           
            if (
                (StoreHelper.UserHasCartCookie(store.Guid))
                || (Request.IsAuthenticated)
                )
            {
                cart = StoreHelper.GetCart();
            }

            //if we can't process cards internally there is no reason (except a free order) to go to the ConfirmOrder.aspx page 
            //and the order can be processed wtithout the user signing in or if the user is already signed in
            if (
                ((!commerceConfig.CanProcessStandardCards)&&(commerceConfig.WorldPayInstallationId.Length == 0))
                //&& ((Request.IsAuthenticated) || (canCheckoutWithoutAuthentication)) //moved login prompt to cart so we don't need to have a link to the confirmorder page
                )
            {
				//lnkConfirmOrder.Visible = false;
				litConfirmOrder.Visible = false;
            }

            if (cart == null) 
            {
                pnlDiscountCode.Visible = false; 
                //lnkConfirmOrder.Visible = false;
				litConfirmOrder.Visible = false;
                return; 
            }

            if ((cart.LastModified < DateTime.UtcNow.AddDays(-1)) && (cart.DiscountCodesCsv.Length > 0))
            {
                StoreHelper.EnsureValidDiscounts(store, cart);
            }

            if (store != null)
            {
                canCheckoutWithoutAuthentication = store.CanCheckoutWithoutAuthentication(cart);
            }

            cartList.Store = store;
            cartList.ShoppingCart = cart;
            cartList.CurrencyCulture = currencyCulture;
			cartList.DisplaySettings = displaySettings;

            cartListAlt.Store = store;
            cartListAlt.ShoppingCart = cart;
            cartListAlt.CurrencyCulture = currencyCulture;

            if (displaySettings.UseAltCartList)
            {
                cartList.Visible = false;
                cartListAlt.Visible = true;
            }

            ConfigureCheckoutButtons();

            int countOfDiscountCodes = Discount.GetCountOfActiveDiscountCodes(store.ModuleGuid);
            pnlDiscountCode.Visible = (countOfDiscountCodes > 0);

            // don't show the discount code panel if the cart is empty
            if (cart.SubTotal == 0) 
            {
                // allow checkout if cart has items (support checkout with free items)
                if (cart.CartOffers.Count == 0)
                {
                    //lnkConfirmOrder.Visible = false;
					litConfirmOrder.Visible = false;
                }
                else
                {
                    //cart has free items
                    //lnkConfirmOrder.Visible = true;
                }
                //litOr.Visible = false;
                //btnPayPal.Visible = false;
                pnlDiscountCode.Visible = false; 
            }

            // kill switch to disable discount codes (doesn't prevent use of ones already in the cart but prevents new uses
            bool disableDiscounts = false;
            ConfigHelper.GetBoolProperty("WebStoreDisabledDiscounts", disableDiscounts);
            if (disableDiscounts) { pnlDiscountCode.Visible = false; }
        }

        private bool ShouldShowPayPal()
        {
            if (store == null) { return false; }
            if (cart == null) { return false; }
            if (cart.SubTotal == 0) { return false; }
            if (cart.OrderTotal == 0) { return false; }
            if (commerceConfig == null) { return false; }
            if ((!Request.IsAuthenticated) && (!canCheckoutWithoutAuthentication)) { return false; }
            if (!commerceConfig.PayPalIsEnabled) { return false; }
			
            return true;
        }

        #region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.Load += new EventHandler(this.Page_Load);

            btnPayPal.Click += new ImageClickEventHandler(btnPayPal_Click);
            btnApplyDiscount.Click += new EventHandler(btnApplyDiscount_Click);

            SuppressPageMenu();
            SuppressGoogleAds();
        }
        #endregion
    }
}
