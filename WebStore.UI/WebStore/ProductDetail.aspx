<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ProductDetail.aspx.cs" Inherits="WebStore.UI.ProductDetailPage" %>
<%@ Register Src="~/WebStore/Controls/CartLink.ascx" TagPrefix="ws" TagName="CartLink" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<ws:CartLink ID="lnkCart" runat="server" EnableViewState="false" />
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="hproduct hreview  panelwrapper webstore webstoreofferdetail">
        <portal:HeadingControl ID="heading" runat="server" CssClass="fn" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">  
        <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent productdetail">
            <webstore:WebStoreDisplaySettings id="displaySettings" runat="server" />
			<portal:mojoRating ID="Rating" runat="server" Visible="false"/>
			
			<asp:Literal ID="litDescription" runat="server" EnableViewState="false" />

			<asp:Repeater ID="rptImages" runat="server">
				<headertemplate>
					<%# displaySettings.ProductDetailImageListMarkupTop %>
				</headertemplate>
				<footertemplate>
					<%# displaySettings.ProductDetailImageListMarkupBottom %>
				</footertemplate>
				<itemtemplate>
					<%# string.Format(displaySettings.ProductDetailImageFormat, Eval("ImageUrl").ToString(), Eval("Alt").ToString(), Eval("Title").ToString()) %>
				</itemtemplate>
			</asp:Repeater>
			
			<asp:Repeater ID="rptOffers" runat="server">
				<HeaderTemplate> 
					<%# displaySettings.ProductDetailOfferListMarkupTop %>
                </HeaderTemplate>
				<footertemplate>
					<%# displaySettings.ProductDetailOfferListMarkupBottom %>
                </footertemplate>
				<ItemTemplate>
					<%# displaySettings.ProductDetailOfferMarkupTop %>
						<%# GetOfferTitle(Convert.ToBoolean(Eval("ShowDetailLink")), Eval("ProductListName").ToString(), Eval("Url").ToString()) %>
						
					<%# displaySettings.ProductDetailOfferAbstractMarkupTop + Eval("Abstract").ToString() + displaySettings.ProductDetailOfferAbstractMarkupBottom%>


						<%# string.Format(displaySettings.ProductDetailOfferPriceFormat,
								string.Format(currencyCulture, "{0:c}",Convert.ToDecimal(Eval("Price")))) %>
						<asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtQuantity" MaximumValue='<%# Eval("MaxPerOrder").ToString() %>' Enabled="false" CssClass="help-block" ValidationGroup='<%# Eval("Guid").ToString()%>'/>
						<%# displaySettings.ProductDetailOfferAddToCartBoxMarkupTop %>
							<%# string.Format(displaySettings.ProductDetailOfferQtyLabelFormat, Resources.WebStoreResources.CartQuantityHeading) %>
							<asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="form-control" TextMode="Number" Max='<%# Eval("MaxPerOrder").ToString() %>' Min="1" CausesValidation="true"/>
							<%# displaySettings.ProductDetailOfferAddToCartBoxButtonWrapTop %><asp:Button ID="btnAddToCart" runat="server" Text='<%# Resources.WebStoreResources.AddToCartLink%>' CommandName="addToCart" CommandArgument='<%# Eval("Guid") %>' CausesValidation="true" ValidationGroup='<%# Eval("Guid").ToString()%>' CssClass="<%# displaySettings.AddToCartButtonCssClass %>" /><%# displaySettings.ProductDetailOfferAddToCartBoxButtonWrapBottom %>
						<%# displaySettings.ProductDetailOfferAddToCartBoxMarkupBottom %>						
					<%# displaySettings.ProductDetailOfferMarkupBottom %>

                </ItemTemplate>
			</asp:Repeater>
                <portal:jPlayerPanel ID="jPlayerPanel" runat="server" SkinID="WebStore" RenderPlayer="false">
					<div class="settingrow preview">
						<asp:HyperLink ID="lnkPreview" CssClass="previewlink" runat="server" Visible='false' />
					</div>
                </portal:jPlayerPanel>
        </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
        <portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
    </portal:InnerWrapperPanel>
</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
