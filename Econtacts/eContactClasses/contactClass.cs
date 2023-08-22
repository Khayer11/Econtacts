using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Econtacts.eContactClasses
{
    class contactClass
    {
        //getter setter properties
        //acts as data carrier in the application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            //step 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writing SQL Query
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();   
            }
            return dt;
        }
        //Inserting Data into Database
        public bool Insert (contactClass c)
        {
            //Creating default return type and setting its value to false
            bool isSuccess = false;

            // Step 1: Connect Database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {

                //Step 2: Create a SQL QUERY to insert Data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                //Creating SQL Command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameter to add data
                cmd.Parameters.AddWithValue("@FirstName",c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                // Connection Open Here
                conn.Open ();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if(rows > 0)
                {
                    isSuccess = true;

                }
                else
                {
                    isSuccess = false;   
                }
            }  
            catch (Exception ex) 
            {

            }
         
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to update data in database from our application
        public bool Update(contactClass c)
        {
            //Creata a default return and set its default to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //SQL to update data to database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID = @ContactID";
                //Creating SQL Command
                SqlCommand cmd = new SqlCommand (sql, conn);
                // Create Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //open Database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greater than zero else its value will be zero
                if(rows > 0 )
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
         

            }


            catch  (Exception ex)
            {

            }
            finally 
            { 
                conn.Close ();
            }

            return isSuccess;
        }
       // Method to Delete Data from database
            public bool Delete (contactClass c)
        {
            // create a default return value and set its value to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // Sql to delete data
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
                
                //Creating SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows is greater than zero else its value is 0
                if (rows > 0 )
                {
                    isSuccess=true;

                }
                else
                {
                    isSuccess =false;
                }
            }
            catch(Exception ex)
            {

            }
            finally { 
                conn.Close (); 
             }
            return isSuccess;
        }
    }
}
