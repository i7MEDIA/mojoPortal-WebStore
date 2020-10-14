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
		public string CartLinkFormat { get; set; } = "<div class='settingrow wrapstorelink'><a href='{0}' class='store-cart-link'>{1}</a></div>";
		public string ConfirmOrderLinkFormat { get; set; } = "<a href='{0}' class='checkoutlink store-cart-confirmorder btn btn-success'>{1}</a>";
		public string LoginToCheckoutCssClass { get; set; } = "store-cart-login btn btn-info";
		public string CartListTopMarkup { get; set; } = "<ul class='cartlist'>";
		public string CartListBottomMarkup { get; set; } = "</ul>";
		public string CartListItemTopMarkup { get; set; } = "<li class='cartitem'>";
		public string CartListItemBottomMarkup { get; set; } = "</li>";
		public string CartListItemNameTopMarkup { get; set; } = "<div class='cartitem'>";
		public string CartListItemNameBottomMarkup { get; set; } = "</div>";
		public string CartListItemPriceTopMarkup { get; set; } = "<div class='cartprice'>";
		public string CartListItemPriceBottomMarkup { get; set; } = "</div>";
		public string CartListItemQtyTopMarkup { get; set; } = "<div class='cartquantity'>";
		public string CartListItemQtyBottomMarkup { get; set; } = "</div>";
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
		public string UpdateQtyButtonCssClass { get; set; } = "store-btn-qty btn btn-success";
		public string DeleteButtonCssClass { get; set; } = "store-btn-delete btn btn-danger";
		public string CartTableTopMarkup { get; set; } = "<table class='cartgrid table table-bordered table-striped'>";
		public string CartTableBottomMarkup { get; set; } = "</table>";
		public string CartItemActionsTopMarkup { get; set; } = "<div class='btn-group btn-group-sm'>";
		public string CartItemActionsBottomMarkup { get; set; } = "</div>";
		public string OfferTableCssClass { get; set; } = "offergrid table table-bordered table-striped";
		public string StoreDescriptionMarkupTop { get; set; } = "<div class='settingrow storedescription'>";
		public string StoreDescriptionMarkupBottom { get; set; } = "</div>";
		public string ProductListProductHeadingLinkFormat { get; set; } = "<h4><a class='fn url productlink' href='{0}'>{1}</a> {2}</h4>";
		public string ProductEditLinkFormat { get; set; } = " <span class='modulelinks'><a href='{0}' title='{1}'>{2}</a></span>";
		public string ProductOfferEditLinkFormat { get; set; } = " <span class='modulelinks'><a href='{0}' title='{1}'>{2}</a></span>";
		public string ProductListProductLinkFormat { get; set; } = "<a class='productdetaillink' href='{0}'>{1}</a>";
		public string ProductListOfferLinkFormat { get; set; } = "<a class='offerdetaillink' href='{0}'><span class='productname'>{1}</span></a> {2}";
		public string ProductListOfferTitleFormat { get; set; } = "<span class='productname'>{0}</span> {1}";
		public string ProductDetailDescriptionMarkupTop { get; set; } = "<div class='description settingrow'>";
		public string ProductDetailDescriptionMarkupBottom { get; set; } = "</div>";
		public string ProductDetailOfferListMarkupTop { get; set; } = "<div class='offerspanel'>";
		public string ProductDetailOfferListMarkupBottom { get; set; } = "</div>";
		public string ProductDetailOfferMarkupTop { get; set; } = "<div class='offercontainer form-group store-product-offer col-md-4'>";
		public string ProductDetailOfferMarkupBottom { get; set; } = "</div>";
		public string ProductDetailImageListMarkupTop { get; set; } = "<div class='productimages settingrow'>";
		public string ProductDetailImageListMarkupBottom { get; set; } = "</div>";
		public string ProductDetailImageFormat { get; set; } = "<figure><img src='{0}' alt='{1}' /><figcaption>{2}</figcaption><figure>";
		public string ProductDetailOfferNameFormat { get; set; } = "<h4 class='store-product-offer-name'>{0}</h4>";
		public string ProductDetailOfferNameLinkFormat { get; set; } = "<h4 class='store-product-offer-name'><a href='{0}' title='{1}'>{2}</a></h4>";

		public string ProductDetailOfferPriceFormat { get; set; } = "<div class='price store-product-offer-price'>{0}</div>";
		public string ProductDetailOfferAddToCartBoxMarkupTop { get; set; } = "<div class='input-group input-group-lg store-product-offer-qty'>";
		public string ProductDetailOfferAddToCartBoxMarkupBottom { get; set; } = "</div>";
		public string ProductDetailOfferQtyLabelFormat { get; set; } = "<div class='input-group-addon store-product-offer-qty'>{0}</div>";
		public string ProductDetailOfferAddToCartBoxButtonWrapTop { get; set; } = "<span class='input-group-btn'>";
		public string ProductDetailOfferAddToCartBoxButtonWrapBottom { get; set; } = "</span>";
		public string ProductDetailOfferAbstractMarkupTop { get; set; } = "<div class='store-product-offer-abstract'>";
		public string ProductDetailOfferAbstractMarkupBottom { get; set; } = "</div>";
		public string ImagePickerModalLinkCssClass { get; set; } = "input-group-addon";
		public string ImagePickerModalLinkText { get; set; } = "&hellip;";
		public string ModalLinkMarkup { get; set; } = "<a href='{0}' data-target='{0}' data-toggle='modal' class='{1}'>{2}</a>";
		public string ModalMarkup { get; set; } = @"<div class='modal fade' id='{0}' tabindex='-1' role='dialog'>
						<div class='modal-dialog' role='document'>
						<div class='modal-content'>
							<div class='modal-header'>
							<button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>
							<h4 class='modal-title'>{1}</h4>
							</div>
							<div class='modal-body'>
								<iframe src='{2}' width='100%' height='100%' border='0'></iframe>
							</div>
						</div><!-- /.modal-content -->
						</div><!-- /.modal-dialog -->
					</div><!-- /.modal -->";


		public string AdminPanelHeadingMarkup { get; set; } = "<h3>{0} <small>{1}</small></h3>";
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