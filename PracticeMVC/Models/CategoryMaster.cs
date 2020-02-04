using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PracticeMVC.Models
{
    public class CategoryMaster
    {
        [Key]
        public int categoryID { get; set; }
        public string categoryName { get; set; }
        [NotMapped]
        public string encryptedID { get { return Cryptography.Encrypt(this.categoryID.ToString(), true); } set { } }
    }
}