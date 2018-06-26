using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoHW56_SQL_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customers> customersList = new List<Customers>();
            string myDataConnctionString = "Data Source=devps2010;Initial Catalog=northwind;Integrated Security=True";
            SqlConnection mySqlConnection = new SqlConnection(myDataConnctionString);
            try
            {
                mySqlConnection.Open();
                Console.WriteLine("the database is open");
                SqlCommand myQuery = new SqlCommand("SELECT c.CustomerID , c.CompanyName, c.Address, c.City, c.Region, c.Country FROM Customers as c", mySqlConnection);
                SqlDataReader DataReader = myQuery.ExecuteReader();


                while (DataReader.Read())
                {
                    customersList.Add(new Customers() { CustomerID=getSafeString(DataReader[0]), CompanyName=getSafeString(DataReader[1]), Address=getSafeString(DataReader[2]), City=getSafeString(DataReader[3]),Region=getSafeString(DataReader[4]),Country=getSafeString(DataReader[5])});
                }

               

                Console.WriteLine("Company Name With A or a");
                Console.WriteLine("---------------------------");

                Customers[] myCustomersArray = customersList.Where(s => s.CompanyName.ToLower().Contains("a")).ToArray();



                foreach (Customers item in myCustomersArray)
                {                            

                    Console.WriteLine(item.CompanyName);
                }

                Console.WriteLine();
                Console.WriteLine($"Total Customers from the DB is {customersList.Count}");
                Console.WriteLine();
                Console.WriteLine($"Total Customers from the DB conatinig A or a in Company name is {myCustomersArray.Length}");

            }
            catch (Exception e)
            {
                Console.WriteLine("Im in Trouble");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }
            finally
            {
                mySqlConnection.Dispose();
            }


            Console.ReadLine();
        }

        /// *********** Method *************** ///////

        static string getSafeString(dynamic dbVal) //מקבל לפונקציה משתנה דינאמי ויחזיר סטרינג 
        {
            if (dbVal == null) return ""; // תחזיר סטרינג ריק NULL אם מה שמתקבל שווה  
            return dbVal.ToString(); // אחרת תחזיר את האובייקט לסטרינג
        }
    }
}




