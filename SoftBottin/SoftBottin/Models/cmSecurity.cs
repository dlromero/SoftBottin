﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoftBottinWS;

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
        /// 
        /// </summary>
        /// <param name="sUser"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool SigIn(string sUser, string sPassword)
        {
            try
            {
                niWsSoftBottin = new wsSoftBottin.SoftBottin();
                return niWsSoftBottin.SigIn(sUser, sPassword);
            }
            catch (Exception ex)
            {
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
}