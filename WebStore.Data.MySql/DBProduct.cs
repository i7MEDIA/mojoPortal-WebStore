using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using mojoPortal.Data;
using MySql.Data.MySqlClient;

namespace WebStore.Data
{
	public static class DBProduct
	{


		public static int Add(
			Guid guid,
			Guid storeGuid,
			Guid taxClassGuid,
			string modelNumber,
			byte status,
			byte fullfillmentType,
			decimal weight,
			decimal quantityOnHand,
			string soldByQtys,
			string imageFileName,
			byte[] imageFileBytes,
			DateTime created,
			Guid createdBy,
			DateTime lastModified,
			Guid lastModifedBy,
			string url,
			string name,
			string description,
			string teaser,
			bool showInProductList,
			bool enableRating,
			string metaDescription,
			string metaKeywords,
			int sortRank1,
			int sortRank2,
			string teaserFile,
			string teaserFileLink,
			string compiledMeta,
			decimal shippingAmount)
		{
			int intShowInProductList = showInProductList ? 1 : 0;
			int intEnableRating = enableRating ? 1 : 0;


			var sqlCommand = @"
INSERT INTO ws_Product (
	Guid, 
	StoreGuid, 
	TaxClassGuid, 
	ModelNumber, 
	Status, 
	FullfillmentType, 
	Weight, 
	QuantityOnHand, 
	SoldByQtys,
	ImageFileName, 
	ImageFileBytes, 
	Created, 
	CreatedBy, 
	LastModified, 
	IsDeleted, 
	Url, 
	Name, 
	Description, 
	Abstract, 
	MetaDescription, 
	MetaKeywords, 
	CompiledMeta, 
	ShowInProductList, 
	EnableRating, 
	SortRank1, 
	SortRank2, 
	TeaserFile, 
	TeaserFileLink, 
	LastModifedBy, 
	ShippingAmount 
	)
VALUES (
	?Guid, 
	?StoreGuid, 
	?TaxClassGuid, 
	?ModelNumber, 
	?Status, 
	?FullfillmentType, 
	?Weight, 
	?QuantityOnHand, 
	?SoldByQtys, 
	?ImageFileName, 
	?ImageFileBytes, 
	?Created, 
	?CreatedBy, 
	?LastModified, 
	0, 
	?Url, 
	?Name, 
	?Description, 
	?Abstract, 
	?MetaDescription, 
	?MetaKeywords, 
	?CompiledMeta, 
	?ShowInProductList, 
	?EnableRating, 
	?SortRank1, 
	?SortRank2, 
	?TeaserFile, 
	?TeaserFileLink, 
	?LastModifedBy, 
	?ShippingAmount 
);";
			var sqlParams = new List<MySqlParameter>() {

				new MySqlParameter("?Guid", MySqlDbType.VarChar, 36) {Direction = ParameterDirection.Input, Value = guid.ToString() },
				new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = storeGuid.ToString() },
				new MySqlParameter("?TaxClassGuid", MySqlDbType.VarChar, 36){Direction = ParameterDirection.Input, Value = taxClassGuid.ToString() },
				new MySqlParameter("?ModelNumber", MySqlDbType.VarChar, 255){Direction = ParameterDirection.Input, Value = modelNumber },
				new MySqlParameter("?Status", MySqlDbType.Int16){Direction = ParameterDirection.Input, Value = status },
				new MySqlParameter("?FullfillmentType", MySqlDbType.Int16){Direction = ParameterDirection.Input, Value = fullfillmentType },
				new MySqlParameter("?Weight", MySqlDbType.Decimal){Direction = ParameterDirection.Input, Value = weight },
				new MySqlParameter("?QuantityOnHand", MySqlDbType.Decimal){Direction = ParameterDirection.Input, Value = quantityOnHand },
				new MySqlParameter("?SoldByQtys", MySqlDbType.VarChar, 50){Direction = ParameterDirection.Input, Value = soldByQtys },
				new MySqlParameter("?ImageFileName", MySqlDbType.VarChar, 255){Direction = ParameterDirection.Input, Value = imageFileName },
				new MySqlParameter("?ImageFileBytes", MySqlDbType.LongBlob){Direction = ParameterDirection.Input, Value = imageFileBytes },
				new MySqlParameter("?Created", MySqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = created },
				new MySqlParameter("?CreatedBy", MySqlDbType.VarChar, 36) {Direction = ParameterDirection.Input, Value = createdBy.ToString() },
				new MySqlParameter("?LastModified", MySqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = lastModified },
				new MySqlParameter("?LastModifedBy", MySqlDbType.VarChar, 36) {Direction = ParameterDirection.Input, Value = lastModifedBy.ToString() },
				new MySqlParameter("?Url", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = url },
				new MySqlParameter("?Name", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = name },
				new MySqlParameter("?Description", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = description },
				new MySqlParameter("?Abstract", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = teaser },
				new MySqlParameter("?ShowInProductList", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = intShowInProductList },
				new MySqlParameter("?EnableRating", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = intEnableRating },
				new MySqlParameter("?MetaDescription", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = metaDescription },
				new MySqlParameter("?MetaKeywords", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = metaKeywords },
				new MySqlParameter("?SortRank1", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = sortRank1 },
				new MySqlParameter("?SortRank2", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = sortRank2 },
				new MySqlParameter("?TeaserFile", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = teaserFile },
				new MySqlParameter("?TeaserFileLink", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = teaserFileLink },
				new MySqlParameter("?CompiledMeta", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = compiledMeta },
				new MySqlParameter("?ShippingAmount", MySqlDbType.Decimal) {Direction = ParameterDirection.Input, Value = shippingAmount }
			};





			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParams.ToArray());
			return rowsAffected;



		}

		public static bool Update(
			Guid guid,
			Guid taxClassGuid,
			string modelNumber,
			byte status,
			byte fullfillmentType,
			decimal weight,
			decimal quantityOnHand,
			string soldByQtys,
			string imageFileName,
			byte[] imageFileBytes,
			DateTime lastModified,
			Guid lastModifedBy,
			string url,
			string name,
			string description,
			string teaser,
			bool showInProductList,
			bool enableRating,
			string metaDescription,
			string metaKeywords,
			int sortRank1,
			int sortRank2,
			string teaserFile,
			string teaserFileLink,
			string compiledMeta,
			decimal shippingAmount)
		{

			int intShowInProductList = showInProductList ? 1 : 0;
			int intEnableRating = enableRating ? 1 : 0;


			var sqlCommand = @"
UPDATE ws_Product 
SET  
	TaxClassGuid = ?TaxClassGuid, 
	ModelNumber = ?ModelNumber, 
	Status = ?Status, 
	FullfillmentType = ?FullfillmentType, 
	Weight = ?Weight, 
	QuantityOnHand = ?QuantityOnHand, 
	ImageFileName = ?ImageFileName, 
	ImageFileBytes = ?ImageFileBytes, 
	Url = ?Url, 
	Name = ?Name, 
	Abstract = ?Abstract, 
	Description = ?Description, 
	MetaDescription = ?MetaDescription, 
	MetaKeywords = ?MetaKeywords, 
	CompiledMeta = ?CompiledMeta, 
	ShowInProductList = ?ShowInProductList, 
	EnableRating = ?EnableRating, 
	SortRank1 = ?SortRank1, 
	SortRank2 = ?SortRank2, 
	TeaserFile = ?TeaserFile, 
	TeaserFileLink = ?TeaserFileLink, 
	LastModified = ?LastModified, 
	LastModifedBy = ?LastModifedBy, 
	ShippingAmount = ?ShippingAmount 
WHERE	Guid = ?Guid 
;";

			var sqlParams = new List<MySqlParameter>() {

				new MySqlParameter("?Guid", MySqlDbType.VarChar, 36) {Direction = ParameterDirection.Input, Value = guid.ToString() },
				new MySqlParameter("?TaxClassGuid", MySqlDbType.VarChar, 36){Direction = ParameterDirection.Input, Value = taxClassGuid.ToString() },
				new MySqlParameter("?ModelNumber", MySqlDbType.VarChar, 255){Direction = ParameterDirection.Input, Value = modelNumber },
				new MySqlParameter("?Status", MySqlDbType.Int16){Direction = ParameterDirection.Input, Value = status },
				new MySqlParameter("?FullfillmentType", MySqlDbType.Int16){Direction = ParameterDirection.Input, Value = fullfillmentType },
				new MySqlParameter("?Weight", MySqlDbType.Decimal){Direction = ParameterDirection.Input, Value = weight },
				new MySqlParameter("?QuantityOnHand", MySqlDbType.Decimal){Direction = ParameterDirection.Input, Value = quantityOnHand },
				new MySqlParameter("?SoldByQtys", MySqlDbType.VarChar, 50){Direction = ParameterDirection.Input, Value = soldByQtys },
				new MySqlParameter("?ImageFileName", MySqlDbType.VarChar, 255){Direction = ParameterDirection.Input, Value = imageFileName },
				new MySqlParameter("?ImageFileBytes", MySqlDbType.LongBlob){Direction = ParameterDirection.Input, Value = imageFileBytes },
				new MySqlParameter("?LastModified", MySqlDbType.DateTime) {Direction = ParameterDirection.Input, Value = lastModified },
				new MySqlParameter("?LastModifedBy", MySqlDbType.VarChar, 36) {Direction = ParameterDirection.Input, Value = lastModifedBy.ToString() },
				new MySqlParameter("?Url", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = url },
				new MySqlParameter("?Name", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = name },
				new MySqlParameter("?Description", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = description },
				new MySqlParameter("?Abstract", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = teaser },
				new MySqlParameter("?ShowInProductList", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = intShowInProductList },
				new MySqlParameter("?EnableRating", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = intEnableRating },
				new MySqlParameter("?MetaDescription", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = metaDescription },
				new MySqlParameter("?MetaKeywords", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = metaKeywords },
				new MySqlParameter("?SortRank1", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = sortRank1 },
				new MySqlParameter("?SortRank2", MySqlDbType.Int32) {Direction = ParameterDirection.Input, Value = sortRank2 },
				new MySqlParameter("?TeaserFile", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = teaserFile },
				new MySqlParameter("?TeaserFileLink", MySqlDbType.VarChar, 255) {Direction = ParameterDirection.Input, Value = teaserFileLink },
				new MySqlParameter("?CompiledMeta", MySqlDbType.Text) {Direction = ParameterDirection.Input, Value = compiledMeta },
				new MySqlParameter("?ShippingAmount", MySqlDbType.Decimal) {Direction = ParameterDirection.Input, Value = shippingAmount }
			};

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParams.ToArray());

			return (rowsAffected > -1);


		}

		public static bool Delete(
			Guid guid,
			DateTime deletedTime,
			Guid deletedBy,
			string deletedFromIP)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("UPDATE ws_Product ");
			sqlCommand.Append("SET  ");
			sqlCommand.Append("IsDeleted = 1, ");
			sqlCommand.Append("DeletedBy = ?DeletedBy, ");
			sqlCommand.Append("DeletedFromIP = ?DeletedFromIP, ");
			sqlCommand.Append("DeletedTime = ?DeletedTime ");

			sqlCommand.Append("WHERE  ");
			sqlCommand.Append("Guid = ?Guid ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[4];

			arParams[0] = new MySqlParameter("?Guid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = guid.ToString();

			arParams[1] = new MySqlParameter("?DeletedBy", MySqlDbType.VarChar, 36);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = deletedBy.ToString();

			arParams[2] = new MySqlParameter("?DeletedFromIP", MySqlDbType.VarChar, 255);
			arParams[2].Direction = ParameterDirection.Input;
			arParams[2].Value = deletedFromIP;

			arParams[3] = new MySqlParameter("?DeletedTime", MySqlDbType.DateTime);
			arParams[3].Direction = ParameterDirection.Input;
			arParams[3].Value = deletedTime;

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand.ToString(),
				arParams);

			return (rowsAffected > -1);



		}

		public static IDataReader Get(Guid guid)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT  * ");

			sqlCommand.Append("FROM	ws_Product ");
			sqlCommand.Append("WHERE ");
			sqlCommand.Append("Guid = ?Guid ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[1];

			arParams[0] = new MySqlParameter("?Guid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = guid.ToString();


			return MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams);

		}

		public static IDataReader GetAll(Guid storeGuid)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT  * ");

			sqlCommand.Append("FROM	ws_Product ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("StoreGuid = ?StoreGuid ");
			sqlCommand.Append("AND IsDeleted = false ");
			sqlCommand.Append("ORDER BY ");
			sqlCommand.Append("Name ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[1];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams);


		}

		public static IDataReader GetForSiteMap(Guid siteGuid, Guid storeGuid)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT ");
			sqlCommand.Append("p.Guid, ");
			sqlCommand.Append("p.StoreGuid, ");
			sqlCommand.Append("p.Url, ");
			sqlCommand.Append("p.LastModified ");

			sqlCommand.Append("FROM	ws_Product p  ");

			sqlCommand.Append("JOIN ");
			sqlCommand.Append("ws_Store s ");
			sqlCommand.Append("ON ");
			sqlCommand.Append("p.StoreGuid = s.Guid ");

			sqlCommand.Append("JOIN (");
			sqlCommand.Append("SELECT DISTINCT op.ProductGuid ");
			sqlCommand.Append("FROM ws_OfferProduct op ");
			sqlCommand.Append("JOIN ws_Offer o ");
			sqlCommand.Append("ON o.Guid = op.OfferGuid ");
			sqlCommand.Append("WHERE ");
			sqlCommand.Append("((o.StoreGuid = ?StoreGuid) OR (?StoreGuid = '00000000-0000-0000-0000-000000000000')) ");

			sqlCommand.Append("AND op.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsVisible = 1 ");

			sqlCommand.Append(") f ");
			sqlCommand.Append("ON f.ProductGuid = p.Guid ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("s.SiteGuid = ?SiteGuid ");
			sqlCommand.Append("AND ((p.StoreGuid = ?StoreGuid) OR (?StoreGuid = '00000000-0000-0000-0000-000000000000')) ");
			sqlCommand.Append("AND p.ShowInProductList = 1 ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");

			sqlCommand.Append("ORDER BY ");
			sqlCommand.Append("p.SortRank1, p.SortRank2, p.Name ");


			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[2];

			arParams[0] = new MySqlParameter("?SiteGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = siteGuid.ToString();

			arParams[1] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = storeGuid.ToString();

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams);

		}

		/// <summary>
		/// Gets a count of rows in the ws_Product table.
		/// </summary>
		public static int GetCount(Guid storeGuid)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT  Count(*) ");
			sqlCommand.Append("FROM	ws_Product p ");

			sqlCommand.Append("JOIN (");
			sqlCommand.Append("SELECT DISTINCT op.ProductGuid ");
			sqlCommand.Append("FROM ws_OfferProduct op ");
			sqlCommand.Append("JOIN ws_Offer o ");
			sqlCommand.Append("ON o.Guid = op.OfferGuid ");
			sqlCommand.Append("WHERE ");
			sqlCommand.Append("o.StoreGuid = ?StoreGuid ");

			sqlCommand.Append("AND op.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsVisible = 1 ");

			sqlCommand.Append(") f ");
			sqlCommand.Append("ON f.ProductGuid = p.Guid ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("p.StoreGuid = ?StoreGuid ");
			sqlCommand.Append("AND p.ShowInProductList = 1 ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[1];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			return Convert.ToInt32(MySqlHelper.ExecuteScalar(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams));
		}

		/// <summary>
		/// Gets a count of rows in the ws_Product table.
		/// </summary>
		public static int GetCountForAdminList(Guid storeGuid)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT  Count(*) ");
			sqlCommand.Append("FROM	ws_Product p ");

			//sqlCommand.Append("JOIN (");
			//sqlCommand.Append("SELECT DISTINCT op.ProductGuid ");
			//sqlCommand.Append("FROM ws_OfferProduct op ");
			//sqlCommand.Append("JOIN ws_Offer o ");
			//sqlCommand.Append("ON o.Guid = op.OfferGuid ");
			//sqlCommand.Append("WHERE ");
			//sqlCommand.Append("o.StoreGuid = ?StoreGuid ");

			//sqlCommand.Append("AND op.IsDeleted = 0 ");
			//sqlCommand.Append("AND o.IsDeleted = 0 ");
			//sqlCommand.Append("AND o.IsVisible = 1 ");

			//sqlCommand.Append(") f ");
			//sqlCommand.Append("ON f.ProductGuid = p.Guid ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("p.StoreGuid = ?StoreGuid ");
			//sqlCommand.Append("AND p.ShowInProductList = 1 ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[1];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			return Convert.ToInt32(MySqlHelper.ExecuteScalar(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams));
		}

		public static DataTable GetPageForAdminList(
			Guid storeGuid,
			int pageNumber,
			int pageSize,
			out int totalPages)
		{
			int pageLowerBound = (pageSize * pageNumber) - pageSize;
			totalPages = 1;
			int totalRows = GetCountForAdminList(storeGuid);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT	p.* ");

			sqlCommand.Append("FROM	ws_Product p  ");


			//sqlCommand.Append("LEFT OUTER JOIN (");
			//sqlCommand.Append("SELECT DISTINCT op.ProductGuid ");
			//sqlCommand.Append("FROM ws_OfferProduct op ");
			//sqlCommand.Append("JOIN ws_Offer o ");
			//sqlCommand.Append("ON o.Guid = op.OfferGuid ");
			//sqlCommand.Append("WHERE ");
			//sqlCommand.Append("o.StoreGuid = ?StoreGuid ");

			//sqlCommand.Append("AND op.IsDeleted = 0 ");
			//sqlCommand.Append("AND o.IsDeleted = 0 ");
			//sqlCommand.Append("AND o.IsVisible = 1 ");

			//sqlCommand.Append(") f ");
			//sqlCommand.Append("ON f.ProductGuid = p.Guid ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("p.StoreGuid = ?StoreGuid ");
			//sqlCommand.Append("AND p.ShowInProductList = 1 ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");



			sqlCommand.Append("ORDER BY ");
			sqlCommand.Append("p.Name ");

			sqlCommand.Append("LIMIT " + pageLowerBound.ToString() + ", ?PageSize ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[2];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			arParams[1] = new MySqlParameter("?PageSize", MySqlDbType.Int32);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = pageSize;

			DataTable dataTable = GetProductEmptyTable();

			using (IDataReader reader = MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams))
			{

				while (reader.Read())
				{
					DataRow row = dataTable.NewRow();
					row["Guid"] = new Guid(reader["Guid"].ToString());
					//row["StoreGuid"] = new Guid(reader["StoreGuid"].ToString());
					//row["TaxClassGuid"] = new Guid(reader["TaxClassGuid"].ToString());
					row["ModelNumber"] = reader["ModelNumber"];

					// doh! actual field is mis-spelled as FullFillmentType
					row["FulFillmentType"] = Convert.ToInt32(reader["FullFillmentType"]);

					row["Weight"] = Convert.ToDecimal(reader["Weight"]);
					//row["RetailPrice"] = Convert.ToDecimal(reader["RetailPrice"]);
					row["Url"] = reader["Url"];
					row["Name"] = reader["Name"];
					row["Abstract"] = reader["Abstract"];
					row["Description"] = reader["Description"];

					dataTable.Rows.Add(row);

				}
			}


			return dataTable;



		}


		public static DataTable GetPage(
			Guid storeGuid,
			int pageNumber,
			int pageSize,
			out int totalPages)
		{
			int pageLowerBound = (pageSize * pageNumber) - pageSize;
			totalPages = 1;
			int totalRows = GetCount(storeGuid);

			if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					totalPages += 1;
				}
			}

			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT	p.* ");

			sqlCommand.Append("FROM	ws_Product p  ");


			sqlCommand.Append("JOIN (");
			sqlCommand.Append("SELECT DISTINCT op.ProductGuid ");
			sqlCommand.Append("FROM ws_OfferProduct op ");
			sqlCommand.Append("JOIN ws_Offer o ");
			sqlCommand.Append("ON o.Guid = op.OfferGuid ");
			sqlCommand.Append("WHERE ");
			sqlCommand.Append("o.StoreGuid = ?StoreGuid ");

			sqlCommand.Append("AND op.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsDeleted = 0 ");
			sqlCommand.Append("AND o.IsVisible = 1 ");

			sqlCommand.Append(") f ");
			sqlCommand.Append("ON f.ProductGuid = p.Guid ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("p.StoreGuid = ?StoreGuid ");
			sqlCommand.Append("AND p.ShowInProductList = 1 ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");

			sqlCommand.Append("ORDER BY ");
			sqlCommand.Append("p.SortRank1, p.SortRank2, p.Name ");

			sqlCommand.Append("LIMIT " + pageLowerBound.ToString() + ", ?PageSize ");
			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[2];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			arParams[1] = new MySqlParameter("?PageSize", MySqlDbType.Int32);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = pageSize;

			DataTable dataTable = GetProductEmptyTable();

			using (IDataReader reader = MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams))
			{

				while (reader.Read())
				{
					DataRow row = dataTable.NewRow();
					row["Guid"] = new Guid(reader["Guid"].ToString());
					//row["StoreGuid"] = new Guid(reader["StoreGuid"].ToString());
					//row["TaxClassGuid"] = new Guid(reader["TaxClassGuid"].ToString());
					row["ModelNumber"] = reader["ModelNumber"];

					// doh! actual field is mis-spelled as FullFillmentType
					row["FulFillmentType"] = Convert.ToInt32(reader["FullFillmentType"]);

					row["Weight"] = Convert.ToDecimal(reader["Weight"]);
					//row["RetailPrice"] = Convert.ToDecimal(reader["RetailPrice"]);
					row["Url"] = reader["Url"];
					row["Name"] = reader["Name"];
					row["Abstract"] = reader["Abstract"];
					row["Description"] = reader["Description"];
					row["EnableRating"] = Convert.ToBoolean(reader["EnableRating"]);
					row["TeaserFile"] = reader["TeaserFile"];
					row["TeaserFileLink"] = reader["TeaserFileLink"];

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

		private static DataTable GetProductEmptyTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Guid", typeof(Guid));
			//dataTable.Columns.Add("StoreGuid", typeof(Guid));
			//dataTable.Columns.Add("TaxClassGuid", typeof(Guid));
			dataTable.Columns.Add("ModelNumber", typeof(string));
			// doh! actual field is mis-spelled as FullFillmentType
			dataTable.Columns.Add("FulFillmentType", typeof(int));
			dataTable.Columns.Add("Weight", typeof(decimal));
			//dataTable.Columns.Add("RetailPrice", typeof(decimal));
			dataTable.Columns.Add("Url", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Abstract", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("EnableRating", typeof(bool));
			dataTable.Columns.Add("TeaserFile", typeof(string));
			dataTable.Columns.Add("TeaserFileLink", typeof(string));
			dataTable.Columns.Add("ShippingAmount", typeof(decimal));

			return dataTable;

		}

		private static DataTable GetOfferProductEmptyTable()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Guid", typeof(Guid));

			dataTable.Columns.Add("OfferGuid", typeof(Guid));
			dataTable.Columns.Add("ModelNumber", typeof(string));
			// doh! actual field is mis-spelled as FullFillmentType
			dataTable.Columns.Add("FulFillmentType", typeof(int));
			dataTable.Columns.Add("Weight", typeof(decimal));

			dataTable.Columns.Add("Url", typeof(string));
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.Columns.Add("Abstract", typeof(string));
			dataTable.Columns.Add("Description", typeof(string));
			dataTable.Columns.Add("EnableRating", typeof(bool));
			dataTable.Columns.Add("TeaserFile", typeof(string));
			dataTable.Columns.Add("TeaserFileLink", typeof(string));
			dataTable.Columns.Add("ShippingAmount", typeof(decimal));

			return dataTable;

		}


		public static DataTable GetListForPageOfOffers(
			Guid storeGuid,
			int pageNumber,
			int pageSize)
		{
			int pageLowerBound = (pageSize * pageNumber) - pageSize;
			//totalPages = 1;
			int totalRows = DBOffer.GetCount(storeGuid);

			//if (pageSize > 0) totalPages = totalRows / pageSize;

			if (totalRows <= pageSize)
			{
				//totalPages = 1;
			}
			else
			{
				int remainder;
				Math.DivRem(totalRows, pageSize, out remainder);
				if (remainder > 0)
				{
					//totalPages += 1;
				}
			}


			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT	p.*, ");
			sqlCommand.Append("op.OfferGuid ");

			sqlCommand.Append("FROM	ws_Product p  ");

			sqlCommand.Append("JOIN ");
			sqlCommand.Append("ws_OfferProduct op ");
			sqlCommand.Append("ON p.Guid = op.ProductGuid ");

			sqlCommand.Append("JOIN ");

			sqlCommand.Append("(SELECT	Guid ");
			sqlCommand.Append("FROM	ws_Offer ");
			sqlCommand.Append("WHERE ");
			sqlCommand.Append("StoreGuid = ?StoreGuid ");
			sqlCommand.Append("and IsDeleted = false ");

			sqlCommand.Append("LIMIT " + pageLowerBound.ToString() + ", ?PageSize ");
			sqlCommand.Append("ORDER BY  ");
			sqlCommand.Append("SortRank1, SortRank2, Name  ");
			sqlCommand.Append(") o ");

			sqlCommand.Append("ON op.OfferGuid = o.Guid ");

			sqlCommand.Append("WHERE ");

			sqlCommand.Append("p.StoreGuid = ?StoreGuid ");
			sqlCommand.Append("AND p.IsDeleted = 0 ");

			//sqlCommand.Append("AND op.OfferGuid IN ");




			sqlCommand.Append("ORDER BY ");
			sqlCommand.Append("p.SortRank1, p.SortRank2, p.Name ");


			sqlCommand.Append(";");

			MySqlParameter[] arParams = new MySqlParameter[2];

			arParams[0] = new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = storeGuid.ToString();

			arParams[1] = new MySqlParameter("?PageSize", MySqlDbType.Int32);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = pageSize;

			DataTable dataTable = GetOfferProductEmptyTable();

			using (IDataReader reader = MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams))
			{

				while (reader.Read())
				{
					DataRow row = dataTable.NewRow();
					row["Guid"] = new Guid(reader["Guid"].ToString());
					row["OfferGuid"] = new Guid(reader["OfferGuid"].ToString());
					row["ModelNumber"] = reader["ModelNumber"];

					// doh! actual field is mis-spelled as FullFillmentType
					row["FulFillmentType"] = Convert.ToInt32(reader["FullFillmentType"]);

					row["Weight"] = Convert.ToDecimal(reader["Weight"]);
					row["Url"] = reader["Url"];
					row["Name"] = reader["Name"];
					row["Abstract"] = reader["Abstract"];
					row["Description"] = reader["Description"];
					row["EnableRating"] = Convert.ToBoolean(reader["EnableRating"]);
					row["TeaserFile"] = reader["TeaserFile"];
					row["TeaserFileLink"] = reader["TeaserFileLink"];
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



		public static IDataReader GetProductByPage(int siteId, int pageId)
		{
			StringBuilder sqlCommand = new StringBuilder();
			sqlCommand.Append("SELECT  pr.*, ");
			sqlCommand.Append("s.ModuleID, ");
			sqlCommand.Append("m.ModuleTitle, ");
			sqlCommand.Append("m.ViewRoles, ");
			sqlCommand.Append("md.FeatureName ");

			sqlCommand.Append("FROM	ws_Product pr ");

			sqlCommand.Append("JOIN	ws_Store s ");
			sqlCommand.Append("ON s.Guid = pr.StoreGuid ");

			sqlCommand.Append("JOIN	mp_Modules m ");
			sqlCommand.Append("ON s.ModuleID = m.ModuleID ");

			sqlCommand.Append("JOIN	mp_ModuleDefinitions md ");
			sqlCommand.Append("ON m.ModuleDefID = md.ModuleDefID ");

			sqlCommand.Append("JOIN	mp_PageModules pm ");
			sqlCommand.Append("ON m.ModuleID = pm.ModuleID ");

			sqlCommand.Append("JOIN	mp_Pages p ");
			sqlCommand.Append("ON p.PageID = pm.PageID ");

			sqlCommand.Append("WHERE ");
			sqlCommand.Append("p.SiteID = ?SiteID ");
			sqlCommand.Append("AND pm.PageID = ?PageID ");
			sqlCommand.Append("AND pr.IsDeleted = 0 ");
			sqlCommand.Append("AND pr.ShowInProductList = 1 ");

			sqlCommand.Append(" ; ");

			MySqlParameter[] arParams = new MySqlParameter[2];

			arParams[0] = new MySqlParameter("?SiteID", MySqlDbType.Int32);
			arParams[0].Direction = ParameterDirection.Input;
			arParams[0].Value = siteId;

			arParams[1] = new MySqlParameter("?PageID", MySqlDbType.Int32);
			arParams[1].Direction = ParameterDirection.Input;
			arParams[1].Value = pageId;

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetReadConnectionString(),
				sqlCommand.ToString(),
				arParams);

		}

		public static int AddHistory(
			Guid guid,
			Guid productGuid,
			Guid storeGuid,
			Guid taxClassGuid,
			string sku,
			byte status,
			byte fullfillmentType,
			decimal weight,
			decimal quantityOnHand,
			string soldByQtys,
			string imageFileName,
			byte[] imageFileBytes,
			DateTime created,
			Guid createdBy,
			DateTime lastModified,
			Guid lastModifedBy,
			DateTime logTime,
			decimal shippingAmount)
		{
			var sqlCommand = @"
INSERT INTO ws_ProductHistory (
	Guid, 
	ProductGuid, 
	StoreGuid, 
	TaxClassGuid, 
	Sku, 
	Status, 
	FullfillmentType, 
	Weight, 
	QuantityOnHand, 
	SoldByQtys, 
	ImageFileName, 
	ImageFileBytes, 
	Created, 
	CreatedBy, 
	LastModified, 
	LastModifedBy, 
	LogTime, 
	ShippingAmount )
VALUES (
	?Guid, 
	?ProductGuid, 
	?StoreGuid, 
	?TaxClassGuid, 
	?Sku, 
	?Status, 
	?FullfillmentType, 
	?Weight, 
	?QuantityOnHand, 
	?SoldByQtys, 
	?ImageFileName, 
	?ImageFileBytes, 
	?Created, 
	?CreatedBy, 
	?LastModified, 
	?LastModifedBy, 
	?LogTime, 
	?ShippingAmount )
;";

			var sqlParams = new List<MySqlParameter>() {
				new MySqlParameter("?Guid", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = guid.ToString() },
				new MySqlParameter("?ProductGuid", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = productGuid.ToString() },
				new MySqlParameter("?StoreGuid", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = storeGuid.ToString() },
				new MySqlParameter("?TaxClassGuid", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = taxClassGuid.ToString() },
				new MySqlParameter("?Sku", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = sku },
				new MySqlParameter("?Status", MySqlDbType.Int16) { Direction = ParameterDirection.Input, Value = status },
				new MySqlParameter("?FullfillmentType", MySqlDbType.Int16) { Direction = ParameterDirection.Input, Value = fullfillmentType },
				new MySqlParameter("?Weight", MySqlDbType.Decimal) { Direction = ParameterDirection.Input, Value = weight },
				new MySqlParameter("?QuantityOnHand", MySqlDbType.Decimal) { Direction = ParameterDirection.Input, Value = quantityOnHand },
				new MySqlParameter("?SoldByQtys", MySqlDbType.VarChar, 50) { Direction = ParameterDirection.Input, Value = soldByQtys },
				new MySqlParameter("?ImageFileName", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = imageFileName },
				new MySqlParameter("?ImageFileBytes", MySqlDbType.LongBlob) { Direction = ParameterDirection.Input, Value = imageFileBytes },
				new MySqlParameter("?Created", MySqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = created },
				new MySqlParameter("?CreatedBy", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = createdBy.ToString() },
				new MySqlParameter("?LastModified", MySqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = lastModified },
				new MySqlParameter("?LastModifedBy", MySqlDbType.VarChar, 36) { Direction = ParameterDirection.Input, Value = lastModifedBy.ToString() },
				new MySqlParameter("?LogTime", MySqlDbType.DateTime) { Direction = ParameterDirection.Input, Value = logTime },
				new MySqlParameter("?ShippingAmount", MySqlDbType.Decimal) { Direction = ParameterDirection.Input, Value = shippingAmount }
			};

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand.ToString(),
				sqlParams.ToArray());
			return rowsAffected;

		}


	}
}
