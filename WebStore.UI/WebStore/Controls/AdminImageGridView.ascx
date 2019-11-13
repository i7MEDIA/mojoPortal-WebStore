<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminImageGridView.ascx.cs" Inherits="WebStore.UI.AdminImageGridView" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<webstore:WebStoreDisplaySettings ID="displaySettings" runat="server" />
<asp:UpdatePanel ID="upImages" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
	<mp:mojoGridView ID="grdImages" runat="server" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" DataKeyNames="Guid" SkinID="StoreAdmin">
		<Columns>
			<asp:TemplateField ItemStyle-CssClass="column-image">
				<ItemTemplate><asp:Image runat="server" ImageUrl='<%# Eval("ImageUrl").ToString() %>' /></ItemTemplate>
				<EditItemTemplate>
					<asp:Panel CssClass="input-group" id="divInputGroup" runat="server">
						<asp:TextBox ID="txtImageUrl" ClientIDMode="static" runat="server" Text='<%# Eval("ImageUrl").ToString() %>' />
					</asp:Panel>
					<asp:Image ID="imgPreview" runat="server" ClientIDMode="static" ImageUrl='<%# Eval("ImageUrl").ToString() %>' />
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="column-sort">
				<ItemTemplate><%# Eval("DisplayOrder") %></ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="txtDisplayOrder" runat="server" TextMode="Number" Text='<%# Eval("DisplayOrder").ToString() %>' />                                                          
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="column-alt">
				<ItemTemplate><%# Eval("Alt") %></ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="txtAlt" runat="server" Text='<%# Eval("Alt").ToString() %>' />                                                          
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="column-title">
				<ItemTemplate><%# Eval("Title") %></ItemTemplate>
				<EditItemTemplate>
					<asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title").ToString() %>' />                                                          
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="column-actions">
				<ItemTemplate><div class="btn-group btn-group-xs">
					<asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-success"
						Text='<%# Resources.WebStoreResources.EditLabel%>' />
					<asp:Button ID="btnDelete" runat="server" Text='<%# Resources.WebStoreResources.DeleteLabel %>'
						CommandName="Delete" CssClass="btn btn-danger" /></div>
				</ItemTemplate>
				<EditItemTemplate><div class="btn-group btn-group-xs">
					<asp:Button ID="btnGridUpdate" runat="server" Text='<%# Resources.WebStoreResources.SaveLabel %>'
						CommandName="Update" CssClass="btn btn-success" />
					<asp:Button ID="btnGridCancel" runat="server" Text='<%# Resources.WebStoreResources.CancelLabel %>'
						CommandName="Cancel" CssClass="btn btn-warning" />
						</div>
				</EditItemTemplate>
			</asp:TemplateField>
		</Columns>
	</mp:mojoGridView>
	<div class="settingrow text-right">
		<asp:Button ID="btnAddImage" runat="server" CssClass="btn btn-success btn-xs" />
	</div>
</ContentTemplate>
</asp:UpdatePanel>
