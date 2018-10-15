<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="ProductDetail.aspx.cs" Inherits="WebStore.UI.ProductDetailPage" %>
<%@ Register Src="~/WebStore/Controls/CartLink.ascx" TagPrefix="ws" TagName="CartLink" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<div class="settingrow">
   <ws:CartLink ID="lnkCart" runat="server" EnableViewState="false" />
</div>
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="hproduct hreview  panelwrapper webstore webstoreofferdetail">
        <portal:HeadingControl ID="heading" runat="server" CssClass="fn" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">  
        <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent productdetail">
            <webstore:WebStoreDisplaySettings id="displaySettings" runat="server" />
			<portal:mojoRating ID="Rating" runat="server" Visible="false"/>
			<portal:BasePanel ID="pnlOffers" runat="server">
				<asp:Repeater ID="rptOffers" runat="server">
					<HeaderTemplate> 
                        <table class='<%# displaySettings.OfferTableCssClass %>'> 
                         <tbody> 
                    </HeaderTemplate> 
					<ItemTemplate>
						<tr class="offercontainer"> 
								<td class="productname"> 
									<%# Eval("ProductListName") %> 
									<asp:HyperLink ID="lnkOfferDetail" runat="server" EnableViewState="false" Visible='<%# Convert.ToBoolean(Eval("ShowDetailLink")) %>' NavigateUrl='<%# SiteRoot + Eval("Url") %>' Text='<%# Resources.WebStoreResources.OfferDetailLink %>' /> 
								</td> 
								<td class="price"> 
									<span class="price"><%# string.Format(currencyCulture, "{0:c}",Convert.ToDecimal(Eval("Price"))) %></span> 
								</td> 
								<td class="quantity"> 
									<asp:TextBox ID="txtQuantity" runat="server" Text="1" Columns="3" /> 
								</td> 
								<td class="addtocartbutton"> 
									<asp:Button ID="btnAddToCart" runat="server" Text='<%# Resources.WebStoreResources.AddToCartLink%>' CommandName="addToCart" CommandArgument='<%# Eval("Guid") %>' CausesValidation="false" CssClass="addtocartbutton jqbutton ui-button ui-widget ui-state-default ui-corner-all" SkinID="PrimaryButton" /> 
								</td> 
						</tr> 
							<%--<div class="offercontainer form-group store-product-offer">
								<div class="productname store-product-offer-name">
									<%# Eval("ProductListName") %>
									<asp:HyperLink ID="lnkOfferDetail" runat="server" EnableViewState="false" Visible='<%# Convert.ToBoolean(Eval("ShowDetailLink")) %>' NavigateUrl='<%# SiteRoot + Eval("Url") %>' Text='<%# Resources.WebStoreResources.OfferDetailLink %>' />
								</div>
								<div class="price store-product-offer-price"><%# string.Format(currencyCulture, "{0:c}",Convert.ToDecimal(Eval("Price"))) %></div>
								<div class="input-group store-product-offer-qty">
									<div class="input-group-addon store-product-offer-qty"><%# Resources.WebStoreResources.CartQuantityHeading %></div>
									<asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="form-control"/>
								</div>
								<asp:Button ID="btnAddToCart" runat="server" Text='<%# Resources.WebStoreResources.AddToCartLink%>' CommandName="addToCart" CommandArgument='<%# Eval("Guid") %>' CausesValidation="false" CssClass="<%# displaySettings.AddToCartButtonCssClass %>" />
							</div>--%>
                        </ItemTemplate>
						<FooterTemplate> 
                                   </tbody> 
							</table> 
                        </FooterTemplate>
				</asp:Repeater>
			</portal:BasePanel>
                <div class="description settingrow" id="divOfferDescription" runat="server" EnableViewState="false">
                    <asp:Literal ID="litDescription" runat="server" EnableViewState="false" />
                </div>
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
