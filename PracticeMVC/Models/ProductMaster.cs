using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PracticeMVC.Models
{
    public class ProductMaster
    {
        [Key]
        public int productID { get; set; }
        public string productName { get; set; }
        [ForeignKey("CategoryMaster")]
        public int categoryID { get; set; }
        [NotMapped]
        public string encryptedID { get { return Cryptography.Encrypt(this.productID.ToString(), true); } set { } }
        [NotMapped]
        public string encryptedCategoryID { get { return Cryptography.Encrypt(this.categoryID.ToString(), true); } set { } }
        public virtual CategoryMaster CategoryMaster { get; set; }
    }
}