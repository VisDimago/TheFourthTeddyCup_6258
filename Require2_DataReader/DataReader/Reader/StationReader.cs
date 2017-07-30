using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace DataReader
{
    static partial class Reader
    {
        public static bool ReadStation(string path)
        {
            using (SqlConnection sqlcon = new SqlConnection(Settings1.Default.SqlConStr))
            {
                try { sqlcon.Open(); }
                catch (Exception) { return false; }
                using (SqlCommand sqlcmd = new SqlCommand("", sqlcon))
                {
                    sqlcmd.CommandText = "insert into [dbo].stationInfo (Region,Station,InPipe,Mileage) values (@Region,@Station,@InPipe,@Mileage)";
                    using (FileStream fs = new FileStream(@path, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs,Encoding.Default))
                        {
                            string temp=reader.ReadLine();
                            while (( temp = reader.ReadLine() ) != null)
                            {
                                string[] temps = temp.Split(',');
                                sqlcmd.Parameters.AddWithValue("@Region", temps[1]);
                                sqlcmd.Parameters.AddWithValue("@Station", temps[0]);
                                sqlcmd.Parameters.AddWithValue("@InPipe", temps[2]);
                                sqlcmd.Parameters.AddWithValue("@Mileage", temps[3]);
                                sqlcmd.ExecuteNonQuery();
                                sqlcmd.Parameters.Clear();
                            }
                        }
                    }
                }
                sqlcon.Close();
            }
            return true;
        }
    }
}
