﻿<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
	CodeBehind="AdminOrderEntry.aspx.cs" Inherits="WebStore.UI.AdminOrderEntryPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
	<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">

		<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper orderentry ">
			<portal:HeadingControl ID="heading" runat="server" />
			<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
				<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
					<h3 class="heading productlistheading">
						<asp:Literal ID="litProductListHeader" runat="server" /></h3>
					<asp:Repeater ID="rptProducts" runat="server">
						<ItemTemplate>
							<div class="hproduct hreview productcontainer">
								<h4>
									<a class="fn url productlink" href='<%# SiteRoot + Eval("Url") %>'>
										<%# Eval("Name") %></a></h4>
								<a class="productdetaillink" href='<%# SiteRoot + Eval("Url") %>'>
									<%# Resources.WebStoreResources.ProductDetailsLink%></a>
								<asp:Repeater ID="rptOffers" runat="server" OnItemCommand="rptOffers_ItemCommand">
									<ItemTemplate>
										<div class="offercontainer">
											<%# Eval("Name") %>
											<asp:HyperLink ID="lnkOfferDetail" runat="server" NavigateUrl='<%# SiteRoot + Eval("Url") %>'
												Text='<%# Resources.WebStoreResources.OfferDetailLink %>' EnableViewState="false" />
											<span class="price">
												<%# string.Format(currencyCulture, "{0:c}",Convert.ToDecimal(Eval("Price"))) %></span>
											<asp:TextBox ID="txtQuantity" runat="server" Text="1" MaxLength="20" CssClass="smalltextbox" />
											<portal:mojoButton ID="btnAddToCart" runat="server" CommandName="AddToCart" CommandArgument='<%# Eval("Guid") %>'
												Text='<%# Resources.WebStoreResources.AddToCartLink%>' SkindID="AddButton" />
										</div>
									</ItemTemplate>
								</asp:Repeater>
							</div>
						</ItemTemplate>
					</asp:Repeater>
					<portal:mojoCutePager ID="pgr" runat="server" />
					<asp:Panel ID="pnlCartItems" runat="server" CssClass="panel panel-default">
						<div class="panel panel-heading">
							<h3 class="heading cartheading">
								<asp:Literal ID="litCartHeader" runat="server" /></h3>
						</div>
						<asp:Repeater ID="rptCartItems" runat="server">
							<HeaderTemplate>
								<div class="panel panel-body">
									<table class="cartgrid table table-bordered table-striped">
										<tr>
											<th>
												<%# Resources.WebStoreResources.CartItemsHeading%>
											</th>
											<th>
												<%# Resources.WebStoreResources.CartPriceHeading%>
											</th>
											<th>
												<%# Resources.WebStoreResources.CartQuantityHeading%>
											</th>
											<th>&nbsp;
											</th>
										</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td>
										<%# Eval("Name") %>
									</td>
									<td>
										<%# string.Format(currencyCulture, "{0:c}", Convert.ToDecimal(Eval("OfferPrice")))%>
									</td>
									<td>
										<asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Columns="4" />
									</td>
									<td>
										<portal:mojoButton ID="btnUpdateQuantity" runat="server" Text='<%# Resources.WebStoreResources.UpdateQuantityButton %>'
											CommandName="updateQuantity" CommandArgument='<%# Eval("ItemGuid") %>' CausesValidation="false"
											SkinID="PrimaryButton" />
										<portal:mojoButton ID="btnDelete" runat="server" CssClass="cartbutton" CommandArgument='<%# Eval("ItemGuid") %>'
											CommandName="delete" Text='<%# Resources.WebStoreResources.DeleteCartItemButton %>'
											CausesValidation="false" SkinID="DeleteButtonSmall" />
									</td>
								</tr>
							</ItemTemplate>
							<FooterTemplate>
								</table>
						</div>
							</FooterTemplate>
						</asp:Repeater>
						<div class="carttotalwrapper panel-footer text-right">
							<asp:Panel ID="pnlSubTotal" runat="server" CssClass="settingrowtight storerow">
								<mp:SiteLabel ID="SiteLabel1" runat="server" CssClass="settinglabeltight storelabel"
									ConfigKey="CartSubTotalLabel" ResourceFile="WebStoreResources" />
								<asp:Literal ID="litSubTotal" runat="server" />
							</asp:Panel>
							<asp:Panel ID="pnlDiscount" runat="server" CssClass="settingrowtight storerow">
								<mp:SiteLabel ID="SiteLabel11" runat="server" CssClass="settinglabeltight storelabel"
									ConfigKey="CartDiscountTotalLabel" ResourceFile="WebStoreResources" />
								<asp:Literal ID="litDiscount" runat="server" />
							</asp:Panel>
							<asp:Panel ID="pnlShippingTotal" runat="server" CssClass="settingrowtight storerow">
								<mp:SiteLabel ID="SiteLabel2" runat="server" CssClass="settinglabeltight storelabel"
									ConfigKey="CartShippingTotalLabel" ResourceFile="WebStoreResources" />
								<asp:Literal ID="litShippingTotal" runat="server" />
							</asp:Panel>
							<asp:Panel ID="pnlTaxTotal" runat="server" CssClass="settingrowtight storerow">
								<strong>
									<mp:SiteLabel ID="SiteLabel3" runat="server" CssClass="settinglabeltight storelabel"
										ConfigKey="CartTaxTotalLabel" ResourceFile="WebStoreResources" />
								</strong>
								<asp:Literal ID="litTaxTotal" runat="server" />
							</asp:Panel>
							<asp:Panel ID="pnlOrderTotal" runat="server" CssClass="settingrowtight storerow">
								<strong>
									<mp:SiteLabel ID="SiteLabel7" runat="server" CssClass="settinglabeltight storelabel"
										ConfigKey="CartOrderTotalLabel" ResourceFile="WebStoreResources" />
								</strong>
								<asp:Literal ID="litOrderTotal" runat="server" />
							</asp:Panel>
						</div>
					</asp:Panel>
					<asp:Panel ID="pnlDiscountCode" runat="server" Visible="false" CssClass="settingrow">
						<mp:SiteLabel ID="SiteLabel4" runat="server" CssClass="storelabel" ForControl="txtDiscountCode"
							ConfigKey="CartDiscountCodeLabel" ResourceFile="WebStoreResources" />
						<asp:TextBox ID="txtDiscountCode" runat="server" />
						<portal:mojoButton ID="btnApplyDiscount" runat="server" SkinID="PrimaryButton" />
						<portal:mojoLabel ID="lblDiscountError" runat="server" CssClass="txterror" />
					</asp:Panel>
					<asp:Panel ID="pnlManualDiscount" runat="server" Visible="false" CssClass="settingrow">
						<mp:SiteLabel ID="SiteLabel5" runat="server" ForControl="txtDiscountAmount" CssClass="storelabel"
							ConfigKey="DiscountAmountLabel" ResourceFile="WebStoreResources" />
						<asp:TextBox ID="txtDiscountAmount" runat="server" />
						<portal:mojoButton ID="btnApplyManualDiscount" runat="server" SkinID="PrimaryButton" />
					</asp:Panel>
					<div id="divCheckoutLink" runat="server" visible="false" class="settingrow">
						<asp:HyperLink ID="lnkCheckout" runat="server" CssClass="admincheckoutlink" SkinID="SuccessButton" />
					</div>
				</portal:InnerBodyPanel>
			</portal:OuterBodyPanel>
			<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
		</portal:InnerWrapperPanel>
	</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
