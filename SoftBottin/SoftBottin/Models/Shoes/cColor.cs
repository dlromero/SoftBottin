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
    public class cColor
    {

        #region Global
        /// <summary>
        /// 
        /// </summary>
        wsSoftBottin.SoftBottin niWsSoftBottin;
        #endregion

        #region Properties
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
        [Required(ErrorMessage = "La descripción es requerida")]
        [DisplayName("Description")]
        public string sDescription
        {
            get; set;
        }

        [Required(ErrorMessage = "La referencia es requerida")]
        [DisplayName("Color")]
        public string sRGB
        {
            get; set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite consultar todos los colores
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetColors(out DataSet dsColors, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.GetColors(out dsColors, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                dsColors = new DataSet();
                return false;
            }
        }
        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite consultar todos los colores por id
        /// </summary>
        /// <param name="iColorID"></param>
        /// <param name="pocColor"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool GetColorsById(int iColorID, out cColor pocColor, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                DataSet dsColor = new DataSet();
                pocColor = new cColor();
                if (niWsSoftBottin.GetColorById(iColorID, out dsColor, out sErrMessage))
                {
                    for (int iShoeTypes = 0; iShoeTypes < dsColor.Tables[0].Rows.Count; iShoeTypes++)
                    {
                        pocColor.iId = Convert.ToInt32(dsColor.Tables[0].Rows[iShoeTypes]["Id"].ToString());
                        pocColor.sDescription = dsColor.Tables[0].Rows[iShoeTypes]["Description"].ToString();
                        pocColor.sRGB = dsColor.Tables[0].Rows[iShoeTypes]["RGB"].ToString();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                pocColor = new cColor();
                return false;
            }
        }
        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite adicion un color
        /// </summary>
        /// <param name="objColor"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool AddColor(cColor objColor, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.AddColor(objColor.sDescription, objColor.sRGB, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite editar un color
        /// </summary>
        /// <param name="objColor"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool EditColor(cColor objColor, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.EditColor(objColor.iId, objColor.sDescription,
                                                   objColor.sRGB, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMessage);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 11 de Julio de 2016 Daniel Romero
        /// Metodo que permite eliminar un color
        /// </summary>
        /// <param name="iIdColor"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool DeleteColor(int iIdColor, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.DeleteColor(iIdColor, out sErrMessage);
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
}