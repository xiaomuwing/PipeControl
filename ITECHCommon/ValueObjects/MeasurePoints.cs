using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ITECHCommon
{
    public class MeasurePoints : BaseObject<MeasurePoint>
    {
        public override List<MeasurePoint> ObjectList { get; set; }
        public override string ConfigFile { get; set; }
        public MeasurePoints() : base("测点")
        {

        }
        public override void Add(MeasurePoint al)
        {
            MeasurePoint mp = ObjectList.Single(x => x.HardwareDeviceAddress == al.HardwareDeviceAddress && x.HardwareChannelAddress == al.HardwareChannelAddress);
            if (mp == null)
            {
                ObjectList.Add(al);
            }
        }
        public void Add(string device, string channel)
        {
            if(GetMeasurePoint(device, channel) == null)
            {
                MeasurePoint mp = new();
                mp.HardwareDeviceAddress = device;
                mp.HardwareChannelAddress = channel;
                mp.ID = ObjectList.Count + 1;
                mp.Name = "控温点" + mp.ID.ToString("00");
                ObjectList.Add(mp);
            }
        }
        public override void Remove(MeasurePoint dc)
        {
            ObjectList.Remove(dc);
        }
        public override void Save()
        {
            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(ObjectList, Formatting.Indented));
        }

        public override MeasurePoint GetObject(string name)
        {
            return ObjectList.Single(x => x.Name == name);
        }
        public MeasurePoint GetMeasurPoint(int id)
        {
            return ObjectList.Single(x => x.ID == id);
        }
        public MeasurePoint GetMeasurePoint(string device, string channel)
        {
            var mps = ObjectList.Where(x => x.HardwareDeviceAddress == device && x.HardwareChannelAddress == channel);
            if(mps.Count() == 0)
            {
                return null;
            }
            return mps.ToArray()[0];
        }
    }
}
