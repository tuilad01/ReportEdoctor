using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AutomationTest1
{
    public class Operator
    {
        private const string FILE_PATH = @"Config.xlsx";
        private const string SHEET_ACCOUNT = "Account";
        private const string SHEET_EXAMPACKET = "ExamPacket";
        private const string SHEET_ROOM = "Room";

        private const string EXPORT_FILE = @"CSKH.xlsx";
        private const string SHEET_CARE = "CSKH";

        private const string JOIN_CHAR = " + ";

        public Config ReadConfig()
        {
            var config = new Config();
            var fi = new FileInfo(FILE_PATH);

            using (var p = new ExcelPackage(fi))
            {
                //Account Sheet
                using (var ws = p.Workbook.Worksheets[SHEET_ACCOUNT])
                {
                    config.UserName = ws.Cells[1, 2].Value.ToString();
                    config.Password = ws.Cells[2, 2].Value.ToString();
                }

                //ExamPacket sheet
                using (var ws = p.Workbook.Worksheets[SHEET_EXAMPACKET])
                {
                    config.ExamPacket = GetListTable(ws);
                }

                //Room sheet
                using (var ws = p.Workbook.Worksheets[SHEET_ROOM])
                {
                    config.Room = GetListTable(ws);
                }
            }
            return config;
        }

        private IList<NameValue> GetListTable(ExcelWorksheet ws)
        {
            var list = new List<NameValue>();

            var start = ws.Dimension.Start;
            var end = ws.Dimension.End;

            for (int row = start.Row; row <= end.Row; row++)
            {
                list.Add(new NameValue
                {
                    Name = ws.Cells[row, 1].Value.ToString(),
                    Value = ws.Cells[row, 2].Value.ToString()
                });
            }

            return list;
        }

        public void ExportReport(IList<Reservation> reservations)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add(SHEET_CARE);

                for (int i = 0; i < reservations.Count; i++)
                {
                    var col = 1;

                    ws.Cells[i, col++].Value = reservations[i].Name;

                    ws.Cells[i, col++].Value = reservations[i].Phone;
                    
                    ws.Cells[i, col++].Value = reservations[i].Address;

                    ws.Cells[i, col++].Value = reservations[i].Ward;

                    ws.Cells[i, col++].Value = reservations[i].District;

                    ws.Cells[i, col++].Value = reservations[i].City;

                    ws.Cells[i, col++].Value = string.Join(JOIN_CHAR, reservations[i].Service);

                    ws.Cells[i, col++].Value = reservations[i].Price;
                }

                if (File.Exists(EXPORT_FILE))
                {
                    File.Delete(EXPORT_FILE);
                }

                p.SaveAs(new FileInfo(EXPORT_FILE));
            }
        }

        private void InitTilteTable (ExcelWorksheet ws)
        {
            //ws.Cells[2, 1].Value = "Name";
            //ws.Cells[2, 1].Style.Font.Bold = true;
        }

        public IList<string> MapName(IList<NameValue> list, IList<string> service)
        {

            for (int i = 0; i < service.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    var a = Regex.Replace(service[i], " {2,}", " ");
                    a = ConvertToUnSign(a)
                        .ToLower()
                        .Trim();

                    var b = Regex.Replace(list[j].Name, " {2,}", " ");
                    b = ConvertToUnSign(b)
                        .ToLower()
                        .Trim();

                    if (a == b)
                    {
                        service[i] = list[j].Value;
                        break;
                    }
                }
            }
            return service;
        }

        private static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}
