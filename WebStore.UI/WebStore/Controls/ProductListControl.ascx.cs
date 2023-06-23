using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using mojoPortal.Web.Framework;
using WebStore.Business;
using Resources;
namespace WebStore.UI
{
    //http://schema.org/Product
    //http://schema.org/Offer

    
    public partial class ProductListControl : UserControl
    {

		#region Properties
		private int pageNumber = 1;
        private int totalPages = 1;
        private int pageSize = 10;
		protected string teaserFileBaseUrl = string.Empty;
		private DataSet dsProducts = null;
		protected bool canEdit = false;
		public int PageId { get; set; } = -1;
		public int ModuleId { get; set; } = -1;
		public Store Store { get; set; } = null;
		public string SiteRoot { get; set; } = string.Empty;
		public CultureInfo CurrencyCulture { get; set; } = CultureInfo.CurrentCulture;
		public bool EnableRatings { get; set; } = false;
		public bool EnableRatingComments { get; set; } = false;
		public Hashtable Settings { get; set; } = null;
		#endregion

		protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Visible) { return; }

            LoadSettings();

            if ((!Page.IsPostBack) && (ParamsAreValid()))
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            
            dsProducts = Store.GetProductPageWithOffers(
                pageNumber,
                pageSize,
                out totalPages);


            string pageUrl = SiteUtils.GetNavigationSiteRoot() + "/WebStore/ProductList.aspx"
                    + "?pageid=" + PageId.ToInvariantString()
                    + "&amp;mid=" + ModuleId.ToInvariantString()
                    + "&amp;pagenumber={0}";

            pgr.PageURLFormat = pageUrl;
            pgr.ShowFirstLast = true;
            pgr.CurrentIndex = pageNumber;
            pgr.PageSize = pageSize;
            pgr.PageCount = totalPages;
            pgr.Visible = (totalPages > 1);
            
            rptProducts.DataSource = dsProducts.Tables["Products"]; 
            rptProducts.DataBind();
            

        }

        void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (dsProducts == null) { return; }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string productGuid = ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString();
                Repeater rptOffers = (Repeater)e.Item.FindControl("rptOffers");

                if (rptOffers == null) { return; }

                string whereClause = string.Format("ProductGuid = '{0}'", productGuid);
                DataView dv = new DataView(dsProducts.Tables["ProductOffers"], whereClause, "", DataViewRowState.CurrentRows);
                rptOffers.DataSource = dv;
                rptOffers.DataBind();

				Repeater rptImages = (Repeater)e.Item.FindControl("rptImages");
				if (rptImages == null) { return; }
				string imagesWhereClause = string.Format("ReferenceGuid = '{0}'", productGuid);
				DataView dvImages = new DataView(dsProducts.Tables["ProductImages"], imagesWhereClause, "", DataViewRowState.CurrentRows)
				{
					Sort = "DisplayOrder ASC"
				};
				rptImages.DataSource = dvImages;
				rptImages.DataBind();


			}
        }

        private void LoadSettings()
        {
            pageNumber = WebUtils.ParseInt32FromQueryString("pagenumber", pageNumber);
            
            if (Page is mojoBasePage)
            {
                mojoBasePage basePage = Page as mojoBasePage;
                if (displaySettings.UsejPlayerForMediaTeasers)
                {
                    basePage.ScriptConfig.IncludejPlayer = true;
                    basePage.ScriptConfig.IncludejPlayerPlaylist = true;
                }
                else
                {
                    //wire up generic html for teasers?
                    //YahooMediaPlayer was removed from mojo at version 2.8.0.7
                    //basePage.ScriptConfig.IncludeYahooMediaPlayer = true;
                }
            }

            SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
            if (siteSettings == null) { return; }

            teaserFileBaseUrl = WebUtils.GetSiteRoot() + "/Data/Sites/" + siteSettings.SiteId.ToString()
                + "/webstoreproductpreviewfiles/";

            if (Settings != null)
            {
                pageSize = WebUtils.ParseInt32FromHashtable(Settings, "ProductListPageSize", pageSize);
            }

			canEdit = WebUser.HasEditPermissions(siteSettings.SiteId, ModuleId, PageId);
        }

        protected string GetProductLink(string productGuid, string url, string name, bool heading)
        {
			string editLinks = string.Empty;
			if (canEdit)
			{
				string editUrl = $"{SiteRoot}/WebStore/AdminProductEdit.aspx?pageid={PageId}&mid={ModuleId}&prod={productGuid}";
				editLinks = string.Format(displaySettings.ProductEditLinkFormat, editUrl, WebStoreResources.ProductEditHeadingTooltip, WebStoreResources.ProductEditHeading);
			}

			if (WebConfigSettings.UseUrlReWriting)
            {
				if (heading)
				{
					return string.Format(displaySettings.ProductListProductHeadingLinkFormat, SiteRoot + url, name, editLinks);
				}
				else
				{
					return string.Format(displaySettings.ProductListProductLinkFormat, SiteRoot + url, name);
				}
			}

			string fullUrl = SiteRoot + "/WebStore/ProductDetail.aspx?pageid="
				+ PageId.ToInvariantString()
				+ "&amp;mid=" + ModuleId.ToInvariantString()
				+ "&amp;product=" + productGuid;

			if (heading)
			{
				return string.Format(displaySettings.ProductListProductHeadingLinkFormat, fullUrl, name, editLinks);
			}

			return string.Format(displaySettings.ProductListProductLinkFormat, fullUrl, name);
		}

		protected string GetOfferLink(string offerGuid, string url, string name, bool showOfferLink)
        {
			string editLinks = string.Empty;
			if (canEdit)
			{
				string editUrl = $"{SiteRoot}/WebStore/AdminOfferEdit.aspx?pageid={PageId}&mid={ModuleId}&offer={offerGuid}";
				editLinks = string.Format(displaySettings.ProductOfferEditLinkFormat, editUrl, WebStoreResources.OfferEditHeadingTooltip, WebStoreResources.OfferEditHeading);
			}

			if (showOfferLink)
			{
				string fullUrl = string.Empty;

				if (WebConfigSettings.UseUrlReWriting)
				{
					fullUrl = SiteRoot + url;
				}
				else
				{
					fullUrl = SiteRoot + "/WebStore/OfferDetail.aspx?pageid="
						+ PageId.ToInvariantString()
						+ "&mid=" + ModuleId.ToInvariantString()
						+ "&offer=" + offerGuid;
				}
				return string.Format(displaySettings.ProductListOfferLinkFormat, fullUrl, name, editLinks);
			}
			//only showing offer title/name and editlink (if can edit)
			return string.Format(displaySettings.ProductListOfferTitleFormat, name, editLinks);
		}

		private bool ParamsAreValid()
        {
            if (Store == null) { return false; }
            if (PageId == -1) { return false; }
            if (ModuleId == -1) { return false; }


            return true;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += new EventHandler(Page_Load);
            rptProducts.ItemDataBound += new RepeaterItemEventHandler(rptProducts_ItemDataBound);
        }

        
    }
}