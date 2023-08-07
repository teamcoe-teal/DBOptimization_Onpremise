using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace RAWTable_Maintenance_Webjob
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
            try
            {

                Console.WriteLine(connectionstring);
                Console.WriteLine("RAWTable Optimization web job started");
                SqlConnection conn = new SqlConnection(connectionstring);

                //conn.Open();
                if (conn.State != ConnectionState.Closed)
                {
                   
                }
                else
                {
                    conn.Open();
                }
                SqlCommand com;
                //Console.WriteLine("opened");
                com = new SqlCommand("RawTableOptimization", conn);
                com.CommandTimeout = 0;
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.ExecuteNonQuery();
                Console.WriteLine("RAWTable Optimization web job ended");
                conn.Close();
                //Console.ReadKey();
            }
            catch (Exception e)
            {
                //throw e;
               // ExceptionSetting.SendErrorTomail(e, connectionstring);
                Console.WriteLine(DateTime.Today.ToString() + "error occured" + e.ToString());
                //Console.ReadKey();
            }
        }


      
    }
}
