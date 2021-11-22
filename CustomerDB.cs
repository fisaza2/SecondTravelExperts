using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TravelExpertWeb;

namespace TravelExpertWeb.App_Code
{
    [DataObject(true)]
    public class CustomerDB
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static bool CheckUserIfExists(string UserName)
        {
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "SELECT count(*) from Customers where UserName=@UserName";
            SqlCommand cmd = new SqlCommand(selectStatement, conn);
            cmd.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                conn.Open();
                int exists = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (exists >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public static int GetNumberOfPurchasesByClient(int CustomerID)
        {
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "SELECT count(*) from Bookings where CustomerId=@CustomerID";
            SqlCommand cmd = new SqlCommand(selectStatement, conn);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            try
            {
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                return count;
            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }


        //select customer by id
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static bool SelectCustomerByID(int CustomerID)
        {
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "SELECT * from Customers where CustomerID=@CustomerID";
            SqlCommand cmd = new SqlCommand(selectStatement, conn);
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);


            try
            {
                conn.Open();
                int exists = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                if (exists >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (SqlException ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static bool RegisterCustomer(//int CustomerId,
            string CustFirstName, string CustLastName, string CustAddress, string CustCity, string CustProv,
            string CustPostal, string CustCountry, string CustHomePhone, string CustBusPhone,
            string CustEmail, int AgentId, string UserName, string Password)
        {
            Customer newCust = new Customer();
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "INSERT INTO Customers VALUES (" +
            "@CustFirstName, @CustLastName, @CustAddress, @CustCity, @CustProv, " +
            "@CustPostal, @CustCountry, @CustHomePhone, @CustBusPhone, @CustEmail, @AgentId, @UserName, @Password)";

            SqlCommand selectCommand = new SqlCommand(selectStatement, conn);

            newCust.CustFirstName = CustFirstName;
            newCust.CustLastName = CustLastName;
            newCust.CustAddress = CustAddress;
            //etc..

            //selectCommand.Parameters.AddWithValue("@CustomerId", CustomerId);
            selectCommand.Parameters.AddWithValue("@CustFirstName", newCust.CustFirstName);
            selectCommand.Parameters.AddWithValue("@CustLastName", newCust.CustLastName);
            selectCommand.Parameters.AddWithValue("@CustAddress", newCust.CustAddress);
            selectCommand.Parameters.AddWithValue("@CustCity", CustCity);
            selectCommand.Parameters.AddWithValue("@CustProv", CustProv);
            selectCommand.Parameters.AddWithValue("@CustPostal", CustPostal);
            selectCommand.Parameters.AddWithValue("@CustCountry", CustCountry);
            selectCommand.Parameters.AddWithValue("@CustHomePhone", CustHomePhone);
            selectCommand.Parameters.AddWithValue("@CustBusPhone", CustBusPhone);
            selectCommand.Parameters.AddWithValue("@CustEmail", CustEmail);
            selectCommand.Parameters.AddWithValue("@AgentId", AgentId);
            selectCommand.Parameters.AddWithValue("@UserName", UserName);
            selectCommand.Parameters.AddWithValue("@Password", Password);

            try
            {
                conn.Open();
                int count = selectCommand.ExecuteNonQuery();
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
                conn.Close();
            }
        }//end RegisterCustomer()

        public static bool UpdateCustomer(Customer updateCust)
        {
            //Customer newCustomer = new Customer();
            SqlConnection conn = TravelExpertsWebDB.GetConnection();

            string selectStatement = "UPDATE Customers SET " +
            "CustFirstName=@CustFirstName, CustLastName=@CustLastName, CustAddress=@CustAddress, CustCity=@CustCity,CustProv=@CustProv, " +
            "CustPostal=@CustPostal, CustCountry=@CustCountry, CustHomePhone=@CustHomePhone, CustBusPhone=@CustBusPhone, CustEmail=@CustEmail " +
            "WHERE CustomerId=@CustomerId";

            SqlCommand selectCommand = new SqlCommand(selectStatement, conn);

            selectCommand.Parameters.AddWithValue("@CustomerId", updateCust.CustomerId);
            selectCommand.Parameters.AddWithValue("@CustFirstName", updateCust.CustFirstName);
            selectCommand.Parameters.AddWithValue("@CustLastName", updateCust.CustLastName);
            selectCommand.Parameters.AddWithValue("@CustAddress", updateCust.CustAddress);
            selectCommand.Parameters.AddWithValue("@CustCity", updateCust.CustCity);
            selectCommand.Parameters.AddWithValue("@CustProv", updateCust.CustProv);
            selectCommand.Parameters.AddWithValue("@CustPostal", updateCust.CustPostal);
            selectCommand.Parameters.AddWithValue("@CustCountry", updateCust.CustCountry);
            selectCommand.Parameters.AddWithValue("@CustHomePhone", updateCust.CustHomePhone);
            selectCommand.Parameters.AddWithValue("@CustBusPhone", updateCust.CustBusPhone);
            selectCommand.Parameters.AddWithValue("@CustEmail", updateCust.CustEmail);

            try
            {
                conn.Open();
                int count = selectCommand.ExecuteNonQuery();
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
                conn.Close();
            }
        }//end RegisterCustomer()

        [DataObjectMethod(DataObjectMethodType.Select)]

        public static Customer GetCustomerInfo(int CustomerID)
        {
            Customer custInfo = null;
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "SELECT CustomerId, CustFirstName, CustLastName, CustAddress, CustCity, CustProv, " +
            "CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail " +
            "FROM Customers WHERE CustomerId=@CustomerID";

            SqlCommand selectCommand = new SqlCommand(selectStatement, conn);
            selectCommand.Parameters.AddWithValue("@CustomerID", CustomerID);

            try
            {
                conn.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())// process next row
                {
                    custInfo = new Customer();

                    custInfo.CustomerId = (int)reader["CustomerId"];
                    custInfo.CustFirstName = reader["CustFirstName"].ToString();
                    custInfo.CustLastName = reader["CustLastName"].ToString();
                    custInfo.CustAddress = reader["CustAddress"].ToString();
                    custInfo.CustCity = reader["CustCity"].ToString();
                    custInfo.CustProv = reader["CustProv"].ToString();
                    custInfo.CustPostal = reader["CustPostal"].ToString();
                    custInfo.CustCountry = reader["CustCountry"].ToString();
                    custInfo.CustHomePhone = reader["CustHomePhone"].ToString();
                    custInfo.CustBusPhone = reader["CustBusPhone"].ToString();
                    custInfo.CustEmail = reader["CustEmail"].ToString();
                    //custInfo.UserName = reader["UserName"].ToString();
                    //custInfo.Password = reader["Password"].ToString
                }
                return custInfo;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            //returning master list
        }//end RegisterCustomer()


        //login method
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Customer CustomerLogin(string UserName, string UserPass)
        {
            Customer custInfo = new Customer();

            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = "SELECT CustomerId, Password from Customers where UserName=@UserName";
            SqlCommand cmdPass = new SqlCommand(selectStatement, conn);
            cmdPass.Parameters.AddWithValue("@UserName", UserName);
            //cmdPass.Parameters.AddWithValue("@UserPass", UserPass);

            try
            {
                conn.Open();
                SqlDataReader reader = cmdPass.ExecuteReader();
                if (reader.Read())// process next row
                {
                    //custInfo.CustomerId = (int)reader["CustomerId"];
                    if (UserPass == reader["Password"].ToString())
                    {
                        custInfo.CustomerId = (int)reader["CustomerId"];
                    }
                }
                return custInfo;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }//end login section

        //setting up GetPurchasedPackagesCustomer() method ***************FABIAN EDITED 2017-07-18
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<CustomerPurchasedPackages> GetPurchasedPackagesCustomer(int CustomerId)
        {
            //creating master list to store values from DB
            List<CustomerPurchasedPackages> packageList = new List<CustomerPurchasedPackages>();

            //setting up connections and select query
            SqlConnection conn = TravelExpertsWebDB.GetConnection();
            string selectStatement = @"SELECT b.CustomerId, b.PackageId, b.BookingId, b.BookingDate, d.Destination, b.TravelerCount, d.BasePrice  " +
                                     "FROM Bookings b " +
                                     "INNER JOIN BookingDetails d ON d.BookingId = b.BookingId " +
                                     "WHERE b.CustomerId=@CustomerId " +
                                     "ORDER BY BookingDate";

            SqlCommand selectCmd = new SqlCommand(selectStatement, conn);

            //swapping values 
            selectCmd.Parameters.AddWithValue("@CustomerId", CustomerId);

            try
            {
                conn.Open();
                SqlDataReader reader = selectCmd.ExecuteReader();
                while (reader.Read())// process next row
                {
                    CustomerPurchasedPackages packList = new CustomerPurchasedPackages();

                    packList.CustomerId = (int)reader["CustomerId"];
                    packList.PackageId = (int)reader["PackageId"];
                    packList.BookingId = (int)reader["BookingId"];
                    packList.BookingDate = (DateTime)reader["BookingDate"];
                    packList.Destination = reader["Destination"].ToString();
                    packList.TravelerCount = (float)Convert.ChangeType(reader["TravelerCount"], typeof(float));
                    packList.BasePrice = (decimal)reader["BasePrice"];

                    packList.Total_Package = packList.BasePrice * (decimal)packList.TravelerCount;
                    packageList.Add(packList);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            //returning master list
            return packageList;
        }
    }
}