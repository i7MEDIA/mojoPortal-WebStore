// Author:					
// Created:				    2011-06-09
// Last Modified:			2011-06-09
// 
// The use and distribution terms for this software are covered by the 
// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)
// which can be found in the file CPL.TXT at the root of this distribution.
// By using this software in any fashion, you are agreeing to be bound by 
// the terms of this license.
//
// You must not remove this notice, or any other, from this software.
//
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.UI
{
    /// <summary>
    /// this control doesn't render anything, it is used only as a themeable collection of settings for things we would like to be able to configure from theme.skin
    /// </summary>
    public class WebStoreDisplaySettings : WebControl
    {

        private bool usejPlayerForMediaTeasers = true;

        public bool UsejPlayerForMediaTeasers
        {
            get { return usejPlayerForMediaTeasers; }
            set { usejPlayerForMediaTeasers = value; }
        }

        private bool useAltCartList = false;

        public bool UseAltCartList
        {
            get { return useAltCartList; }
            set { useAltCartList = value; }
        }

		private string emptyCartFormat = "<div class='alert alert-info'>{0} <a class='alert-link' href='{1}'>{2}</a></div>";
		public string EmptyCartFormat
		{
			get => emptyCartFormat;
			set => emptyCartFormat = value;
		}

		private string continueShoppingLinkFormat = "<a href='{0}' class='keepshopping store-cart-keepshopping btn btn-info'>{1}</a>";
		public string ContinueShoppingLinkFormat
		{
			get => continueShoppingLinkFormat;
			set => continueShoppingLinkFormat = value;
		}

		private string startShoppingLinkFormat = "<a href='{0}' class='store-cart-startshopping btn btn-info'>{1}</a>";
		public string StartShoppingLinkFormat
		{
			get => startShoppingLinkFormat;
			set => startShoppingLinkFormat = value;
		}

		private string confirmOrderLinkFormat = "<a href='{0}' class='checkoutlink store-cart-confirmorder btn btn-success'>{1}</a>";
		public string ConfirmOrderLinkFormat
		{
			get => confirmOrderLinkFormat;
			set => confirmOrderLinkFormat = value;
		}

		private string loginToCheckoutCssClass = "store-cart-login btn btn-info";
		public string LoginToCheckoutCssClass
		{
			get => loginToCheckoutCssClass;
			set => loginToCheckoutCssClass = value;
		}

		private string cartCheckoutDiscountDivCssClass = "settingrow discountcode store-cart-discountcode";
		public string CartCheckoutDiscountDivCssClass
		{
			get => cartCheckoutDiscountDivCssClass;
			set => cartCheckoutDiscountDivCssClass = value;
		}

		private string cartCheckoutLinksDivCssClass = "settingrow checkoutlinks store-cart-checkoutlinks";
		public string CartCheckoutLinksDivCssClass
		{
			get => cartCheckoutLinksDivCssClass;
			set => cartCheckoutLinksDivCssClass = value;
		}

		private string cartCheckoutLinksDivAnonymousExtraCssClass = "store-cart-checkoutlinks-anon";
		public string CartCheckoutLinksDivAnonymousExtraCssClass
		{
			get => cartCheckoutLinksDivAnonymousExtraCssClass;
			set => cartCheckoutLinksDivAnonymousExtraCssClass = value;
		}

		private string cartPayPalDivCssClass = "settingrow paypalrow store-cart-paypal";
		public string CartPayPalDivCssClass
		{
			get => cartPayPalDivCssClass;
			set => cartPayPalDivCssClass = value;
		}

		private string cartSubTotalFormat = "<div class='settingrowtight carttotalwrapper storerow subtotal'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartSubTotalFormat
		{
			get => cartSubTotalFormat;
			set => cartSubTotalFormat = value;
		}

		private string cartTotalFormat = "<div class='settingrowtight carttotalwrapper storerow total'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartTotalFormat
		{
			get => cartTotalFormat;
			set => cartTotalFormat = value;
		}

		private string cartDiscountTotalFormat = "<div class='settingrowtight carttotalwrapper storerow discount'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartDiscountTotalFormat
		{
			get => cartDiscountTotalFormat;
			set => cartDiscountTotalFormat = value;
		}

		private string cartShippingTotalFormat = "<div class='settingrowtight carttotalwrapper storerow shipping'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartShippingTotalFormat
		{
			get => cartShippingTotalFormat;
			set => cartShippingTotalFormat = value;
		}

		private string additionalBodyClass = string.Empty;
		public string AdditionalBodyClass
		{
			get => additionalBodyClass;
			set => additionalBodyClass = value;
		}

		private string productDetailsRatingPanelDivCssClass = "productratingwrapper";
		public string ProductDetailsRatingPanelDivCssClass
		{
			get => productDetailsRatingPanelDivCssClass;
			set => productDetailsRatingPanelDivCssClass = value;
		}

		//private string addToCartButtonCssClass = "addtocartbutton jqbutton ui-button ui-widget ui-state-default ui-corner-all";
		private string addToCartButtonCssClass = "store-btn-addtocart btn btn-success";
		public string AddToCartButtonCssClass
		{
			get => addToCartButtonCssClass;
			set => addToCartButtonCssClass = value;
		}
		protected override void Render(HtmlTextWriter writer)
        {
            if (HttpContext.Current == null)
            {
                writer.Write("[" + this.ID + "]");
                return;
            }

            // nothing to render
        }
    }
}