﻿using SoftBottinWS;
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

        public int iCost
        {
            get; set;
        }

        public int iPrice
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
                niWsSoftBottin.GetShoesByType(iIdType, out dsShoes, out sErrMessage);

                DataTable workTable = dsShoes.Tables[0];
                DataColumn workCol = workTable.Columns.Add("Imagebase64", typeof(string));

                for (int iShoes = 0; iShoes < dsShoes.Tables[0].Rows.Count; iShoes++)
                {
                    if (!string.IsNullOrEmpty(dsShoes.Tables[0].Rows[iShoes]["ImageShoe"].ToString()))
                    {
                        byte[] bytes = (byte[])dsShoes.Tables[0].Rows[iShoes]["ImageShoe"];
                        var base64 = Convert.ToBase64String(bytes);
                        var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);
                        dsShoes.Tables[0].Rows[iShoes]["Imagebase64"] = imgSrc;
                    }
                }

                //DataSet dsImges = new DataSet();
                //cShoe nicShoe = new cShoe();
                //nicShoe.GetShoeImages(0, out dsImges, out sErrMessage);


                //byte[] bytes = (byte[])dsImges.Tables[0].Rows[0][5];

                //var base64 = Convert.ToBase64String(bytes);
                //var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

                //ViewBag.imgSrc = imgSrc;

                return true;

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
                            int iSalePrice, int iShoeType, List<cShoeDetail> lsColorDetail,
                            List<cShoeImage> lsShoeImage,
                            out int iIdInsert,
                            out int iIdDetailInsert,
                            out string sErrMessage)
        {
            try
            {
                iIdDetailInsert = -1;
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                iIdInsert = 0;
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
                                                     lsColorDetail[iColor].iQuantityExisting,
                                                     lsColorDetail[iColor].iQuantitySold,
                                                     out iIdDetailInsert,
                                                     out sErrMessage);
                        foreach (cShoeImage item in lsShoeImage)
                        {
                            AddImageShoe(iIdDetailInsert, item.sFileName, item.sContentType, item.bArrayImage, out sErrMessage);
                        }
                    }

                    return true;
                }
                else
                {
                    iIdDetailInsert = -1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                iIdInsert = -1;
                iIdDetailInsert = -1;
                return false;
            }
        }

        /// <summary>
        /// Daniel Romero 19 de Junio de 2016
        /// Metodo que se crea para agregar una imagen de un zapato
        /// </summary>
        /// <param name="iIdShoe"></param>
        /// <param name="sName"></param>
        /// <param name="sType"></param>
        /// <param name="btFileByte"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool AddImageShoe(int iIdShoe, string sName, string sType, byte[] btFileByte, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                if (niWsSoftBottin.AddImageShoe(iIdShoe, sName, sType, btFileByte, out sErrMessage))
                {
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

        /// <summary>
        /// Daniel Romero 19 de Junio de 2016
        /// Metodo que se crea para obtener una imagen de un zapato
        /// </summary>
        /// <param name="iIdShoe"></param>
        /// <param name="dsShoes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoeImages(int iIdShoe, out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.GetShoeImages(iIdShoe, out dsShoes, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                dsShoes = new DataSet();
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

        public string sImageBase64 { get; set; }
    }

    public partial class cShoeDetail
    {

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

        public int iQuantityExisting
        {
            get; set;
        }

        public int iQuantitySold
        {
            get; set;
        }
    }


    public partial class cShoeImage
    {

        public byte[] bArrayImage
        {
            get; set;
        }

        public string sFileName
        {
            get; set;
        }

        public string sContentType
        {
            get; set;
        }
    }

}