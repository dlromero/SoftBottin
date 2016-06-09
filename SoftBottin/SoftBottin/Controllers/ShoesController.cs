using Newtonsoft.Json;
using SoftBottin.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Controllers
{
    public class ShoesController : Controller
    {
        // GET: Shoes
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TiposZapatos()
        {
            try
            {

                return View();
            }
            catch (Exception)
            {

                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Zapatos()
        {
            try
            {
                cShoeType niShoeType = new cShoeType();
                DataSet dsShoesTypes = new DataSet();
                string sErrMessage = "";
                niShoeType.GetShoesTypes(out dsShoesTypes, out sErrMessage);

                List<SelectListItem> lsShoesTypes = new List<SelectListItem>();
                for (int iShoesTypes = 0; iShoesTypes < dsShoesTypes.Tables[0].Rows.Count; iShoesTypes++)
                {
                    SelectListItem slItem = new SelectListItem();
                    slItem.Text = dsShoesTypes.Tables[0].Rows[iShoesTypes]["Name"].ToString();
                    slItem.Value = dsShoesTypes.Tables[0].Rows[iShoesTypes]["Id"].ToString();
                    lsShoesTypes.Add(slItem);
                }
                ViewBag.lsShoesTypes = lsShoesTypes;
                return View();
            }
            catch (Exception)
            {

                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxHandler()
        {
            cShoeType niShoeType = new cShoeType();
            DataSet dsShoesTypes = new DataSet();
            string sErrMessage = "";
            niShoeType.GetShoesTypes(out dsShoesTypes, out sErrMessage);

            List<string[]> lsShoesTypes = new List<string[]>();
            for (int iShoesTypes = 0; iShoesTypes < dsShoesTypes.Tables[0].Rows.Count; iShoesTypes++)
            {
                lsShoesTypes.Add(new string[] { dsShoesTypes.Tables[0].Rows[iShoesTypes]["Id"].ToString(),
                                                dsShoesTypes.Tables[0].Rows[iShoesTypes]["Name"].ToString(),
                                                dsShoesTypes.Tables[0].Rows[iShoesTypes]["Description"].ToString(),
                                                dsShoesTypes.Tables[0].Rows[iShoesTypes]["Ref"].ToString() });
            }
            return Json(new
            {
                sEcho = 1,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = lsShoesTypes
            },
            JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearTipoZapato()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShoeType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearTipoZapato(cShoeType objShoeType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string sErrMessage = "";
                    cShoeType niShoeType = new cShoeType();
                    niShoeType.AddShoeType(objShoeType, out sErrMessage);
                    ViewBag.AddSuccefull = true;
                    return View("TiposZapatos");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EditarTipoZapato()
        {
            try
            {

                cShoeType niShoeType = new cShoeType();
                DataSet dsShoesTypes = new DataSet();
                string sErrMessage = "";
                niShoeType.GetShoesTypes(out dsShoesTypes, out sErrMessage);

                List<SelectListItem> lsShoesTypes = new List<SelectListItem>();
                for (int iShoesTypes = 0; iShoesTypes < dsShoesTypes.Tables[0].Rows.Count; iShoesTypes++)
                {
                    SelectListItem slItem = new SelectListItem();
                    slItem.Text = dsShoesTypes.Tables[0].Rows[iShoesTypes]["Name"].ToString();
                    slItem.Value = dsShoesTypes.Tables[0].Rows[iShoesTypes]["Id"].ToString();
                    lsShoesTypes.Add(slItem);
                }
                ViewBag.lsShoesTypes = lsShoesTypes;
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iIdShoeType"></param>
        /// <returns></returns>
        public JsonResult GetShoeById(int iIdShoeType)
        {
            try
            {
                cShoeType niShoeType = new cShoeType();
                cShoeType niResult = new cShoeType();
                string sErrMessage = "";
                niShoeType.GetShoesTypesById(iIdShoeType, out niResult, out sErrMessage);

                return Json(JsonConvert.SerializeObject(niResult), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new JsonResult() { };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShoeType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditarTipoZapato(cShoeType objShoeType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cShoeType niShoeType = new cShoeType();
                    string sErrMessage = "";
                    niShoeType.EditShoeType(objShoeType, out sErrMessage);
                    return View("TiposZapatos");
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShoeType"></param>
        /// <returns></returns>
        public ActionResult EliminarTipoZapato(int iShoeTypeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cShoeType niShoeType = new cShoeType();
                    string sErrMessage = "";
                    niShoeType.DeleteShoeType(iShoeTypeId, out sErrMessage);
                    return View("TiposZapatos");
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearZapato()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

    }
}