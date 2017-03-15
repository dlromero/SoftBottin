using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Models
{
    public class cmProductDetail
    {

        #region Global
        /// <summary>
        /// 
        /// </summary>
        string sErrMsj;
        /// <summary>
        /// 
        /// </summary>
        wsSoftBottin.SoftBottin niWsSoftBottin;
        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string sProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float fPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sColorSelect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sQuantitySelect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sSizeSelect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> slColors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> slQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> slSizes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge4 { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Daniel Romero 16 de Julio de 2016
        /// Metodo que se crea para consultar las caracteristicas de un zapato
        /// </summary>
        /// <param name="iShoeId"></param>
        /// <param name="dsShoe"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetFeaturesByShoe(int iShoeId, out cmProductDetail niProductDetailt, out string sErrMessage)
        {
            try
            {
                DataSet dsShoe = new DataSet();
                niProductDetailt = new cmProductDetail();
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                if (niWsSoftBottin.GetFeaturesByShoe(iShoeId, out dsShoe, out sErrMessage))
                {



                    niProductDetailt = new cmProductDetail();
                    List<SelectListItem> lsQuantity = new List<SelectListItem>();
                    int maxQuantity = 5;

                    if (dsShoe.Tables != null)
                    {
                        if (dsShoe.Tables[0].Rows.Count > 0)
                        {
                            maxQuantity = Convert.ToInt32(dsShoe.Tables[0].Rows[0]["ptQuantityExisting"].ToString());
                        }
                    }
                    maxQuantity = maxQuantity > 5 ? 5 : maxQuantity;

                    for (int iQuantity = 0; iQuantity < maxQuantity; iQuantity++)
                    {
                        SelectListItem sliNew = new SelectListItem();
                        sliNew.Text = (iQuantity + 1).ToString();
                        sliNew.Value = (iQuantity + 1).ToString();
                        lsQuantity.Add(sliNew);
                    }
                    List<SelectListItem> lsSisez = new List<SelectListItem>();
                    DataSet dsSizes = new DataSet();
                    if (niWsSoftBottin.GetSizesByShoe(iShoeId, out dsSizes, out sErrMessage))
                    {
                        for (int iSizes = 0; iSizes < dsSizes.Tables[0].Rows.Count; iSizes++)
                        {
                            SelectListItem sliNew3 = new SelectListItem();
                            sliNew3.Text = dsSizes.Tables[0].Rows[iSizes]["Size"].ToString();
                            sliNew3.Value = dsSizes.Tables[0].Rows[iSizes]["Size"].ToString();
                            lsSisez.Add(sliNew3);
                        }
                    }

                    niProductDetailt.slQuantity = lsQuantity;
                    niProductDetailt.slSizes = lsSisez;

                    for (int iShoe = 0; iShoe < dsShoe.Tables[0].Rows.Count; iShoe++)
                    {
                        niProductDetailt.sProductName = dsShoe.Tables[0].Rows[iShoe]["pdName"].ToString();
                        niProductDetailt.sProductId = dsShoe.Tables[0].Rows[iShoe]["ptIdProduct"].ToString();
                        niProductDetailt.fPrice = string.IsNullOrEmpty(dsShoe.Tables[0].Rows[iShoe]["pdSalePrice"].ToString())
                                                   ? 0 : Convert.ToSingle(dsShoe.Tables[0].Rows[iShoe]["pdSalePrice"].ToString());
                        niProductDetailt.sDescription = dsShoe.Tables[0].Rows[iShoe]["pdDescription"].ToString();


                        if (!string.IsNullOrEmpty(dsShoe.Tables[0].Rows[iShoe]["imgImage"].ToString()))
                        {
                            byte[] bytes = (byte[])dsShoe.Tables[0].Rows[iShoe]["imgImage"];
                            var base64 = Convert.ToBase64String(bytes);
                            var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

                            if (iShoe == 0)
                            {
                                niProductDetailt.sPathImageSmall1 = imgSrc;
                                niProductDetailt.sPathImageLarge1 = imgSrc;
                            }
                            if (iShoe == 1)
                            {
                                niProductDetailt.sPathImageSmall2 = imgSrc;
                                niProductDetailt.sPathImageLarge2 = imgSrc;
                            }
                            if (iShoe == 2)
                            {
                                niProductDetailt.sPathImageSmall3 = imgSrc;
                                niProductDetailt.sPathImageLarge3 = imgSrc;
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                niProductDetailt = new cmProductDetail();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }

        #endregion

    }
}