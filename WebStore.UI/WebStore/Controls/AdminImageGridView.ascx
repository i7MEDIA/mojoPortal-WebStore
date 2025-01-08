<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminImageGridView.ascx.cs" Inherits="WebStore.UI.AdminImageGridView" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>

<webstore:WebStoreDisplaySettings ID="displaySettings" runat="server" />

<asp:UpdatePanel ID="upImages" UpdateMode="Conditional" runat="server">
	<ContentTemplate>
		<mp:mojoGridView runat="server"
			ID="grdImages"
			AllowPaging="false"
			AllowSorting="false"
			AutoGenerateColumns="false"
			DataKeyNames="Guid"
			CssClass="table table-condensed table-bordered table-striped"
			SkinID="StoreAdmin">
			<columns>
				<asp:TemplateField ItemStyle-CssClass="column-image">
					<itemtemplate>
						<asp:Image runat="server" ImageUrl='<%# Eval("ImageUrl").ToString() %>' />
					</itemtemplate>

					<edititemtemplate>
						<div class="settingrow">
							<asp:Panel CssClass="input-group" ID="divInputGroup" runat="server">
								<asp:TextBox ID="txtImageUrl" ClientIDMode="static" runat="server" Text='<%# Eval("ImageUrl").ToString() %>' />
							</asp:Panel>
							<asp:Image ID="imgPreview" runat="server" ClientIDMode="static" ImageUrl='<%# Eval("ImageUrl").ToString() %>' />
						</div>

						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel2" runat="server" CssClass="settinglabel" ResourceFile="WebStoreResources" ConfigKey="DisplayOrderLabel" ForControl="txtDisplayOrder" />
							<asp:TextBox ID="txtDisplayOrder" runat="server" TextMode="Number" Text='<%# Eval("DisplayOrder").ToString() %>' />
						</div>

						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel1" runat="server" CssClass="settinglabel" ResourceFile="WebStoreResources" ConfigKey="AltLabel" ForControl="txtAlt" />
							<asp:TextBox ID="txtAlt" runat="server" Text='<%# Eval("Alt").ToString() %>' />
						</div>

						<div class="settingrow">
							<mp:SiteLabel ID="lblTitle" runat="server" CssClass="settinglabel" ResourceFile="WebStoreResources" ConfigKey="ImageTitleLabel" ForControl="txtTitle" />
							<asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title").ToString() %>' />
						</div>

						<div class="btn-group btn-group-sm">
							<asp:Button ID="btnGridUpdate" runat="server" Text='<%# Resources.WebStoreResources.SaveLabel %>'
								CommandName="Update" CssClass="btn btn-success" />
							<asp:Button ID="btnGridCancel" runat="server" Text='<%# Resources.WebStoreResources.CancelLabel %>'
								CommandName="Cancel" CssClass="btn btn-warning" />
						</div>
					</edititemtemplate>
				</asp:TemplateField>

				<asp:TemplateField ItemStyle-CssClass="column-sort">
					<itemtemplate><%# Eval("DisplayOrder") %></itemtemplate>
				</asp:TemplateField>

				<asp:TemplateField ItemStyle-CssClass="column-alt">
					<itemtemplate><%# Eval("Alt") %></itemtemplate>
				</asp:TemplateField>

				<asp:TemplateField ItemStyle-CssClass="column-title">
					<itemtemplate><%# Eval("Title") %></itemtemplate>
				</asp:TemplateField>

				<asp:TemplateField ItemStyle-CssClass="column-actions">
					<itemtemplate>
						<div class="btn-group btn-group-xs">
							<asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-success"
								Text='<%# Resources.WebStoreResources.EditLabel%>' />
							<asp:Button ID="btnDelete" runat="server" Text='<%# Resources.WebStoreResources.DeleteLabel %>'
								CommandName="Delete" CssClass="btn btn-danger" />
						</div>
					</itemtemplate>
				</asp:TemplateField>
			</columns>
		</mp:mojoGridView>

		<div class="settingrow text-right">
			<portal:mojoButton ID="btnAddImage" runat="server" SkinID="InfoButton" />
		</div>
	</ContentTemplate>
</asp:UpdatePanel>
