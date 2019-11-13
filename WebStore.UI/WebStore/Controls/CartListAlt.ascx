<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartListAlt.ascx.cs" Inherits="WebStore.UI.Controls.CartListAlt" %>

<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<webstore:webstoredisplaysettings id="displaySettings" runat="server" />
<asp:Repeater ID="rptCartItems" runat="server">
    <HeaderTemplate>
			<%# displaySettings.CartListTopMarkup %>
    </HeaderTemplate>
    <ItemTemplate>
		<%# string.Concat(
		displaySettings.CartListItemTopMarkup,
		displaySettings.CartListItemNameTopMarkup, 
		Eval("Name"), 
		displaySettings.CartListItemNameBottomMarkup,
		displaySettings.CartListItemPriceTopMarkup, 
		string.Format(CurrencyCulture, "{0:c}", Convert.ToDecimal(Eval("OfferPrice"))), 
		displaySettings.CartListItemPriceBottomMarkup,
		displaySettings.CartListItemQtyTopMarkup) %>
				<asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtQuantity" MaximumValue='<%# Eval("MaxPerOrder").ToString() %>' Enabled="false" CssClass="help-block" ValidationGroup='<%# Eval("ItemGuid").ToString()%>' />
				<asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' CssClass="smalltextbox" TextMode="Number" Max='<%# Eval("MaxPerOrder").ToString() %>' Min="1" />
                <portal:mojoButton ID="btnUpdateQuantity" runat="server" Text='<%# Resources.WebStoreResources.UpdateQuantityButton %>' CommandName="updateQuantity" CommandArgument='<%# Eval("ItemGuid") %>' CausesValidation="true" ValidationGroup='<%# Eval("ItemGuid").ToString()%>' CssClass="cartbutton" />
                <portal:mojoButton ID="btnDelete" runat="server" CssClass="cartbutton" CommandArgument='<%# Eval("ItemGuid") %>' CommandName="delete" Text='<%# Resources.WebStoreResources.DeleteCartItemButton %>' CausesValidation="false" />
		<%# string.Concat(displaySettings.CartListItemQtyBottomMarkup, displaySettings.CartListItemBottomMarkup) %>
	</ItemTemplate>
    <FooterTemplate>
		<%# displaySettings.CartListBottomMarkup %>
    </FooterTemplate>
</asp:Repeater>