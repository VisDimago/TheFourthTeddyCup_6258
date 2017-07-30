using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataReader
{
    static partial class Reader
    {
        public static bool ReadDaysInfo(string[] files)
        {
            if (!File.Exists(Settings1.Default.@TempPath + "station.csv"))
            {
                FileStream StationOutStream = new FileStream(Settings1.Default.@TempPath + "station.csv", FileMode.Create, FileAccess.Write);
                StreamWriter StationWriter = new StreamWriter(StationOutStream, Encoding.Default);
                StationWriter.WriteLine("RecordDate,TrainNum,Station,ArriveTime,DepartsTime");
                StationWriter.Close();
                StationOutStream.Close();
                StationOutStream.Dispose();
                StationWriter.Dispose();
            }

            if (!File.Exists(Settings1.Default.@TempPath + "count.csv"))
            {
                FileStream CountOutStream = new FileStream(Settings1.Default.@TempPath + "count.csv", FileMode.Create, FileAccess.Write);
                StreamWriter CountWriter = new StreamWriter(CountOutStream, Encoding.Default);
                CountWriter.WriteLine("RecordDate,TrainNum,AboardStation,DebusStation,FlowCount");
                CountWriter.Close();
                CountOutStream.Close();
                CountWriter.Dispose();
                CountOutStream.Dispose();
            }

            if (!File.Exists(Settings1.Default.@TempPath + "train.csv"))
            {
                FileStream TrainOutStream = new FileStream(Settings1.Default.@TempPath + "train.csv", FileMode.Create, FileAccess.Write);
                StreamWriter TrainWriter = new StreamWriter(TrainOutStream, Encoding.Default);
                TrainWriter.WriteLine("TrainNum,RecordDate,OriginStation,TerminalStation,MaxCount,LoadFactors");
                    
                TrainWriter.Close();
                TrainOutStream.Close();
                TrainWriter.Dispose();
                TrainOutStream.Dispose();
            }


            foreach (string i in files)
            {
                
                if (i.Contains(".xls") || i.Contains(".csv"))
                    Reader.GetDayInfo(@i);
            }
            return true;
        }
    }
}
