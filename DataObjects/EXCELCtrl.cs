using InstrumentsCtrl;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace DataObjects
{

    public static class EXCELCtrl
    {
        public static List<ControlCircle> ControlCircles { get; set; } = new();
        public static List<Channel> Channels { get; set; } = new();
        public static async Task ReadEXCEL(string file)
        {
            DataTable heatSource = await ExcelToDataTable(file, "热源", true);
            DataTable tempCtrl = await ExcelToDataTable(file, "温控点", true);
            DataTable ctrlZone = await ExcelToDataTable(file, "执行过程", true);

            ControlCircles.Clear();
            Channels.Clear();

            Channels.AddRange(ReadDCChannels(heatSource));
            Channels.AddRange(ReadTempChannels(tempCtrl));

            int id = 1;
            foreach (var channel in Channels)
            {
                channel.ID = id;
                channel.InitDChannel();
                id += 1;
            }

            for (int i = 1; i <= ctrlZone.Rows.Count; i++)
            {
                DataRow row = ctrlZone.Rows[i - 1];
                if (!int.TryParse(row.ItemArray[0].ToString(), out int zoneid))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项执行过程序号输入不正确");
                }
                var tempChannel = GetChannel(row.ItemArray[1].ToString()) ??
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温控点输入不正确，" + row.ItemArray[1].ToString() + "没有对应的定义");
                tempChannel.Function = ChannelFunction.控温点;
                var dcSN = row.ItemArray[7].ToString().Replace("，", ",");
                List<string> dcSNs = new();
                foreach (var sn in dcSN.Split(','))
                {
                    var dcChannel = GetChannel(row.ItemArray[7].ToString()) ??
                        throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项热源输入不正确，" + sn.ToString() + "没有对应的定义");
                    dcSNs.Add(sn);
                }
                string ct = row.ItemArray[2].ToString();
                if (!Enum.TryParse(ct, out ControlType controlType))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项控温类型输入不正确");
                }
                double iOut = 0, vOut = 0, minTemp = 0, maxTemp = 0, targetTemp = 0;
                switch (controlType)
                {
                    case ControlType.开关量:
                        if (!double.TryParse(row.ItemArray[3].ToString(), out vOut))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项电压输出输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[4].ToString(), out iOut))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项电流输出输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[5].ToString(), out minTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温度下限输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[6].ToString(), out maxTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温度上限输入不正确");
                        }
                        break;
                    case ControlType.PID:
                        if (!double.TryParse(row.ItemArray[8].ToString(), out targetTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项目标温度输入不正确");
                        }
                        break;
                }
                if (!double.TryParse(row.ItemArray[9].ToString(), out double lastTime))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项持续时间输入不正确");
                }
                var tmpCircle = GetControlCircle(zoneid, ControlCircles);
                ControlLine line = new();
                line.ControlType = controlType;
                line.TempChannelSN = tempChannel.SN;
                line.DCChannelSNs = dcSNs;
                line.VOut = vOut;
                line.IOut = iOut;
                line.TempMax = maxTemp;
                line.TempMin = minTemp;
                line.TargetTemp = targetTemp;
                if (tmpCircle == null)
                {
                    ControlCircle circle = new();
                    circle.ID = zoneid;
                    circle.LastTime = TimeSpan.FromMinutes(lastTime);
                    circle.Lines.Add(line);
                    ControlCircles.Add(circle);
                }
                else
                {
                    tmpCircle.LastTime = TimeSpan.FromMinutes(lastTime);
                    tmpCircle.Lines.Add(line);
                }
            }
        }
        public static async Task<List<ControlCircle>> ReloadEXCEL(string file)
        {
            List<ControlCircle> circles = new();

            DataTable heatSource = await ExcelToDataTable(file, "热源", true);
            DataTable tempCtrl = await ExcelToDataTable(file, "温控点", true);
            DataTable ctrlZone = await ExcelToDataTable(file, "执行过程", true);
            Channels.Clear();

            Channels.AddRange(ReadDCChannels(heatSource));
            Channels.AddRange(ReadTempChannels(tempCtrl));
            int id = 1;
            foreach (var channel in Channels)
            {
                channel.ID = id;
                channel.InitDChannel();
                id += 1;
            }
            for (int i = 1; i <= ctrlZone.Rows.Count; i++)
            {
                DataRow row = ctrlZone.Rows[i - 1];
                if (!int.TryParse(row.ItemArray[0].ToString(), out int zoneid))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项执行过程序号输入不正确");
                }
                var tempChannel = GetChannel(row.ItemArray[1].ToString()) ??
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温控点输入不正确，" + row.ItemArray[1].ToString() + "没有对应的定义");
                tempChannel.Function = ChannelFunction.控温点;
                var dcSN = row.ItemArray[7].ToString().Replace("，", ",");
                List<string> dcSNs = new();
                foreach (var sn in dcSN.Split(','))
                {
                    var dcChannel = GetChannel(row.ItemArray[7].ToString()) ??
                        throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项热源输入不正确，" + sn.ToString() + "没有对应的定义");
                    dcSNs.Add(sn);
                }
                string ct = row.ItemArray[2].ToString();
                if (!Enum.TryParse(ct, out ControlType controlType))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项控温类型输入不正确");
                }
                double iOut = 0, vOut = 0, minTemp = 0, maxTemp = 0, targetTemp = 0;
                switch (controlType)
                {
                    case ControlType.开关量:
                        if (!double.TryParse(row.ItemArray[3].ToString(), out vOut))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项电压输出输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[4].ToString(), out iOut))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项电流输出输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[5].ToString(), out minTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温度下限输入不正确");
                        }
                        if (!double.TryParse(row.ItemArray[6].ToString(), out maxTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项温度上限输入不正确");
                        }
                        break;
                    case ControlType.PID:
                        if (!double.TryParse(row.ItemArray[8].ToString(), out targetTemp))
                        {
                            throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项目标温度输入不正确");
                        }
                        break;
                }
                if (!double.TryParse(row.ItemArray[9].ToString(), out double lastTime))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在执行过程配置表中的第" + i.ToString() + "项持续时间输入不正确");
                }
                ControlLine line = new();
                line.ControlType = controlType;
                line.TempChannelSN = tempChannel.SN;
                line.DCChannelSNs = dcSNs;
                line.VOut = vOut;
                line.IOut = iOut;
                line.TempMax = maxTemp;
                line.TempMin = minTemp;
                line.TargetTemp = targetTemp;

                var tmpCircle = GetControlCircle(zoneid, circles);
                if (tmpCircle == null)
                {
                    ControlCircle circle = new();
                    circle.ID = zoneid;
                    circle.LastTime = TimeSpan.FromMinutes(lastTime);
                    line.Parent = circle;
                    circle.Lines.Add(line);
                    circles.Add(circle);
                }
                else
                {
                    line.Parent = tmpCircle;
                    tmpCircle.LastTime = TimeSpan.FromMinutes(lastTime);
                    tmpCircle.Lines.Add(line);
                }
            }
            return circles;
        }
        private static Task<DataTable> ExcelToDataTable(string file, string sheetName, bool isFirstRowColumn)
        {
            return Task.Run(() =>
            {
                DataTable data = new();
                //try
                //{
                FileStream fs = new(file, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet;
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数  

                    int startRow;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Numeric:
                                        double cellValue = cell.NumericCellValue;
                                        DataColumn column = new(cellValue.ToString());
                                        data.Columns.Add(column);

                                        break;
                                    case CellType.String:
                                        string cellValue2 = cell.StringCellValue;
                                        if (cellValue2 != null)
                                        {
                                            DataColumn columnS = new(cellValue2);
                                            data.Columns.Add(columnS);
                                        }
                                        break;
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount + 1; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null)
                        {
                            continue; //没有数据的行默认是null　　　　　　　  
                        }
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null  
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            });

            //}
            //catch
            //{
            //    return null;
            //}
        }
        private static List<Channel> ReadDCChannels(DataTable heatSource)
        {
            List<Channel> channels = new();
            for (int id = 1; id <= heatSource.Rows.Count; id++)
            {
                DataRow heatRow = heatSource.Rows[id - 1];
                string sn = heatRow[0].ToString();
                string userName = heatRow[1].ToString();
                if (!Enum.TryParse(heatRow[2].ToString(), true, out DeviceType deviceType))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项设备类型输入不正确，无此设备类型");
                }
                if (!Enum.TryParse(heatRow[3].ToString(), true, out DeviceModel deviceModel))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项设备型号输入不正确，" + heatRow[3].ToString() + "不在支持范围");
                }
                string address = heatRow[4].ToString();
                if (!address.Contains("COM") && !IsValidIP(address))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项设备地址输入不正确");
                }
                string port = heatRow[5].ToString();
                if (IsValidIP(address) && !ushort.TryParse(port, out _))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项端口号输入不正确");
                }
                if (deviceModel == DeviceModel.IT6832 && !ushort.TryParse(port, out _))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项端口号输入不正确,IT6832的端口号位置需输入表号");
                }
                string channelAddress = heatRow[6].ToString();
                string yunit = heatRow[7].ToString();
                if (!int.TryParse(heatRow.ItemArray[8].ToString(), out int showDecimal))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项小数位数输入不正确");
                }
                if (!float.TryParse(heatRow.ItemArray[9].ToString(), out float maxV))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项最最大电压输入不正确");
                }
                if (!float.TryParse(heatRow.ItemArray[10].ToString(), out float maxI))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项最大电流输入不正确");
                }
                if (!float.TryParse(heatRow.ItemArray[11].ToString(), out float rh))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项热源阻值输入不正确");
                }
                if (!float.TryParse(heatRow.ItemArray[12].ToString(), out float rl))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在热源配置表中的第" + id.ToString() + "项导线阻值输入不正确");
                }
                Channel tmp1 = new();
                tmp1.Name = deviceType.ToString() + "_" + deviceModel.ToString() + "_" + address.Replace('.', '_');
                if (!string.IsNullOrEmpty(channelAddress))
                {
                    tmp1.Name += "_" + channelAddress;
                }
                if (Channels.Contains(tmp1))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：热源" + address + "多次使用");
                }
                tmp1.Function = ChannelFunction.热源;
                tmp1.SN = sn;
                tmp1.UserName = userName;
                tmp1.DeviceType = deviceType;
                tmp1.DeviceModel = deviceModel;
                tmp1.HardwareDeviceAddress = address;
                tmp1.HardwareDevicePort = string.IsNullOrEmpty(port) ? (ushort)0 : ushort.Parse(port);
                tmp1.HardwareChannelAddress = channelAddress;
                tmp1.YUnit = yunit;
                tmp1.ShowDecimal = showDecimal;
                tmp1.Vmax = maxV;
                tmp1.Imax = maxI;
                tmp1.RH = rh;
                tmp1.RL = rl;
                tmp1.XDelta = 1000;
                channels.Add(tmp1);
            }
            return channels;
        }
        private static List<Channel> ReadTempChannels(DataTable tempCtrl)
        {
            List<Channel> channels = new();
            for (int id = 1; id <= tempCtrl.Rows.Count; id++)
            {
                DataRow tempRow = tempCtrl.Rows[id - 1];
                string sn = tempRow[0].ToString();
                string userName = tempRow[1].ToString();
                if (!Enum.TryParse(tempRow[2].ToString(), true, out DeviceType deviceType))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项设备类型输入不正确，无此设备类型");
                }
                if (!Enum.TryParse(tempRow[3].ToString(), true, out DeviceModel deviceModel))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项设备型号输入不正确，" + tempRow[3].ToString() + "不在支持范围");
                }
                string address = tempRow[4].ToString();
                if (!address.Contains("COM") && !IsValidIP(address))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项设备地址输入不正确");
                }
                string port = tempRow[5].ToString();
                if (IsValidIP(address) && !ushort.TryParse(tempRow[5].ToString(), out _))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项端口号输入不正确");
                }
                string channelAddress = tempRow[6].ToString();
                string yunit = tempRow[7].ToString();
                if (!int.TryParse(tempRow.ItemArray[8].ToString(), out int showDecimal))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项小数位数输入不正确");
                }
                if (!float.TryParse(tempRow.ItemArray[9].ToString(), out float alertMax))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项报警上限输入不正确");
                }
                if (!float.TryParse(tempRow.ItemArray[10].ToString(), out float alertMin))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：在温控点配置表中的第" + id.ToString() + "项报警下限输入不正确");
                }
                Channel tmp1 = new();
                tmp1.Name = deviceType.ToString() + "_" + deviceModel.ToString() + "_" + address.Replace('.', '_');
                if (!string.IsNullOrEmpty(channelAddress))
                {
                    tmp1.Name += "_" + channelAddress;
                }
                if (channels.Contains(tmp1))
                {
                    throw new Exception("要导入的EXCEL配置文件不合规：温控点 " + address + "_" + port + "_" + channelAddress + " 多次使用");
                }
                tmp1.Function = ChannelFunction.监测温度点;
                tmp1.SN = sn;
                tmp1.UserName = userName;
                tmp1.DeviceType = deviceType;
                tmp1.DeviceModel = deviceModel;
                tmp1.HardwareDeviceAddress = address;
                tmp1.HardwareDevicePort = ushort.Parse(port);
                tmp1.HardwareChannelAddress = channelAddress;
                tmp1.YUnit = yunit;
                tmp1.ShowDecimal = showDecimal;
                tmp1.AlertMax = alertMax;
                tmp1.AlertMin = alertMin;
                tmp1.XDelta = 1000;
                channels.Add(tmp1);
            }
            return channels;
        }
        private static ControlCircle GetControlCircle(int circleID, List<ControlCircle> circles)
        {
            foreach (ControlCircle c in circles)
            {
                if (c.ID == circleID)
                {
                    return c;
                }
            }
            return null;
        }
        private static Channel GetChannel(string sn)
        {
            if (string.IsNullOrEmpty(sn))
            {
                return null;
            }
            foreach (Channel c in Channels)
            {
                if (c.SN == sn)
                {
                    return c;
                }
            }
            return null;
        }
        private static bool IsValidIP(string ipAddress)
        {
            return IPAddress.TryParse(ipAddress, out _);
        }
    }
}
