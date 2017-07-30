using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace DataReader
{
    static partial class DataFormer
    {
        public static bool ForecastData(string FilePath)
        {
            DataTable[] dt = new DataTable[14];
            for (int d = 0; d < 14; d++)
            {
                dt[d] = new DataTable();
                for (int i = 0; i < 14; i++)
                {
                    dt[d].Columns.Add();
                    dt[d].Rows.Add();
                }
            }
            List<string> Station = new List<string> { "管外", "ZD111", "ZD311", "ZD326", "ZD192", "ZD022", "ZD250", "ZD062", "ZD120", "ZD121", "ZD143", "ZD370", "ZD190" };
            FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs, Encoding.Default);
            string temp;
            while (( temp = reader.ReadLine() ) != null)
            {
                int AboardStation = Station.IndexOf(temp.Split(',')[0].Trim());
                int DebusStation = Station.IndexOf(temp.Split(',')[1].Trim());
                for (int i = 0; i < 14; i++)
                {
                    dt[i].Rows[DebusStation][AboardStation] = int.Parse(reader.ReadLine().Trim());
                }
            }
            reader.Close();
            reader.Dispose();
            fs.Close();
            fs.Dispose();

            fs = new FileStream(Settings1.Default.@TempPath + "未来14天客流预测.csv", FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs, Encoding.Default);
            DateTime date = DateTime.Parse("2016/03/21");
            for (int d = 0; d < 14; d++)
            {
                writer.WriteLine("日期：{0}", date.ToShortDateString());
                writer.WriteLine(",上车站,管外,ZD111,ZD311,ZD326,ZD192,ZD022,ZD250,ZD062,ZD120,ZD121,ZD143,ZD370,ZD190,下车人数合计");
                writer.WriteLine("下车站,,,,,,,,,,,,,,,");


                for (int i = 0; i < Station.Count; i++)
                {
                    writer.Write(Station[i] + ",,");
                    int sum = 0;
                    for (int j = 0; j < Station.Count; j++)
                    {
                        if (dt[d].Rows[i][j].ToString() != "")
                        {
                            int count = int.Parse(dt[d].Rows[i][j].ToString());
                            sum += count;
                            writer.Write(count);
                            writer.Write(",");
                        }
                        else writer.Write(",");
                    }
                    writer.WriteLine(sum);
                }


                writer.Write("上车人数合计,,");
                int AllCount = 0;
                for (int i = 0; i < Station.Count; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < Station.Count; j++)
                        if (dt[d].Rows[j][i].ToString()!="")
                        {
                            sum += int.Parse(dt[d].Rows[j][i].ToString());
                        }
                    AllCount += sum;
                    writer.Write(sum);
                    writer.Write(",");
                }
                writer.WriteLine(AllCount);
                writer.WriteLine();
                writer.WriteLine();
                writer.WriteLine();
                date = date.AddDays(1);
            }

            writer.Close();
            writer.Dispose();

            return true;
        }
    }
}
