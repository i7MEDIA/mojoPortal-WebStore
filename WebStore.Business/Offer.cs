using mojoPortal.Business;
using System;
using System.Data;
using WebStore.Data;

namespace WebStore.Business;

/// <summary>
/// Represents an Offer. Products are not sold directly but via offers.
/// It allows bundling products together for sale.
/// </summary>
public class Offer : IIndexableContent
{
	#region Constructors

	public Offer()
	{ }


	public Offer(Guid guid)
	{
		GetOffer(guid);
	}

	#endregion


	#region Private Properties

	private bool availabilityChecked = false;
	private bool isAvailable = false;

	#endregion


	#region Public Properties

	public Guid Guid { get; set; } = Guid.Empty;
	public Guid StoreGuid { get; set; } = Guid.Empty;
	public Guid TaxClassGuid { get; set; } = Guid.Empty;
	public string Name { get; set; } = string.Empty;
	public string ProductListName { get; set; } = string.Empty;
	public string MetaKeywords { get; set; } = string.Empty;
	public string MetaDescription { get; set; } = string.Empty;
	public string CompiledMeta { get; set; } = string.Empty;
	public decimal Price { get; set; } = 0;
	public int MaxPerOrder { get; set; } = 0;
	public string Url { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Teaser { get; set; } = string.Empty;
	public int SortRank1 { get; set; } = 5000;
	public int SortRank2 { get; set; } = 5000;
	public bool IsVisible { get; set; }
	public bool IsSpecial { get; set; }
	public bool IsDonation { get; set; } = false;
	public bool UserCanSetPrice { get; set; } = false;
	public bool ShowDetailLink { get; set; } = true;
	public DateTime Created { get; set; } = DateTime.UtcNow;
	public Guid CreatedBy { get; set; } = Guid.Empty;
	public string CreatedFromIP { get; set; } = string.Empty;
	public DateTime LastModified { get; set; } = DateTime.UtcNow;
	public Guid LastModifiedBy { get; set; } = Guid.Empty;
	public string LastModifiedFromIP { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime DeletedTime { get; set; } = DateTime.MaxValue;
	public Guid DeletedBy { get; set; } = Guid.Empty;
	public string DeletedFromIP { get; set; } = string.Empty;

	public bool IsAvailable
	{
		get
		{
			if (!availabilityChecked)
			{
				CheckAvailability();
			}

			return isAvailable;
		}
	}

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

	#endregion


	#region Private Methods

	private void CheckAvailability()
	{
		// TODO: lookup
		isAvailable = true;
		availabilityChecked = true;
	}

	private void GetOffer(Guid guid)
	{
		using IDataReader reader = DBOffer.GetOne(guid);

		if (reader.Read())
		{
			Guid = new Guid(reader["Guid"].ToString());
			StoreGuid = new Guid(reader["StoreGuid"].ToString());
			IsVisible = Convert.ToBoolean(reader["IsVisible"]);
			IsSpecial = Convert.ToBoolean(reader["IsSpecial"]);
			Created = Convert.ToDateTime(reader["Created"]);
			CreatedBy = new Guid(reader["CreatedBy"].ToString());
			Url = reader["Url"].ToString();
			CreatedFromIP = reader["CreatedFromIP"].ToString();
			IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);

			if (reader["DeletedTime"] != DBNull.Value)
			{
				DeletedTime = Convert.ToDateTime(reader["DeletedTime"]);
			}

			if (reader["DeletedBy"] != DBNull.Value)
			{
				DeletedBy = new Guid(reader["DeletedBy"].ToString());
			}

			DeletedFromIP = reader["DeletedFromIP"].ToString();
			LastModified = Convert.ToDateTime(reader["LastModified"]);
			LastModifiedBy = new Guid(reader["LastModifiedBy"].ToString());
			LastModifiedFromIP = reader["LastModifiedFromIP"].ToString();

			Name = reader["Name"].ToString();
			Description = reader["Description"].ToString();
			Teaser = reader["Abstract"].ToString();

			if (reader["TaxClassGuid"] != DBNull.Value)
			{
				TaxClassGuid = new Guid(reader["TaxClassGuid"].ToString());
			}

			IsDonation = Convert.ToBoolean(reader["IsDonation"]);

			if (reader["Price"] != DBNull.Value)
			{
				Price = Convert.ToDecimal(reader["Price"].ToString());
			}

			ShowDetailLink = Convert.ToBoolean(reader["ShowDetailLink"]);
			ProductListName = reader["ProductListName"].ToString();

			UserCanSetPrice = Convert.ToBoolean(reader["UserCanSetPrice"]);

			MaxPerOrder = Convert.ToInt32(reader["MaxPerOrder"]);
			SortRank1 = Convert.ToInt32(reader["SortRank1"]);
			SortRank2 = Convert.ToInt32(reader["SortRank2"]);
			MetaDescription = reader["MetaDescription"].ToString();
			MetaKeywords = reader["MetaKeywords"].ToString();
			CompiledMeta = reader["CompiledMeta"].ToString();
		}
	}

	private bool Create()
	{
		Guid = Guid.NewGuid();

		var rowsAffected = DBOffer.Create(
			Guid,
			StoreGuid,
			IsVisible,
			IsSpecial,
			TaxClassGuid,
			Url,
			Created,
			CreatedBy,
			CreatedFromIP,
			LastModified,
			LastModifiedBy,
			LastModifiedFromIP,
			IsDonation,
			Name,
			Description,
			Teaser,
			Price,
			ProductListName,
			ShowDetailLink,
			UserCanSetPrice,
			MaxPerOrder,
			SortRank1,
			SortRank2,
			MetaDescription,
			MetaKeywords,
			CompiledMeta
		);

		var result = rowsAffected > 0;

		if (result)
		{
			var e = new ContentChangedEventArgs();
			OnContentChanged(e);
		}

		return result;
	}


	private bool Update()
	{
		var offer = new Offer(Guid);

		DBOffer.AddOfferHistory(
			Guid.NewGuid(),
			offer.Guid,
			offer.StoreGuid,
			offer.IsVisible,
			offer.IsSpecial,
			offer.Created,
			offer.CreatedBy,
			offer.CreatedFromIP,
			offer.IsDeleted,
			offer.DeletedTime,
			offer.DeletedBy,
			offer.DeletedFromIP,
			offer.LastModified,
			offer.LastModifiedBy,
			offer.LastModifiedFromIP,
			DateTime.UtcNow,
			offer.TaxClassGuid,
			offer.Url
		);

		var result = DBOffer.Update(
			Guid,
			IsVisible,
			IsSpecial,
			TaxClassGuid,
			Url,
			LastModified,
			LastModifiedBy,
			LastModifiedFromIP,
			IsDonation,
			Name,
			Description,
			Teaser,
			Price,
			ProductListName,
			ShowDetailLink,
			UserCanSetPrice,
			MaxPerOrder,
			SortRank1,
			SortRank2,
			MetaDescription,
			MetaKeywords,
			CompiledMeta
		);

		if (result)
		{
			var e = new ContentChangedEventArgs();
			OnContentChanged(e);
		}

		return result;
	}

	#endregion


	#region Public Methods

	public bool Save()
	{
		bool result;

		if (Guid != Guid.Empty)
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

	public static bool Delete(Guid guid, Guid deletedBy, string deletedFromIP)
	{
		var offer = new Offer(guid);

		DBOffer.AddOfferHistory(
			Guid.NewGuid(),
			offer.Guid,
			offer.StoreGuid,
			offer.IsVisible,
			offer.IsSpecial,
			offer.Created,
			offer.CreatedBy,
			offer.CreatedFromIP,
			offer.IsDeleted,
			offer.DeletedTime,
			offer.DeletedBy,
			offer.DeletedFromIP,
			offer.LastModified,
			offer.LastModifiedBy,
			offer.LastModifiedFromIP,
			DateTime.UtcNow,
			offer.TaxClassGuid,
			offer.Url
		);

		return DBOffer.Delete(
			guid,
			DateTime.UtcNow,
			deletedBy,
			deletedFromIP
		);
	}


	/// <summary>
	/// this is for admin view and includes hidden products where IsVisible is false
	/// </summary>
	/// <param name="storeGuid"></param>
	/// <param name="languageGuid"></param>
	/// <param name="pageNumber"></param>
	/// <param name="pageSize"></param>
	/// <param name="totalPages"></param>
	/// <returns></returns>
	public static IDataReader GetPage(
		Guid storeGuid,
		int pageNumber,
		int pageSize,
		out int totalPages
	)
	{
		return DBOffer.GetPage(
			storeGuid,
			pageNumber,
			pageSize,
			out totalPages
		);
	}


	public static DataTable GetPublicPage(
		Guid storeGuid,
		int pageNumber,
		int pageSize,
		out int totalPages
	)
	{
		return DBOffer.GetPublicPage(
			storeGuid,
			pageNumber,
			pageSize,
			out totalPages
		);
	}


	/// <summary>
	/// Gets a page of data from the ws_Offer table.
	/// </summary>
	/// <param name="pageNumber">The page number.</param>
	/// <param name="pageSize">Size of the page.</param>
	/// <param name="totalPages">total pages</param>
	public static IDataReader GetPageForProductList(
		Guid storeGuid,
		int pageNumber,
		int pageSize,
		out int totalPages
	)
	{
		return DBOffer.GetPageForProductList(
			storeGuid,
			pageNumber,
			pageSize,
			out totalPages
		);
	}


	public static DataTable GetListForPageOfProducts(
		Guid storeGuid,
		int pageNumber,
		int pageSize
	)
	{
		return DBOffer.GetListForPageOfProducts(storeGuid, pageNumber, pageSize);
	}


	public static DataTable GetByProduct(Guid productGuid)
	{
		return DBOffer.GetByProduct(productGuid);
	}


	public static IDataReader GetTop10Specials(Guid storeGuid)
	{
		return DBOffer.GetTop10Specials(storeGuid);
	}


	public static DataTable GetBySitePage(int siteId, int pageId)
	{
		var dataTable = new DataTable();

		dataTable.Columns.Add("ModuleID", typeof(int));
		dataTable.Columns.Add("ModuleTitle", typeof(string));
		dataTable.Columns.Add("Guid", typeof(Guid));
		dataTable.Columns.Add("Url", typeof(string));
		dataTable.Columns.Add("Name", typeof(string));
		dataTable.Columns.Add("Abstract", typeof(string));
		dataTable.Columns.Add("Description", typeof(string));
		dataTable.Columns.Add("MetaDescription", typeof(string));
		dataTable.Columns.Add("MetaKeywords", typeof(string));
		dataTable.Columns.Add("ViewRoles", typeof(string));

		dataTable.Columns.Add("Created", typeof(DateTime));
		dataTable.Columns.Add("LastModified", typeof(DateTime));

		using IDataReader reader = DBOffer.GetByPage(siteId, pageId);

		while (reader.Read())
		{
			DataRow row = dataTable.NewRow();

			row["ModuleID"] = reader["ModuleID"];
			row["ModuleTitle"] = reader["ModuleTitle"];
			row["Guid"] = new Guid(reader["Guid"].ToString());
			row["Url"] = reader["Url"];
			row["Name"] = reader["Name"];
			row["Abstract"] = reader["Abstract"];
			row["Description"] = reader["Description"];
			row["MetaDescription"] = reader["MetaDescription"];
			row["MetaKeywords"] = reader["MetaKeywords"];
			row["ViewRoles"] = reader["ViewRoles"];

			row["Created"] = Convert.ToDateTime(reader["Created"]);
			row["LastModified"] = Convert.ToDateTime(reader["LastModified"]);

			dataTable.Rows.Add(row);
		}

		return dataTable;
	}

	#endregion


	#region IIndexableContent

	public event ContentChangedEventHandler ContentChanged;

	protected void OnContentChanged(ContentChangedEventArgs e) => ContentChanged?.Invoke(this, e);

	#endregion
}
