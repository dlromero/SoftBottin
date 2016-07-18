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

        #region Zapatos
        /// <summary>
        ///  Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para consultar los colores existentes
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetShoesByType(int iShoeType, out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = (from pds in dbConection.Products
                                join typ in dbConection.ProductTypes
                                on pds.Type equals typ.Id
                                join prd in dbConection.ProductDetails
                                on pds.Id equals prd.IdProduct
                                join clr in dbConection.Colors
                                on prd.IdColor equals clr.Id
                                join img in dbConection.Images
                                on pds.Id equals img.IdProduct into imageGroup
                                from images in imageGroup.DefaultIfEmpty().Take(1)
                                where pds.Type.Equals(iShoeType)
                                select new
                                {
                                    IdProduct = pds.Id,
                                    NameProduct = pds.Name,
                                    DescriptionProduct = pds.Description,
                                    QuantityExisting = prd.QuantityExisting,
                                    QuantitySold = prd.QuantitySold,
                                    PurchasePrice = pds.PurchasePrice,
                                    SalePrice = pds.SalePrice,
                                    IdType = pds.Type,
                                    TypeDescription = typ.Name,
                                    IdProductDetail = prd.Id,
                                    Size = prd.Size,
                                    Quantity = prd.Quantity,
                                    IdColor = clr.Id,
                                    ColorDescription = clr.Description,
                                    RGB = clr.RGB,
                                    ImageShoe = images.Image1 == null ? null : images.Image1
                                }).OrderByDescending(x => x.IdType);

                if (products.ToList().Count > 0)
                {
                    dsShoes = cUtilities.ToDataSet(products.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 12 de Junio de 2016
        /// Metodo que se crea para crear un zapato
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sDescription"></param>
        /// <param name="sRef"></param>
        /// <param name="iQuantityExisting"></param>
        /// <param name="iQuantitySold"></param>
        /// <param name="iPurchasePrice"></param>
        /// <param name="iSalePrice"></param>
        /// <param name="iShoeType"></param>
        /// <param name="iIdInsert"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddShoe(string sName, string sDescription, string sRef,
                            int iQuantityExisting, int iQuantitySold, int iPurchasePrice,
                            int iSalePrice, int iShoeType, out int iIdInsert, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.Product niProduct = new SoftBottinBD.Product
                {
                    Name = sName,
                    Description = sDescription,
                    QuantityExisting = iQuantityExisting,
                    QuantitySold = iQuantitySold,
                    PurchasePrice = iPurchasePrice,
                    SalePrice = iSalePrice,
                    Type = iShoeType
                };

                dbConection.Products.InsertOnSubmit(niProduct);
                dbConection.SubmitChanges();

                iIdInsert = niProduct.Id;
                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                iIdInsert = -1;
                return false;
            }
        }


        /// <summary>
        /// Daniel Romero 12 de Junio de 2016
        /// Metodo que se crea para crear el detalle de un zapato
        /// </summary>
        /// <param name="iIdShoe"></param>
        /// <param name="iIdColor"></param>
        /// <param name="iSize"></param>
        /// <param name="iQuantity"></param>
        /// <param name="iQuantityExisting"></param>
        /// <param name="iQuantitySold"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddShoeDetail(int iIdShoe, int iIdColor, int iSize,
                                  int iQuantity, int iQuantityExisting,
                                  int iQuantitySold, out int iIdDetailInsert, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.ProductDetail niProductDetail = new SoftBottinBD.ProductDetail
                {
                    IdProduct = iIdShoe,
                    IdColor = iIdColor,
                    Size = iSize,
                    Quantity = iQuantity,
                    QuantityExisting = iQuantityExisting,
                    QuantitySold = iQuantitySold
                };

                dbConection.ProductDetails.InsertOnSubmit(niProductDetail);
                dbConection.SubmitChanges();
                iIdDetailInsert = niProductDetail.Id;

                return true;
            }
            catch (Exception ex)
            {
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                iIdDetailInsert = -1;
                return false;
            }
        }

        /// <summary>
        /// Daniel Romero 18 de Junio de 2016
        /// Metodo que se crea para subir una imagen de un zapato
        /// </summary>
        /// <param name="iIdShoe"></param>
        /// <param name="sName"></param>
        /// <param name="sType"></param>
        /// <param name="btFileByte"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddImageShoe(string sName, string sType, byte[] btFileByte, int iProductId, bool bIsPrincipal, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.Image niImage = new SoftBottinBD.Image
                {
                    Description = sName,
                    Name = sName,
                    Type = sType,
                    Image1 = btFileByte,
                    IdProduct = iProductId,
                    isPrincipal = bIsPrincipal
                };

                dbConection.Images.InsertOnSubmit(niImage);
                dbConection.SubmitChanges();
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
        /// Daniel Romero 19 de Junio de 2016
        /// Metodo que se crea para obtener una imagen de un zapato
        /// </summary>
        /// <param name="iShoeID"></param>
        /// <param name="dsShoes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetShoeImages(int iProductId, out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var imag = (from img in dbConection.Images
                            where img.IdProduct.Equals(iProductId)
                            select img).Take(1);

                if (imag.ToList().Count > 0)
                {
                    dsShoes = cUtilities.ToDataSet(imag.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// Daniel Romero 19 de Junio de 2016
        /// Metodo que se crea para obtener los zapatos
        /// </summary>
        /// <param name="dsShoes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetShoes(out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = from rsl in dbConection.Products
                               join img in dbConection.Images
                                 on rsl.Id equals img.IdProduct
                               where img.isPrincipal.Equals(true)
                               select new
                               {
                                   Id = rsl.Id,
                                   Name = rsl.Name,
                                   Description = rsl.Description,
                                   QuantityExisting = rsl.QuantityExisting,
                                   QuantitySold = rsl.QuantitySold,
                                   PurchasePrice = rsl.PurchasePrice,
                                   SalePrice = rsl.SalePrice,
                                   ShoeType = rsl.Type,
                                   ShoeImage = img.Image1,
                                   TotalQuantityExisting = (from rsl2 in dbConection.ProductDetails
                                                            where rsl2.IdProduct.Equals(rsl.Id)
                                                            select rsl2).Sum(rsl3 => rsl3.QuantityExisting)
                               };

                if (products.ToList().Count > 0)
                {
                    dsShoes = cUtilities.ToDataSet(products.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Daniel Romero 4 de Julio de 2016
        /// Metodo que se crea para dar de baja un zapato
        /// </summary>
        /// <param name="iShoeId"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool ProductOut(int iShoeId, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var products = from rsl in dbConection.ProductDetails
                               where rsl.Id.Equals(iShoeId)
                               select rsl;

                foreach (SoftBottinBD.ProductDetail item in products)
                {
                    item.QuantityExisting = 0;
                    dbConection.SubmitChanges();
                }

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
        /// Daniel Romero 16 de Julio de 2016
        /// Metodo que se crea para consultar las caracteristicas de un zapato
        /// </summary>
        /// <param name="iShoeId"></param>
        /// <param name="dsShoes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetFeaturesByShoe(int iProductId, out DataSet dsShoes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = from rsl in dbConection.ProductDetails
                               join rsl2 in dbConection.Products
                                 on rsl.IdProduct equals rsl2.Id
                               join rsl3 in dbConection.Images
                                 on rsl.IdProduct equals rsl3.IdProduct
                               where rsl2.Id.Equals(iProductId)
                               select new
                               {
                                   ptId = rsl.Id,
                                   ptIdProduct = rsl.IdProduct,
                                   IdColor = rsl.Color,
                                   Size = rsl.Size,
                                   Quantity = rsl.Quantity,
                                   ptQuantityExisting = rsl.QuantityExisting,
                                   ptQuantitySold = rsl.QuantitySold,
                                   pdPurchasePrice = rsl2.PurchasePrice,
                                   pdSalePrice = rsl2.SalePrice,
                                   pdType = rsl2.Type,
                                   pdName = rsl2.Name,
                                   pdDescription = rsl2.Description,
                                   imgDescription = rsl3.Description,
                                   imgImage = rsl3.Image1,
                                   imgisPrincipal = rsl3.isPrincipal
                               };

                if (products.ToList().Count > 0)
                {
                    dsShoes = cUtilities.ToDataSet(products.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 17 de Julio de 2016
        /// Metodo que se crea para consultar las tallas de un zapato
        /// </summary>
        /// <param name="iProductId"></param>
        /// <param name="dsShoes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetSizesByShoe(int iProductId, out DataSet dsSizes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var products = from rsl in dbConection.Products
                               join rsl2 in dbConection.ProductDetails
                                 on rsl.Id equals rsl2.IdProduct                              
                               where rsl.Id.Equals(iProductId)
                               select new
                               {
                                   ptId = rsl.Id,
                                   Name = rsl.Name,
                                   Size = rsl2.Size
                               };

                if (products.ToList().Count > 0)
                {
                    dsSizes = cUtilities.ToDataSet(products.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsSizes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsSizes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        

        #endregion

        #region Tipos de Zapatos
        /// <summary>
        /// Daniel Romero 4 de Junio de 2016
        /// Metodo que se crea para insertar un nuevo tipo de zapato
        /// </summary>
        /// <param name="sName"></param>
        /// <param name="sDescription"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddShoeType(string sName, string sDescription, string sRef, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.ProductType niProductType = new SoftBottinBD.ProductType
                {
                    Name = sName,
                    Description = sDescription,
                    Ref = sRef
                };

                dbConection.ProductTypes.InsertOnSubmit(niProductType);
                dbConection.SubmitChanges();
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
        /// Daniel Romero 4 de Junio de 2016
        /// Metodo que se crea para consultar los tipos de zapatos
        /// </summary>
        /// <param name="dsShoesTypes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetShoesTypes(out DataSet dsShoesTypes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var shoesTypes = from showTypes in dbConection.ProductTypes
                                 select showTypes;

                if (shoesTypes.ToList().Count > 0)
                {
                    dsShoesTypes = cUtilities.ToDataSet(shoesTypes.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoesTypes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoesTypes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iShoeType"></param>
        /// <param name="dsShoesTypes"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetShoesTypesById(int iShoeType, out DataSet dsShoesTypes, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var shoesTypes = from shoeTypesq in dbConection.ProductTypes
                                 where shoeTypesq.Id.Equals(iShoeType)
                                 select shoeTypesq;

                if (shoesTypes.ToList().Count > 0)
                {
                    dsShoesTypes = cUtilities.ToDataSet(shoesTypes.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsShoesTypes = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsShoesTypes = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 5 de Junio de 2016
        /// Metodo que se crea para modificar un tipo de zapato segun el id
        /// </summary>
        /// <param name="iId"></param>
        /// <param name="sName"></param>
        /// <param name="sDescription"></param>
        /// <param name="sRef"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool EditShoeType(int iId, string sName, string sDescription, string sRef, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.ProductType niProductType = new SoftBottinBD.ProductType
                {
                    Id = iId,
                    Name = sName,
                    Description = sDescription,
                    Ref = sRef
                };

                var query = from pty in dbConection.ProductTypes
                            where pty.Id.Equals(iId)
                            select pty;

                foreach (SoftBottinBD.ProductType pty in query)
                {
                    pty.Name = niProductType.Name;
                    pty.Description = niProductType.Description;
                    pty.Ref = niProductType.Ref;
                }
                dbConection.SubmitChanges();
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
        /// 5 de Junio de 2016
        /// Metodo que se crea para eliminar un tipo de zapato segun el id
        /// </summary>
        /// <param name="iId"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DeleteShoeType(int iId, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var query = from pty in dbConection.ProductTypes
                            where pty.Id.Equals(iId)
                            select pty;

                foreach (var detail in query)
                {
                    dbConection.ProductTypes.DeleteOnSubmit(detail);
                }
                dbConection.SubmitChanges();
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

        #region Colores
        /// <summary>
        ///  Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para consultar los colores existentes
        /// </summary>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetColors(out DataSet dsColors, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var colors = from clr in dbConection.Colors
                             select clr;

                if (colors.ToList().Count > 0)
                {
                    dsColors = cUtilities.ToDataSet(colors.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsColors = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsColors = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para consultar los colores existentes por id 
        /// </summary>
        /// <param name="iColorId"></param>
        /// <param name="dsColors"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool GetColorById(int iColorId, out DataSet dsColors, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var colors = from clr in dbConection.Colors
                             where clr.Id.Equals(iColorId)
                             select clr;

                if (colors.ToList().Count > 0)
                {
                    dsColors = cUtilities.ToDataSet(colors.ToList());
                    sErrMessage = "";
                    return true;
                }
                else
                {
                    dsColors = new DataSet();
                    sErrMessage = "Not Found";
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsColors = new DataSet();
                cUtilities.WriteLog(ex.Message, out sErrMsj);
                sErrMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Daniel Romero 11 de Junio de 2016
        /// Metodo que se crea para insertar un nuevo color
        /// </summary>
        /// <param name="sDescription"></param>
        /// <param name="sRGB"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool AddColor(string sDescription, string sRGB, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.Color niColor = new SoftBottinBD.Color
                {
                    Description = sDescription,
                    RGB = sRGB
                };

                dbConection.Colors.InsertOnSubmit(niColor);
                dbConection.SubmitChanges();
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
        /// 11 de Junio de 2016
        /// Metodo que se crea para modificar un color segun el id
        /// </summary>
        /// <param name="iId"></param>
        /// <param name="sDescription"></param>
        /// <param name="sRGB"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool EditColor(int iId, string sDescription, string sRGB, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.Color niColors = new SoftBottinBD.Color
                {
                    Id = iId,
                    Description = sDescription,
                    RGB = sRGB
                };

                var query = from clr in dbConection.Colors
                            where clr.Id.Equals(iId)
                            select clr;

                foreach (SoftBottinBD.Color clr in query)
                {
                    clr.Description = niColors.Description;
                    clr.RGB = niColors.RGB;
                }
                dbConection.SubmitChanges();
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
        /// 11 de Junio de 2016
        /// Metodo que se crea para eliminar un color segun el id
        /// </summary>
        /// <param name="iId"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DeleteColor(int iId, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                var query = from clr in dbConection.Colors
                            where clr.Id.Equals(iId)
                            select clr;

                foreach (var detail in query)
                {
                    dbConection.Colors.DeleteOnSubmit(detail);
                }
                dbConection.SubmitChanges();
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

        #region Inicio de sesion
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
        public bool LogIn(string sUserName, string sPassword, out DataSet dsUser, out string sErrMessage)
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

        /// <summary>
        /// Daniel Romero 28 de Mayo de 2016
        /// Metodo que se crea para crear un nuevo usuario
        /// </summary>
        /// <param name="sFirstName"></param>
        /// <param name="sLastName"></param>
        /// <param name="sEmail"></param>
        /// <param name="sPassword"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool SignIn(string sFirstName, string sLastName, string sEmail, string sPassword, out string sErrMessage)
        {
            try
            {
                sErrMessage = "";
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();
                SoftBottinBD.User niUser = new SoftBottinBD.User
                {
                    FirstName = sFirstName,
                    LastName = sLastName,
                    Email = sEmail

                };

                dbConection.Users.InsertOnSubmit(niUser);
                dbConection.SubmitChanges();


                SoftBottinBD.UserAccount niUserAccount = new SoftBottinBD.UserAccount
                {
                    UserName = sEmail,
                    Password = sPassword,
                    UserId = niUser.Id,
                    RoleId = 2
                };

                dbConection.UserAccounts.InsertOnSubmit(niUserAccount);
                dbConection.SubmitChanges();

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
        /// Daniel Romero 26 de Junio de 2016
        /// Metodo que se crea para verificar que no se repitan los correos electronicos
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="dsUser"></param>
        /// <param name="sErrMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public bool CheckEmail(string sEmail, out DataSet dsUser, out string sErrMessage)
        {
            try
            {
                dbConection = new SoftBottinBD.SoftBottinDataClassesDataContext();

                var userSigIn = from user in dbConection.UserAccounts
                                where user.UserName.Equals(sEmail)
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

        #endregion
    }
}
