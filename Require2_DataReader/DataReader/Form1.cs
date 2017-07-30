using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace DataReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //CheckForIllegalCrossThreadCalls = false;
            System.Console.SetOut(new TextBoxWriter(textBox1));
        }
        List<string> lines = new List<string>();


        private void button1_Click(object sender, EventArgs e)
        {
            //Reader.Weather(@"D:\Desktop\B题：附件\附件4：车站所属地区气象信息.csv");
            //Reader.GetDayInfo(@"D:\Desktop\20150101.csv");
        }

        private void 天气信息导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(GetWeatherInfo);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 从文件导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(GetTrainInfoByFile);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 从文件夹导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(GetTrainInfoByDir);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();

        }

        private void GetAllFiles(string path, List<string[]> list)
        {
            list.Add(Directory.GetFiles(path));
            foreach (string i in Directory.GetDirectories(path))
            {
                GetAllFiles(i, list);
            }
        }



        private void GetTrainInfoByDir()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                System.Console.Write("开始导入列车运行数据！请勿关闭程序！");
                System.Console.Write("正在预处理列车运行数据！");

                List<string[]> list = new List<string[]>();
                GetAllFiles(dlg.SelectedPath, list);

                int count = 0, finished = 0;
                foreach (string[] i in list)
                {
                    count += i.Count();
                }

                foreach (string[] i in list)
                {

                    Reader.ReadDaysInfo(i);
                    finished += i.Count();
                    toolStripProgressBar1.Value = ( finished * 100 ) / count;
                }
                System.Console.Write("预处理完成。");

                System.Console.Write("开始上传列车运行数据！请勿关闭程序！");
                if (Reader.UploadTrainInfo())
                {
                    System.Console.Write("列车运行数据上传成功。");
                    System.Console.Write("列车运行数据导入完成。");
                }
                else
                {
                    System.Console.Write("列车运行数据上传失败！");
                }
            }
            toolStripProgressBar1.Value = 0;
            dlg.Dispose();
            Thread.CurrentThread.Abort();

        }

        private void GetTrainInfoByFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Console.Write("开始导入列车运行数据！请勿关闭程序！");

                System.Console.Write("正在预处理列车运行数据！");

                Reader.ReadDaysInfo(dlg.FileNames);
                System.Console.Write("预处理完成。");

                System.Console.Write("开始上传列车运行数据！请勿关闭程序！");

                if (Reader.UploadTrainInfo())
                {
                    if (File.Exists(Settings1.Default.@TempPath + "train.csv"))
                        File.Delete(Settings1.Default.@TempPath + "train.csv");
                    if (File.Exists(Settings1.Default.@TempPath + "count.csv"))
                        File.Delete(Settings1.Default.@TempPath + "count.csv");
                    if (File.Exists(Settings1.Default.@TempPath + "station.csv"))
                        File.Delete(Settings1.Default.@TempPath + "station.csv");
                    System.Console.Write("列车运行数据上传成功。");
                    System.Console.Write("列车运行数据导入完成。");

                }
                else
                {
                    System.Console.Write("列车运行数据上传失败！");

                }
            }

            dlg.Dispose();
            Thread.CurrentThread.Abort();
        }

        private void GetWeatherInfo()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Console.Write("开始导入天气数据！请勿关闭程序！");

                if (Reader.ReadWeather(@dlg.FileName))
                {
                    System.Console.Write("天气数据导入完成。");

                }
                else
                {
                    System.Console.Write("天气数据上传失败！");

                }
            }
            dlg.Dispose();
            Thread.CurrentThread.Abort();
        }

        private void GetStationInfo()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Console.Write("开始导入车站数据！请勿关闭程序！");

                if (Reader.ReadStation(@dlg.FileName))
                {
                    System.Console.Write("车站数据导入完成。");

                }
                else
                {
                    System.Console.Write("车站数据上传失败！");

                }
            }
            dlg.Dispose();
            Thread.CurrentThread.Abort();
        }

        private void GetRegionInfo()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Console.Write("开始导入地区数据！请勿关闭程序！");

                if (Reader.ReadRegion(@dlg.FileName))
                {
                    System.Console.Write("地区数据导入完成。");

                }
                else
                {
                    System.Console.Write("地区数据上传失败！");

                }
            }
            dlg.Dispose();
            Thread.CurrentThread.Abort();
        }

        private void Upload()
        {
            if (Reader.UploadTrainInfo())
            {
                if (File.Exists(Settings1.Default.@TempPath + "train.csv"))
                    File.Delete(Settings1.Default.@TempPath + "train.csv");
                if (File.Exists(Settings1.Default.@TempPath + "count.csv"))
                    File.Delete(Settings1.Default.@TempPath + "count.csv");
                if (File.Exists(Settings1.Default.@TempPath + "station.csv"))
                    File.Delete(Settings1.Default.@TempPath + "station.csv");
                System.Console.Write("列车运行数据上传成功。");
                System.Console.Write("列车运行数据导入完成。");
            }
            else
            {
                System.Console.Write("列车运行数据上传失败！");
            }
            Thread.CurrentThread.Abort();
        }

        private void DownloadFlowInfo()
        {
            Console.WriteLine("开始下载站点间日客流量！");
            List<string> slist = new List<string>();

            slist.Add("管外");
            slist.Add("ZD111");
            slist.Add("ZD311");
            slist.Add("ZD326");
            slist.Add("ZD192");
            slist.Add("ZD022");
            slist.Add("ZD250");
            slist.Add("ZD062");
            slist.Add("ZD120");
            slist.Add("ZD121");
            slist.Add("ZD143");
            slist.Add("ZD370");
            slist.Add("ZD190");

            if (DBReader.DownloadFlowInfo(slist))
            {
                Console.WriteLine("站点间日客流量下载成功！");
            }
            else
            {
                Console.WriteLine("站点间日客流量下载失败！");
            }
            Thread.CurrentThread.Abort();
        }

        private void FormatForcecast()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Multiselect = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                System.Console.Write("开始整理预测数据！请勿关闭程序！");

                if (DataFormer.ForecastData(dlg.FileName))
                {
                    System.Console.Write("预测数据整理完成！");

                }
                else
                {
                    System.Console.Write("预测数整理失败！");

                }
            }
            dlg.Dispose();
            Thread.CurrentThread.Abort();
        }

        private void 地区信息导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(GetRegionInfo);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 车站信息导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(GetStationInfo);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 上传缓存数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(Upload);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 站点间日客流数据导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(DownloadFlowInfo);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }

        private void 预测数据整理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread td = new Thread(FormatForcecast);
            td.SetApartmentState(ApartmentState.STA);
            td.Start();
        }
    }



    public class TextBoxWriter : System.IO.TextWriter
    {
        TextBox lstBox;
        delegate void VoidAction();

        public TextBoxWriter(TextBox box)
        {
            lstBox = box;
        }

        public override void Write(string value)
        {
            VoidAction action = delegate
            {
                List<string> temp = new List<string>(lstBox.Lines);

                temp.Insert(0, string.Format("[{0:HH:mm:ss}]{1}", DateTime.Now, value));
                lstBox.Lines = temp.ToArray();
            };
            lstBox.BeginInvoke(action);
        }

        public override void WriteLine(string value)
        {
            VoidAction action = delegate
            {
                List<string> temp = new List<string>(lstBox.Lines);
                temp.Insert(0, string.Format("[{0:HH:mm:ss}]{1}", DateTime.Now, value));
                lstBox.Lines = temp.ToArray();
            };
            lstBox.BeginInvoke(action);
        }

        public override System.Text.Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }


}
