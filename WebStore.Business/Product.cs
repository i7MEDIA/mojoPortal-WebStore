/// Author:					
/// Created:				2007-02-24
/// Last Modified:			2015-04-13 (Joe Davis)
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Data;
using WebStore.Data;
using mojoPortal.Business;
using mojoPortal.Business.Commerce;

namespace WebStore.Business
{
    /// <summary>
    /// Represents a product
    /// </summary>
    public class Product : IIndexableContent
    {

        #region Constructors

        public Product()
        {
        }


        public Product(Guid guid)
        {
            GetProduct(guid);
        }

		#endregion
		#region Public Properties

		public Guid Guid { get; set; } = Guid.Empty;
		public Guid StoreGuid { get; set; } = Guid.Empty;

		public Guid LanguageGuid { get; } = Guid.Empty;

		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string MetaKeywords { get; set; } = string.Empty;

		public string MetaDescription { get; set; } = string.Empty;

		public string CompiledMeta { get; set; } = string.Empty;

		public int SortRank1 { get; set; } = 5000;

		public int SortRank2 { get; set; } = 5000;

		public bool ShowInProductList { get; set; } = true;

		public bool EnableRating { get; set; } = true;

		public string Teaser { get; set; } = string.Empty;

		private Guid taxClassGuid = Guid.Empty;
		public Guid TaxClassGuid
        {
            get { return taxClassGuid; }
            set
            {
                if (taxClassGuid != value)
                {
                    taxClassGuid = value;
                    taxClass = null;
                }

            }
        }

		private TaxClass taxClass = null;
		public TaxClass TaxClass
        {
            get
            {
                if (taxClass == null) taxClass = new TaxClass(taxClassGuid);
                return taxClass;
            }
        }
		public string Sku { get; set; } = string.Empty;
		public string ModelNumber { get; set; } = string.Empty;

		private byte status;
		public ProductStatus Status
        {
            get { return ProductStatusFromInt32(status); }
            set { status = (byte)value; }
        }

		private byte fulfillmentType = 3; //none
		public FulfillmentType FulfillmentType
		{
			get => FulfillmentTypeFromInt32(fulfillmentType);
			set => fulfillmentType = (byte)value;
		}
		public decimal Weight { get; set; } = 0;
		//public int QuantityOnHand { get; set; } = 1;
		public decimal QuantityOnHand { get; set; } = 1;
		public string SoldByQtys { get; set; } = "1";
		public string ImageFileName { get; set; }
		public byte[] ImageFileBytes { get; set; } = null;
		public DateTime Created { get; set; } = DateTime.UtcNow;
		public Guid CreatedBy { get; set; } = Guid.Empty;
		public DateTime LastModified { get; set; } = DateTime.UtcNow;
		public Guid LastModifedBy { get; set; } = Guid.Empty;

		/// <summary>
		/// This is not persisted to the db. It is only set and used when indexing forum threads in the search index.
		/// Its a convenience because when we queue the task to index on a new thread we can only pass one object.
		/// So we store extra properties here so we don't need any other objects.
		/// </summary>
		public int SiteId { get; set; } = -1;

		/// <summary>
		/// This is not persisted to the db. It is only set and used when indexing forum threads in the search index.
		/// Its a convenience because when we queue the task to index on a new thread we can only pass one object.
		/// So we store extra properties here so we don't need any other objects.
		/// </summary>
		public string SearchIndexPath { get; set; } = string.Empty;

		public string TeaserFile { get; set; } = string.Empty;

		public string TeaserFileLink { get; set; } = string.Empty;

		public decimal ShippingAmount { get; set; } = 0;

		#endregion

		#region Private Methods

		private void GetProduct(Guid guid) 
		{
            using (IDataReader reader = DBProduct.Get(guid))
            {
                if (reader.Read())
                {
                    this.Guid = new Guid(reader["Guid"].ToString());
                    this.StoreGuid = new Guid(reader["StoreGuid"].ToString());
                    this.taxClassGuid = new Guid(reader["TaxClassGuid"].ToString());
                    //this.sku = reader["Sku"].ToString();
                    this.Name = reader["Name"].ToString();
                    this.Description = reader["Description"].ToString();
                    this.ModelNumber = reader["ModelNumber"].ToString();
                    this.status = Convert.ToByte(reader["Status"]);
                    this.fulfillmentType = Convert.ToByte(reader["FullfillmentType"]);
                    this.Weight = Convert.ToDecimal(reader["Weight"]);
                    this.QuantityOnHand = Convert.ToInt32(reader["QuantityOnHand"]);
                    this.SoldByQtys = reader["SoldByQtys"].ToString();
                    this.ImageFileName = reader["ImageFileName"].ToString();
					// TODO:
					//this.imageFileBytes = Byte[]
					this.Created = Convert.ToDateTime(reader["Created"]);
                    this.CreatedBy = new Guid(reader["CreatedBy"].ToString());
                    this.LastModified = Convert.ToDateTime(reader["LastModified"]);
                    this.LastModifedBy = new Guid(reader["LastModifedBy"].ToString());

                    this.Url = reader["Url"].ToString();
                    this.Teaser = reader["Abstract"].ToString();
                    this.ShowInProductList = Convert.ToBoolean(reader["ShowInProductList"]);
                    this.EnableRating = Convert.ToBoolean(reader["EnableRating"]);

                    this.SortRank1 = Convert.ToInt32(reader["SortRank1"]);
                    this.SortRank2 = Convert.ToInt32(reader["SortRank2"]);
                    this.TeaserFile = reader["TeaserFile"].ToString();
                    this.TeaserFileLink = reader["TeaserFileLink"].ToString();
                    this.MetaDescription = reader["MetaDescription"].ToString();
                    this.MetaKeywords = reader["MetaKeywords"].ToString();
                    this.CompiledMeta = reader["CompiledMeta"].ToString();
                    if (reader["ShippingAmount"] != DBNull.Value)
                    {
                        this.ShippingAmount = Convert.ToDecimal(reader["ShippingAmount"]);
                    }
                    

                }

            }
		
		}

        private bool Create()
        {
            this.Guid = Guid.NewGuid();

            int rowsAffected = DBProduct.Add(
                this.Guid,
                this.StoreGuid,
                this.taxClassGuid,
                this.ModelNumber,
                this.status,
                this.fulfillmentType,
                this.Weight,
                this.QuantityOnHand,
                this.SoldByQtys,
				this.ImageFileName,
                this.ImageFileBytes,
                this.Created,
                this.CreatedBy,
                this.LastModified,
                this.LastModifedBy,
                this.Url,
                this.Name,
                this.Description,
                this.Teaser,
                this.ShowInProductList,
                this.EnableRating,
                this.MetaDescription,
                this.MetaKeywords,
                this.SortRank1,
                this.SortRank2,
                this.TeaserFile,
                this.TeaserFileLink,
                this.CompiledMeta,
                this.ShippingAmount);

            bool result = (rowsAffected > 0);
            if (result)
            {
                ContentChangedEventArgs e = new ContentChangedEventArgs();
                OnContentChanged(e);
            }

            return result;

        }



        private bool Update()
        {
            Product product = new Product(this.Guid);
            DBProduct.AddHistory(
                Guid.NewGuid(),
                product.Guid,
                product.StoreGuid,
                product.TaxClassGuid,
                string.Empty,
                ConvertProductStatusToByte(product.Status),
                ConvertFulfillmentTypeToByte(product.FulfillmentType),
                product.Weight,
                product.QuantityOnHand,
                product.SoldByQtys,
				product.ImageFileName,
                product.ImageFileBytes,
                product.Created,
                product.CreatedBy,
                product.LastModified,
                product.LastModifedBy,
                DateTime.UtcNow,
                product.ShippingAmount);

            bool result = DBProduct.Update(
                this.Guid,
                this.taxClassGuid,
                this.ModelNumber,
                this.status,
                this.fulfillmentType,
                this.Weight,
                this.QuantityOnHand,
                this.SoldByQtys,
				this.ImageFileName,
                this.ImageFileBytes,
                this.LastModified,
                this.LastModifedBy,
                this.Url,
                this.Name,
                this.Description,
                this.Teaser,
                this.ShowInProductList,
                this.EnableRating,
                this.MetaDescription,
                this.MetaKeywords,
                this.SortRank1,
                this.SortRank2,
                this.TeaserFile,
                this.TeaserFileLink,
                this.CompiledMeta,
                this.ShippingAmount);

            if (result)
            {
                ContentChangedEventArgs e = new ContentChangedEventArgs();
                OnContentChanged(e);
            }

            return result;

        }

        

        #endregion

        #region Public Methods


        public bool Save()
        {
            
            bool result = false;

            if (this.Guid != Guid.Empty)
            {
                result = Update();
            }
            else
            {
                result = Create();
            }

            return result;
        }




        #endregion

        #region Static Methods

        public static bool Delete(
            Guid productGuid, 
            Guid userGuid, 
            string userIPAddress)
        {

            Product product = new Product(productGuid);
            if (
                (product.Guid == productGuid)
                &&(productGuid != Guid.Empty)
                )
            {
                DBProduct.AddHistory(
                    Guid.NewGuid(),
                    product.Guid,
                    product.StoreGuid,
                    product.TaxClassGuid,
                    product.Sku,
                    ConvertProductStatusToByte(product.Status),
                    ConvertFulfillmentTypeToByte(product.FulfillmentType),
                    product.Weight,
                    product.QuantityOnHand,
                    product.SoldByQtys,
					product.ImageFileName,
                    product.ImageFileBytes,
                    product.Created,
                    product.CreatedBy,
                    product.LastModified,
                    product.LastModifedBy,
                    DateTime.UtcNow,
                    product.ShippingAmount);

                OfferProduct.DeleteByProduct(
                    productGuid,
                    userGuid,
                    userIPAddress);
            }
            
            return DBProduct.Delete(
                productGuid,
                DateTime.UtcNow, 
                userGuid, 
                userIPAddress);
        }


        public static IDataReader GetAll(Guid storeGuid)
        {
            return DBProduct.GetAll(storeGuid);
        }

        public static IDataReader GetForSiteMap(Guid siteGuid, Guid storeGuid)
        {
            return DBProduct.GetForSiteMap(siteGuid, storeGuid);
        }

        public static DataTable GetPageForAdminList(
            Guid storeGuid,
            int pageNumber,
            int pageSize,
            out int totalPages)
        {
            return DBProduct.GetPageForAdminList(storeGuid, pageNumber, pageSize, out totalPages);
        }

        
        
        public static DataTable GetPage(
            Guid storeGuid,
            int pageNumber, 
            int pageSize,
            out int totalPages)
        {

            return DBProduct.GetPage(
                storeGuid,
                pageNumber,
                pageSize,
                out totalPages);
		
        }

        public static DataTable GetBySitePage(int siteId, int pageId)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("ModuleID", typeof(int));
            dataTable.Columns.Add("ModuleTitle", typeof(string));
            dataTable.Columns.Add("Guid", typeof(Guid));
            dataTable.Columns.Add("ModelNumber", typeof(string));
            dataTable.Columns.Add("Url", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Abstract", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("MetaDescription", typeof(string));
            dataTable.Columns.Add("MetaKeywords", typeof(string));
            dataTable.Columns.Add("ViewRoles", typeof(string));

            dataTable.Columns.Add("Created", typeof(DateTime));
            dataTable.Columns.Add("LastModified", typeof(DateTime));
            dataTable.Columns.Add("ShippingAmount", typeof(Decimal));

            using (IDataReader reader = DBProduct.GetProductByPage(siteId, pageId))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();


                    row["ModuleID"] = reader["ModuleID"];
                    row["ModuleTitle"] = reader["ModuleTitle"];
                    row["Guid"] = new Guid(reader["Guid"].ToString());
                    row["ModelNumber"] = reader["ModelNumber"];
                    row["Url"] = reader["Url"];
                    row["Name"] = reader["Name"];
                    row["Abstract"] = reader["Abstract"];
                    row["Description"] = reader["Description"];
                    row["MetaDescription"] = reader["MetaDescription"];
                    row["MetaKeywords"] = reader["MetaKeywords"];
                    row["ViewRoles"] = reader["ViewRoles"];

                    row["Created"] = Convert.ToDateTime(reader["Created"]);
                    row["LastModified"] = Convert.ToDateTime(reader["LastModified"]);
                    if (reader["ShippingAmount"] != DBNull.Value)
                    {
                        row["ShippingAmount"] = Convert.ToDecimal(reader["ShippingAmount"]);
                    }
                    else
                    {
                        row["ShippingAmount"] = 0;
                    }
                    
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        public static DataTable GetListForPageOfOffers(
            Guid storeGuid,
            int pageNumber,
            int pageSize)
        {
            return DBProduct.GetListForPageOfOffers(storeGuid, pageNumber, pageSize);
        }

        private static byte ConvertProductStatusToByte(ProductStatus status)
        {
            switch (status)
            {
                case ProductStatus.Available:
                    return 1;

                case ProductStatus.Discontinued:
                    return 2;

                case ProductStatus.Planned:
                    return 3;
            }

            return 3;

        }

        private static byte ConvertFulfillmentTypeToByte(FulfillmentType fulfillmentType)
        {
            switch (fulfillmentType)
            {
                case FulfillmentType.None:
                    return 3;

                case FulfillmentType.Download:
                    return 1;

                case FulfillmentType.PhysicalShipment:
                    return 2;

            }

            return 3;
        }

        public static ProductStatus ProductStatusFromInt32(int input)
        {
            return (ProductStatus)Enum.ToObject(typeof(ProductStatus), input);

        }

        public static ProductStatus ProductStatusFromString(string input)
        {
            int i;
            if(!int.TryParse(input, out i))
            {
                i = -1;
            }
            return ProductStatusFromInt32(i);

        }

        public static FulfillmentType FulfillmentTypeFromInt32(int input)
        {
            return (FulfillmentType)Enum.ToObject(typeof(FulfillmentType), input);

        }

        public static FulfillmentType FulfillmentTypeFromString(string input)
        {
            int i;
            if (!int.TryParse(input, out i))
            {
                i = -1;
            }
            return FulfillmentTypeFromInt32(i);

        }

        #endregion


        #region IIndexableContent
        public event ContentChangedEventHandler ContentChanged;

        protected void OnContentChanged(ContentChangedEventArgs e)
        {
			ContentChanged?.Invoke(this, e);
		}
        #endregion

    }

}
