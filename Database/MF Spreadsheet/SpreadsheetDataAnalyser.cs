using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class SpreadsheetDataAnalyser
    {
        public static List<List<string>> GetData(Spreadsheet spreadsheet,int from, int to)
        {
            List<List<string>> result = new();
            for (int i = from; i < to; i++)
            {
                result.Add(FileManager.GetRow(spreadsheet, i));
            }
            return result;
        }
    }
}
