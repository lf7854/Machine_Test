using PracticeMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PracticeMVC.Controllers
{
    public class DefaultController : Controller
    {
        DatabaseContext databaseContext;
        public ActionResult Index()
        {
            databaseContext = new DatabaseContext();
            return View();
        }

        [HttpPost]
        public JsonResult getCategory(string id)
        {
            databaseContext = new DatabaseContext();
            List<CategoryMaster> categoryMasters = new List<CategoryMaster>();
            if (!string.IsNullOrEmpty(id))
            {
                int decryptedID = Convert.ToInt32(Cryptography.Decrypt(id, true));
                categoryMasters = databaseContext.CategoryMasters.Where(x => x.categoryID == decryptedID).ToList();
            }
            else
            {
                categoryMasters = databaseContext.CategoryMasters.ToList();
            }
            return Json(categoryMasters, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult createCategory(string categoryName)
        {
            string message = string.Empty;
            try
            {
                databaseContext = new DatabaseContext();
                CategoryMaster categoryMaster = new CategoryMaster();
                categoryMaster.categoryName = categoryName;
                databaseContext.CategoryMasters.Add(categoryMaster);
                databaseContext.SaveChanges();
                message = "SUCCESS";
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult updateCategory(string categoryID, string categoryName)
        {
            string message = string.Empty;
            try
            {
                databaseContext = new DatabaseContext();
                CategoryMaster categoryMaster = new CategoryMaster();
                categoryMaster.categoryID = Convert.ToInt32(Cryptography.Decrypt(categoryID, true));
                categoryMaster.categoryName = categoryName;
                databaseContext.Entry(categoryMaster).State = System.Data.Entity.EntityState.Modified;
                databaseContext.SaveChanges();
                message = "SUCCESS";
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult deleteCategory(string categoryID)
        {
            string message = string.Empty;
            try
            {
                databaseContext = new DatabaseContext();
                int decryptedID = Convert.ToInt32(Cryptography.Decrypt(categoryID, true));
                List<ProductMaster> products = databaseContext.ProductMasters.Where(x => x.categoryID == decryptedID).ToList();
                foreach (ProductMaster product in products)
                {
                    databaseContext.ProductMasters.Remove(product);
                }
                CategoryMaster categoryMaster = databaseContext.CategoryMasters.Find(decryptedID);
                databaseContext.CategoryMasters.Remove(categoryMaster);
                databaseContext.SaveChanges();
                message = "SUCCESS";
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public string getProduct(string id)
        {
            ModelProducts obj = new ModelProducts();
            DataTable dt = obj.getProductDetails(!string.IsNullOrEmpty(id) ? Convert.ToInt32(Cryptography.Decrypt(id, true)) : 0);
            dt.Columns.Add("encryptedID", typeof(string));
            dt.Columns.Add("encryptedID_Category", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["encryptedID"] = Cryptography.Encrypt(dt.Rows[i]["productID"].ToString(), true);
                dt.Rows[i]["encryptedID_Category"] = Cryptography.Encrypt(dt.Rows[i]["categoryID"].ToString(), true);
            }
            dt.AcceptChanges();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            var lst = dt.AsEnumerable().Select(r => r.Table.Columns.Cast<DataColumn>().Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])).ToDictionary(z => z.Key, z => z.Value)).ToList();
            return jsSerializer.Serialize(lst);
        }

        [HttpPost]
        public ActionResult createProduct(string categoryID, string productName)
        {
            ModelProducts obj = new ModelProducts();
            string message = string.Empty;
            try
            {
                int result = obj.insertProductDetails(Convert.ToInt32(Cryptography.Decrypt(categoryID, true)), productName);
                if (result != -1)
                {
                    message = "SUCCESS";
                }
                else
                {
                    message = "ERROR: Error occurred while saving record!";
                }
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult updateProduct(string categoryID, string productName, string productID)
        {
            ModelProducts obj = new ModelProducts();
            string message = string.Empty;
            try
            {
                int result = obj.updateProductDetails(Convert.ToInt32(Cryptography.Decrypt(categoryID, true)), Convert.ToInt32(Cryptography.Decrypt(productID, true)), productName);
                if (result != -1)
                {
                    message = "SUCCESS";
                }
                else
                {
                    message = "ERROR: Error occurred while updating record!";
                }
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult deleteProduct(string productID)
        {
            ModelProducts obj = new ModelProducts();
            string message = string.Empty;
            try
            {
                int result = obj.deleteProductDetails(Convert.ToInt32(Cryptography.Decrypt(productID, true)));
                if (result != -1)
                {
                    message = "SUCCESS";
                }
                else
                {
                    message = "ERROR: Error occurred while deleting record!";
                }
            }
            catch (Exception ex)
            {
                message = "ERROR: " + ex.Message + " --> " + ex.InnerException;
            }
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
    }
}