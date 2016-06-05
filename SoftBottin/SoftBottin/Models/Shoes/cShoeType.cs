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
    public class cShoeType
    {

        #region Global

        /// <summary>
        /// 
        /// </summary>
        wsSoftBottin.SoftBottin niWsSoftBottin;
        /// <summary>
        /// 
        /// </summary>
        public int iId
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "El nombre es requerido")]
        [DisplayName("Name")]
        public string sName
        {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Description")]
        public string sDescription
        {
            get; set;
        }

        [Required(ErrorMessage = "La referencia es requerida")]
        [DisplayName("Referencia")]
        public string sRef
        {
            get; set;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public cShoeType()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShowType"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool AddShoeType(cShoeType objShowType, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.AddShoeType(objShowType.sName, objShowType.sDescription, objShowType.sRef, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsShoesTypes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoesTypes(out DataSet dsShoesTypes, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.GetShoesTypes(out dsShoesTypes, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                dsShoesTypes = new DataSet();
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iShoeType"></param>
        /// <param name="pocShoeType"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetShoesTypesById(int iShoeType, out cShoeType pocShoeType, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                DataSet dsShoesTypes = new DataSet();
                pocShoeType = new cShoeType();
                if (niWsSoftBottin.GetShoesTypesById(iShoeType, out dsShoesTypes, out sErrMessage))
                {
                    for (int iShoeTypes = 0; iShoeTypes < dsShoesTypes.Tables[0].Rows.Count; iShoeTypes++)
                    {
                        pocShoeType.iId = Convert.ToInt32(dsShoesTypes.Tables[0].Rows[iShoeTypes]["Id"].ToString());
                        pocShoeType.sName = dsShoesTypes.Tables[0].Rows[iShoeTypes]["Name"].ToString();
                        pocShoeType.sDescription = dsShoesTypes.Tables[0].Rows[iShoeTypes]["Description"].ToString();
                        pocShoeType.sRef = dsShoesTypes.Tables[0].Rows[iShoeTypes]["Ref"].ToString();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                pocShoeType = new cShoeType();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShowType"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool EditShoeType(cShoeType objShowType, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.EditShoeType(objShowType.iId, objShowType.sName,
                                                   objShowType.sDescription, objShowType.sRef,
                                                   out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIdShoeType"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool DeleteShoeType(int iIdShoeType, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.DeleteShoeType(iIdShoeType, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }



    }
}