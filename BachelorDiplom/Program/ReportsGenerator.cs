using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace BachelorLibAPI.Program
{
    public static class ExcelReportsGenerator
    {
        public static void GenerateReport(List<List<string>> report, string fileName)
        {
            var xlApp = new Application();
            Workbook xlWorkBook = null;
            
            try
            {
                fileName = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) +
                           @"\..\..\Reports\" + fileName + ".xls";
                xlWorkBook = xlApp.Workbooks.Add();

                // ReSharper disable once UseIndexedProperty
                var xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                for (var i = 0; i < report.Count; i++)
                    for (var j = 0; j < report[i].Count; ++j)
                    {
                        xlWorkSheet.Cells[i + 1, j + 1] = report[i][j];
                        if(j == 0) 
                            ((Range) xlWorkSheet.Cells[i + 1, j + 1]).Font.Bold = true;
                    }

                if(File.Exists(fileName)) File.Delete(fileName);
                xlWorkBook.SaveAs(fileName);
                MessageBox.Show(@"Отчёт сгенерирован");

                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch(Exception)
            {
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw;
            }
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка при очисте памяти после объекта " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
