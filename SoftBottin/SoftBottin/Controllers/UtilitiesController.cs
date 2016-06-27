using SoftBottin.Models.Shoes;
using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Data;
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
                    cShoeType niShoeType = new cShoeType();
                    cShoe niShoe = new cShoe();
                    DataSet dsShoesTypes = new DataSet();
                    DataSet dsShoe = new DataSet();
                    string sErrMessage = "";
                    niShoeType.GetShoesTypes(out dsShoesTypes, out sErrMessage);
                    niShoe.GetShoes(out dsShoe, out sErrMessage);
                    List<cShoesByType> poShoesByType = new List<cShoesByType>();
                    niShoe.GetShoes(dsShoesTypes, dsShoe, out poShoesByType, out sErrMessage);

                    ViewBag.ShoesByType = poShoesByType;
                    ViewBag.ShoeTypes = dsShoesTypes;
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