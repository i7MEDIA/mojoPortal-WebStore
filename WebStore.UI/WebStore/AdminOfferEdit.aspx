<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master"
	CodeBehind="AdminOfferEdit.aspx.cs" Inherits="WebStore.UI.AdminOfferEditPage" %>
<%@ Register TagPrefix="webstore" TagName="AdminImageGridView" Src="~/WebStore/Controls/AdminImageGridView.ascx" %>
<%@ Register Namespace="WebStore.UI" Assembly="WebStore.UI" TagPrefix="webstore" %>
<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
	<webstore:WebStoreDisplaySettings id="displaySettings" runat="server" />

	<portal:OuterWrapperPanel ID="pnlOuterWrap" runat="server">
		<portal:InnerWrapperPanel ID="pnlInnerWrap" runat="server" CssClass="panelwrapper webstore webstoreadminoffer">
			<portal:HeadingControl ID="heading" runat="server" CssClass="m-b-0"/>
			<a href='<%= GetReturnUrl() %>' class="btn btn-link"><%= Resources.WebStoreResources.BackToOffersListLabel %></a>

			<portal:OuterBodyPanel ID="pnlOuterBody" runat="server">
				<portal:InnerBodyPanel ID="pnlInnerBody" runat="server" CssClass="modulecontent">
					<asp:Panel id="divtabs" runat="server" DefaultButton="btnSaveContinue">
						<div class="settingrow">
							<mp:SiteLabel ID="SiteLabel1" runat="server" CssClass="settinglabel" ConfigKey="OfferNameLabel"
								ResourceFile="WebStoreResources" ForControl="txtName" />
							<asp:TextBox ID="txtName" runat="server" MaxLength="255" CssClass="verywidetextbox forminput" />
						</div>
						<div class="mojo-tabs">
							<ul>
								<li class="selected"><a href="#tabSettings">
									<asp:Literal ID="litSettingsTab" runat="server" /></a></li>
								<li><a href="#tabImages">
									<asp:Literal ID="litImagesTab" runat="server" /></a></li>
								<li><a href="#tabDescription">
									<asp:Literal ID="litDescriptionTab" runat="server" /></a></li>
								<li><a href="#tabProducts">
									<asp:Literal ID="litProductsTab" runat="server"/></a></li>
								<li><a href="#tabAvailability">
									<asp:Literal ID="litAvailabilityTab" runat="server" /></a></li>
								<li><a href="#tabMeta">
									<asp:Literal ID="litMetaTab" runat="server" /></a></li>
							</ul>
							<div id="tabSettings">
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel6" runat="server" CssClass="settinglabel" ConfigKey="OfferNameOnProductListLabel"
										ResourceFile="WebStoreResources" ForControl="txtProductList" />
									<asp:TextBox ID="txtProductListName" runat="server" MaxLength="255" CssClass="verywidetextbox forminput" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel8" runat="server" CssClass="settinglabel" ConfigKey="PrimarySortRankLabel"
										ResourceFile="WebStoreResources" ForControl="txtSortRank1" />
									<asp:TextBox ID="txtSortRank1" runat="server" Text="5000" MaxLength="20" CssClass="forminput smalltextbox" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel9" runat="server" CssClass="settinglabel" ConfigKey="SecondarySortRankLabel"
										ResourceFile="WebStoreResources" ForControl="txtSortRank2" />
									<asp:TextBox ID="txtSortRank2" runat="server" Text="5000" MaxLength="20" CssClass="forminput smalltextbox" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel4" runat="server" CssClass="settinglabel" ConfigKey="OfferPriceLabel"
										ResourceFile="WebStoreResources" ForControl="txtPrice" />
									<asp:TextBox ID="txtPrice" runat="server" MaxLength="255" CssClass="forminput mediumtextbox" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel11" runat="server" CssClass="settinglabel" ConfigKey="OfferMaxPerOrder"
										ResourceFile="WebStoreResources" ForControl="txtMaxPerOrder" />
									<asp:TextBox ID="txtMaxPerOrder" runat="server" MaxLength="255" CssClass="forminput mediumtextbox" TextMode="Number" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="lblTaxClassGuid" runat="server" CssClass="settinglabel" ConfigKey="OfferTaxClassLabel"
										ResourceFile="WebStoreResources" ForControl="ddTaxClassGuid" />
									<asp:DropDownList ID="ddTaxClassGuid" runat="server" EnableTheming="false" DataValueField="Guid"
										DataTextField="Title" CssClass="forminput" />
								</div>

								<div class="settingrow">
									<mp:SiteLabel ID="lblIsSpecial" runat="server" CssClass="settinglabel" ConfigKey="OfferIsSpecialLabel"
										ResourceFile="WebStoreResources" ForControl="chkIsSpecial" />
									<asp:CheckBox ID="chkIsSpecial" runat="server" CssClass="forminput" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel3" runat="server" CssClass="settinglabel" ConfigKey="OfferIsDonationLabel"
										ResourceFile="WebStoreResources" ForControl="chkIsDonation" />
									<asp:CheckBox ID="chkIsDonation" runat="server" CssClass="forminput" />
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel7" runat="server" CssClass="settinglabel" ConfigKey="OfferShowDetailLink"
										ResourceFile="WebStoreResources" ForControl="chkShowDetailLink" />
									<asp:CheckBox ID="chkShowDetailLink" runat="server" CssClass="forminput" />
								</div>
							</div>
							<div id="tabImages">
								<mp:SiteLabel ID="lblImagesAfterSave" runat="server" ResourceFile="WebStoreResources" ConfigKey="AddImagesAfterSaveOffer" Visible="false"/>
								<webstore:AdminImageGridView ID="grdImages" runat="server" />
							</div>

							<div id="tabDescription">
								<div class="settingrow edit-abstract">
									<asp:Literal ID="litAbstractHeader" runat="server" EnableViewState="false" />
									<mpe:EditorControl ID="edAbstract" runat="server" />
								</div>
								<div class="settingrow edit-description">
									<asp:Literal ID="litDescriptionHeader" runat="server" EnableViewState="false" />
									<mpe:EditorControl ID="edDescription" runat="server" />
								</div>
							</div>

							<div id="tabProducts">
								<mp:SiteLabel ID="lblProductsAfterSave" runat="server" ResourceFile="WebStoreResources" ConfigKey="AddProductsAfterSave" Visible="false" />
								<asp:UpdatePanel ID="upProducts" UpdateMode="Conditional" runat="server">
										<ContentTemplate>
											<mp:mojoGridView ID="grdOfferProduct" runat="server" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false" CssClass="editgrid" DataKeyNames="Guid" SkinID="StoreAdmin">
												<columns>
													<asp:TemplateField ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
														<ItemTemplate>
															<asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-link" Text='<%# Resources.WebStoreResources.OfferProductGridEditButton %>' />
														</ItemTemplate>
														<EditItemTemplate><div class="btn-group btn-group-xs">
															<asp:Button ID="btnGridUpdate" runat="server" Text='<%# Resources.WebStoreResources.OfferProductGridUpdateButton %>'
																CommandName="Update" CssClass="btn btn-success" />
															<asp:Button ID="btnGridDelete" runat="server" Text='<%# Resources.WebStoreResources.OfferProductGridDeleteButton %>'
																CommandName="Delete" CssClass="btn btn-danger" />
															<asp:Button ID="btnGridCancel" runat="server" Text='<%# Resources.WebStoreResources.OfferProductGridCancelButton %>'
																CommandName="Cancel" CssClass="btn btn-warning" />
																</div>
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="Name" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
														<ItemTemplate><%# Eval("Name") %></ItemTemplate>
														<EditItemTemplate>
																<asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Name") %>' CssClass="productname" />                                                          
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="FullfillType" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
														<ItemTemplate>
															<%# WebStore.Helpers.StoreHelper.GetFulfillmentTypeLabel(Eval("FullfillType").ToString())%>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:HiddenField ID="hdnFType" runat="server" Value='<%# Eval("FullfillType") %>' />
															<asp:HiddenField ID="hdnFTerms" runat="server" Value='<%# Eval("FullFillTermsGuid") %>' />
															<asp:Label ID="lblFulfilltype" runat="server" Text='<%# WebStore.Helpers.StoreHelper.GetFulfillmentTypeLabel(Eval("FullfillType").ToString())%>' />
															<asp:DropDownList ID="ddFullFillTerms" runat="server" DataValueField="Guid" DataTextField="Name" CssClass="forminput" />
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField SortExpression="Quantity" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
														<ItemTemplate><%# Eval("Quantity") %></ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox ID="txtQuantity" Text='<%# Eval("Quantity") %>' runat="server" TextMode="Number" MaxLength="8" CssClass="forminput" />
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField HeaderStyle-CssClass="editgridheader">
														<ItemTemplate>
															<%# Eval("SortOrder") %>
														</ItemTemplate>
														<EditItemTemplate>
															<asp:TextBox ID="txtSortOrder" Text='<%# Eval("SortOrder") %>' runat="server" TextMode="Number" MaxLength="8" CssClass="forminput" />
														</EditItemTemplate>
													</asp:TemplateField>
												</columns>
											</mp:mojoGridView>
											<div class="settingrow text-right">
												<asp:HyperLink ID="lnkProductsAdd" runat="server" CssClass="cblink btn btn-success btn-xs" />
												<asp:HiddenField ID="hdnProductGuid" runat="server" />
												<asp:HiddenField ID="hdnFulfillmentType" runat="server" />
												<asp:HiddenField ID="hdnFulfillmentTermsGuid" runat="server" />
												<asp:ImageButton ID="btnAddFromGreyBox" runat="server" />
											</div>
										</ContentTemplate>
									</asp:UpdatePanel>
							</div>
							<div id="tabAvailability">
								<div class="settingrow">
									<%= Resources.WebStoreResources.OfferAvailabilityInstructions %>
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="lblIsVisible" runat="server" CssClass="settinglabel" ConfigKey="OfferIsVisibleLabel"
										ResourceFile="WebStoreResources" ForControl="chkIsVisible" />
									<asp:CheckBox ID="chkIsVisible" runat="server" CssClass="forminput" />
								</div>
								<mp:SiteLabel ID="lblAvailabilityAfterSave" runat="server" ResourceFile="WebStoreResources" ConfigKey="AddAvailabilityAfterSave" Visible="false" />
								<asp:UpdatePanel ID="upAvailability" UpdateMode="Conditional" runat="server">
									<ContentTemplate>
										<mp:mojoGridView ID="grdOfferAvailability" runat="server" AllowPaging="false" AllowSorting="false"
											AutoGenerateColumns="false" CssClass="editgrid" DataKeyNames="Guid" SkinID="plain">
											<columns>
												<asp:TemplateField ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip='<%# Resources.WebStoreResources.OfferAvailabilityGridEditButton %>'
															AlternateText='<%# Resources.WebStoreResources.OfferAvailabilityGridEditButton %>'
															ImageUrl="~/Data/SiteImages/edit.gif" />
													</ItemTemplate>
													<EditItemTemplate><div class="btn-group btn-group-xs">
														<asp:Button ID="btnGridUpdate" runat="server" Text='<%# Resources.WebStoreResources.OfferAvailabilityGridUpdateButton %>'
															CommandName="Update" SkinID="SaveButton" CssClass="btn btn-success" />
														<asp:Button ID="btnGridDelete" runat="server" Text='<%# Resources.WebStoreResources.OfferAvailabilityGridDeleteButton %>'
															CommandName="Delete" SkinID="DeleteButton" CssClass="btn btn-danger" />
														<asp:Button ID="btnGridCancel" runat="server" Text='<%# Resources.WebStoreResources.OfferAvailabilityGridCancelButton %>'
															CommandName="Cancel" SkinID="LinkButton" CssClass="btn btn-warning" />
														</div>
													</EditItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="BeginUTC" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<%# Eval("BeginUTC") %>
													</ItemTemplate>
													<EditItemTemplate>
														<mp:DatePickerControl ID="dpBeginDate" runat="server" Text='<%# GetBeginDate(Eval("BeginUTC")) %>' Columns="15" Required="True" MaxLength="50" ShowTime="True" SkinID="AdminOffer"/>
													</EditItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="EndUTC" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<%# Eval("EndUTC") %>
													</ItemTemplate>
													<EditItemTemplate>
														<mp:DatePickerControl ID="dpEndDate" runat="server" Text='<%# Bind("EndUTC") %>' Columns="15" Required="False" MaxLength="50" ShowTime="True"/>
													</EditItemTemplate>
												</asp:TemplateField>
												<%--<asp:TemplateField SortExpression="RequiresOfferCode" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<%# Eval("RequiresOfferCode") %>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:CheckBox ID="chkRequiresOfferCode" Checked='<%# Eval("RequiresOfferCode") %>' runat="server" />
													</EditItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="OfferCode" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<%# Eval("OfferCode") %>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtOfferCode" Text='<%# Eval("OfferCode") %>' runat="server" MaxLength="50" />
													</EditItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField SortExpression="MaxAllowedPerCustomer" ItemStyle-CssClass="editgridcell" HeaderStyle-CssClass="editgridheader">
													<ItemTemplate>
														<%# Eval("MaxAllowedPerCustomer") %>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtMaxAllowedPerCustomer" Columns="20" Text='<%# Eval("MaxAllowedPerCustomer") %>' runat="server" MaxLength="4" />
													</EditItemTemplate>
												</asp:TemplateField>--%>
											</columns>
										</mp:mojoGridView>
										<div class="settingrow text-right">
											<asp:Button ID="btnAddNewAvailability" runat="server" CssClass="btn btn-success btn-xs"/>
										</div>
									</ContentTemplate>
								</asp:UpdatePanel>
							</div>
							<div id="tabMeta">
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel2" runat="server" ForControl="txtMetaDescription" CssClass="settinglabel"
										ConfigKey="MetaDescriptionLabel" ResourceFile="WebStoreResources">
									</mp:SiteLabel>
									<asp:TextBox ID="txtMetaDescription" runat="server" Columns="50" MaxLength="255"
										CssClass="forminput verywidetextbox"></asp:TextBox>
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="SiteLabel10" runat="server" ForControl="txtMetaKeywords" CssClass="settinglabel"
										ConfigKey="MetaKeywordsLabel" ResourceFile="WebStoreResources">
									</mp:SiteLabel>
									<asp:TextBox ID="txtMetaKeywords" runat="server" Columns="50" MaxLength="255" CssClass="forminput verywidetextbox"></asp:TextBox>
								</div>
								<div class="settingrow">
									<mp:SiteLabel ID="lblAdditionalMetaTags" runat="server" CssClass="settinglabel" ConfigKey="MetaAdditionalLabel"
										ResourceFile="WebStoreResources">
									</mp:SiteLabel>
									<portal:mojoHelpLink ID="MojoHelpLink25" runat="server" HelpKey="pagesettingsadditionalmetahelp" />
								</div>
								<div class="settingrow">
									<asp:UpdatePanel ID="updMetaLinks" runat="server" UpdateMode="Conditional">
										<ContentTemplate>
											<mp:mojoGridView ID="grdMetaLinks" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
												DataKeyNames="Guid">
												<columns>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:Button ID="btnEditMetaLink" runat="server" CommandName="Edit" Text='<%# Resources.WebStoreResources.ContentMetaGridEditButton %>' />
															<asp:ImageButton ID="btnMoveUpMetaLink" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
																CommandName="MoveUp" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.WebStoreResources.ContentMetaGridMoveUpButton %>'
																Visible='<%# (Convert.ToInt32(Eval("SortRank")) > 3) %>' />
															<asp:ImageButton ID="btnMoveDownMetaLink" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
																CommandName="MoveDown" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.WebStoreResources.ContentMetaGridMoveDownButton %>' />
														</ItemTemplate>
														<EditItemTemplate>
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<%# Eval("Rel") %>
														</ItemTemplate>
														<EditItemTemplate>
															<div class="settingrow">
																<mp:SiteLabel ID="lblNameMetaRel" runat="server" ForControl="txtRel" CssClass="settinglabel"
																	ConfigKey="ContentMetaRelLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtRel" CssClass="widetextbox forminput" runat="server" Text='<%# Eval("Rel") %>' />
																<asp:RequiredFieldValidator ID="reqMetaName" runat="server" ControlToValidate="txtRel"
																	ErrorMessage='<%# Resources.WebStoreResources.ContentMetaLinkRelRequired %>'
																	ValidationGroup="metalink" />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblMetaHref" runat="server" ForControl="txtHref" CssClass="settinglabel"
																	ConfigKey="ContentMetaMetaHrefLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtHref" CssClass="verywidetextbox forminput" runat="server" Text='<%# Eval("Href") %>' />
																<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHref"
																	ErrorMessage='<%# Resources.WebStoreResources.ContentMetaLinkHrefRequired %>'
																	ValidationGroup="metalink" />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblScheme" runat="server" ForControl="txtScheme" CssClass="settinglabel"
																	ConfigKey="ContentMetHrefLangLabel" ResourceFile="Resource" />
																<asp:TextBox ID="txtHrefLang" CssClass="widetextbox forminput" runat="server" Text='<%# Eval("HrefLang") %>' />
															</div>
															<div class="settingrow">
																<asp:Button ID="btnUpdateMetaLink" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridUpdateButton %>'
																	CommandName="Update" ValidationGroup="metalink" CausesValidation="true" SkinID="SaveButton" />
																<asp:Button ID="btnDeleteMetaLink" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridDeleteButton %>'
																	CommandName="Delete" CausesValidation="false" SkinID="DeleteButtonSmall" />
																<asp:Button ID="btnCancelMetaLink" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridCancelButton %>'
																	CommandName="Cancel" CausesValidation="false" SkinID="LinkButton" />
															</div>
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<%# Eval("Href") %>
														</ItemTemplate>
														<EditItemTemplate>
														</EditItemTemplate>
													</asp:TemplateField>
												</columns>
											</mp:mojoGridView>
											<div class="settingrow">
												<table>
													<tr>
														<td>
															<asp:Button ID="btnAddMetaLink" runat="server" />&nbsp;
														</td>
														<td>
															<asp:UpdateProgress ID="prgMetaLinks" runat="server" AssociatedUpdatePanelID="updMetaLinks">
																<ProgressTemplate>
																	<img src='<%= Page.ResolveUrl("~/Data/SiteImages/indicators/indicator1.gif") %>'
																		alt=' ' />
																</ProgressTemplate>
															</asp:UpdateProgress>
														</td>
													</tr>
												</table>
											</div>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
								<div class="settingrow">
									<asp:UpdatePanel ID="upMeta" runat="server" UpdateMode="Conditional">
										<ContentTemplate>
											<mp:mojoGridView ID="grdContentMeta" runat="server" CssClass="editgrid" AutoGenerateColumns="false"
												DataKeyNames="Guid">
												<columns>
													<asp:TemplateField>
														<ItemTemplate>
															<asp:Button ID="btnEditMeta" runat="server" CommandName="Edit" Text='<%# Resources.WebStoreResources.ContentMetaGridEditButton %>' />
															<asp:ImageButton ID="btnMoveUpMeta" runat="server" ImageUrl="~/Data/SiteImages/up.gif"
																CommandName="MoveUp" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.WebStoreResources.ContentMetaGridMoveUpButton %>'
																Visible='<%# (Convert.ToInt32(Eval("SortRank")) > 3) %>' />
															<asp:ImageButton ID="btnMoveDownMeta" runat="server" ImageUrl="~/Data/SiteImages/dn.gif"
																CommandName="MoveDown" CommandArgument='<%# Eval("Guid") %>' AlternateText='<%# Resources.WebStoreResources.ContentMetaGridMoveDownButton %>' />
														</ItemTemplate>
														<EditItemTemplate>
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<%# Eval("Name") %>
														</ItemTemplate>
														<EditItemTemplate>
															<div class="settingrow">
																<mp:SiteLabel ID="lblName" runat="server" ForControl="txtName" CssClass="settinglabel"
																	ConfigKey="ContentMetaNameLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtName" CssClass="widetextbox forminput" runat="server" Text='<%# Eval("Name") %>' />
																<asp:RequiredFieldValidator ID="reqMetaName" runat="server" ControlToValidate="txtName"
																	ErrorMessage='<%# Resources.WebStoreResources.ContentMetaNameRequired %>' ValidationGroup="meta" />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblMetaContent" runat="server" ForControl="txtMetaContent" CssClass="settinglabel"
																	ConfigKey="ContentMetaMetaContentLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtMetaContent" CssClass="verywidetextbox forminput" runat="server"
																	Text='<%# Eval("MetaContent") %>' />
																<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
																	ErrorMessage='<%# Resources.WebStoreResources.ContentMetaContentRequired %>'
																	ValidationGroup="meta" />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblScheme" runat="server" ForControl="txtScheme" CssClass="settinglabel"
																	ConfigKey="ContentMetaSchemeLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtScheme" CssClass="widetextbox forminput" runat="server" Text='<%# Eval("Scheme") %>' />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblLangCode" runat="server" ForControl="txtLangCode" CssClass="settinglabel"
																	ConfigKey="ContentMetaLangCodeLabel" ResourceFile="WebStoreResources" />
																<asp:TextBox ID="txtLangCode" CssClass="smalltextbox forminput" runat="server" Text='<%# Eval("LangCode") %>' />
															</div>
															<div class="settingrow">
																<mp:SiteLabel ID="lblDir" runat="server" ForControl="ddDirection" CssClass="settinglabel"
																	ConfigKey="ContentMetaDirLabel" ResourceFile="WebStoreResources" />
																<asp:DropDownList ID="ddDirection" runat="server" CssClass="forminput">
																	<asp:ListItem Text="" Value=""></asp:ListItem>
																	<asp:ListItem Text="ltr" Value="ltr"></asp:ListItem>
																	<asp:ListItem Text="rtl" Value="rtl"></asp:ListItem>
																</asp:DropDownList>
															</div>
															<div class="settingrow">
																<asp:Button ID="btnUpdateMeta" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridUpdateButton %>'
																	CommandName="Update" ValidationGroup="meta" CausesValidation="true" SkinID="SaveButton" />
																<asp:Button ID="btnDeleteMeta" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridDeleteButton %>'
																	CommandName="Delete" CausesValidation="false" SkinID="DeleteButtonSmall" />
																<asp:Button ID="btnCancelMeta" runat="server" Text='<%# Resources.WebStoreResources.ContentMetaGridCancelButton %>'
																	CommandName="Cancel" CausesValidation="false" SkinID="LinkButton" />
															</div>
														</EditItemTemplate>
													</asp:TemplateField>
													<asp:TemplateField>
														<ItemTemplate>
															<%# Eval("MetaContent") %>
														</ItemTemplate>
														<EditItemTemplate>
														</EditItemTemplate>
													</asp:TemplateField>
												</columns>
											</mp:mojoGridView>
											<div class="settingrow">
												<table>
													<tr>
														<td>
															<asp:Button ID="btnAddMeta" runat="server" />&nbsp;
														</td>
														<td>
															<asp:UpdateProgress ID="prgMeta" runat="server" AssociatedUpdatePanelID="upMeta">
																<ProgressTemplate>
																	<img src='<%= Page.ResolveUrl("~/Data/SiteImages/indicators/indicator1.gif") %>' alt=' ' />
																</ProgressTemplate>
															</asp:UpdateProgress>
														</td>
													</tr>
												</table>
											</div>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</div>
						</div>
						<div class="btn-toolbar">
							<div class="btn-group">
								<portal:mojoButton ID="btnSave" runat="server" ValidationGroup="Product" SkinID="SaveButton" CommandName="Save" />
								<portal:mojoButton ID="btnSaveContinue" runat="server" ValidationGroup="Product" SkinID="InfoButton" CommandName="SaveContinue"/>

							</div>
							<div class="btn-group">
								<portal:mojoButton ID="btnDelete" runat="server" CausesValidation="false" SkinID="DeleteButtonBig" />
							</div>
						</div>
						<div>
							<asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName"
								Display="None" ValidationGroup="Product" />
							<asp:RequiredFieldValidator ID="reqPrice" runat="server" ControlToValidate="txtPrice"
								Display="None" ValidationGroup="Product" />
							<asp:ValidationSummary ID="vSummary" runat="server" ValidationGroup="Product" />
							<portal:mojoLabel ID="lblError" runat="server" CssClass="txterror" />
						</div>
					</asp:Panel>
				</portal:InnerBodyPanel>
			</portal:OuterBodyPanel>
			<portal:EmptyPanel id="divCleared" runat="server" CssClass="cleared" SkinID="cleared"></portal:EmptyPanel>
		</portal:InnerWrapperPanel>
	</portal:OuterWrapperPanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
