// Author:					
// Created:				    2011-06-09
// Last Modified:			2018-10-12
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

		public bool UsejPlayerForMediaTeasers { get; set; } = true;

		public bool UseAltCartList { get; set; } = false;
		public string EmptyCartFormat { get; set; } = "<div class='alert alert-info'>{0} <a class='alert-link' href='{1}'>{2}</a></div>";
		public string ContinueShoppingLinkFormat { get; set; } = "<a href='{0}' class='keepshopping store-cart-keepshopping btn btn-info'>{1}</a>";
		public string StartShoppingLinkFormat { get; set; } = "<a href='{0}' class='store-cart-startshopping btn btn-info'>{1}</a>";
		public string ConfirmOrderLinkFormat { get; set; } = "<a href='{0}' class='checkoutlink store-cart-confirmorder btn btn-success'>{1}</a>";
		public string LoginToCheckoutCssClass { get; set; } = "store-cart-login btn btn-info";
		public string CartCheckoutActionsDivClass { get; set; } = "row";
		public string CartCheckoutActionsEmptyCartOutsideTopMarkup { get; set; } = "";
		public string CartCheckoutActionsEmptyCartOutsideBottomMarkup { get; set; } = "";
		public string CartCheckoutDiscountDivCssClass { get; set; } = "settingrow discountcode store-cart-discountcode";
		public string CartCheckoutLinksDivCssClass { get; set; } = "settingrow checkoutlinks store-cart-checkoutlinks";
		public string CartCheckoutLinksDivOutsideTopMarkup { get; set; } = "";
		public string CartCheckoutLinksDivOutsideBottomMarkup { get; set; } = "";
		public string CartCheckoutLinksDivAnonymousExtraCssClass { get; set; } = "store-cart-checkoutlinks-anon";
		public string CartCheckoutLinksDivAnonymousOutsideTopMarkup { get; set; } = "";
		public string CartCheckoutLinksDivAnonymousOutsideBottomMarkup { get; set; } = "";
		public string CartPayPalDivCssClass { get; set; } = "settingrow paypalrow store-cart-paypal";
		public string CartPayPalDivOutsideTopMarkup { get; set; } = "";
		public string CartPayPalDivOutsideBottomMarkup { get; set; } = "";
		public string CartSubTotalFormat { get; set; } = "<div class='settingrowtight carttotalwrapper storerow subtotal'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartTotalFormat { get; set; } = "<div class='settingrowtight carttotalwrapper storerow total'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartDiscountTotalFormat { get; set; } = "<div class='settingrowtight carttotalwrapper storerow discount'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string CartShippingTotalFormat { get; set; } = "<div class='settingrowtight carttotalwrapper storerow shipping'><label class='settinglabeltight storelabel'>{0}</label> {1}</div>";
		public string AdditionalBodyClass { get; set; } = string.Empty;
		public string ProductDetailsRatingPanelDivCssClass { get; set; } = "productratingwrapper";
		public string ProductDetailsOffersDivCssClass { get; set; } = "clearpanel offerspanel";
		public string AddToCartButtonCssClass { get; set; } = "store-btn-addtocart btn btn-success";
		public string CartTableCssClass { get; set; } = "cartgrid table table-bordered table-striped";
		public string OfferTableCssClass { get; set; } = "offergrid table table-bordered table-striped";
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