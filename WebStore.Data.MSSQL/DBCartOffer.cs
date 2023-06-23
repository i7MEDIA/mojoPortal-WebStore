﻿using System;
using System.Data;
using mojoPortal.Data;

namespace WebStore.Data
{

	public static class DBCartOffer
	{

		public static int Add(
			Guid itemGuid,
			Guid cartGuid,
			Guid offerGuid,
			Guid taxClassGuid,
			decimal offerPrice,
			DateTime addedToCart,
			int quantity,
			decimal tax,
			bool isDonation,
			int maxPerOrder)
		{

			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_Insert", 10);
			sph.DefineSqlParameter("@ItemGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, itemGuid);
			sph.DefineSqlParameter("@CartGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, cartGuid);
			sph.DefineSqlParameter("@OfferGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, offerGuid);
			sph.DefineSqlParameter("@OfferPrice", SqlDbType.Decimal, ParameterDirection.Input, offerPrice);
			sph.DefineSqlParameter("@AddedToCart", SqlDbType.DateTime, ParameterDirection.Input, addedToCart);
			sph.DefineSqlParameter("@Quantity", SqlDbType.Int, ParameterDirection.Input, quantity);
			sph.DefineSqlParameter("@TaxClassGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, taxClassGuid);
			sph.DefineSqlParameter("@Tax", SqlDbType.Decimal, ParameterDirection.Input, tax);
			sph.DefineSqlParameter("@IsDonation", SqlDbType.Bit, ParameterDirection.Input, isDonation);
			sph.DefineSqlParameter("@MaxPerOrder", SqlDbType.Int, ParameterDirection.Input, maxPerOrder);
			int rowsAffected = sph.ExecuteNonQuery();
			return rowsAffected;

		}


		public static bool Update(
			Guid itemGuid,
			Guid offerGuid,
			Guid taxClassGuid,
			decimal offerPrice,
			DateTime addedToCart,
			int quantity,
			decimal tax,
			bool isDonation,
			int maxPerOrder)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_Update", 9);
			sph.DefineSqlParameter("@ItemGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, itemGuid);
			sph.DefineSqlParameter("@OfferGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, offerGuid);
			sph.DefineSqlParameter("@OfferPrice", SqlDbType.Decimal, ParameterDirection.Input, offerPrice);
			sph.DefineSqlParameter("@AddedToCart", SqlDbType.DateTime, ParameterDirection.Input, addedToCart);
			sph.DefineSqlParameter("@Quantity", SqlDbType.Int, ParameterDirection.Input, quantity);
			sph.DefineSqlParameter("@TaxClassGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, taxClassGuid);
			sph.DefineSqlParameter("@Tax", SqlDbType.Decimal, ParameterDirection.Input, tax);
			sph.DefineSqlParameter("@IsDonation", SqlDbType.Bit, ParameterDirection.Input, isDonation);
			sph.DefineSqlParameter("@MaxPerOrder", SqlDbType.Int, ParameterDirection.Input, maxPerOrder);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);

		}

		public static bool Delete(Guid itemGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_Delete", 1);
			sph.DefineSqlParameter("@ItemGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, itemGuid);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);

		}

		public static bool DeleteAnonymousByStore(Guid storeGuid, DateTime olderThan)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_DeleteAnonymousByStore", 2);
			sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
			sph.DefineSqlParameter("@OlderThan", SqlDbType.DateTime, ParameterDirection.Input, olderThan);

			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);

		}

		public static bool DeleteByStore(Guid storeGuid, DateTime olderThan)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_DeleteByStore", 2);
			sph.DefineSqlParameter("@StoreGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, storeGuid);
			sph.DefineSqlParameter("@OlderThan", SqlDbType.DateTime, ParameterDirection.Input, olderThan);

			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);

		}

		public static bool DeleteByCart(Guid cartGuid)
		{

			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetWriteConnectionString(), "ws_CartOffers_DeleteByCart", 1);
			sph.DefineSqlParameter("@CartGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, cartGuid);
			int rowsAffected = sph.ExecuteNonQuery();
			return (rowsAffected > 0);

		}

		public static IDataReader Get(Guid itemGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_CartOffers_SelectOne", 1);
			sph.DefineSqlParameter("@ItemGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, itemGuid);
			return sph.ExecuteReader();

		}

		public static IDataReader GetByCart(Guid cartGuid)
		{
			SqlParameterHelper sph = new SqlParameterHelper(WebStoreConnectionString.GetReadConnectionString(), "ws_CartOffers_SelectByCart", 1);
			sph.DefineSqlParameter("@CartGuid", SqlDbType.UniqueIdentifier, ParameterDirection.Input, cartGuid);
			return sph.ExecuteReader();
		}
	}
}
