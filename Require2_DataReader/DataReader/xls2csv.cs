using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.IO;



namespace DataReader
{
    static partial class Reader
    {
        private static void xls2csv(string Path)
        {
            Object Nothing = System.Reflection.Missing.Value;//由于COM组件很多值需要用Missing.Value代替   
            Microsoft.Office.Interop.Excel.Application ExclApp = new Microsoft.Office.Interop.Excel.Application();// 初始化
            Microsoft.Office.Interop.Excel.Workbook ExclDoc = ExclApp.Workbooks.Open(Path, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);//打开Excl工作薄   

            string newpath = Settings1.Default.@TempPath + "temp.csv";
            try
            {
                Object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV;
                ExclApp.DisplayAlerts = false;
                //ExclDoc.SaveAs(Path.Replace("xls", "csv"), format, Nothing, Nothing, Nothing, Nothing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);
                ExclDoc.SaveAs(newpath, format, Nothing, Nothing, Nothing, Nothing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);
            }
            catch (Exception) { }

            ExclDoc.Close(Nothing, Nothing, Nothing);
            NAR(ExclDoc);
            ExclApp.Quit();
            NAR(ExclApp);
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }
        private static void NAR(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch { }
            finally
            {
                o = null;
            }
        }



    }
}
