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
        public static bool UploadTrainInfo()
        {
            using (SqlConnection sqlcon = new SqlConnection(Settings1.Default.SqlConStr))
            {
                try { sqlcon.Open(); }
                catch (Exception) { return false; }
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    sqlcmd.Connection = sqlcon;

                    #region train.csv
                    using (FileStream fs = new FileStream(Settings1.Default.@TempPath + "train.csv", FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                        {
                            string temp = reader.ReadLine();

                            while (( temp = reader.ReadLine() ) != null)
                            {

                                string[] temps = temp.Split(',');
                                Decimal d;
                                bool check = Decimal.TryParse(temps[5], out d);
                                sqlcmd.CommandText = "insert into [dbo].trainRecord (TrainNum,RecordDate,OriginStation,TerminalStation,MaxCount";
                                if (check) sqlcmd.CommandText += ",LoadFactors";
                                sqlcmd.CommandText += ") values (@TrainNum,@RecordDate,@FromStation,@ArrStation,@PersonQuota";
                                  if (check) sqlcmd.CommandText += "  ,@LoadFactors";
                                sqlcmd.CommandText += ")";
                                sqlcmd.Parameters.AddWithValue("@TrainNum", temps[0]);
                                sqlcmd.Parameters.AddWithValue("@RecordDate", temps[1]);
                                sqlcmd.Parameters.AddWithValue("@FromStation", temps[2]);
                                sqlcmd.Parameters.AddWithValue("@ArrStation", temps[3]);
                                sqlcmd.Parameters.AddWithValue("@PersonQuota", temps[4]);
                                if (check) sqlcmd.Parameters.AddWithValue("@LoadFactors", Decimal.Parse(temps[5]));
                                sqlcmd.ExecuteNonQuery();
                                sqlcmd.Parameters.Clear();

                            }
                        }
                    }
                    #endregion

                    #region station.csv
                    using (FileStream fs = new FileStream(Settings1.Default.@TempPath + "station.csv", FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                        {
                            string temp = reader.ReadLine();

                            while (( temp = reader.ReadLine() ) != null)
                            {
                                sqlcmd.CommandText = "insert into [dbo].stationRecord (RecordDate,TrainNum,Station";

                                string[] temps = temp.Split(',');
                                DateTime t1, t2;
                                bool b1 = DateTime.TryParse(temps[3], out t1);
                                bool b2 = DateTime.TryParse(temps[4], out t2);
                                if (b1) sqlcmd.CommandText += ",ArriveTime";
                                if (b2) sqlcmd.CommandText += ",DepartsTime";

                                sqlcmd.CommandText += ") values (@RecordDate,@TrainNum,@CurSite";

                                if (b1) sqlcmd.CommandText += ",@ArrStaTime";
                                if (b2) sqlcmd.CommandText += ",@DepartsTime";

                                sqlcmd.CommandText += ")";


                                sqlcmd.Parameters.AddWithValue("@RecordDate", temps[0]);
                                sqlcmd.Parameters.AddWithValue("@TrainNum", temps[1]);
                                sqlcmd.Parameters.AddWithValue("@CurSite", temps[2]);
                                if (b1) sqlcmd.Parameters.AddWithValue("@ArrStaTime", t1);
                                if (b2) sqlcmd.Parameters.AddWithValue("@DepartsTime", t2);
                                sqlcmd.ExecuteNonQuery();
                                sqlcmd.Parameters.Clear();

                            }
                        }
                    }
                    #endregion


                    #region count.csv
                    using (FileStream fs = new FileStream(Settings1.Default.@TempPath + "count.csv", FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                        {
                            string temp = reader.ReadLine();
                            sqlcmd.CommandText = "insert into [dbo].flowInfo (" + temp + ") values (@RecordDate,@TrainNum,@AboardStation,@DebusStation,@StaPeoNum)";
                            while (( temp = reader.ReadLine() ) != null)
                            {
                                string[] temps = temp.Split(',');
                                sqlcmd.Parameters.AddWithValue("@RecordDate", temps[0]);
                                sqlcmd.Parameters.AddWithValue("@TrainNum", temps[1]);
                                sqlcmd.Parameters.AddWithValue("@AboardStation", temps[2]);
                                sqlcmd.Parameters.AddWithValue("@DebusStation", temps[3]);
                                sqlcmd.Parameters.AddWithValue("@StaPeoNum", temps[4]);
                                sqlcmd.ExecuteNonQuery();
                                sqlcmd.Parameters.Clear();

                            }
                        }
                    }
                    #endregion

                }
                sqlcon.Close();
            }
            return true;
        }
    }
}
