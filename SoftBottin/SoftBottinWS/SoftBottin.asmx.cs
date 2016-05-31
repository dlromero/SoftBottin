using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.Linq;

namespace SoftBottinWS
{
    /// <summary>
    /// Summary description for SoftBottin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoftBottin : System.Web.Services.WebService
    {
        #region Globlal
        /// <summary>
        /// 
        /// </summary>
        SoftBottinBD.SoftBottinDataClassesDataContext dbConection;
        /// <summary>
        /// 
        /// </summary>
        string sErrMsj;
        #endregion

        #region Methods
        /// <summary>
        /// Daniel Romero 20 de Febrero de 2016
        /// Metodo que permite traer todos los productos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool GetProducts()
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = from product in dbConection.Products
                               join productDetail in dbConection.ProductDetails
                                 on product.Id equals productDetail.IdProduct
                               join type in dbConection.ProductTypes
                                 on product.Type equals type.Id
                               join color in dbConection.Colors
                                 on productDetail.IdColor equals color.Id
                               select new
                               {
                                   ProductID = product.Id,
                                   ProductDetailID = productDetail.Id,
                                   ProductDetailColor = color.Description,
                                   ProductDetailSize = productDetail.Size,
                                   ProductDetailQuantity = productDetail.Quantity,
                                   Name = product.Name,
                                   Description = product.Description,
                                   QuantityExisting = product.QuantityExisting,
                                   QuantitySold = product.QuantitySold,
                                   PurchasePrice = product.PurchasePrice,
                                   SalePrice = product.SalePrice,
                                   TypeId = product.Type,
                                   Type = type.Name
                               };

                DataSet dsData = cUtilities.ToDataSet(products.ToList());

                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 20 de Febrero de 2016
        /// Metodo que permite consultar un producto segun el Id
        /// </summary>
        /// <param name="iProductId"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetProductById(int iProductId)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = from product in dbConection.Products
                               join productDetail in dbConection.ProductDetails
                                 on product.Id equals productDetail.IdProduct
                               join type in dbConection.ProductTypes
                                 on product.Type equals type.Id
                               join color in dbConection.Colors
                                 on productDetail.IdColor equals color.Id
                               where product.Id == iProductId
                               select new
                               {
                                   ProductID = product.Id,
                                   ProductDetailID = productDetail.Id,
                                   ProductDetailColor = color.Description,
                                   ProductDetailSize = productDetail.Size,
                                   ProductDetailQuantity = productDetail.Quantity,
                                   Name = product.Name,
                                   Description = product.Description,
                                   QuantityExisting = product.QuantityExisting,
                                   QuantitySold = product.QuantitySold,
                                   PurchasePrice = product.PurchasePrice,
                                   SalePrice = product.SalePrice,
                                   TypeId = product.Type,
                                   Type = type.Name
                               };

                DataSet dsData = cUtilities.ToDataSet(products.ToList());

                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 4 de Marzo de 2016
        /// Metodo que se crea para agregar un nuevo correo a la base de datos
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public bool AddEmailNewUser(string sEmail, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.User niUser = new SoftBottinBD.User
                {
                    Email = sEmail
                };
                dbConection.Users.InsertOnSubmit(niUser);
                try
                {
                    dbConection.SubmitChanges();
                }
                catch (Exception ex)
                {
                    cUtilities.WriteLog(ex.Message, out sErrMsj);
                    sErrMessage = ex.Message;
                    return false;
                }
                sErrMessage = "";
                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 28 de Mayo de 2016
        /// Metodo que se crea para verificar si un usario puede ingresar a la aplicacion
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SigIn(string sUserName, string sPassword, out DataSet dsUser, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.UserAccount niUserAccount = new SoftBottinBD.UserAccount
                {
                    UserName = sUserName,
                    Password = sPassword
                };

                var userSigIn = from user in dbConection.UserAccounts
                                where user.UserName.Equals(niUserAccount.UserName) &&
                                      user.Password.Equals(niUserAccount.Password)
                                select user;

                if (userSigIn.ToList().Count > 0)
                {
                    dsUser = cUtilities.ToDataSet(userSigIn.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsUser = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }


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
}
