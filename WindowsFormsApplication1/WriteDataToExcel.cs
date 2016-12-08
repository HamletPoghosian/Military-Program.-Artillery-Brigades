﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class WriteDataToExcel
    {
        Excel.Workbook workbook;
        Excel.Worksheet excelSheet;
        Excel.Application excelData;
        Excel.Range testCell;
        object misValue = System.Reflection.Missing.Value;

        public WriteDataToExcel()
        {

        }
        public WriteDataToExcel(DataModel datamodel)
        {
            string pathDirectory = GetDefaultDirectory();
            CreatdataExcel(pathDirectory, datamodel);
        }
        private string GetDefaultDirectory()
        {
            string directoryName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\GeneratedFile";
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            return directoryName;
        }
        public void CreatdataExcel(string path, DataModel datamodel)
        {
            excelData = new Excel.Application();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(path).Append("\\").Append(datamodel.SolderName).Append("-").Append(datamodel.SoldeSurername).Append(".csv");
            List<string> listofDataName = datamodel.DatamodelValue();

            Excel.Workbook excelWorkBook = excelData.Workbooks.Add(misValue);
            excelSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
            excelWorkBook.SaveAs(stringBuilder.ToString(), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
            int excelCallsCount = TestExcelCalls(stringBuilder.ToString());
            for (int i = 0; i < listofDataName.Count; i++)
            {
                ExcelGeneralDataDesign(excelCallsCount,i+1);
                excelSheet.Cells[excelCallsCount, i + 1] = listofDataName[i];

                for (int j = 0; j < listofDataName.Count - 1;)
                {
                    if (i == 4 || i == 13)
                        excelSheet.Cells[excelCallsCount + 1, i + 1] = datamodel.DatamodelValueAge(i);
                    else
                        excelSheet.Cells[excelCallsCount + 1, i + 1] = datamodel.DatamodelValueStringParametrs(i);
                    break;

                }

            }
            excelWorkBook.SaveAs(stringBuilder.ToString(), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);

            excelWorkBook.Close(true, misValue, misValue);
            excelData.Quit();
        }

        public int TestExcelCalls(string path)
        {
            int count = 0;

            excelData = new Excel.Application();
            workbook = excelData.Workbooks.Open(path);
            Excel.Worksheet excelSheet = workbook.ActiveSheet;
            for (int i = 1; i < 10000; i++)
            {
                if (excelSheet.Cells[i, 1].Value == null)
                {
                    count = i;
                    break;
                }
            }
            workbook.Close();
            return count;

        }
        private void ExcelGeneralDataDesign(int index1, int index2)
        {
            

            excelSheet.Cells[index1, index2].Font.Color = ColorTranslator.ToOle(Color.White);
            excelSheet.Cells[index1, index2].Font.Size = 14;
            excelSheet.Cells[index1,index2].Interior.Color =ColorTranslator.ToOle(Color.DarkGreen);
            
        }
    }
}
