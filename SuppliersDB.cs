/*
 * Author: Fisaza2
 * Date: July 04, 2017
 * Description: SuppliersDB 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperts
{
    public class SuppliersDB
    {
        // Pull data from Database from Suppliers Table to put it into a list for comboBox
        public static List<Supplier> GetAllSuppliers()
        {
            List<Supplier> supplierIDs = new List<Supplier>();//Empty List
            Supplier nextSupplierID; //for reference

            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupName " +
                                     "FROM Suppliers ";
                                     //"WHERE SupplierId = @SupplierId";
            SqlCommand selectCommand = new SqlCommand(selectStatement, con);
            
            try
            {
                con.Open();
                //nextSupplierID = new Supplier();
                //nextSupplierID.SupplierId = (int)selectCommand.ExecuteScalar();
                //supplierIDs.Add(nextSupplierID);

               
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    nextSupplierID = new Supplier();
                    nextSupplierID.SupName = reader["SupName"].ToString();
                    supplierIDs.Add(nextSupplierID);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return supplierIDs;
        }

        // Get every supplier ID and Name
        public static Supplier GetSupplier(int supplierID)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT SupplierId, SupName " +
                                     "FROM Suppliers " +
                                     "WHERE SupplierId = @SupplierId";

            SqlCommand selectCommand = new SqlCommand(selectStatement, con);
            selectCommand.Parameters.AddWithValue("@SupplierId", supplierID);

            try
            {
                con.Open();
                SqlDataReader Reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (Reader.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.SupplierId = (int)Reader["SupplierId"];
                    supplier.SupName = Reader["SupName"].ToString();
                    return supplier;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        //Method to Add a supplier
        public static void AddSupplier(Supplier supplier)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();

            string insertStatement = "INSERT INTO Suppliers (SupplierId, SupName) " +
                                     "VALUES (@SupplierId, @SupName)";

            SqlCommand addCommand = new SqlCommand(insertStatement, con);
            addCommand.Parameters.AddWithValue("@SupplierId", supplier.SupplierId);
            addCommand.Parameters.AddWithValue("@SupName", supplier.SupName);

            try
            {
                con.Open();
                addCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        // Method to Update Supplier
        public static bool UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();

            string updateStatement = "UPDATE Suppliers " +
                                     "SET SupplierId = @newSupplierId, SupName = @newSupName " +
                                     "WHERE SupplierId = @oldSupplierId";

            SqlCommand updateCommand = new SqlCommand(updateStatement, con);
            updateCommand.Parameters.AddWithValue("@newSupplierId", newSupplier.SupplierId);
            updateCommand.Parameters.AddWithValue("@newSupName", newSupplier.SupName);
            updateCommand.Parameters.AddWithValue("@oldSupplierId", oldSupplier.SupplierId);

            try
            {
                con.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        // Method to Delete Supplier
        public static bool DeleteSupplier(Supplier sup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Suppliers " + 
                                     "WHERE SupplierId = @SupplierId";

            SqlCommand deleteCommand = new SqlCommand(deleteStatement, con);
            deleteCommand.Parameters.AddWithValue("@SupplierId", sup.SupplierId);

            try
            {
                con.Open();
                int count = deleteCommand.ExecuteNonQuery();
                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
