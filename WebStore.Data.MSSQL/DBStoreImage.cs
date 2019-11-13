using System;
using System.Data;
using mojoPortal.Data;

namespace WebStore.Data
{
	public static class DBStoreImage
    {
        
        public static int Add(
			Guid guid,
            Guid referenceGuid,
            Guid storeGuid,
			string imageUrl,
			int displayOrder,
            string alt,
            string title)
        {
            SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_Images_Insert", 7);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            sph.DefineSqlParameter("@ReferenceGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, referenceGuid);
            sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
            sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
            sph.DefineSqlParameter("@DisplayOrder", SqlDbType.Int, ParameterDirection.Input, displayOrder);
            sph.DefineSqlParameter("@Alt", SqlDbType.NVarChar, 255, ParameterDirection.Input, alt);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);


			int rowsAffected = sph.ExecuteNonQuery();
            return rowsAffected;
			
        }


        public static bool Update(
            Guid guid,
			string imageUrl,
            int displayOrder,
            string alt,
			string title
			)
        {
            SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_Images_Update", 5);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
			sph.DefineSqlParameter("@ImageUrl", SqlDbType.NVarChar, 255, ParameterDirection.Input, imageUrl);
			sph.DefineSqlParameter("@DisplayOrder", SqlDbType.Int, ParameterDirection.Input, displayOrder);
            sph.DefineSqlParameter("@Alt", SqlDbType.NVarChar, 255, ParameterDirection.Input, alt);
            sph.DefineSqlParameter("@Title", SqlDbType.NVarChar, 255, ParameterDirection.Input, title);

            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);
            
            

        }

        public static bool Delete(Guid guid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_Images_Delete", 1);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            int rowsAffected = sph.ExecuteNonQuery();
            return (rowsAffected > 0);

            
        }
		public static bool DeleteByReference(Guid referenceGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_Images_DeleteByReference", 1);
			sph.DefineSqlParameter("@ReferenceGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, referenceGuid);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);
		}

		public static bool DeleteByStore(Guid storeGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_Images_DeleteByStore", 1);
			sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);
		}
		public static IDataReader Get(Guid guid)
        {
            SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_Images_SelectOne", 1);
            sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
            return sph.ExecuteReader();
        }

		public static IDataReader GetByReference(Guid referenceGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_Images_SelectByReference", 1);
			sph.DefineSqlParameter("@ReferenceGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, referenceGuid);
			return sph.ExecuteReader();
		}

		public static IDataReader GetByStore(Guid storeGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_Images_SelectByStore", 1);
			sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
			return sph.ExecuteReader();
		}

		public static IDataReader GetForPageOfReference(Guid storeGuid, int pageNumber, int pageSize)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_Images_SelectByStore", 1);
			sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
			return sph.ExecuteReader();
		}
	}
}
