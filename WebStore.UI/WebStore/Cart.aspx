<%@ Page Language="C#" AutoEventWireup="false"  MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="Cart.aspx.cs" Inherits="WebStore.UI.CartPage" %>
<%@ Register TagPrefix="webstore" TagName="CartList" Src="~/WebStore/Controls/CartList.ascx" %>
<%@ Register TagPrefix="webstore" TagName="CartListAlt" Src="~/WebStore/Controls/CartListAlt.ascx" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
    <portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper webstore webstorecart store-cart">
        <portal:HeadingControl ID="heading" runat="server" />
        <portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
        <portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
        <webstore:WebStoreDisplaySettings id="displaySettings" runat="server" />
			<asp:Literal ID="litEmptyCart" runat="server" />
            <asp:Panel ID="pnlCartItems" runat="server" CssClass="cart store-cart-items">
                <webstore:CartList ID="cartList" runat="server" />
                <webstore:CartListAlt ID="cartListAlt" runat="server" Visible="false" />
            </asp:Panel>

			<asp:Literal ID="litSubTotal" runat="server" />
			<asp:Literal ID="litShippingTotal" runat="server" />
            <asp:Literal ID="litDiscount" runat="server" />
            <asp:Literal ID="litTotal" runat="server" />
           
			<portal:FormGroupPanel ID="pnlDiscountCode" runat="server">
                <mp:SiteLabel ID="SiteLabel4" runat="server" CssClass="storelabel" ConfigKey="CartDiscountCodeLabel" ResourceFile="WebStoreResources" />
                <asp:TextBox ID="txtDiscountCode" runat="server" CssClass="discountcode" />
                <portal:mojoButton ID="btnApplyDiscount" runat="server"  />
                <portal:mojoLabel ID="lblDiscountError" runat="server" CssClass="txterror warning alert alert-warning" />        
			</portal:FormGroupPanel>
            
			<portal:FormGroupPanel id="pnlCheckoutLinks" runat="server">
				<asp:Literal ID="litConfirmOrder" runat="server" EnableViewState="false" />
				<asp:Literal ID="litKeepShopping" runat="server" EnableViewState="false" />
				<portal:LoginLink id="lnkLogin" runat="server" />
			</portal:FormGroupPanel>
			
			<portal:FormGroupPanel ID="pnlPayPal" runat="server" Visible="false">
                <asp:ImageButton ID="btnPayPal" runat="server" ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                    AlternateText="Checkout with PayPal" />
                <br />
                    <portal:mojoLabel ID="lblMessage" runat="server" CssClass="txterror info alert alert-info"></portal:mojoLabel>
                <asp:Literal ID="litPayPalFormVariables" runat="server" />
			</portal:FormGroupPanel>
            <portal:CommerceTestModeWarning ID="commerceWarning" runat="server" />
			<div class="clearpanel"></div>
            <asp:Literal ID="litCartFooter" runat="server" EnableViewState="false" />
        </portal:InnerBodyPanel>
        </portal:OuterBodyPanel>
    </portal:InnerWrapperPanel>
    </portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
