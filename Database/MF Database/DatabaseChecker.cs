using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class DatabaseChecker
    {
        DatabaseManager database;
        public DatabaseChecker(DatabaseManager parentDatabase)
        {
            database = parentDatabase;
        }
        public bool CheckIfSpreadsheetExist(string name)
        {
            return database.spreadsheetNames.Contains(name);
        }
        public bool CheckIfTwoSpreadsheetsHaveTheSameField(SpreadsheetManager firstSpreadsheet, string firstFieldName, SpreadsheetManager secondSpreadsheet, string secondFieldName)
        {
            if (firstSpreadsheet.CheckIfFieldExist(firstFieldName) && secondSpreadsheet.CheckIfFieldExist(secondFieldName))
            {
                List<string> firstField = firstSpreadsheet.GetAllFieldData(firstFieldName);
                List<string> secondField = secondSpreadsheet.GetAllFieldData(secondFieldName);
                return firstField.SequenceEqual(secondField);
            }
            else
            {
                return false;
            }
        }
        public List<Field> MergeSpreadsheetFieldsByCommonField(SpreadsheetManager firstSpreadsheet, SpreadsheetManager secondSpreadsheet, int firstCommonFieldId, int secondCommonFieldId)
        {
            List<Field> firstFields = firstSpreadsheet.fields;
            Field firstCommonField = firstFields[firstCommonFieldId];
            firstFields.RemoveAt(firstCommonFieldId);
            List<Field> secondFields = secondSpreadsheet.fields;
            secondFields.RemoveAt(secondCommonFieldId);
            List<Field> allFields = firstFields;
            allFields.AddRange(secondFields);
            allFields.Insert(0, firstCommonField);
            return allFields;
        }
        public List<List<string>> MergeData(SpreadsheetManager firstSpreadsheet, SpreadsheetManager secondSpreadsheet, int firstCommonFieldId, int secondCommonFieldId)
        {
            List<List<string>> firstData = firstSpreadsheet.GetData();
            foreach (List<string> row in firstData)
            {
                row.Insert(0, row[firstCommonFieldId]);
                row.RemoveAt(firstCommonFieldId + 1);
            }
            List<List<string>> secondData = secondSpreadsheet.GetData();

            foreach (List<string> row in secondData)
            {
                row.RemoveAt(secondCommonFieldId);
            }
            for (int i = 0; i < firstData.Count; i++)
            {
                firstData[i].AddRange(secondData[i]);
            }
            return firstData;
        }
    }
}
