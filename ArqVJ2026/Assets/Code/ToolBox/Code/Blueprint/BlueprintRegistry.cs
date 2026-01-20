using ianco99.ToolBox.Services;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;

namespace ianco99.ToolBox.Blueprints
{
    public sealed class BlueprintRegistry : IService
    {
        public bool IsPersistance => true;

        internal Dictionary<string, BlueprintData> BlueprintDatas => blueprintDatas;

        private readonly Dictionary<string, BlueprintData> blueprintDatas;

        public BlueprintRegistry(string blueprintPath)
        {
            blueprintDatas = new Dictionary<string, BlueprintData>();

            using (FileStream file = new FileStream(blueprintPath, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = new XSSFWorkbook(file);

                for (int i = 0; i < workbook.NumberOfSheets; i++)
                {
                    blueprintDatas.Add(workbook.GetSheetName(i), new BlueprintData(workbook.GetSheet(workbook.GetSheetName(i))));
                }
            }
        }

        public List<string> BlueprintsOF(string blueprintTable) => BlueprintDatas[blueprintTable].BlueprintIDs;
        public List<string> ParametersOf(string blueprintTable) => BlueprintDatas[blueprintTable].Parameters;
    }
}