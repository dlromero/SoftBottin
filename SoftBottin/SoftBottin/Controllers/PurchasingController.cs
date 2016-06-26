using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Controllers
{
    public class PurchasingController : UtilitiesController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseSummary()
        {
            return View();
        }
    }
}