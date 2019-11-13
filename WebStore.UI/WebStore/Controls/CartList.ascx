<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="CartList.ascx.cs" Inherits="WebStore.UI.Controls.CartList" %>

<asp:Repeater ID="rptCartItems" runat="server">
    <HeaderTemplate>
        <%# DisplaySettings.CartTableTopMarkup %>
			<thead>
				<tr>
					<th class="cartitems">
						<%# Resources.WebStoreResources.CartItemsHeading%>
					</th>
					<th class="cartprice">
						<%# Resources.WebStoreResources.CartPriceHeading%>
					</th>
					<th class="cartquantity">
						<%# Resources.WebStoreResources.CartQuantityHeading%>
					</th>
					<th class="cartactions">&nbsp;</th>
				</tr>
			</thead>
			<tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
			<td class="cartitems"><%# Eval("Name") %> <asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtQuantity" MaximumValue='<%# Eval("MaxPerOrder").ToString() %>' Enabled="false" CssClass="help-block" ValidationGroup='<%# Eval("ItemGuid").ToString()%>' /></td>
            <td class="cartprice"><%# string.Format(CurrencyCulture, "{0:c}", Convert.ToDecimal(Eval("OfferPrice")))%></td>
            <td class="cartquantity"><asp:TextBox ID="txtQuantity" runat="server" Text='<%# Eval("Quantity") %>' Columns="4" TextMode="Number" Max='<%# Convert.ToInt32(Eval("MaxPerOrder")) == 0 ? "" : Eval("MaxPerOrder").ToString() %>' Min="1" /></td>
            <td class="cartactions"><%# DisplaySettings.CartItemActionsTopMarkup %>
                <portal:mojobutton id="btnUpdateQuantity" runat="server" text='<%# Resources.WebStoreResources.UpdateQuantityButton %>' commandname="updateQuantity" commandargument='<%# Eval("ItemGuid") %>' CausesValidation="true" ValidationGroup='<%# Eval("ItemGuid").ToString()%>' cssclass='<%# DisplaySettings.UpdateQtyButtonCssClass %>' />
                <portal:mojobutton id="btnDelete" runat="server" cssclass='<%# DisplaySettings.DeleteButtonCssClass %>' commandargument='<%# Eval("ItemGuid") %>'
					commandname="delete" text='<%# Resources.WebStoreResources.DeleteCartItemButton %>' causesvalidation="false" skinid="DeleteButtonSmall" />
				<%# DisplaySettings.CartItemActionsBottomMarkup %></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
		</tbody>
    <%# DisplaySettings.CartTableBottomMarkup %>
	</FooterTemplate>
</asp:Repeater>
