using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace PracticeMVC.Models
{
    public class ModelProducts
    {
        string defaultConnection = System.Configuration.ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;

        public DataTable getProductDetails(int productID)
        {
            SqlConnection sqlConnection = new SqlConnection(defaultConnection);
            try
            {
                string query = "SP_GetProductDetails";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);
                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                sqlConnection.Close();
                SqlConnection.ClearPool(sqlConnection);
            }
        }

        public int insertProductDetails(int categoryID, string productName)
        {
            int result = -1;
            SqlConnection sqlConnection = new SqlConnection(defaultConnection);
            try
            {
                string query = "SP_InsertProductDetails";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CategoryID", categoryID);
                sqlCommand.Parameters.AddWithValue("@ProductName", productName);
                sqlConnection.Open();
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                SqlConnection.ClearPool(sqlConnection);
            }
            return result;
        }

        public int updateProductDetails(int categoryID, int productID, string productName)
        {
            int result = -1;
            SqlConnection sqlConnection = new SqlConnection(defaultConnection);
            try
            {
                string query = "SP_UpdateProductDetails";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@CategoryID", categoryID);
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);
                sqlCommand.Parameters.AddWithValue("@ProductName", productName);
                sqlConnection.Open();
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                SqlConnection.ClearPool(sqlConnection);
            }
            return result;
        }

        public int deleteProductDetails(int productID)
        {
            int result = -1;
            SqlConnection sqlConnection = new SqlConnection(defaultConnection);
            try
            {
                string query = "SP_DeleteProductDetails";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@ProductID", productID);
                sqlConnection.Open();
                result = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
                SqlConnection.ClearPool(sqlConnection);
            }
            return result;
        }
    }
}