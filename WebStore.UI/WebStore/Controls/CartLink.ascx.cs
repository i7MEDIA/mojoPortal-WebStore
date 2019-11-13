using System.Globalization;
using System.Web.UI;
using Resources;
using mojoPortal.Web;

namespace WebStore.UI.Controls
{
    public partial class CartLink : UserControl
    {
        public int PageID = -1;
        public int ModuleID = -1;

        protected override void Render(HtmlTextWriter writer)
        {
            if (PageID == -1) { return; }
            if (ModuleID == -1) { return; }
            
            writer.Write(
                string.Format(
                        displaySettings.CartLinkFormat,
                        $"{SiteUtils.GetNavigationSiteRoot()}/WebStore/Cart.aspx?pageid={PageID.ToString(CultureInfo.InvariantCulture)}&amp;mid={ModuleID.ToString(CultureInfo.InvariantCulture)}",
                        WebStoreResources.CartLink));
        }

    }
}
