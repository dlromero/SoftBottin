using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Name")]
        public string sName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Descripción")]
        public string sDescription
        {
            get;
            set;
        }

        [Required(ErrorMessage = "La referencia es requerida")]
        [DisplayName("Referencia")]
        public string sRef
        {
            get;
            set;
        }

        [Required(ErrorMessage = "El costo es requerido")]
        [DisplayName("Costo")]
        public int iCost
        {
            get;
            set;
        }

        [Required(ErrorMessage = "El precio es requerido")]
        [DisplayName("Precio")]
        public int iPrice
        {
            get;
            set;
        }

        [Required(ErrorMessage = "El tipo de zapato es requerido")]
        [DisplayName("Tipo de Zapato")]
        public int iShoeType
        {
            get;
            set;
        }

        [Required(ErrorMessage = "El color es requerido")]
        [DisplayName("Color")]
        public int iColor
        {
            get;
            set;
        }

        public string sColorDetail
        {
            get;
            set;
        }

        public List<cColor> lsColors
        {
            get;
            set;
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
                int iCount = 0;
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
                    }

                    foreach (cShoeImage item in lsShoeImage)
                    {
                        AddImageShoe(item.sFileName, item.sContentType, item.bArrayImage, iIdInsert, (iCount == 0 ? true : false), out sErrMessage);
                        iCount++;
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
        public bool AddImageShoe(string sName, string sType, byte[] btFileByte, int iProductId, bool bIsPrincipal, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                if (niWsSoftBottin.AddImageShoe(sName, sType, btFileByte, iProductId, bIsPrincipal, out sErrMessage))
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


        /// <summary>
        /// 27 de Julio de 2016 Daniel Romero
        /// Metodo que permite consultar los zapatos
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoes(out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                niWsSoftBottin.GetShoes(out dsShoes, out sErrMessage);
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
        /// 27 de Julio de 2016 Daniel Romero
        /// Metodo que permite consultar los zapatos
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoes(DataSet dsShoesTypes, DataSet dsShoes, out List<cShoesByType> poShoesByType, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                poShoesByType = new List<cShoesByType>();

                for (int iShoesTypes = 0; iShoesTypes < dsShoesTypes.Tables[0].Rows.Count; iShoesTypes++)
                {
                    EnumerableRowCollection<DataRow> query = from rsl in dsShoes.Tables[0].AsEnumerable()
                                                             where rsl.Field<Int32>("ShoeType").Equals(Convert.ToInt32(dsShoesTypes.Tables[0].Rows[iShoesTypes]["Id"].ToString()))
                                                             select
                                                             rsl;

                    DataView view = query.AsDataView();

                    for (int iShoe = 0; iShoe < view.Count; iShoe++)
                    {
                        cShoesByType nicShoesByType = new cShoesByType();
                        nicShoesByType.iId = Convert.ToInt32(view[iShoe]["Id"].ToString());
                        nicShoesByType.sName = view[iShoe]["Name"].ToString();
                        nicShoesByType.sDescription = view[iShoe]["Description"].ToString();
                        nicShoesByType.iQuantityExisting = Convert.ToInt32(view[iShoe]["QuantityExisting"].ToString());
                        nicShoesByType.iQuantitySold = Convert.ToInt32(view[iShoe]["QuantitySold"].ToString());
                        nicShoesByType.fPurchasePrice = Convert.ToSingle(view[iShoe]["PurchasePrice"].ToString());
                        nicShoesByType.fSalePrice = Convert.ToSingle(view[iShoe]["SalePrice"].ToString());
                        nicShoesByType.iType = Convert.ToInt32(view[iShoe]["ShoeType"].ToString());
                        nicShoesByType.iTotalQuantityExisting = Convert.ToInt32(view[iShoe]["TotalQuantityExisting"].ToString());

                        if (!string.IsNullOrEmpty(view[iShoe]["ShoeImage"].ToString()))
                        {
                            byte[] bytes = (byte[])view[iShoe]["ShoeImage"];
                            var base64 = Convert.ToBase64String(bytes);
                            var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);
                            nicShoesByType.simgSrc = imgSrc;
                        }
                        poShoesByType.Add(nicShoesByType);
                    }

                }


                return true;

            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                poShoesByType = new List<cShoesByType>();
                return false;
            }
        }


        /// <summary>
        ///4  de Julio de 2016 Daniel Romero
        /// Metodo que permite dar de baja un zapato
        /// </summary>
        /// <param name="iShoeId"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool ShoeOut(int iShoeId, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.ProductOut(iShoeId, out sErrMessage);
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
            get;
            set;
        }

        public string sNameProduct
        {
            get;
            set;
        }

        public string sQuantityExisting
        {
            get;
            set;
        }

        public string sQuantitySold
        {
            get;
            set;
        }

        public string sPurchasePrice
        {
            get;
            set;
        }

        public string sSalePrice
        {
            get;
            set;
        }

        public string sIdType
        {
            get;
            set;
        }

        public string sIdProductDetail
        {
            get;
            set;
        }

        public string sSize
        {
            get;
            set;
        }

        public string sQuantity
        {
            get;
            set;
        }

        public string sIdColor
        {
            get;
            set;
        }

        public string sColorDescription
        {
            get;
            set;
        }

        public string sRGB
        {
            get;
            set;
        }

        public string sTypeDescription
        {
            get;
            set;
        }

        public string sDescriptionProduct
        {
            get;
            set;
        }

        public string sImageBase64 { get; set; }
    }

    public partial class cShoeDetail
    {

        public int iIdColor
        {
            get;
            set;
        }

        public int iSize
        {
            get;
            set;
        }

        public int iQuantity
        {
            get;
            set;
        }

        public int iQuantityExisting
        {
            get;
            set;
        }

        public int iQuantitySold
        {
            get;
            set;
        }
    }


    public partial class cShoeImage
    {

        public byte[] bArrayImage
        {
            get;
            set;
        }

        public string sFileName
        {
            get;
            set;
        }

        public string sContentType
        {
            get;
            set;
        }
    }

    public partial class cShoesByType
    {
        public int iId { get; set; }

        public string sName { get; set; }

        public string sDescription { get; set; }

        public int iQuantityExisting { get; set; }

        public int iQuantitySold { get; set; }

        public float fPurchasePrice { get; set; }

        public float fSalePrice { get; set; }

        public int iType { get; set; }

        public string simgSrc { get; set; }

        public int iTotalQuantityExisting { get; set; }

    }

}