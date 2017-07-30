using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DataReader
{
    static partial class DBReader
    {
        public static bool DownloadFlowInfo(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    Console.WriteLine("开始下载{0}到{1}的数据", list[i].Trim(), list[j].Trim());
                    if (!DownloadFlowInfoS2S(list[i].Trim(), list[j].Trim()))
                    {
                        Console.WriteLine("数据下载过程出错");
                        return false;
                    }

                }
            }
            return true;
        }

        static bool DownloadFlowInfoS2S(string Aboard, string Debus)
        {

            SqlDataReader reader;
            SqlConnection sqlcon = new SqlConnection(Settings1.Default.SqlConStr);

            try
            {
                sqlcon.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("无法连接数据库!");
                return false;
            }

            string SqlTextAboard = string.Empty;
            string SqlTextDebus = string.Empty;



            if (Aboard == "管外") SqlTextAboard = "in (select Station from stationInfo where InPipe='否') ";
            else SqlTextAboard = "in ( select Station from stationInfo where LEFT(Station, 5) = " + CheckStation(Aboard, sqlcon) + ")";
            if (Debus == "管外") SqlTextAboard = "in (select Station from stationInfo where InPipe='否') ";
            else SqlTextDebus = "in ( select Station from stationInfo where LEFT(Station, 5) = " + CheckStation(Debus, sqlcon) + ")";

            using (SqlCommand sqlcmd = new SqlCommand(string.Format("use Railway select  RecordDate,sum(FlowCount) as Flow from flowInfo where AboardStation {0} and DebusStation {1} group by RecordDate order by RecordDate", SqlTextAboard, SqlTextDebus), sqlcon))
            {
                reader = sqlcmd.ExecuteReader();
            }

            if (!Directory.Exists(Settings1.Default.@TempPath + @"StationFlowInfo\"))
            {
                Directory.CreateDirectory(Settings1.Default.@TempPath + @"StationFlowInfo\");
            }

            FileStream fs = new FileStream(Settings1.Default.@TempPath + @"StationFlowInfo\" + Aboard + "至" + Debus + ".csv", FileMode.Create, FileAccess.Write);
            StreamWriter Writer = new StreamWriter(fs, Encoding.Default);


            Writer.WriteLine("Date,Count");
            int counter = 0;
            while (reader.Read())
            {
                counter++;
                Writer.WriteLine(reader[0].ToString().Split(' ')[0] + "," + reader[1]);
            }
            Console.WriteLine("{0}到{1}共有{2}条记录！", Aboard, Debus, counter.ToString());


            Writer.Close();
            fs.Close();
            Writer.Dispose();
            fs.Dispose();

            return true;
        }

        private static string CheckStation(string Station, SqlConnection sqlcon)
        {
            using (SqlCommand sqlcmd = new SqlCommand("select Station from StationInfo where Station = @station", sqlcon))
            {
                sqlcmd.Parameters.AddWithValue("@station", Station.Substring(0, 5));
                if (sqlcmd.ExecuteNonQuery() != 0) return "'" + Station.Substring(0, 5) + "'";
                else throw new Exception("数据库中不存在此车站！");
            }
        }
    }
}
