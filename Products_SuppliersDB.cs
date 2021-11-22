/*
 * Author: 
 * Date: July, 2017
 * Description:  Database methods for retrieving, inserting, updating, and deleting records 
 *               from the Products_Suppliers table
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts {
    public static class Products_SuppliersDB {

        // retrieves and returns a list of all Products_Suppliers from database
        public static List<Products_Suppliers> GetAllProductsSuppliers() {
            List<Products_Suppliers> prodSupList = new List<Products_Suppliers>();
            Products_Suppliers prodSup;
            SqlConnection con = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, con);
            
            try {
                con.Open();
                SqlDataReader psReader = selectCommand.ExecuteReader(); // read data into list
                while (psReader.Read()) { 
                    prodSup = new Products_Suppliers((int)psReader["ProductSupplierId"], 
                                                     (int)psReader["ProductId"], 
                                                     (int)psReader["SupplierId"]);
                    prodSupList.Add(prodSup); // add to list
                }
            } catch (SqlException ex) { throw ex; }
            finally { con.Close(); }
            return prodSupList;
        }


        // retrieves and returns a single Products_Suppliers from database based on ProductSupplierId
        public static Products_Suppliers GetProductSupplier(int prodSupId) {
            Products_Suppliers prodSup;
            SqlConnection con = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                     "FROM Products_Suppliers " +
                                     "WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, con);
            selectCommand.Parameters.AddWithValue("@ProductSupplierId", prodSupId);

            try {
                con.Open();
                SqlDataReader psReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (psReader.Read()) {
                    prodSup = new Products_Suppliers((int)psReader["ProductSupplierId"],
                                                     (int)psReader["ProductId"],
                                                     (int)psReader["SupplierId"]);
                    return prodSup;
                } else { return null; }
            } catch (SqlException ex) { throw ex; } finally { con.Close(); }
        }


        // inserts a new Products_Suppliers record into the database
        public static int AddProductSupplier(Products_Suppliers prodSup) {
            SqlConnection con = TravelExpertsDB.GetConnection();

            string insertStatement = "INSERT INTO Products_Suppliers " +
                                     "(ProductId, SupplierId) " +
                                     "VALUES (@ProductId, @SupplierId";
            SqlCommand insertCommand = new SqlCommand(insertStatement, con);
            insertCommand.Parameters.AddWithValue("@ProductId", prodSup.ProductID);
            insertCommand.Parameters.AddWithValue("@SupplierId", prodSup.SupplierID);

            try {
                con.Open();
                insertCommand.ExecuteNonQuery();
                string selectStatement = "SELECT IDENT_CURRENT('Products_Suppliers') FROM Products_Suppliers";
                SqlCommand selectCommand = new SqlCommand(selectStatement, con);
                int prodSupId = Convert.ToInt32(selectCommand.ExecuteScalar());
                return prodSupId;

            } catch (SqlException ex ) { throw ex; }
            finally { con.Close(); }
        }


        // updates record in the database
        public static bool UpdateProdSup(int oldProdSupId, Products_Suppliers newProdSup) {
            SqlConnection con = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Products_Suppliers " +
                                     "SET ProductId = '@ProductId', SupplierId = '@SupplierId' " +
                                     "WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand updateCommand = new SqlCommand(updateStatement, con);
            updateCommand.Parameters.AddWithValue("@ProductId", newProdSup.ProductID);
            updateCommand.Parameters.AddWithValue("@SupplierId", newProdSup.SupplierID);
            updateCommand.Parameters.AddWithValue("@ProductSupplierId", oldProdSupId);
            try {
                con.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            } catch (SqlException ex) { throw ex; }
            finally { con.Close(); }
        }


        // deletes a record from the database
        public static bool DeleteProdSup(int prodSupId) {
            SqlConnection con = TravelExpertsDB.GetConnection();

            string deleteStatement = "DELETE FROM Products_Suppliers " +
                                     "WHERE ProductSupplierId = @ProductSupplierId";
            SqlCommand deleteCommand = new SqlCommand(deleteStatement, con);
            deleteCommand.Parameters.AddWithValue("@ProductSupplierId", prodSupId);
            try {
                con.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            } catch (SqlException ex) { throw ex; }
            finally { con.Close(); }
        }

        // count distinct products in list
        public static int CountProducts() {
            int count = 0;
            SqlConnection con = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT COUNT (DISTINCT ProductId) FROM Products_Suppliers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, con);

            try {
                con.Open();
                count = Convert.ToInt32(selectCommand.ExecuteScalar().ToString());
                return count;
            } catch (SqlException ex) { throw ex; } finally { con.Close(); }
        }

        // count all records
        public static int CountAll() {
            int count = 0;
            SqlConnection con = TravelExpertsDB.GetConnection();

            string selectStatement = "SELECT COUNT (*) FROM Products_Suppliers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, con);

            try {
                con.Open();
                count = Convert.ToInt32(selectCommand.ExecuteScalar().ToString());
                return count;
            } catch (SqlException ex) { throw ex; } finally { con.Close(); }
        }
    }
}
