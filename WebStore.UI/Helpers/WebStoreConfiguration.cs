
using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using mojoPortal.Web.Framework;

namespace WebStore.UI
{
    public class WebStoreConfiguration
    {
        public WebStoreConfiguration()
        { }

        public WebStoreConfiguration(Hashtable settings)
        {
            LoadSettings(settings);

        }

        private void LoadSettings(Hashtable settings)
        {
            if (settings == null) { throw new ArgumentException("must pass in a hashtable of settings"); }

            EnableRatingsInProductList = WebUtils.ParseBoolFromHashtable(settings, "EnableContentRatingInProductListSetting", EnableRatingsInProductList);

            EnableRatingComments = WebUtils.ParseBoolFromHashtable(settings, "EnableRatingCommentsSetting", EnableRatingComments);

            IndexOffersInSearch = WebUtils.ParseBoolFromHashtable(settings, "IndexOffersSetting", IndexOffersInSearch);


            if (settings.Contains("ProductListGroupingSetting"))
            {
                GroupingMode = settings["ProductListGroupingSetting"].ToString();
            }

            if (settings.Contains("CartPageFooter"))
            {
                CartPageFooter = settings["CartPageFooter"].ToString();
            }

			if (settings.Contains("ImageStartPath"))
			{
				ImageStartPath = settings["ImageStartPath"].ToString();
			}

		}

		public string CartPageFooter { get; private set; } = string.Empty;

		public bool EnableRatingsInProductList { get; private set; } = false;

		public bool EnableRatingComments { get; private set; } = false;

		public bool IndexOffersInSearch { get; private set; } = false;

		public string GroupingMode { get; private set; } = "GroupByProduct";

		public string ImageStartPath { get; set; } = "";

		public static bool IsDemo
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:IsDemo", false); }
        }

        public static bool LogDoNothingOrderHandler
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:LogDoNothingOrderHandler", false); }
        }

        public static bool UseNoIndexFollowMetaOnLists
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:UseNoIndexFollowMetaOnLists", true); }
        }

        public static bool LogCardTransactionStatus
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:LogCardTransactionStatus", false); }
        }

        public static bool LogCardFailedTransactionStatus
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:LogCardFailedTransactionStatus", false); }
        }

        public static bool DisableOrderConfirmationEmail
        {
            get { return ConfigHelper.GetBoolProperty("WebStore:DisableOrderConfirmationEmail", false); }
        }

        


    }
}