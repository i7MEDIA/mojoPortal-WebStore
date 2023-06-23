/// Author:					
/// Created:				2008-10-19
/// Last Modified:			2019-10-25
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
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using mojoPortal.Business;
using WebStore.Business;
using Resources;
using WebStore.Helpers;
namespace WebStore.UI
{
    public partial class ProductDetailPage : mojoBasePage
    {
        protected int pageId = -1;
        protected int moduleId = -1;
        protected Guid productGuid = Guid.Empty;
        private Store store = null;
        private CommerceConfiguration commerceConfig;
        private Product product;
        protected CultureInfo currencyCulture = CultureInfo.CurrentCulture;
        private bool enableRatingComments = false;
        protected string teaserFileBaseUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteUtils.SslIsAvailable()) { SiteUtils.ForceSsl(); }

            LoadParams();
            if (!UserCanViewPage(moduleId))
            {
                SiteUtils.RedirectToAccessDeniedPage();
                return;
            }

            LoadSettings();
            if ((store == null)||(store.IsClosed))
            {
                WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
                return;
            }
           

            PopulateLabels();
            if (!Page.IsPostBack)
            {
                PopulateControls();
            }
            AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsWebStoreSection", "store");

        }

        private void PopulateControls()
        {
            if (product == null) { return; }

            heading.Text = Server.HtmlEncode(product.Name);
			if (UserCanEditModule(moduleId))
			{
				string editUrl = $"{SiteRoot}/WebStore/AdminProductEdit.aspx?pageid={pageId}&mid={moduleId}&prod={productGuid}";
				string editLink = string.Format(displaySettings.ProductEditLinkFormat, editUrl, WebStoreResources.ProductEditHeadingTooltip, WebStoreResources.ProductEditHeading);
				heading.LiteralExtraMarkup += editLink;
			}
            litDescription.Text = displaySettings.ProductDetailDescriptionMarkupTop + product.Description + displaySettings.ProductDetailDescriptionMarkupBottom;
            MetaDescription = product.MetaDescription;
            MetaKeywordCsv = product.MetaKeywords;
            AdditionalMetaMarkup = product.CompiledMeta;
			//pnlOffers.CssClass = displaySettings.ProductDetailsOffersDivCssClass;
            if (product.TeaserFile.Length > 0)
            {
                lnkPreview.Text = product.Name;
                lnkPreview.NavigateUrl = teaserFileBaseUrl + product.TeaserFile;
                lnkPreview.Visible = true;
				string teaserFileExt = product.TeaserFile.Substring(product.TeaserFile.LastIndexOf('.'));
				var mediaExtensions = WebConfigSettings.JPlayerVideoFileExtensions.SplitOnCharAndTrim('|');
				mediaExtensions.AddRange(WebConfigSettings.JPlayerAudioFileExtensions.SplitOnCharAndTrim('|'));

				if (mediaExtensions.Contains(teaserFileExt) && displaySettings.UsejPlayerForMediaTeasers)
                {
                    jPlayerPanel.RenderPlayer = true;
                }
            }

   //         if (product.EnableRating)
   //         {
			//	//mojoRating rating = new mojoRating();
			//	((mojoRating)Rating).ContainerCssClass = displaySettings.ProductDetailsRatingPanelDivCssClass;
			//	((mojoRating)Rating).ShowPrompt = true;
   //             ((mojoRating)Rating).ContentGuid = product.Guid;
			//	((mojoRating)Rating).AllowFeedback = enableRatingComments;
			//	((mojoRating)Rating).PromptText = WebStoreResources.RatingPrompt;
			//	((mojoRating)Rating).Visible = true;
			//}

			DataTable dtOffers = Offer.GetByProduct(product.Guid);
            rptOffers.DataSource = dtOffers;
            rptOffers.DataBind();

			rptImages.DataSource = StoreImage.GetByReference(product.Guid);
			rptImages.DataBind();
        }

        void rptOffers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "addToCart":
                default:

					//Page.Validate("Product");


					Cart cart = StoreHelper.GetCart();
                    if (cart == null) { return; }

                    string strGuid = e.CommandArgument.ToString();
                    if (strGuid.Length != 36) { return; }

                    Guid offerGuid = new Guid(strGuid);
                    Offer offer = new Offer(offerGuid);

                    int quantity = 1;
                    TextBox txtQty = e.Item.FindControl("txtQuantity") as TextBox;
					//RangeValidator rangeValidator = e.Item.FindControl("rvQuantity") as RangeValidator;

					if (txtQty != null)
                    {
                        try
                        {
                            int.TryParse(txtQty.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out quantity);
                        }
                        catch (ArgumentException) { }
                    }
                    if (quantity < 0) { quantity = 1; }

					//if (Convert.ToInt32(rangeValidator.MaximumValue) < quantity)
					//{
					//	rangeValidator.Visible = true;
					//	return;
					//}

                    if (cart.AddOfferToCart(offer, quantity))
                    {
                        // redirect to cart page
                        WebUtils.SetupRedirect(this, SiteRoot +
                            "/WebStore/Cart.aspx?pageid="
                            + pageId.ToString()
                            + "&mid=" + moduleId.ToString()
                            + "&cart=" + cart.CartGuid.ToString());

                        return;

                    }

                    WebUtils.SetupRedirect(this, Request.RawUrl);
                    
                    break;


            }

        }

		private void rptOffers_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item)
			{
				RangeValidator rangeValidator = e.Item.FindControl("rvQuantity") as RangeValidator;
				TextBox txtQty = e.Item.FindControl("txtQuantity") as TextBox;
				//Button btnAddToCart = e.Item.FindControl("btnAddToCart") as Button;
				if (Convert.ToInt32(rangeValidator.MaximumValue) != 0)
				{
					rangeValidator.Enabled = true;
					rangeValidator.ControlToValidate = txtQty.ID;
					rangeValidator.CssClass = "help-block";
					rangeValidator.ErrorMessage = string.Format(WebStoreResources.OfferMaxPerOrderExceeded, rangeValidator.MaximumValue.ToString());
				}
			}
		}
		private void PopulateLabels()
        {
            Control c = Master.FindControl("Breadcrumbs");
            if (c != null)
            {
                BreadcrumbsControl crumbs = (BreadcrumbsControl)c;
                crumbs.ForceShowBreadcrumbs = true;
                if (product != null)
                {
                    crumbs.AddedCrumbs = crumbs.ItemWrapperTop + "<a class='selectedpage' href='"
                        + Request.RawUrl
                        + "'>" + Server.HtmlEncode(product.Name)
                        + "</a>" + crumbs.ItemWrapperBottom;
                }
            }

            if (product != null)
            {
                Title = SiteUtils.FormatPageTitle(siteSettings, CurrentPage.PageName + " - " + product.Name);
                
            }


            lnkCart.PageID = pageId;
            lnkCart.ModuleID = moduleId;

        }

		protected string GetOfferTitle(bool showlink, string name, string url)
		{
			string title = string.Empty;
			if (showlink)
			{
				title = string.Format(displaySettings.ProductDetailOfferNameLinkFormat, SiteRoot + url, Resources.WebStoreResources.OfferDetailLink, name);
			}
			else
			{
				title =string.Format(displaySettings.ProductDetailOfferNameFormat, name);
			}
			return title;
		}

        private void LoadSettings()
        {
            commerceConfig = SiteUtils.GetCommerceConfig();
            store = StoreHelper.GetStore();

            if (store == null) { return; }

            //currencyCulture = ResourceHelper.GetCurrencyCulture(store.DefaultCurrency);
            currencyCulture = ResourceHelper.GetCurrencyCulture(siteSettings.GetCurrency().Code);

            if (productGuid != Guid.Empty)
            {
                product = new Product(productGuid);
                //offerPrice = offer.Price;
            }

            Hashtable Settings = ModuleSettings.GetModuleSettings(moduleId);
            enableRatingComments = WebUtils.ParseBoolFromHashtable(
                Settings, "EnableRatingCommentsSetting", enableRatingComments);

            teaserFileBaseUrl = WebUtils.GetSiteRoot() + "/Data/Sites/" + siteSettings.SiteId.ToInvariantString()
                + "/webstoreproductpreviewfiles/";

      
            if (displaySettings.UsejPlayerForMediaTeasers)
            {
                ScriptConfig.IncludejPlayer = true;
                ScriptConfig.IncludejPlayerPlaylist = true;
               
            }
            else
            {
				//wire up generic html for teasers?
				//YahooMediaPlayer was removed from mojo at version 2.8.0.7
				//ScriptConfig.IncludeYahooMediaPlayer = true;
			}

			if (pageId > -1)
			{
				PageSettings pageSettings = new PageSettings(SiteId, pageId);
				if (pageSettings != null && !String.IsNullOrWhiteSpace(pageSettings.BodyCssClass))
				{
					AddClassToBody(pageSettings.BodyCssClass.Replace("temp-ws-hide", string.Empty));
				}
			}

            AddClassToBody("webstore webstoreproductdetail");

        }

        private void LoadParams()
        {
            pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
            moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
            productGuid = WebUtils.ParseGuidFromQueryString("product", productGuid);

        }

        

			#region OnInit

        protected override void OnPreInit(EventArgs e)
        {
            AllowSkinOverride = true;
            base.OnPreInit(e);
        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(this.Page_Load);
            rptOffers.ItemCommand += new RepeaterCommandEventHandler(rptOffers_ItemCommand);
			rptOffers.ItemDataBound += new RepeaterItemEventHandler(rptOffers_ItemDataBound);
            AppendQueryStringToAction = false;
            SuppressPageMenu();
            SuppressGoogleAds();


        }
			#endregion
	}
}
