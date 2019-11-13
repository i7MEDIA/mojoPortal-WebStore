using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using WebStore.Data;

namespace WebStore.Business
{
    /// <summary>
    /// Represents an Image associated with a product or offer
    /// </summary>
    public class StoreImage
    {
		public StoreImage()
		{ }
        public StoreImage(Guid guid)
        {
            Guid = guid;
            GetImage(guid);
        }

		public Guid Guid { get; set; } = Guid.Empty;
		public Guid ReferenceGuid { get; set; } = Guid.Empty;
		public Guid StoreGuid { get; set; } = Guid.Empty;
		public string ImageUrl { get; set; }
		public int DisplayOrder { get; set; } = 0;
		public string Alt { get; set; }
		public string Title { get; set; }



		#region Private Methods

		private void GetImage(Guid guid) 
		{
            using (IDataReader reader = DBStoreImage.Get(guid))
            {
                if (reader.Read())
                {
					Guid = guid;
					ReferenceGuid = new Guid(reader["ReferenceGuid"].ToString());
					StoreGuid = new Guid(reader["StoreGuid"].ToString());
					ImageUrl = reader["ImageUrl"].ToString();
					DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]);
                    Alt = reader["Alt"].ToString();
                    Title = reader["Title"].ToString();
                }
            }
		}

		private bool Create()
        {
            int rowsAffected = DBStoreImage.Add(
                Guid,
				ReferenceGuid,
				StoreGuid,
                ImageUrl,
                DisplayOrder,
                Alt,
                Title);

            return (rowsAffected > 0);

        }


        private bool Update()
        {

            return DBStoreImage.Update(
				Guid,
                ImageUrl,
                DisplayOrder,
                Alt,
                Title);

        }

        private bool Exists()
        {
            bool result = false;
            using (IDataReader reader = DBStoreImage.Get(Guid))
            {
                if (reader.Read())
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion

        #region Public Methods


        public bool Save()
        {
            if (Exists())
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }




        #endregion

        #region Static Methods
		public static List<StoreImage> GetByReference(Guid referenceGuid)
		{
			var images = new List<StoreImage>();
			using (IDataReader reader = DBStoreImage.GetByReference(referenceGuid))
			{
				while (reader.Read())
				{
					images.Add(new StoreImage()
					{
						Guid = new Guid(reader["Guid"].ToString()),
						ReferenceGuid = referenceGuid,
						StoreGuid = new Guid(reader["StoreGuid"].ToString()),
						ImageUrl = reader["ImageUrl"].ToString(),
						DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
						Alt = reader["Alt"].ToString(),
						Title = reader["Title"].ToString()
					});
				}
			}
			return images;
		}
		public static List<StoreImage> GetByStore(Guid storeGuid)
		{
			var images = new List<StoreImage>();
			using (IDataReader reader = DBStoreImage.GetByStore(storeGuid))
			{
				while (reader.Read())
				{
					images.Add(new StoreImage()
					{
						Guid = new Guid(reader["Guid"].ToString()),
						ReferenceGuid = new Guid(reader["ReferenceGuid"].ToString()),
						StoreGuid = storeGuid,
						ImageUrl = reader["ImageUrl"].ToString(),
						DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
						Alt = reader["Alt"].ToString(),
						Title = reader["Title"].ToString()
					});
				}
			}
			return images;
		}

		public static DataTable GetTableByStore(Guid storeGuid)
		{
			return ListToTable(GetByStore(storeGuid));
		}

		public static DataTable GetTableForPageOfProducts(
			Guid storeGuid,
			int pageNumber,
			int pageSize)
		{
			var images = new List<StoreImage>();
			using (IDataReader reader = DBStoreImage.GetForPageOfReference(storeGuid, pageNumber, pageSize))
			{
				while (reader.Read())
				{
					images.Add(new StoreImage()
					{
						Guid = new Guid(reader["Guid"].ToString()),
						ReferenceGuid = new Guid(reader["ReferenceGuid"].ToString()),
						StoreGuid = storeGuid,
						ImageUrl = reader["ImageUrl"].ToString(),
						DisplayOrder = Convert.ToInt32(reader["DisplayOrder"]),
						Alt = reader["Alt"].ToString(),
						Title = reader["Title"].ToString()
					});
				}
			}
			return ListToTable(images);

		}

		private static DataTable ListToTable<T>(List<T> items)
		{
			// need to move this to a mojo helper 
			DataTable dataTable = new DataTable(typeof(T).Name);

			//Get all the properties
			PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (PropertyInfo prop in Props)
			{
				//Defining type of data column gives proper data table 
				var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
				//Setting column names as Property names
				dataTable.Columns.Add(prop.Name, type);
			}
			foreach (T item in items)
			{
				var values = new object[Props.Length];
				for (int i = 0; i < Props.Length; i++)
				{
					//inserting property values to datatable rows
					values[i] = Props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}
			//put a breakpoint here and check datatable
			return dataTable;
		}
		public static bool Delete(Guid guid)
        {
            return DBStoreImage.Delete(guid);
        }

		public static bool DeleteByReference(Guid referenceGuid)
		{
			return DBStoreImage.DeleteByReference(referenceGuid);
		}
		public static bool DeleteByStore(Guid storeGuid)
		{
			return DBStoreImage.DeleteByStore(storeGuid);
		}

		#endregion


	}

}
