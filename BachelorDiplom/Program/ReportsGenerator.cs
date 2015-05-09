using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

//using Microsoft.Office.Interop.Excel;

namespace BachelorLibAPI.Program
{
    public class ExcelReportsGenerator
    {
        public static void GenerateReport()
        {
            var xlApp = new Application();
            Workbook xlWorkBook = null;
            
            try
            {
                if (xlApp == null)
                {
                    MessageBox.Show("Excel не установлен!", "Внимание!");
                    return;
                }

                xlWorkBook = xlApp.Workbooks.Add();

                var xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);
                xlWorkSheet.Cells[1, 1] = "Sheet 1 content";

                xlWorkBook.SaveAs(@"P:\Study\sem8\diploma\project\BachelorDiplom\BachelorDiplom\Reports\test.xlsx");
                MessageBox.Show("Отчёт сгенерирован в файл" + @"P:\Study\sem8\diploma\project\BachelorDiplom\BachelorDiplom\Reports\test.xlsx");

                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch(Exception ex)
            {
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
                throw ex;
            }
        }

        private static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Ошибка при очисте памяти после объекта " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
