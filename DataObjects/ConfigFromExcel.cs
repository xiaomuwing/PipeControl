using System;
using System.Collections.Generic;
using System.IO;

namespace DataObjects
{
    public class ConfigFromExcel
    {
        public string FileName { get; set; }
        public TempCtrlType TempCtrlType { get; set; }
        public List<OutputDataAbsolute> AbsoluteDataList { get; set; } = new();
        public List<OutputDataRelative> RelativeDataList { get; set; } = new();
        public ConfigFromExcel(string file, TempCtrlType tempCtrlType)
        {
            FileName = file;
            TempCtrlType = tempCtrlType;
        }
        public void ReadData()
        {
            int id = 1;
            using FileStream fs = new(FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {

                string[] strs = sr.ReadLine().Split(',');
                if (TempCtrlType == TempCtrlType.导入列表_相对时间)
                {
                    OutputDataRelative outputDataRelative = new();
                    outputDataRelative.ID = id;
                    outputDataRelative.TimeSpan = new(0, 0, int.Parse(strs[0]));
                    outputDataRelative.OutputValue = double.Parse(strs[1]);
                    RelativeDataList.Add(outputDataRelative);
                }
                if (TempCtrlType == TempCtrlType.导入列表_绝对时间)
                {
                    OutputDataAbsolute outputDataAbsolute = new();
                    outputDataAbsolute.ID = id;
                    outputDataAbsolute.DateTime = DateTime.Parse(strs[0]);
                    outputDataAbsolute.OutputValue = double.Parse(strs[1]);
                    AbsoluteDataList.Add(outputDataAbsolute);
                }
                id += 1;
            }
        }
    }
}
