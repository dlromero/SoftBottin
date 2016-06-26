using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Controllers
{
    public class UtilitiesController : Controller
    {
        public string sErrMessage = "";

        public bool CheckSession()
        {
            try
            {
                if (Session["userName"] != null)
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

    }
}