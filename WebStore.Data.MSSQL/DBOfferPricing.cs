using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using mojoPortal.Data;

namespace WebStore.Data
{
	public static class DBOfferPricing
	{
		public static int Add(
			Guid guid,
			Guid offerGuid,
			bool requiresOfferCode,
			string offerCode,
			int maxAllowedPerCustomer,
			DateTime created,
			Guid createdBy,
			string createdFromIP,
			DateTime lastModified,
			Guid lastModifedBy,
			string lastModifedFromIP)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_OfferPricing_Add", 13);
			sph.DefineSqlParameter("@Guid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, guid);
			sph.DefineSqlParameter("@OfferGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, offerGuid);
			//sph.DefineSqlParameter("@BeginUTC", SqlDbType.DateTime, ParameterDirection.Input, beginUtc);
			//sph.DefineSqlParameter("@EndUTC", SqlDbType.DateTime, ParameterDirection.Input, endUtc);
			sph.DefineSqlParameter("@RequiresOfferCode", SqlDbType.Bit, ParameterDirection.Input, requiresOfferCode);
			sph.DefineSqlParameter("@OfferCode", SqlDbType.NVarChar, 50, ParameterDirection.Input, offerCode);
			sph.DefineSqlParameter("@MaxAllowedPerCustomer", SqlDbType.Int, ParameterDirection.Input, maxAllowedPerCustomer);
			sph.DefineSqlParameter("@Created", SqlDbType.DateTime, ParameterDirection.Input, created);
			sph.DefineSqlParameter("@CreatedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, createdBy);
			sph.DefineSqlParameter("@CreatedFromIP", SqlDbType.NVarChar, 255, ParameterDirection.Input, createdFromIP);
			sph.DefineSqlParameter("@LastModified", SqlDbType.DateTime, ParameterDirection.Input, lastModified);
			sph.DefineSqlParameter("@LastModifedBy", SqlDbType.UniqueIdentifier, ParameterDirection.Input, lastModifedBy);
			sph.DefineSqlParameter("@LastModifedFromIP", SqlDbType.NVarChar, 255, ParameterDirection.Input, lastModifedFromIP);
			int rowsAffected = sph.ExecuteNonQuery();
			return rowsAffected;
		}
	}
}
