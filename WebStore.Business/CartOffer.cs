/// Author:					
/// Created:				2007-03-14
/// Last Modified:			2009-02-01
/// 
/// The use and distribution terms for this software are covered by the 
/// Common Public License 1.0 (http://opensource.org/licenses/cpl.php)  
/// which can be found in the file CPL.TXT at the root of this distribution.
/// By using this software in any fashion, you are agreeing to be bound by 
/// the terms of this license.
///
/// You must not remove this notice, or any other, from this software.

using System;
using System.Collections.Generic;
using System.Data;
using WebStore.Data;

namespace WebStore.Business
{
    /// <summary>
    /// Represents an offer in the cart
    /// </summary>
    [Serializable()]
    public class CartOffer
    {

        #region Constructors

        public CartOffer()
        { }

		#endregion

		#region Properties

		public string Name { get; set; } = string.Empty;
		public Guid ItemGuid { get; set; } = Guid.Empty;
		public Guid CartGuid { get; set; } = Guid.Empty;
		public Guid OfferGuid { get; set; } = Guid.Empty;
		//public Guid PriceGuid
		//{
		//    get { return priceGuid; }
		//    set { priceGuid = value; }
		//}
		//public Guid CurrencyGuid
		//{
		//    get { return currencyGuid; }
		//    set { currencyGuid = value; }
		//}
		public Guid TaxClassGuid { get; set; } = Guid.Empty;
		public decimal OfferPrice { get; set; } = 0;
		public decimal Tax { get; set; } = 0;
		public DateTime AddedToCart { get; set; } = DateTime.UtcNow;
		public int Quantity { get; set; } = 1;
		public bool IsDonation { get; set; } = false;
		public int MaxPerOrder { get; set; } = 0;
		#endregion


		#region Private Methods

		/// <summary>
		/// Persists a new instance of CartOffer. Returns true on success.
		/// </summary>
		/// <returns></returns>
		private bool Create()
        {
            this.ItemGuid = Guid.NewGuid();

            int rowsAffected = DBCartOffer.Add(
                this.ItemGuid,
                this.CartGuid,
                this.OfferGuid,
                this.TaxClassGuid,
                this.OfferPrice,
                this.AddedToCart,
                this.Quantity,
                this.Tax,
                this.IsDonation,
				this.MaxPerOrder);

            return (rowsAffected > 0);

        }


        /// <summary>
        /// Updates this instance of CartOffer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        private bool Update()
        {

            return DBCartOffer.Update(
                this.ItemGuid,
                this.OfferGuid,
                this.TaxClassGuid,
                this.OfferPrice,
                this.AddedToCart,
                this.Quantity,
                this.Tax,
                this.IsDonation,
				this.MaxPerOrder);

        }


        #endregion


        /// <summary>
        /// Saves this instance of CartOffer. Returns true on success.
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            if (this.ItemGuid != Guid.Empty)
            {
                return Update();
            }
            else
            {
                return Create();
            }
        }


        public static List<CartOffer> GetByCart(Guid cartGuid)
        {
            IDataReader reader = DBCartOffer.GetByCart(cartGuid);
            return LoadListFromReader(reader);


        }

        private static List<CartOffer> LoadListFromReader(IDataReader reader)
        {
            List<CartOffer> cartOfferList = new List<CartOffer>();
            try
            {
                while (reader.Read())
                {
                    CartOffer cartOffer = new CartOffer();
                    cartOffer.ItemGuid = new Guid(reader["ItemGuid"].ToString());
                    cartOffer.CartGuid = new Guid(reader["CartGuid"].ToString());
                    cartOffer.OfferGuid = new Guid(reader["OfferGuid"].ToString());

                    cartOffer.TaxClassGuid = new Guid(reader["TaxClassGuid"].ToString());
                    cartOffer.OfferPrice = Convert.ToDecimal(reader["OfferPrice"]);
                    cartOffer.AddedToCart = Convert.ToDateTime(reader["AddedToCart"]);
                    cartOffer.Quantity = Convert.ToInt32(reader["Quantity"]);
                    cartOffer.Name = reader["Name"].ToString();
                    if (reader["Tax"] != DBNull.Value)
                    {
                        cartOffer.Tax = Convert.ToDecimal(reader["Tax"]);
                    }

                    cartOffer.IsDonation = Convert.ToBoolean(reader["IsDonation"]);
					cartOffer.MaxPerOrder = Convert.ToInt32(reader["MaxPerOrder"]);
                    cartOfferList.Add(cartOffer);

                }
            }
            finally
            {
                reader.Close();
            }

            return cartOfferList;

        }

        public static bool DeleteByCart(Guid cartGuid)
        {
            return DBCartOffer.DeleteByCart(cartGuid);
        }

        public static bool DeleteAnonymousByStore(Guid storeGuid, DateTime olderThan)
        {
            return DBCartOffer.DeleteAnonymousByStore(storeGuid, olderThan);
        }

        public static bool DeleteByStore(Guid storeGuid, DateTime olderThan)
        {
            return DBCartOffer.DeleteByStore(storeGuid, olderThan);
        }

    }

}
