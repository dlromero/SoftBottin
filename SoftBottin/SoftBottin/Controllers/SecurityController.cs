﻿using SoftBottin.Models;
using SoftBottin.Models.Shoes;
using SoftBottinWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SoftBottin.Controllers
{
    public class SecurityController : UtilitiesController
    {
        #region Global
        /// <summary>
        /// 
        /// </summary>
        string sErrMsj;
        #endregion


        // GET: Security
        [CompressFilter]
        public ActionResult Principal()
        {

            try
            {
                //cShoeType niShoeType = new cShoeType();
                //cShoe niShoe = new cShoe();
                //DataSet dsShoesTypes = new DataSet();
                //DataSet dsShoe = new DataSet();
                //string sErrMessage = "";
                //niShoeType.GetShoesTypes(out dsShoesTypes, out sErrMessage);
                //niShoe.GetShoes(out dsShoe, out sErrMessage);
                //List<cShoesByType> poShoesByType = new List<cShoesByType>();
                //niShoe.GetShoes(dsShoesTypes, dsShoe, out poShoesByType, out sErrMessage);

                //ViewBag.ShoesByType = poShoesByType;
                //ViewBag.ShoeTypes = dsShoesTypes;
                return View();
            }
            catch (Exception)
            {

                return View();
            }

        }

        [CompressFilter]
        public ActionResult SubPrincipal()
        {

            try
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
                return View();
            }
            catch (Exception)
            {

                return View();
            }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objShoe"></param>
        /// <returns></returns>
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
                string sErrMessage = "";
                cmProductDetail niProductDetailt = new cmProductDetail();
                cmProductDetail niProductDetailt2 = new cmProductDetail();
                niProductDetailt.GetFeaturesByShoe(Convert.ToInt32(sProductReference), out niProductDetailt2, out sErrMessage);
                return View(niProductDetailt2);
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
        public JsonResult LogIn(string objUser)
        {
            try
            {
                JavaScriptSerializer jSerializer = new JavaScriptSerializer();
                cmUser user = jSerializer.Deserialize<cmUser>(objUser);
                cmSecurity niSecurity = new cmSecurity();
                DataSet dsUser = new DataSet();
                bool bAccess = niSecurity.LogIn(user.sUserName, user.sPassword, out dsUser, out sErrMsj);
                if (bAccess)
                {
                    Session["userName"] = user.sUserName;
                    Session["RoleID"] = dsUser.Tables[0].Rows[0]["RoleId"].ToString();
                    TempData["userName"] = user.sUserName;
                }


                return new JsonResult() { Data = bAccess };
            }
            catch (Exception)
            {
                return new JsonResult() { };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public JsonResult SignIn(string objUser)
        {
            try
            {
                Session.Remove("SignIn");
                JavaScriptSerializer jSerializer = new JavaScriptSerializer();
                cmUserSgnIn user = jSerializer.Deserialize<cmUserSgnIn>(objUser);
                cmSecurity niSecurity = new cmSecurity();
                DataSet dsUser = new DataSet();
                bool bAccess = niSecurity.SignIn(user.sFirstName, user.sLastName, user.sEmail, user.sPassword, out sErrMsj);

                if (bAccess)
                {
                    TempData["SignIn"] = true;
                }

                return new JsonResult()
                {
                    Data = bAccess
                };
            }
            catch (Exception)
            {
                return new JsonResult() { };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOut()
        {
            try
            {
                Session.RemoveAll();
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
                return View("Principal");
            }
            catch (Exception)
            {
                return View("Principal");
            }
        }


        /// <summary>
        /// Daniel Romero 26 de Junio de 2016
        /// Metodo que permite verificar la existencia de un email repetido
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public JsonResult CheckEmail(string sEmail)
        {
            try
            {

                cmSecurity niSecurity = new cmSecurity();
                DataSet dsUser = new DataSet();
                bool bAccess = niSecurity.CheckEmail(sEmail, out dsUser, out sErrMsj);


                if (dsUser.Tables.Count > 0)
                {
                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        bAccess = false;
                    }
                }
                else
                {
                    bAccess = true;
                }

                return new JsonResult()
                {
                    Data = bAccess
                };
            }
            catch (Exception)
            {
                return new JsonResult() { };
            }
        }


        public ActionResult ViewShoppingCart()
        {
            return View();
        }

    }
}