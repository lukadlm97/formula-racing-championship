using System.Data;
using FormulaCar.Championships.Domain.Entities;
using FormulaCar.Championships.Importers.Configurations;
using FormulaCar.Championships.Importers.Utilities;
using FormulaCar.Championships.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FormulaCar.Championships.Importers.Loaders;

public class CsvLoader : ICsvLoader
{
    private readonly ImportSettings _importSettings;

    public CsvLoader(IOptions<ImportSettings> options)
    {
        _importSettings = options.Value;
    }
    public IEnumerable<DriverImportFormat> GetDrivers()
    {
        var drivers = new List<DriverImportFormat>();
        try
        {
            var _xl = new Microsoft.Office.Interop.Excel.Application();
            var wb = _xl.Workbooks.Open(Path.Combine(Environment.CurrentDirectory, _importSettings.DriversCsv));
            var sheets = wb.Sheets;
            DataSet dataSet = null;

            if (sheets != null && sheets.Count != 0)
            {
                dataSet = new DataSet();
                foreach (var item in sheets)
                {
                    var sheet = (Microsoft.Office.Interop.Excel.Worksheet)item;
                    DataTable dt = null;

                    if (sheet != null)
                    {
                        dt = new DataTable();
                        var ColumnCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Rows[1, Type.Missing])
                            .Columns.Count;
                        var rowCount = ((Microsoft.Office.Interop.Excel.Range)sheet.UsedRange.Columns[1, Type.Missing])
                            .Rows.Count;

                        for (var j = 0; j < ColumnCount; j++)
                        {
                            var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[1, j + 1];
                            var column = new DataColumn(true ? (string)cell.Value : string.Empty);
                            dt.Columns.Add(column);
                        }

                        for (var i = 0; i < rowCount - 1; i++)
                        {
                            var r = dt.NewRow();
                            for (var j = 0; j < ColumnCount; j++)
                            {
                                var cell = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[i + 1 + (true ? 1 : 0),
                                    j + 1];
                                r[j] = cell.Value;
                            }

                            if (r[4] != DBNull.Value && r[5] != DBNull.Value && r[6] != DBNull.Value &&
                                r[7] != DBNull.Value)
                            {
                                var newItem = new DriverImportFormat()
                                {
                                    FirstName = (string)r[4],
                                    LastName = (string)r[5],
                                    DateOfBirth = GetDriverDoB(r[6]),
                                    Country = new Country
                                    {
                                        Name = (string)r[7]
                                    }
                                };
                                newItem.IsActive = newItem.DateOfBirth.AddYears(44).Year > DateTime.Now.Year;
                                drivers.Add(newItem); 
                            }
                            else
                            {
                                Console.WriteLine("Continue!!!");
                            }
                            
                        }
                    }

                    dataSet.Tables.Add(dt);
                }
            }

            return drivers;
        }
        catch (Exception e)
        {
            throw e;
            return
                null;
        }
    }

    private static DateTime GetDriverDoB(object rawDate)
    {
        DateTime dob = DateTime.MinValue;
        var dateAsString = (string)rawDate;

        if (DateTime.TryParse(dateAsString, out dob))
        {
            return dob;
        }
        return dob;
    }
    
}