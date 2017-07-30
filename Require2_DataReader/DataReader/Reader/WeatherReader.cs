using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;

namespace DataReader
{
    static partial class Reader
    {
        public static bool ReadWeather(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                {
                    using (SqlConnection sqlcon = new SqlConnection(Settings1.Default.SqlConStr))
                    {
                        try { sqlcon.Open(); }
                        catch (Exception) { return false; }
                        string buffer;
                        string[] dataArray;
                        buffer = reader.ReadLine();
                        while (( buffer = reader.ReadLine() ) != null)
                        {
                            dataArray = buffer.Split(',', '/', '℃', '级', '年', '月', '日', '风');
                            using (SqlCommand sqlcmd = new SqlCommand())
                            {
                                sqlcmd.CommandText = "insert into [dbo].weather (Region,RecordDate,WeatherStart,WeatherChange,TemperatureLowest,TemperatureHighest,WindDirectStart,WindDirectChange,WindPowerStart,WindPowerChange) values (@Region,@RecordDate,@WeatherStart,@WeatherChange,@TairLowest,@TairHighest,@WindDirectStart,@WindDirectChange,@WindPowerStart,@WindPowerChange)";
                                sqlcmd.Connection = sqlcon;
                                sqlcmd.Parameters.AddWithValue("@Region", dataArray[16]);
                                sqlcmd.Parameters.AddWithValue("@RecordDate", dataArray[0] + "-" + dataArray[1] + "-" + dataArray[2]);
                                sqlcmd.Parameters.AddWithValue("@WeatherStart", dataArray[4]);
                                sqlcmd.Parameters.AddWithValue("@WeatherChange", dataArray[5]);
                                sqlcmd.Parameters.AddWithValue("@TairLowest", dataArray[6]);
                                sqlcmd.Parameters.AddWithValue("@TairHighest", dataArray[8]);
                                sqlcmd.Parameters.AddWithValue("@WindDirectStart", dataArray[10]);
                                sqlcmd.Parameters.AddWithValue("@WindDirectChange", dataArray[13]);
                                sqlcmd.Parameters.AddWithValue("@WindPowerStart", dataArray[11]);
                                sqlcmd.Parameters.AddWithValue("@WindPowerChange", dataArray[14]);
                                sqlcmd.ExecuteNonQuery();
                            }
                        }
                        sqlcon.Close();
                    }
                }
            }
            return true;
        }

    }
}
