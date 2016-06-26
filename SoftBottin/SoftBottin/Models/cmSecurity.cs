using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftBottinWS;
using System.Data;

namespace SoftBottin.Models
{
    public class cmSecurity
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
        /// Daniel Romero 4 de Marzo de 2016
        /// Propiedad que se crea para alamacenar el correo electronico del cliente
        /// </summary>
        public string sEmailUserPrincipal
        {
            get;
            set;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Daniel Romero 4 de Marzo de 2016
        /// Creacion de metodo para Almacenar el correo electronico del usuario
        /// </summary>
        /// <returns></returns>
        public bool AddNewEmailUser(string sEmail, out string sErrMessage)
        {
            try
            {
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.AddEmailNewUser(sEmail, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 30 de Mayo de 2016
        /// Creacion de metodo para Autenticarce en el sistema
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool LogIn(string sUser, string sPassword, out DataSet dsUser, out string sErrMessage)
        {
            try
            {
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                sErrMessage = "";
                return niWsSoftBottin.LogIn(sUser, sPassword, out dsUser, out sErrMessage);
            }
            catch (Exception ex)
            {
                dsUser = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 26 de Junio de 2016
        /// Creacion de metodo para Crear usuario en el sistema
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <param name="sEmail"></param>
        /// <param name="sPassword"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        public bool SignIn(string sFirstName, string sLastName, string sEmail, string sPassword, out string sErrMessage)
        {
            try
            {
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                sErrMessage = "";
                return niWsSoftBottin.SignIn(sFirstName, sLastName, sEmail, sPassword, out sErrMessage);
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }


        public bool CheckEmail(string sEmail, out DataSet dsUser, out string sErrMessage)
        {
            try
            {
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                sErrMessage = "";
                return niWsSoftBottin.CheckEmail(sEmail, out dsUser, out sErrMessage);
            }
            catch (Exception ex)
            {
                dsUser = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }

        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class cmProduct
    {
        public string sReference { get; set; }
        public string sPrice { get; set; }
        public string sColor { get; set; }
        public string sSize { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class cmUser
    {
        public string sUserName { get; set; }
        public string sPassword { get; set; }
    }

    public partial class cmUserSgnIn
    {
        public string sFirstName { get; set; }
        public string sLastName { get; set; }
        public string sEmail { get; set; }
        public string sPassword { get; set; }
    }
}