using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
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
			string sqlCommand = @"
				INSERT INTO `ws_Images` (
					`Guid`,
					`ReferenceGuid`,
					`StoreGuid`,
					`ImageUrl`,
					`DisplayOrder`,
					`Alt`,
					`Title`
				)
				VALUES (
					?Guid,
					?ReferenceGuid,
					?StoreGuid,
					?ImageUrl,
					?DisplayOrder,
					?Alt,
					?Title
				);";

			var sqlParams = new List<MySqlParameter>
			{
				new MySqlParameter("?Guid", MySqlDbType.Guid) { Direction = ParameterDirection.Input, Value = guid },
				new MySqlParameter("?ReferenceGuid", MySqlDbType.Guid) { Direction = ParameterDirection.Input, Value = referenceGuid },
				new MySqlParameter("?StoreGuid", MySqlDbType.Guid) { Direction = ParameterDirection.Input, Value = storeGuid },
				new MySqlParameter("?ImageUrl", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = imageUrl },
				new MySqlParameter("?DisplayOrder", MySqlDbType.Int32) { Direction = ParameterDirection.Input, Value = displayOrder },
				new MySqlParameter("?Alt", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = alt },
				new MySqlParameter("?Title", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = title },
			};


			int rowsAffected = Convert.ToInt32(
				MySqlHelper.ExecuteNonQuery(
					ConnectionString.GetWriteConnectionString(),
					sqlCommand,
					sqlParams.ToArray()
				)
			);

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

			string sqlCommand = @"
				UPDATE `ws_Images`
				SET
					`ImageUrl` = ?ImageUrl,
					`DisplayOrder` = ?DisplayOrder,
					`Alt` = ?Alt,
					`Title` = ?Title
				WHERE
					`Guid` = ?Guid;";

			var sqlParams = new List<MySqlParameter>
			{
				new MySqlParameter("?Guid", MySqlDbType.Guid) { Direction = ParameterDirection.Input, Value = guid },
				new MySqlParameter("?ImageUrl", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = imageUrl },
				new MySqlParameter("?DisplayOrder", MySqlDbType.Int32) { Direction = ParameterDirection.Input, Value = displayOrder },
				new MySqlParameter("?Alt", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = alt },
				new MySqlParameter("?Title", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Input, Value = title },
			};


			int rowsAffected = Convert.ToInt32(
				MySqlHelper.ExecuteNonQuery(
					ConnectionString.GetWriteConnectionString(),
					sqlCommand,
					sqlParams.ToArray()
				)
			);

			return rowsAffected > 0;

		}

		public static bool Delete(Guid guid)
		{
			const string sqlCommand = "DELETE FROM `ws_Images` WHERE `Guid` = ?Guid;";

			var sqlParam = new MySqlParameter("?Guid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = guid
			};

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);

			return rowsAffected > 0;
		}
		public static bool DeleteByReference(Guid referenceGuid)
		{
			const string sqlCommand = "DELETE FROM `ws_Images` WHERE `ReferenceGuid` = ?ReferenceGuid;";

			var sqlParam = new MySqlParameter("?ReferenceGuid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = referenceGuid
			};

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);

			return rowsAffected > 0;
		}

		public static bool DeleteByStore(Guid storeGuid)
		{
			const string sqlCommand = "DELETE FROM `ws_Images` WHERE `StoreGuid` = ?StoreGuid;";

			var sqlParam = new MySqlParameter("?StoreGuid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = storeGuid
			};

			int rowsAffected = MySqlHelper.ExecuteNonQuery(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);

			return rowsAffected > 0;
		}
		public static IDataReader Get(Guid guid)
		{
			const string sqlCommand = "SELECT * FROM `ws_Images` WHERE `Guid` = ?Guid ORDER BY `DisplayOrder` asc, `Title` asc;";

			var sqlParam = new MySqlParameter("?Guid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = guid
			};

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);
		}

		public static IDataReader GetByReference(Guid referenceGuid)
		{
			const string sqlCommand = "SELECT * FROM `ws_Images` WHERE `ReferenceGuid` = ?ReferenceGuid ORDER BY `DisplayOrder` asc, `Title` asc;";

			var sqlParam = new MySqlParameter("?ReferenceGuid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = referenceGuid
			};

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);
		}

		public static IDataReader GetByStore(Guid storeGuid)
		{
			const string sqlCommand = "SELECT * FROM `ws_Images` WHERE `StoreGuid` = ?StoreGuid ORDER BY `ReferenceGuid`, `DisplayOrder`, `Title`;";

			var sqlParam = new MySqlParameter("?StoreGuid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = storeGuid
			};

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);
		}

		public static IDataReader GetForPageOfReference(Guid storeGuid, int pageNumber, int pageSize)
		{
			const string sqlCommand = "SELECT * FROM `ws_Images` WHERE `StoreGuid` = ?StoreGuid ORDER BY `ReferenceGuid`, `DisplayOrder`, `Title`;";

			var sqlParam = new MySqlParameter("?StoreGuid", MySqlDbType.Guid)
			{
				Direction = ParameterDirection.Input,
				Value = storeGuid
			};

			return MySqlHelper.ExecuteReader(
				ConnectionString.GetWriteConnectionString(),
				sqlCommand,
				sqlParam
			);
		}
	}
}
