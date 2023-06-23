<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
	CodeBehind="AdminProduct.aspx.cs" Inherits="WebStore.UI.AdminProductPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
	<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
		<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper webstore webstoreadminproduct">
			<portal:HeadingControl ID="heading" runat="server" />
			<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
				<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
					<mp:mojoGridView ID="grdProduct" runat="server"
						AllowPaging="false"
						AllowSorting="false"
						AutoGenerateColumns="false"
						DataKeyNames="Guid">
						<Columns>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:HyperLink ID="lnkEdit" runat="server" Text='<%# Resources.WebStoreResources.ProductGridEditButton %>'
										ToolTip='<%# Resources.WebStoreResources.ProductGridEditButton %>' NavigateUrl='<%# SiteRoot +"/WebStore/AdminProductEdit.aspx" + BuildProductQueryString(Eval("Guid").ToString()) %>' />
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<%# Eval("ModelNumber") %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<%# Eval("Name") %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<%--<portal:mojoRating runat="server" ID="Rating" ContentGuid='<%# Eval("Guid") %>' ShowPrompt="false" />--%>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</mp:mojoGridView>
					<div class="settingrow">
						<asp:HyperLink ID="lnkNewProduct" runat="server" SkinID="AddButton" />
					</div>
					<portal:mojoCutePager ID="pgrProduct" runat="server" />
					<portal:EmptyPanel id="EmptyPanel1" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
				</portal:InnerBodyPanel>
			</portal:OuterBodyPanel>
			<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
		</portal:InnerWrapperPanel>
	</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
