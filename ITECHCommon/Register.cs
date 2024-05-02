using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;

namespace PipeControl.Common
{
    public class Register
    {
        readonly int[] intCode = new int[127];
        readonly char[] charCode = new char[25];
        readonly int[] intNumber = new int[25];
        public string MNum { get; private set; }
        public string RNum { get; private set; }
        public Register()
        {
            FlushRegFile();
            GetMNum();
            GetRNum();
        }
        static string GetDiskVolumnSerialNumber()
        {
            string result;
            using (ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\""))
            {
                disk.Get();
                result = disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }
            return result;
        }
        static string GetCpu()
        {
            string strCpu = null;
            using (ManagementClass myCpu = new ManagementClass("win32_Processor"))
            {
                ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuCollection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                }
            }
            return strCpu;
        }
        void GetMNum()
        {
            MNum = GetCpu() + GetDiskVolumnSerialNumber();
            MNum = MNum.Substring(0, 24);    //截取前24位作为机器码
        }
        internal static string GetFeatureCode()
        {
            string result = GetCpu() + GetDiskVolumnSerialNumber();
            result = result.Substring(0, 24);
            return result;
        }
        void SetIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }
        void GetRNum()
        {
            SetIntCode();
            for (int i = 1; i < charCode.Length; i++)   //存储机器码
            {
                charCode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)  //改变ASCII码值
            {
                intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
            }
            for (int k = 1; k < intNumber.Length; k++)  //生成注册码
            {
                if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k] <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    RNum += Convert.ToChar(intNumber[k]).ToString();
                }
                else if (intNumber[k] > 122)  //判断如果大于z
                {
                    RNum += Convert.ToChar(intNumber[k] - 10).ToString();
                }
                else
                {
                    RNum += Convert.ToChar(intNumber[k] - 9).ToString();
                }
            }

            RNum = ChangeString(RNum, 1, 3);
            RNum = ChangeString(RNum, 2, 6);
            RNum = ChangeString(RNum, 3, 8);
            RNum = ChangeString(RNum, 4, 19);
            RNum = ChangeString(RNum, 5, 21);
            RNum = ChangeString(RNum, 6, 17);
            RNum = ChangeString(RNum, 7, 15);
            RNum = ChangeString(RNum, 8, 20);
            RNum = ChangeString(RNum, 9, 13);
        }
        private static string ChangeString(string str, int i, int j)
        {
            char[] vs = str.ToArray();
            char m = vs[i];
            vs[i] = vs[j];
            vs[j] = m;

            string result = new string(vs);
            return result;

        }
        public void WriteReg()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Interop.ImcDataLib.dll";
            byte[] b = Encoding.Default.GetBytes(MNum);
            for (int i = 0; i < b.Length; i++)
            {
                b[i] -= 1;
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                fs.Seek(32768, SeekOrigin.Begin);
                fs.Write(b, 0, 24);
                fs.Flush();
                fs.Close();
            }
        }
        public bool IsReg()
        {
            string code = ReadRegFile();
            if (code == MNum)
            {
                return true;
            }
            return false;
        }
        private static void FlushRegFile()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\Interop.ImcDataLib.dll";
            if (!File.Exists(fileName))
            {
                byte[] r = Properties.Resources.Interop_ImcDataLib;
                FileStream fs = null;
                try
                {
                    fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Interop.ImcDataLib.dll", FileMode.Create);
                    using (BinaryWriter sw = new BinaryWriter(fs))
                    {
                        fs = null;
                        sw.Write(r);
                        sw.Flush();
                    }
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                    }
                }
            }
        }
        public static string ReadRegFile()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\Interop.ImcDataLib.dll";
            byte[] b = new byte[24];
            using (FileStream fs = File.OpenRead(fileName))
            {
                BinaryReader br = new BinaryReader(fs);
                fs.Seek(32768, SeekOrigin.Begin);
                b = br.ReadBytes(24);
                br.Close();
                br.Dispose();
                fs.Close();
            }
            for (int i = 0; i < b.Length; i++)
            {
                b[i] += 1;
            }
            string result = Encoding.Default.GetString(b);
            return result;
        }
    }
}
