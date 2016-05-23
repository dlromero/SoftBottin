using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

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
        SoftBottin.ZapaTakeDataClassesDataContext dbConection;
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
                dbConection = new SoftBottin.ZapaTakeDataClassesDataContext();
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
                dbConection = new SoftBottin.ZapaTakeDataClassesDataContext();
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
                dbConection = new SoftBottin.ZapaTakeDataClassesDataContext();
                SoftBottin.User niUser = new SoftBottin.User
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
        #endregion
    }
}
