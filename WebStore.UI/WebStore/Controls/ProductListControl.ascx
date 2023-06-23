<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="ProductListControl.ascx.cs" Inherits="WebStore.UI.ProductListControl" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<webstore:WebStoreDisplaySettings id="displaySettings" runat="server" />
<asp:Repeater ID="rptProducts" runat="server" >
    <ItemTemplate>
        <div class="hproduct hreview productcontainer store-product">
            <%# GetProductLink(Eval("Guid").ToString(), Eval("Url").ToString(), Eval("Name").ToString(), heading: true) %>
            <%--<portal:mojoRating runat="server" ID="Rating" ContentGuid='<%# Eval("Guid") %>' Visible='<%# (EnableRatings && Convert.ToBoolean(Eval("EnableRating"))) %>' AllowFeedback='<%# EnableRatingComments %>' ShowPrompt="true" PromptText='<%# Resources.WebStoreResources.RatingPrompt %>'  />--%>
            <div class="description"><%# Eval("Abstract") %></div>
            <%# GetProductLink(Eval("Guid").ToString(), Eval("Url").ToString(), Resources.WebStoreResources.ProductDetailsLink, heading: false) %>
            <asp:HyperLink ID="lnkPreview" CssClass="previewlink" runat="server" Visible='<%# (Eval("TeaserFile").ToString().Length > 0) %>' Text='<%# Eval("TeaserFileLink") %>' NavigateUrl='<%# teaserFileBaseUrl + Eval("TeaserFile") %>' />
			<asp:Repeater ID="rptImages" runat="server">
				<HeaderTemplate>
					<div class="productimages">
				</HeaderTemplate>
				<FooterTemplate>
					</div>
				</FooterTemplate>
				<ItemTemplate>
					<img src='<%# Eval("ImageUrl").ToString() %>' alt='<%# Eval("Alt").ToString() %>' title='<%# Eval("Title").ToString() %>' />
				</ItemTemplate>
			</asp:Repeater>
			<div class="productoffers">
            <asp:Repeater id="rptOffers" runat="server">
				<ItemTemplate>
					<div class="offercontainer"><%# GetOfferLink(Eval("Guid").ToString(), Eval("Url").ToString(), Eval("ProductListName").ToString(), Convert.ToBoolean(Eval("ShowDetailLink"))) %>
						<span class="price"><%# string.Format(CurrencyCulture, "{0:c}",Convert.ToDecimal(Eval("Price"))) %></span>
						<a class="addtocartlink" rel="nofollow" href='<%# SiteRoot + "/WebStore/CartAdd.aspx?offer=" + Eval("Guid") + "&pageid=" + PageId.ToString() + "&mid=" + ModuleId.ToString() %>'>
							<span class="linktext"><%# Resources.WebStoreResources.AddToCartLink%></span></a>
					</div>
				</ItemTemplate>
            </asp:Repeater>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<portal:mojoCutePager ID="pgr" runat="server" />
    