<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/App_MasterPages/layout.Master" CodeBehind="AdminDiscountEdit.aspx.cs" Inherits="WebStore.UI.AdminDiscountEditPage" %>

<asp:Content ContentPlaceHolderID="leftContent" ID="MPLeftPane" runat="server" />
<asp:Content ContentPlaceHolderID="mainContent" ID="MPContent" runat="server">
	<asp:Panel ID="pnl1" runat="server" CssClass="panelwrapper ">
		<div class="modulecontent">
			<div class="settingrow">
				<mp:sitelabel id="lblDiscountCode" runat="server" forcontrol="txtDiscountCode" cssclass="settinglabel" configkey="DiscountCodeLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtDiscountCode" runat="server" CssClass="forminput widetextbox" MaxLength="255" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblDescription" runat="server" forcontrol="txtDescription" cssclass="settinglabel" configkey="DiscountDescriptionLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtDescription" runat="server" CssClass="forminput widetextbox" MaxLength="255" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblValidityStartDate" runat="server" forcontrol="dpBeginDate" cssclass="settinglabel" configkey="DiscountValidityStartDateLabel" resourcefile="WebStoreResources" />
				<mp:datepickercontrol id="dpBeginDate" runat="server" showtime="True" cssclass="forminput" skinid="AdminDiscount"></mp:datepickercontrol>
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblValidityEndDate" runat="server" forcontrol="dpEndDate" cssclass="settinglabel" configkey="DiscountValidityEndDateLabel" resourcefile="WebStoreResources" />
				<mp:datepickercontrol id="dpEndDate" runat="server" showtime="True" cssclass="forminput"></mp:datepickercontrol>
				<asp:Label ID="lblLeaveBlankForNoEndDate" runat="server" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblUseCountLabel" runat="server" cssclass="settinglabel" configkey="DiscountUseCountLabel" resourcefile="WebStoreResources" />
				<asp:Label ID="lblCountOfUse" runat="server" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="SiteLabel2" runat="server" forcontrol="ckAllowOtherDiscounts" cssclass="settinglabel" configkey="AllowOtherDiscountsLabel" resourcefile="WebStoreResources" />
				<asp:CheckBox ID="ckAllowOtherDiscounts" runat="server" CssClass="forminput" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblMaxCount" runat="server" forcontrol="txtMaxCount" cssclass="settinglabel" configkey="DiscountMaxCountLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtMaxCount" runat="server" CssClass="forminput normaltextbox" MaxLength="50" Text="0" />
				<asp:Label ID="lblZeroIsUnlimitedUse" runat="server" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblMinOrderAmount" runat="server" forcontrol="txtMinOrderAmount" cssclass="settinglabel" configkey="DiscountMinOrderAmountLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtMinOrderAmount" runat="server" CssClass="forminput normaltextbox" MaxLength="50" Text="0" />
				<asp:Label ID="lblZeroMeansNoMinimum" runat="server" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblAbsoluteDiscount" runat="server" forcontrol="txtAbsoluteDiscount" cssclass="settinglabel" configkey="DiscountAbsoluteDiscountLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtAbsoluteDiscount" runat="server" CssClass="forminput normaltextbox" MaxLength="50" Text="0" />
			</div>
			<div class="settingrow">
				<mp:sitelabel id="lblPercentageDiscount" runat="server" forcontrol="txtPercentageDiscount" cssclass="settinglabel" configkey="DiscountPercentageDiscountLabel" resourcefile="WebStoreResources" />
				<asp:TextBox ID="txtPercentageDiscount" runat="server" CssClass="forminput normaltextbox" MaxLength="50" Text="0" />
			</div>
			<div>
				<asp:ValidationSummary ID="vSummary" runat="server" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqDiscountCode" runat="server" ControlToValidate="txtDiscountCode"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqDescription" runat="server" ControlToValidate="txtDescription"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqBeginDate" runat="server" ControlToValidate="dpBeginDate"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqMaxUseCount" runat="server" ControlToValidate="txtMaxCount"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqMinOrderAmount" runat="server" ControlToValidate="txtMinOrderAmount"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqDiscountAmount" runat="server" ControlToValidate="txtAbsoluteDiscount"
					Display="None" ValidationGroup="Discount" />
				<asp:RequiredFieldValidator ID="reqPercentDiscount" runat="server" ControlToValidate="txtPercentageDiscount"
					Display="None" ValidationGroup="Discount" />
				<portal:mojolabel id="lblError" runat="server" cssclass="txterror warning" />

			</div>
			<div class="settingrow">
				<mp:sitelabel id="SiteLabel1" runat="server" cssclass="settinglabel" configkey="spacer" />
				<portal:mojobutton id="btnSave" runat="server" validationgroup="Discount" skinid="SaveButton" />
				<portal:mojobutton id="btnDelete" runat="server" causesvalidation="false" skinid="DeleteButtonSmall" />
			</div>
		</div>
	</asp:Panel>
</asp:Content>
<asp:Content ContentPlaceHolderID="rightContent" ID="MPRightPane" runat="server" />
<asp:Content ContentPlaceHolderID="pageEditContent" ID="MPPageEdit" runat="server" />
