using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectTemplate{

	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]

	public class ProjectServices : System.Web.Services.WebService{
		////////////////////////////////////////////////////////////////////////
		///replace the values of these variables with your database credentials
		////////////////////////////////////////////////////////////////////////
		private string dbID = "fall2023team4";
		private string dbPass = "fall2023team4";
		private string dbName = "fall2023team4";
		////////////////////////////////////////////////////////////////////////
		
		////////////////////////////////////////////////////////////////////////
		///call this method anywhere that you need the connection string!
		////////////////////////////////////////////////////////////////////////
		private string getConString(){
			return "SERVER=107.180.1.16; PORT=3306; DATABASE=" + dbName+"; UID=" + dbID + "; PASSWORD=" + dbPass;
		}
        ////////////////////////////////////////////////////////////////////////
        //Section for all Web Methods
       

        /////////////////////////////////////////////////////////////////////////
        //don't forget to include this decoration above each method that you want
        //to be exposed as a web service!
        [WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string TestConnection(){
			try{
				string testQuery = "select * from Employees";

				/////////////////////////////////////////////////////////////////
				///here's an example of using the getConString method!
				/////////////////////////////////////////////////////////////////
				MySqlConnection con = new MySqlConnection(getConString());
				////////////////////////////////////////////////////////////////

				MySqlCommand cmd = new MySqlCommand(testQuery, con);
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable table = new DataTable();
				adapter.Fill(table);
				return "Success! " + dbID;
			}
			catch (Exception e){
				return "Something went wrong, please check your credentials and db name and try again.  Error: "+e.Message;
			}
		}
        ///////////////////////////////////////////////////////////////////////
        // log in / log out
        //EXAMPLE OF A SIMPLE SELECT QUERY (PARAMETERS PASSED IN FROM CLIENT)

        // Employee/Admin
        [WebMethod(EnableSession = true)] //NOTICE: gotta enable session on each individual method
        public bool LogOnEmployees(string uid, string pass){
            //we return this flag to tell them if they logged in or not
            bool success = false;

            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            //NOTICE: we added admin to what we pull, so that we can store it along with the id in the session
            string sqlSelect = "SELECT employee_name FROM Employees WHERE employee_username=@idValue and employee_password=@passValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(uid));
            sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(pass));

            //a data adapter acts like a bridge between our command object and 
            //the data we are trying to get back and put in a table object
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            //here's the table we want to fill with the results from our query
            DataTable sqlDt = new DataTable();
            //here we go filling it!
            sqlDa.Fill(sqlDt);
            //check to see if any rows were returned.  If they were, it means it's 
            //a legit account
            if (sqlDt.Rows.Count > 0){
                //if we found an account, store the id and admin status in the session
                //so we can check those values later on other method calls to see if they 
                //are 1) logged in at all, and 2) and admin or not
                Session["employee_name"] = sqlDt.Rows[0]["employee_name"];
                success = true;
            }
            //return the result!
            return success;
        }

        // Customer
        [WebMethod(EnableSession = true)] //NOTICE: gotta enable session on each individual method
        public bool LogOnCustomers(string uid, string pass){
            //we return this flag to tell them if they logged in or not
            bool success = false;

            //our connection string comes from our web.config file like we talked about earlier
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //here's our query.  A basic select with nothing fancy.  Note the parameters that begin with @
            //NOTICE: we added admin to what we pull, so that we can store it along with the id in the session
            string sqlSelect = "SELECT Customer_name FROM Customers WHERE Customer_username=@idValue and Customer_password=@passValue";

            //set up our connection object to be ready to use our connection string
            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            //set up our command object to use our connection, and our query
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            //tell our command to replace the @parameters with real values
            //we decode them because they came to us via the web so they were encoded
            //for transmission (funky characters escaped, mostly)
            sqlCommand.Parameters.AddWithValue("@idValue", HttpUtility.UrlDecode(uid));
            sqlCommand.Parameters.AddWithValue("@passValue", HttpUtility.UrlDecode(pass));

            //a data adapter acts like a bridge between our command object and 
            //the data we are trying to get back and put in a table object
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
            //here's the table we want to fill with the results from our query
            DataTable sqlDt = new DataTable();
            //here we go filling it!
            sqlDa.Fill(sqlDt);
            //check to see if any rows were returned.  If they were, it means it's 
            //a legit account
            if (sqlDt.Rows.Count > 0){
                //if we found an account, store the id and admin status in the session
                //so we can check those values later on other method calls to see if they 
                //are 1) logged in at all, and 2) and admin or not
                Session["Customer_name"] = sqlDt.Rows[0]["Customer_name"];
                success = true;
            }
            //return the result!
            return success;
        }

        [WebMethod(EnableSession = true)]
        public bool LogOff(){
            //if they log off, then we remove the session.  That way, if they access
            //again later they have to log back on in order for their ID to be back
            //in the session!
            Session.Abandon();
            return true;
        }

        //End of login/loggoff Methods
        /////////////////////////////////////////////////////////////////////////

        //Request Account Employee
        //EXAMPLE OF AN INSERT QUERY WITH PARAMS PASSED IN.  BONUS GETTING THE INSERTED ID FROM THE DB!
        [WebMethod(EnableSession = true)]
        public void RequestAccountEmployee( string employeeName, string employeeUsername, string employeePassword){
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //the only thing fancy about this query is SELECT LAST_INSERT_ID() at the end.  All that
            //does is tell mySql server to return the primary key of the last inserted row.
            string sqlSelect = "insert into Employees ( employee_name, employee_username, employee_password) " +
                "values(@employee_name, @employee_username, @employee_password); SELECT LAST_INSERT_ID();";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            //sqlCommand.Parameters.AddWithValue("@employee_id", HttpUtility.UrlDecode(employeeId));
            sqlCommand.Parameters.AddWithValue("@employee_name", HttpUtility.UrlDecode(employeeName));
            sqlCommand.Parameters.AddWithValue("@employee_username", HttpUtility.UrlDecode(employeeUsername));
            sqlCommand.Parameters.AddWithValue("@employee_password", HttpUtility.UrlDecode(employeePassword));
        

            //this time, we're not using a data adapter to fill a data table.  We're just
            //opening the connection, telling our command to "executescalar" which says basically
            //execute the query and just hand me back the number the query returns (the ID, remember?).
            //don't forget to close the connection!
            sqlConnection.Open();
            //we're using a try/catch so that if the query errors out we can handle it gracefully
            //by closing the connection and moving on
            try{
                int accountID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                //here, you could use this accountID for additional queries regarding
                //the requested account.  Really this is just an example to show you
                //a query where you get the primary key of the inserted row back from
                //the database!
            }
            catch (Exception e){
            }
            sqlConnection.Close();
        }

        // Request Account Customer
        //EXAMPLE OF AN INSERT QUERY WITH PARAMS PASSED IN.  BONUS GETTING THE INSERTED ID FROM THE DB!
        [WebMethod(EnableSession = true)]
        public void RequestAccountCustomer( string customerName, string customerUsername, string customerPasswordd, string customerPhone, string customerEmail
            , string customerStreet, string customerState, string customerZip){
            string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            //the only thing fancy about this query is SELECT LAST_INSERT_ID() at the end.  All that
            //does is tell mySql server to return the primary key of the last inserted row.
            string sqlSelect = "insert into Customers (customer_name, customer_username, customer_password, customer_phone, " +
                "customer_email, customer_address, customer_address_state, customer_zip) " +
                "values(@customer_name, @customer_username, @customer_password, @customer_phone, @customer_email" +
                ", @Customer_address, @customer_address_state, @customer_zip); SELECT LAST_INSERT_ID();";

            MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
            MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@customer_name", HttpUtility.UrlDecode(customerName));
            sqlCommand.Parameters.AddWithValue("@customer_username", HttpUtility.UrlDecode(customerUsername));
            sqlCommand.Parameters.AddWithValue("@customer_password", HttpUtility.UrlDecode(customerPasswordd));
            sqlCommand.Parameters.AddWithValue("@customer_phone", HttpUtility.UrlDecode(customerPhone));
            sqlCommand.Parameters.AddWithValue("@customer_email", HttpUtility.UrlDecode(customerEmail));
            sqlCommand.Parameters.AddWithValue("@Customer_address", HttpUtility.UrlDecode(customerStreet));
            sqlCommand.Parameters.AddWithValue("@customer_address_state", HttpUtility.UrlDecode(customerState));
            sqlCommand.Parameters.AddWithValue("@customer_zip", HttpUtility.UrlDecode(customerZip));
 

            //this time, we're not using a data adapter to fill a data table.  We're just
            //opening the connection, telling our command to "executescalar" which says basically
            //execute the query and just hand me back the number the query returns (the ID, remember?).
            //don't forget to close the connection!
            sqlConnection.Open();
            //we're using a try/catch so that if the query errors out we can handle it gracefully
            //by closing the connection and moving on
            try{
                int accountID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                //here, you could use this accountID for additional queries regarding
                //the requested account.  Really this is just an example to show you
                //a query where you get the primary key of the inserted row back from
                //the database!
            }
            catch (Exception e){
            }
            sqlConnection.Close();
        }
        // End of Account Web Methods... Login, logout, request account Emp/Cust...
        /////////////////////////////////////////////////////////////////////////








        [WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string GetCustID(){
			try{
                //here we are grabbing that connection string from our web.config file
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
				//here's our query.  A basic select with nothing fancy.
				string sqlSelect = "SELECT customer_id, customer_name FROM Customers WHERE customer_name=\"customer_name\";";



                //set up our connection object to be ready to use our connection string
                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                //set up our command object to use our connection, and our query
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);


                //a data adapter acts like a bridge between our command object and 
                //the data we are trying to get back and put in a table object
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //here's the table we want to fill with the results from our query
                DataTable sqlDt = new DataTable();
                //here we go filling it!
                sqlDa.Fill(sqlDt);
                //return the number of rows we have, that's how many accounts are in the system!
                return sqlDt.Rows.Count.ToString();
            }
			catch (Exception e){
				return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
			}
		}

        /////////////////////////////////////////////////////////////////////////
        
        /////////////////////////////////////////////////////////////////////////
        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string GetOrdID(){
            try{
                //here we are grabbing that connection string from our web.config file
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //here's our query.  A basic select with nothing fancy.
                string sqlSelect = "SELECT order_id FROM Orders\r\nORDER BY id DESC\r\nLIMIT 1;";



                //set up our connection object to be ready to use our connection string
                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                //set up our command object to use our connection, and our query
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);


                //a data adapter acts like a bridge between our command object and 
                //the data we are trying to get back and put in a table object
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //here's the table we want to fill with the results from our query
                DataTable sqlDt = new DataTable();
                //here we go filling it!
                sqlDa.Fill(sqlDt);
                //return the number of rows we have, that's how many accounts are in the system!
                return sqlDt.Rows.Count.ToString();
            }
            catch (Exception e){
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        //////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string GetProdID(){
            try{
                //here we are grabbing that connection string from our web.config file
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //here's our query.  A basic select with nothing fancy.
                string sqlSelect = "SELECT product_id, product_name FROM Products WHERE product_name=\"productNameGoesHere\";";



                //set up our connection object to be ready to use our connection string
                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                //set up our command object to use our connection, and our query
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);


                //a data adapter acts like a bridge between our command object and 
                //the data we are trying to get back and put in a table object
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //here's the table we want to fill with the results from our query
                DataTable sqlDt = new DataTable();
                //here we go filling it!
                sqlDa.Fill(sqlDt);
                //return the number of rows we have, that's how many accounts are in the system!
                return sqlDt.Rows.Count.ToString();
            }
            catch (Exception e){
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string InsertOrder(){
            try{
                //here we are grabbing that connection string from our web.config file
                string sqlConnectString = System.Configuration.ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                //here's our query.  A basic select with nothing fancy.
                string sqlSelect = "INSERT INTO Orders (customer_id, order_id, product_id, order_amount, order_date)\r\nVALUES (value1, value2, ...);";



                //set up our connection object to be ready to use our connection string
                MySqlConnection sqlConnection = new MySqlConnection(sqlConnectString);
                //set up our command object to use our connection, and our query
                MySqlCommand sqlCommand = new MySqlCommand(sqlSelect, sqlConnection);


                //a data adapter acts like a bridge between our command object and 
                //the data we are trying to get back and put in a table object
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(sqlCommand);
                //here's the table we want to fill with the results from our query
                DataTable sqlDt = new DataTable();
                //here we go filling it!
                sqlDa.Fill(sqlDt);
                //return the number of rows we have, that's how many accounts are in the system!
                return sqlDt.Rows.Count.ToString();
            }
            catch (Exception e){
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////

    }
}
