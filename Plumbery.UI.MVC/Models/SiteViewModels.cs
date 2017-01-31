using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.UI.MVC.Models {
    public class CreateSiteViewModels {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Abbreviation")]
        public string Abbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Postal Code")]
        public string Postal_Code { get; set; }
    }
    public class EditSiteViewModels {
        /// <summary>
        /// 
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Abbreviation")]
        public string Abbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Address Line 1")]
        public string Address1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Address Line 2")]
        public string Address2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Postal Code")]
        public string Postal_Code { get; set; }
    }
}
