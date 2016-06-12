using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SoftBottin.Models.Shoes
{
    public class cShoe
    {
        #region Global
        /// <summary>
        /// 
        /// </summary>
        wsSoftBottin.SoftBottin niWsSoftBottin;
        #endregion

        #region Properties

        public string sName
        {
            get; set;
        }

        public string sDescription
        {
            get; set;
        }

        public string sRef
        {
            get; set;
        }

        public string sCost
        {
            get; set;
        }

        public string sPrice
        {
            get; set;
        }

        public int iShoeType
        {
            get; set;
        }

        public int iColor
        {
            get; set;
        }

        public string sColorDetail
        {
            get; set;
        }

        public List<cColor> lsColors
        {
            get; set;
        }




        #endregion

        #region Methods

        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite consultar los zapatos por tipo de zapato
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoesByType(int iIdType, out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.GetShoesByType(iIdType, out dsShoes, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                dsShoes = new DataSet();
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sDescription"></param>
        /// <param name="sRef"></param>
        /// <param name="iQuantityExisting"></param>
        /// <param name="iQuantitySold"></param>
        /// <param name="iPurchasePrice"></param>
        /// <param name="iSalePrice"></param>
        /// <param name="iShoeType"></param>
        /// <param name="lsColorDetail"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool AddShoe(string sName, string sDescription, string sRef,
                            int iQuantityExisting, int iQuantitySold, int iPurchasePrice,
                            int iSalePrice, int iShoeType, List<cShoeDetail> lsColorDetail, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                int iIdInsert = 0;
                if (niWsSoftBottin.AddShoe(sName, sDescription, sRef, iQuantityExisting,
                                      iQuantitySold, iPurchasePrice, iSalePrice, iShoeType,
                                      out iIdInsert, out sErrMessage))
                {

                    for (int iColor = 0; iColor < lsColorDetail.Count; iColor++)
                    {
                        niWsSoftBottin.AddShoeDetail(iIdInsert,
                                                     lsColorDetail[iColor].iIdColor,
                                                     lsColorDetail[iColor].iSize,
                                                     lsColorDetail[iColor].iQuantity,
                                                     out sErrMessage);
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }


        #endregion
    }

    public partial class cShoeTemp
    {
        public string sIdProduct
        {
            get; set;
        }

        public string sNameProduct
        {
            get; set;
        }

        public string sQuantityExisting
        {
            get; set;
        }

        public string sQuantitySold
        {
            get; set;
        }

        public string sPurchasePrice
        {
            get; set;
        }

        public string sSalePrice
        {
            get; set;
        }

        public string sIdType
        {
            get; set;
        }

        public string sIdProductDetail
        {
            get; set;
        }

        public string sSize
        {
            get; set;
        }

        public string sQuantity
        {
            get; set;
        }

        public string sIdColor
        {
            get; set;
        }

        public string sColorDescription
        {
            get; set;
        }

        public string sRGB
        {
            get; set;
        }

        public string sTypeDescription
        {
            get; set;
        }

        public string sDescriptionProduct
        {
            get; set;
        }
    }

    public partial class cShoeDetail
    {

        public int iIdProduct
        {
            get; set;
        }

        public int iIdColor
        {
            get; set;
        }

        public int iSize
        {
            get; set;
        }

        public int iQuantity
        {
            get; set;
        }
    }

}