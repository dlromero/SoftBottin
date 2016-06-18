using Newtonsoft.Json;
using SoftBottin.Models.Shoes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Controllers
{
    public class ShoesController : Controller
    {
        #region Zapatos
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
        /// <param name="iIdShoeType"></param>
        /// <returns></returns>
        public ActionResult AjaxHandlerZapatos(int iIdShoeType)
        {
            cShoe niShoe = new cShoe();
            DataSet dsShoes = new DataSet();
            string sErrMessage = "";
            niShoe.GetShoesByType(iIdShoeType, out dsShoes, out sErrMessage);

            List<cShoeTemp> lscShoe = new List<cShoeTemp>();

            if (dsShoes.Tables.Count > 0)
            {
                for (int iShoes = 0; iShoes < dsShoes.Tables[0].Rows.Count; iShoes++)
                {
                    cShoeTemp niShoeTemp = new cShoeTemp();
                    niShoeTemp.sIdProduct = dsShoes.Tables[0].Rows[iShoes]["IdProduct"].ToString();
                    niShoeTemp.sNameProduct = dsShoes.Tables[0].Rows[iShoes]["NameProduct"].ToString();
                    niShoeTemp.sDescriptionProduct = dsShoes.Tables[0].Rows[iShoes]["DescriptionProduct"].ToString();
                    niShoeTemp.sQuantityExisting = dsShoes.Tables[0].Rows[iShoes]["QuantityExisting"].ToString();
                    niShoeTemp.sQuantitySold = dsShoes.Tables[0].Rows[iShoes]["QuantitySold"].ToString();
                    niShoeTemp.sPurchasePrice = dsShoes.Tables[0].Rows[iShoes]["PurchasePrice"].ToString();
                    niShoeTemp.sSalePrice = dsShoes.Tables[0].Rows[iShoes]["SalePrice"].ToString();
                    niShoeTemp.sIdType = dsShoes.Tables[0].Rows[iShoes]["IdType"].ToString();
                    niShoeTemp.sIdProductDetail = dsShoes.Tables[0].Rows[iShoes]["IdProductDetail"].ToString();
                    niShoeTemp.sSize = dsShoes.Tables[0].Rows[iShoes]["Size"].ToString();
                    niShoeTemp.sQuantity = dsShoes.Tables[0].Rows[iShoes]["Quantity"].ToString();
                    niShoeTemp.sIdColor = dsShoes.Tables[0].Rows[iShoes]["IdColor"].ToString();
                    niShoeTemp.sColorDescription = dsShoes.Tables[0].Rows[iShoes]["ColorDescription"].ToString();
                    niShoeTemp.sRGB = dsShoes.Tables[0].Rows[iShoes]["RGB"].ToString();
                    niShoeTemp.sTypeDescription = dsShoes.Tables[0].Rows[iShoes]["TypeDescription"].ToString();

                    lscShoe.Add(niShoeTemp);
                }
            }


            return Json(JsonConvert.SerializeObject(lscShoe), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearZapato()
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

                cColor niColor = new cColor();
                DataSet dsColors = new DataSet();
                niColor.GetColors(out dsColors, out sErrMessage);


                List<SelectListItem> lsColors = new List<SelectListItem>();
                for (int iColors = 0; iColors < dsColors.Tables[0].Rows.Count; iColors++)
                {
                    SelectListItem slItem = new SelectListItem();
                    slItem.Text = dsColors.Tables[0].Rows[iColors]["Description"].ToString();
                    slItem.Value = dsColors.Tables[0].Rows[iColors]["Id"].ToString();
                    lsColors.Add(slItem);
                }



                ViewBag.lsShoesTypes = lsShoesTypes;
                ViewBag.lsColors = lsColors;


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
        /// <param name="objShoe"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearZapato(cShoe objShoe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<cShoeDetail> lscShoeType = JsonConvert.DeserializeObject<List<cShoeDetail>>(objShoe.sColorDetail);

                    string sErrMessage = "";
                    cShoe niShoe = new cShoe();
                    int iIdInsert = 0;
                    if (niShoe.AddShoe(objShoe.sName, objShoe.sDescription, objShoe.sRef, 0,
                                   0, objShoe.iCost, objShoe.iPrice, objShoe.iShoeType, lscShoeType,
                                   out iIdInsert, out sErrMessage))
                    {


                        #region Imagenes

                        List<HttpPostedFileBase> lsFilesFinal = new List<HttpPostedFileBase>();
                        if (Session["lsFilesFinal"] != null)
                        {
                            lsFilesFinal = (List<HttpPostedFileBase>)Session["lsFilesFinal"];
                        }


                        foreach (HttpPostedFileBase item in lsFilesFinal)
                        {
                            byte[] data;
                            using (Stream inputStream = item.InputStream)
                            {
                                MemoryStream memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }
                                data = memoryStream.ToArray();
                            }
                            niShoe.AddImageShoe(iIdInsert, item.FileName, item.ContentType, data, out sErrMessage);
                        }

                        #endregion

                        #region Cargar correctamnete Zapatos
                        cShoeType niShoeType = new cShoeType();
                        DataSet dsShoesTypes = new DataSet();
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
                        #endregion

                        return View("Zapatos");
                    }
                    else
                    {
                        return View("CrearZapatos");
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }



        public ActionResult Imagenes()
        {
            return PartialView();
        }


        public JsonResult UploadFile()
        {
            try
            {
                List<HttpPostedFileBase> lsFilesFinal = new List<HttpPostedFileBase>();
                if (Session["lsFilesFinal"] != null)
                {
                    lsFilesFinal = (List<HttpPostedFileBase>)Session["lsFilesFinal"];
                }

                foreach (string item in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                    string fileName = file.FileName;
                    string UploadPath = "~/Images/";

                    if (file.ContentLength == 0)
                        continue;
                    if (file.ContentLength > 0)
                    {
                        string path = Path.Combine(HttpContext.Request.MapPath(UploadPath), fileName);
                        string extension = Path.GetExtension(file.FileName);
                        lsFilesFinal.Add(file);
                        //file.SaveAs(path);
                    }
                }
                Session["lsFilesFinal"] = lsFilesFinal;
                return Json("");

            }
            catch (Exception)
            {
                return Json("There is error try again later");
            }
        }

        #endregion

        #region Tipos de Zapatos
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
        public ActionResult AjaxHandlerTiposZapatos()
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

        #endregion

        #region Colores
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Colores()
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
        ///  Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para consultar los colores existentes
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxHandlerColores()
        {
            cColor niColores = new cColor();
            DataSet dsColors = new DataSet();
            string sErrMessage = "";
            niColores.GetColors(out dsColors, out sErrMessage);

            List<string[]> lsColors = new List<string[]>();
            for (int iColors = 0; iColors < dsColors.Tables[0].Rows.Count; iColors++)
            {
                lsColors.Add(new string[] { dsColors.Tables[0].Rows[iColors]["Id"].ToString(),
                                            dsColors.Tables[0].Rows[iColors]["Description"].ToString(),
                                           dsColors.Tables[0].Rows[iColors]["RGB"].ToString() });
            }

            return Json(new
            {
                sEcho = 1,
                iTotalRecords = 97,
                iTotalDisplayRecords = 3,
                aaData = lsColors
            },
            JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        ///  Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para cargar la vista de creacion de color
        /// </summary>
        /// <returns></returns>
        public ActionResult CrearColor()
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
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para cargar crear un color
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CrearColor(cColor objColor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string sErrMessage = "";
                    cColor niColor = new cColor();
                    niColor.AddColor(objColor, out sErrMessage);
                    ViewBag.AddSuccefull = true;
                    return View("Colores");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para la vista de edicion de colores
        /// </summary>
        /// <returns></returns>
        public ActionResult EditarColor()
        {
            try
            {
                cColor niColor = new cColor();
                DataSet dsColors = new DataSet();
                string sErrMessage = "";
                niColor.GetColors(out dsColors, out sErrMessage);

                List<SelectListItem> lsColors = new List<SelectListItem>();
                for (int iColors = 0; iColors < dsColors.Tables[0].Rows.Count; iColors++)
                {
                    SelectListItem slItem = new SelectListItem();
                    slItem.Text = dsColors.Tables[0].Rows[iColors]["Description"].ToString();
                    slItem.Value = dsColors.Tables[0].Rows[iColors]["Id"].ToString();
                    lsColors.Add(slItem);
                }
                ViewBag.lsColors = lsColors;
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para editar un color
        /// </summary>
        /// <param name="objColor"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditarColor(cColor objColor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cColor niColor = new cColor();
                    string sErrMessage = "";
                    niColor.EditColor(objColor, out sErrMessage);
                    return View("Colores");
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para traer los colores por id
        /// </summary>
        /// <param name="iIdColor"></param>
        /// <returns></returns>
        public JsonResult GetColorById(int iIdColor)
        {
            try
            {
                cColor niColor = new cColor();
                string sErrMessage = "";
                niColor.GetColorsById(iIdColor, out niColor, out sErrMessage);

                return Json(JsonConvert.SerializeObject(niColor), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return new JsonResult() { };
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para eliminar un color por id
        /// </summary>
        /// <param name="objShoeType"></param>
        /// <returns></returns>
        public ActionResult EliminarColor(int iIdColor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cColor niColor = new cColor();
                    string sErrMessage = "";
                    niColor.DeleteColor(iIdColor, out sErrMessage);
                    return View("Colores");
                }

                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }


        #endregion

    }
}