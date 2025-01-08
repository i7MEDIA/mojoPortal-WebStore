using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using mojoPortal.Web.UI;
using Resources;
using System;
using System.Collections;
using System.Globalization;
using System.Web.UI;
using WebStore.Business;

namespace WebStore.UI;

public partial class ProductListPage : mojoBasePage
{
	protected Store store;
	protected CultureInfo currencyCulture = CultureInfo.CurrentCulture;
	private bool enableRatingsInProductList = false;
	private bool enableRatingComments = false;
	private Hashtable Settings = null;
	private Module module = null;
	private int pageId = -1;
	private int moduleId = -1;


	protected void Page_Load(object sender, EventArgs e)
	{
		LoadParams();

		if (!UserCanViewPage(moduleId))
		{
			SiteUtils.RedirectToAccessDeniedPage();
			return;
		}

		LoadSettings();

		if (store == null || store.IsClosed)
		{
			WebUtils.SetupRedirect(this, SiteUtils.GetCurrentPageUrl());
			return;
		}

		PopulateLabels();
		PopulateControls();

		AnalyticsSection = ConfigHelper.GetStringProperty("AnalyticsWebStoreSection", "store");
	}


	private void PopulateControls()
	{ }


	private void PopulateLabels()
	{
		Title = SiteUtils.FormatPageTitle(siteSettings, WebStoreResources.ProductList);
		TitleControl.EditUrl = SiteRoot + "/WebStore/AdminDashboard.aspx";
		TitleControl.EditText = WebStoreResources.StoreManagerLink;

		if (module != null)
		{
			TitleControl.ModuleInstance = module;
		}

		TitleControl.CanEdit = UserCanEditModule(moduleId);

		Control c = Master.FindControl("Breadcrumbs");

		if (c != null && store != null)
		{
			BreadcrumbsControl crumbs = (BreadcrumbsControl)c;
			crumbs.ForceShowBreadcrumbs = true;

		}

		litProductListHeading.Text = WebStoreResources.ProductList;
	}


	private void LoadSettings()
	{
		siteSettings = CacheHelper.GetCurrentSiteSettings();
		store = new Store(siteSettings.SiteGuid, moduleId);
		Settings = ModuleSettings.GetModuleSettings(moduleId);
		module = GetModule(moduleId);

		lnkCart.PageID = pageId;
		lnkCart.ModuleID = moduleId;

		enableRatingsInProductList = WebUtils.ParseBoolFromHashtable(
			Settings,
			"EnableContentRatingInProductListSetting",
			enableRatingsInProductList
		);

		enableRatingComments = WebUtils.ParseBoolFromHashtable(
			Settings,
			"EnableRatingCommentsSetting",
			enableRatingComments
		);

		productList1.Store = store;
		productList1.PageId = pageId;
		productList1.ModuleId = moduleId;
		productList1.SiteRoot = SiteRoot;
		productList1.CurrencyCulture = currencyCulture;
		productList1.EnableRatings = enableRatingsInProductList;
		productList1.EnableRatingComments = enableRatingComments;
		productList1.Settings = Settings;

		AddClassToBody("webstore webstoreproductlist");

		if (WebStoreConfiguration.UseNoIndexFollowMetaOnLists)
		{
			SiteUtils.AddNoIndexFollowMeta(Page);
		}
	}


	private void LoadParams()
	{
		pageId = WebUtils.ParseInt32FromQueryString("pageid", pageId);
		moduleId = WebUtils.ParseInt32FromQueryString("mid", moduleId);
	}


	#region OnInit

	protected override void OnPreInit(EventArgs e)
	{
		AllowSkinOverride = true;
		base.OnPreInit(e);
	}


	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		Load += new EventHandler(Page_Load);

		SuppressPageMenu();
	}

	#endregion
}
