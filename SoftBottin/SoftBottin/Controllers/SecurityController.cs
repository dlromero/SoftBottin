using SoftBottin.Models;
using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SoftBottin.Controllers
{
    public class SecurityController : Controller
    {
        #region Global
        /// <summary>
        /// 
        /// </summary>
        string sErrMsj;
        #endregion


        // GET: Security
        public ActionResult Principal()
        {
            return View();
        }

        // GET: Security/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Security/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Daniel Romero 4 de Marzo de 2016
        /// Metodo de tipo post, que se crea para almacenar el correo electronico del cliente
        /// </summary>
        /// <returns></returns>
        [ActionName("AddNewEmailUser")]
        public JsonResult AddNewEmailUser(string sEmail)
        {
            try
            {
                cmSecurity niSecurity = new cmSecurity();
                bool bSuccesfull = true;
                bSuccesfull = niSecurity.AddNewEmailUser(sEmail, out sErrMsj);
                return new JsonResult()
                {
                    Data = bSuccesfull
                };
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                return new JsonResult()
                {
                    Data = ex.Message
                };
            }
        }


        [ActionName("AddNewProduct")]
        public JsonResult AddNewProduct(string objShoe)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                cmProduct user = jss.Deserialize<cmProduct>(objShoe);
                return new JsonResult()
                {
                    Data = true
                };
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                return new JsonResult()
                {
                    Data = ex.Message
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductDetail(string sProductReference)
        {
            try
            {

                cmProductDetail niProductDetailt = new cmProductDetail();
                niProductDetailt.sProductName = "Tacon Rosa Noruego";
                niProductDetailt.fPrice = 86000;
                niProductDetailt.sDescription = "Zapato que es valorado por su gran color";
                List<SelectListItem> lsColors = new List<SelectListItem>();
                SelectListItem sliNew = new SelectListItem();
                sliNew.Text = "Rosa";
                sliNew.Value = "0";
                lsColors.Add(sliNew);
                SelectListItem sliNew2 = new SelectListItem();
                sliNew2.Text = "Azul";
                sliNew2.Value = "1";
                lsColors.Add(sliNew2);


                List<SelectListItem> lsSisez = new List<SelectListItem>();
                SelectListItem sliNew3 = new SelectListItem();
                sliNew3.Text = "35";
                sliNew3.Value = "0";
                lsSisez.Add(sliNew3);
                SelectListItem sliNew24 = new SelectListItem();
                sliNew24.Text = "36";
                sliNew24.Value = "1";
                lsSisez.Add(sliNew24);


                niProductDetailt.slColors = lsColors;
                niProductDetailt.slSizes = lsSisez;

                niProductDetailt.sPathImageSmall1 = "Tacones/Zapato1_Small.jpg";
                niProductDetailt.sPathImageLarge1 = "Tacones/Zapato1.jpg";
                niProductDetailt.sPathImageSmall2 = "Tacones/Zapato1_Perfil_2_Small.jpg";
                niProductDetailt.sPathImageLarge2 = "Tacones/Zapato1_Perfil_2.jpg";
                niProductDetailt.sPathImageSmall3 = "Tacones/Zapato1_Perfil_3_Small.jpg";
                niProductDetailt.sPathImageLarge3 = "Tacones/Zapato1_Perfil_3.jpg";
                niProductDetailt.sPathImageSmall4 = "Tacones/Zapato1_Perfil_2_Small.jpg";
                niProductDetailt.sPathImageLarge4 = "Tacones/Zapato1_Perfil_2.jpg";


                return View(niProductDetailt);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}