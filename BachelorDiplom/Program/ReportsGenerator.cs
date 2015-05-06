using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using Microsoft.Office.Interop.Excel;

namespace BachelorLibAPI.Reports
{
    public class ExcelReportsGenerator
    {
        public static void generateReport()
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook = null;
            
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

                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch(Exception ex)
            {
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
                throw ex;
            }
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
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
