using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Collections;

namespace DataReader
{
    static partial class Reader
    {
        private static void GetDayInfo(string file)
        {

            #region  文件类型调整
            string Path;
            if (file.Contains(".xls"))
            {
                xls2csv(file);
                Path = Settings1.Default.@TempPath + "temp.csv";
            }
            else
            {
                Path = file;
            }
            #endregion

            #region 文件流的打开
            FileStream InStream = new FileStream(@Path, FileMode.Open, FileAccess.Read);
            FileStream StationOutStream = new FileStream(Settings1.Default.@TempPath + "station.csv", FileMode.Append, FileAccess.Write);
            FileStream CountOutStream = new FileStream(Settings1.Default.@TempPath + "count.csv", FileMode.Append, FileAccess.Write);
            FileStream TrainOutStream = new FileStream(Settings1.Default.@TempPath + "train.csv", FileMode.Append, FileAccess.Write);

            StreamReader reader = new StreamReader(InStream, Encoding.Default);
            StreamWriter StationWriter = new StreamWriter(StationOutStream, Encoding.Default);
            StreamWriter CountWriter = new StreamWriter(CountOutStream, Encoding.Default);
            StreamWriter TrainWriter = new StreamWriter(TrainOutStream, Encoding.Default);
            #endregion



            List<string[]> list = new List<string[]>();
            string temp;
            if (reader.ReadLine().Contains("旅客列车梯形密度表（ 日均 ）"))
            {
                string[] DateInfo = reader.ReadLine().Split(',', ' ', '—');
                //string DateBegin = DateInfo[3];
                //string DateEnd = DateInfo[4];
                string Date = DateInfo[3];
                Date = Date.Insert(6, "-");
                Date = Date.Insert(4, "-");
                System.Console.Write("正在处理{0}的数据！",Date);
                while (( temp = reader.ReadLine() ) != null)
                {
                    if (temp.Contains("客座率"))
                    {
                        //列车运行信息提取
                        string[] TrainInfo = temp.Split(' ', '—', '%', ',', '：');
                        TrainWriter.WriteLine(TrainInfo[0] + "," + Date + "," + TrainInfo[1] + "," + TrainInfo[2] + "," + TrainInfo[8] + "," + TrainInfo[11]);

                        //列车客流信息提取
                        list.Clear();
                        while (!( temp = reader.ReadLine() ).Contains("上车人数合计"))
                        {
                            string[] array = temp.Split(',');
                            list.Add(array);
                        }

                        //站点信息输出
                        HashSet<string> set = new HashSet<string>();
                        Dictionary<string, DateTime> arr = new Dictionary<string, DateTime>();
                        Dictionary<string, DateTime> left = new Dictionary<string, DateTime>();


                        DateTime Datetemp = DateTime.Parse(Date);

                        DateTime dt = DateTime.MinValue;
                        for (int j = 2; !list[0][j].Contains("下车人数合计"); j++)
                        {
                            set.Add(list[0][j]);
                            DateTime time = DateTime.Parse(Datetemp.Date.ToShortDateString() + " " + list[1][j]);
                            if (time < dt)
                            {
                                time = time.AddDays(1);
                                Datetemp = Datetemp.AddDays(1);
                            }
                            left.Add(list[0][j], time);
                            dt = time;
                        }
                        Datetemp = DateTime.Parse(Date);
                        dt = DateTime.MinValue;
                        for (int i = 3; i < list.Count; i++)
                        {
                            set.Add(list[i][0]);
                            DateTime time = DateTime.Parse(Datetemp.Date.ToShortDateString() + " " + list[i][1]);
                            if (time < dt)
                            {
                                time = time.AddDays(1);
                                Datetemp = Datetemp.AddDays(1);
                            }
                            arr.Add(list[i][0], time);
                            dt = time;
                        }
                        foreach (string i in set)
                        {
                            DateTime arrtime;
                            DateTime lefttime;
                            StationWriter.Write(Date + "," + TrainInfo[0] + "," + i + ",");
                            if (arr.TryGetValue(i, out arrtime)) StationWriter.Write(arrtime);
                            StationWriter.Write(",");
                            if (left.TryGetValue(i, out lefttime)) StationWriter.Write(lefttime);
                            StationWriter.WriteLine("");

                        }




                        #region 区分管内管外
                        ////管内区间定位
                        //int ca, cb;
                        //for (ca = 2; ca < list.Count; ca++)
                        //{
                        //    if (Settings1.Default.StationName.Contains(list[ca][0])) break;
                        //}
                        //if (ca == list.Count) break;

                        //for (cb = list.Count - 1; cb > 1; cb--)
                        //    if (Settings1.Default.StationName.Contains(list[cb][0])) break;
                        //int ra, rb, rmax;
                        //for (rmax = 1; !list[0][rmax].Contains("下车人数合计"); rmax++) ; rmax++;
                        //for (ra = 2; ra < rmax; ra++) if (Settings1.Default.StationName.Contains(list[0][ra])) break;
                        //for (rb = rmax - 1; rb > 1; rb--) if (Settings1.Default.StationName.Contains(list[0][rb])) break;

                        ////前后管外区间区分
                        //string up, down;
                        //int cmp = Settings1.Default.StationName.IndexOf(list[cb + 1][0]) - Settings1.Default.StationName.IndexOf(list[ca + 1][0]);
                        //if (cmp > 0)
                        //{
                        //    up = "上行管外";
                        //    down = "下行管外";
                        //}
                        //else if (cmp < 0)
                        //{
                        //    down = "上行管外";
                        //    up = "下行管外";
                        //}
                        //else
                        //{
                        //    throw new Exception("无法判别管外区间！");
                        //}

                        ////管内 人数合计
                        //for (int j = ra; j <= rb; j++)
                        //    for (int i = ca; i <= cb; i++)
                        //        if (list[i][j] != "" && list[i][j] != " ")
                        //            CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + list[0][j] + "," + list[i][0] + "," + list[i][j]);

                        ////前管外-前管外 人数合计
                        //int all = 0;
                        //for (int j = 2; j < ra; j++)
                        //    for (int i = 2; i < cb; i++)
                        //        if (list[i][j] != "" && list[i][j] != " ")
                        //            all += int.Parse(list[i][j]);
                        //if (all != 0)
                        //    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + up + "," + up + "," + all.ToString());

                        ////后管外-后管外 人数合计
                        //all = 0;
                        //for (int j = rb + 1; j < rmax; j++)
                        //    for (int i = cb + 1; i < list.Count; i++)
                        //        if (list[i][j] != "" && list[i][j] != " ")
                        //            all += int.Parse(list[i][j]);
                        //if (all != 0)
                        //    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + down + "," + down + "," + all.ToString());

                        ////前管外-后管外 人数合计
                        //all = 0;
                        //for (int i = cb + 1; i < list.Count; i++)
                        //    for (int j = 2; j < ra; j++)
                        //        if (list[i][j] != "" && list[i][j] != " ")
                        //            all += int.Parse(list[i][j]);
                        //if (all != 0)
                        //    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + up + "," + down + "," + all.ToString());

                        ////前管外-管内、管内-后管外 人数合计
                        //for (int i = ca; i <= cb; i++)
                        //{
                        //    all = 0;
                        //    for (int j = 2; j < ra; j++)
                        //        if (list[i][j] != "" && list[i][j] != " ")
                        //            all += int.Parse(list[i][j]);
                        //    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + up + "," + list[i][0] + "," + all.ToString());
                        //}
                        //for (int i = ra; i <= rb; i++)
                        //{
                        //    all = 0;
                        //    for (int j = cb+1; j < list.Count; j++)
                        //        if (list[j][i] != "" && list[j][i] != " ")
                        //            all += int.Parse(list[j][i]);
                        //    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + list[0][i] + "," + down + "," + all.ToString());
                        //}
                        #endregion

                        #region 不区分管内管外
                        for (int j = 2; !list[0][j].Contains("下车人数合计"); j++)
                            for (int i = 2; i < list.Count; i++)
                            {
                                if (list[i][j] != "" && list[i][j] != " ")
                                    CountWriter.WriteLine(Date + "," + TrainInfo[0] + "," + list[0][j] + "," + list[i][0] + "," + list[i][j]);
                            }
                        #endregion


                    }
                }
            }

            #region 文件流的关闭与释放
            reader.Close();
            StationWriter.Close();
            CountWriter.Close();
            TrainWriter.Close();

            InStream.Close();
            StationOutStream.Close();
            CountOutStream.Close();
            TrainOutStream.Close();

            reader.Dispose();
            StationWriter.Dispose();
            CountWriter.Dispose();
            TrainWriter.Dispose();

            InStream.Dispose();
            StationOutStream.Dispose();
            CountOutStream.Dispose();
            TrainOutStream.Dispose();

            if (file != Path)
            {
                File.Delete(@Path);
            }
            #endregion

        }


    }
}
