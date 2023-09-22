using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectTemplate
{
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	[System.Web.Script.Services.ScriptService]

	public class ProjectServices : System.Web.Services.WebService
	{
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
		private string getConString() {
			return "SERVER=107.180.1.16; PORT=3306; DATABASE=" + dbName+"; UID=" + dbID + "; PASSWORD=" + dbPass;
		}
		////////////////////////////////////////////////////////////////////////



		/////////////////////////////////////////////////////////////////////////
		//don't forget to include this decoration above each method that you want
		//to be exposed as a web service!
		[WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string TestConnection()
		{
			try
			{
				string testQuery = "select * from Employees";

				////////////////////////////////////////////////////////////////////////
				///here's an example of using the getConString method!
				////////////////////////////////////////////////////////////////////////
				MySqlConnection con = new MySqlConnection(getConString());
				////////////////////////////////////////////////////////////////////////

				MySqlCommand cmd = new MySqlCommand(testQuery, con);
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable table = new DataTable();
				adapter.Fill(table);
				return "Success! " + dbID;
			}
			catch (Exception e)
			{
				return "Something went wrong, please check your credentials and db name and try again.  Error: "+e.Message;
			}
		}
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		[WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string GetCustID()
		{
			try
			{
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
			catch (Exception e)
			{
				return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
			}
		}

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string GetOrdID()
        {
            try
            {
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
            catch (Exception e)
            {
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string GetProdID()
        {
            try
            {
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
            catch (Exception e)
            {
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string InsertOrder()
        {
            try
            {
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
            catch (Exception e)
            {
                return "Something went wrong, please check your credentials and db name and try again.  Error: " + e.Message;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
