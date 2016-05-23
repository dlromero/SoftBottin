using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftBottin.Models
{
    public class cmProductDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public string sProductName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float fPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sColorSelect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sSizeSelect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> slColors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<SelectListItem> slSizes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageSmall4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sPathImageLarge4 { get; set; }


    }
}