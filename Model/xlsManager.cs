using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Model
{
    public class XlsManager : IXlsManager
    {
        string returnPath;
        bool isExcelInstaled;
        int countOffers = 0;
        int _rows;
        int _collumns;
        List<int> collumns = new List<int>();
        List<int> _compareCollumns = new List<int>();
        _Application excel;
        Workbook referenceOfferWorkbook;
        Worksheet referenceOfferWorksheet;
        Workbook newWorkbook;
        Worksheet newWorksheet;
        public XlsManager()
        {
        }

        public string CompareOffers(string referenceOfferPath,
            List<string> offersPaths, string MainFolderPath,
            List<int> compareCollumns) 
        {
            _compareCollumns = compareCollumns;
            countOffers = offersPaths.Count();
            excel = new _Excel.Application();
            isExcelInstaled = Type.GetTypeFromProgID("Excel.Application") != null ? true : false;
            if (isExcelInstaled)
            {
                try
                {
                    returnPath = CreateNewFile(MainFolderPath);
                    referenceOfferWorkbook = excel.Workbooks.Open(referenceOfferPath);
                }
                catch 
                {
                    return null;
                }
                referenceOfferWorksheet = referenceOfferWorkbook.Worksheets[1];
                _rows = referenceOfferWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                _collumns = referenceOfferWorksheet.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Column;
                newWorksheet = newWorkbook.Worksheets[1];

                foreach (var item in offersPaths)
                {
                    Workbook offerWorkbook;
                    try
                    {
                         offerWorkbook = excel.Workbooks.Open(item);
                    }
                    catch 
                    {
                        newWorkbook.Close();
                        return null;
                    }
                    var offerWorksheet = excel.Worksheets[1];
                    CopyOffer(offerWorksheet, newWorksheet);
                    Save(offerWorkbook);
                    Close(offerWorkbook);
                    countOffers--;
                }
                CopyOffer(referenceOfferWorksheet, newWorksheet);
                referenceOfferWorkbook.Save();
                referenceOfferWorkbook.Close();
                countOffers = offersPaths.Count();
                if (compareCollumns != null)
                {
                    Validate(countOffers);
                }
                addFirstLine();
                foreach (var item in offersPaths)
                {
                    addOfferName(Path.GetFileName(item), countOffers);
                    countOffers--;
                }
                addOfferName(Path.GetFileName(referenceOfferPath), 0);
                newWorkbook.Save();
                newWorkbook.Close();
                excel.Quit();
            }
            return returnPath;
        }

        private string CreateNewFile(string path)
        {
            var folderManager = new FolderManager();
            var newFilePath = System.IO.Path.Combine(path, "Porównanie Ofert", "Porównanie Ofert");
            folderManager.DeleteFolder(Path.Combine(path, "Porównanie Ofert"));
            folderManager.CreateNewFolder(path, "Porównanie Ofert");
            try
            {
                this.newWorkbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                SaveAs(newWorkbook, newFilePath);
            }
            catch 
            {
                newWorkbook.Close();
                return null;
            }
            return newFilePath;
        }

        private void CopyOffer(Worksheet worksheet, Worksheet newWorksheetItem)
        {
            _Excel.Range range1 = (_Excel.Range)worksheet.
                Range[worksheet.Cells[1, 1], worksheet.Cells[_rows, _collumns]].Cells;
            range1.Copy();
            var range2 = (_Excel.Range)newWorksheetItem.
                Range[newWorksheetItem.Cells[1, (countOffers * _collumns) + 1], newWorksheetItem.Cells[_rows, (countOffers * _collumns) + 1]].Cells;
            range2.PasteSpecial(_Excel.XlPasteType.xlPasteFormats,
                Microsoft.Office.Interop.Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
            range2 = (_Excel.Range)newWorksheetItem.
               Range[newWorksheetItem.Cells[1, (countOffers * _collumns) + 1], newWorksheetItem.Cells[_rows, (countOffers * _collumns) + 1]].Cells;
            range1 = (_Excel.Range)worksheet.
                Range[worksheet.Cells[1, 1], worksheet.Cells[_rows, _collumns]].Cells;
            range1.Copy(range2);
            Borders(range2);
            for (int i = 1; i < _collumns; i++)
            {
                newWorksheet.Columns[(countOffers * _collumns) + i].ColumnWidth = worksheet.Columns[i].ColumnWidth;
            }
            newWorkbook.Save();
        }

        private void  Borders(_Excel.Range range)
        {
        _Excel.Borders borders = range.Borders;
        borders[_Excel.XlBordersIndex.xlEdgeLeft].LineStyle = _Excel.XlLineStyle.xlContinuous;
        borders[_Excel.XlBordersIndex.xlEdgeTop].LineStyle = _Excel.XlLineStyle.xlContinuous;
        borders[_Excel.XlBordersIndex.xlEdgeBottom].LineStyle = _Excel.XlLineStyle.xlContinuous;
        borders[_Excel.XlBordersIndex.xlEdgeRight].LineStyle = _Excel.XlLineStyle.xlContinuous;
        borders[_Excel.XlBordersIndex.xlEdgeLeft].Weight = _Excel.XlBorderWeight.xlMedium;
        //borders[_Excel.XlBordersIndex.xlEdgeRight].Weight = _Excel.XlBorderWeight.xlMedium;
            //borders[_Excel.XlBordersIndex.xlEdgeTop].Weight = _Excel.XlBorderWeight.xlMedium;
            //borders[_Excel.XlBordersIndex.xlEdgeBottom].Weight = _Excel.XlBorderWeight.xlMedium;

        }

        
        private void Validate(int offers)
        {
            int offerIteration;
            foreach (var item in _compareCollumns)
            {
               offerIteration = offers;
                do
                {
                    for (int i = 1; i < _rows+1; i++)
                    {
                        var value = newWorksheet.Cells[i, item].Value;
                        var value2 = newWorksheet.Cells[i, _collumns * offerIteration + item].Value;
                        if (value != null && value2 != null)
                        {
                            if (string.Equals(value.GetType(), value2.GetType()))
                            {
                                if (value != null)
                                {
                                    if (value.GetType().Name == "String")
                                    {
                                        if (!string.Equals(newWorksheet.Cells[i, item].Value, newWorksheet.Cells[i, _collumns * offerIteration + item].Value))
                                        {
                                            newWorksheet.Cells[i, _collumns * offerIteration + item].Interior.Color = XlRgbColor.rgbRed;
                                        }
                                    }
                                    else
                                    {
                                        if (newWorksheet.Cells[i, item].Value != newWorksheet.Cells[i, _collumns * offerIteration + item].Value)
                                        {
                                            newWorksheet.Cells[i, _collumns * offerIteration + item].Interior.Color = XlRgbColor.rgbRed;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                newWorksheet.Cells[i, _collumns * offerIteration + item].Interior.Color = XlRgbColor.rgbRed;
                            }
                        }
                        else if (value == null && value2 == null)
                        { }
                        else
                        {
                            newWorksheet.Cells[i, _collumns * offerIteration + item].Interior.Color = XlRgbColor.rgbRed;
                        }
                    }
                    offerIteration--;
                } while (offerIteration > 0);
            }
            newWorkbook.Save();
        }

        private void addFirstLine()
        {
            Range line = (Range)newWorksheet.Rows[1];
            line.Insert();
        }

        private void addOfferName(string name, int offerNumber)
        {

            var range = (_Excel.Range)newWorksheet.Range[newWorksheet.Cells[1, (offerNumber * _collumns) + 1],
                newWorksheet.Cells[1, (offerNumber * _collumns) + _collumns]].Cells;
            range.Merge();
            newWorksheet.Cells[1, offerNumber * _collumns + 1].Value2 = name;
            newWorksheet.Cells[1, offerNumber * _collumns + 1].Style.Font.Size = 20;
            newWorksheet.Cells[1, offerNumber * _collumns + 1].Style.Font.Bold = true;
            var borders = range.Borders;
            borders[_Excel.XlBordersIndex.xlEdgeLeft].LineStyle = _Excel.XlLineStyle.xlContinuous;
            borders[_Excel.XlBordersIndex.xlEdgeRight].LineStyle = _Excel.XlLineStyle.xlContinuous;
            borders[_Excel.XlBordersIndex.xlEdgeBottom].LineStyle = _Excel.XlLineStyle.xlContinuous;
            borders[_Excel.XlBordersIndex.xlEdgeLeft].Weight = _Excel.XlBorderWeight.xlMedium;
            borders[_Excel.XlBordersIndex.xlEdgeRight].Weight = _Excel.XlBorderWeight.xlMedium;
            borders[_Excel.XlBordersIndex.xlEdgeBottom].Weight = _Excel.XlBorderWeight.xlMedium;
        }


        private void Save(Workbook wb)
        {
            wb.Save();
        }

        private void SaveAs(Workbook wb, string path)
        {
            wb.SaveAs(path);
        }

        private void Close(Workbook wb)
        {
            wb.Close();
        }
    }
}
