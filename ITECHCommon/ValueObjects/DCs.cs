using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PipeControl.Common
{
    public class DCs : BaseObject<DC>
    {
        public override List<DC> ObjectList { get; set; }
        public override string ConfigFile { get; set; }
        public DCs() : base("热源")
        {

        }
        public void AddDC(string ip)
        {
            if(GetObject(ip) == null)
            {
                DC dc = new(ip);
                dc.ID = ObjectList.Count + 1;
                int ip4 = int.Parse(dc.IPAddress.Split('.')[3]);
                dc.Name = "热源" + ip4.ToString("00");
                ObjectList.Add(dc);
            }
        }
        public override void Add(DC al)
        {
            if (GetObject(al.IPAddress) == null)
            {
                al.ID = ObjectList.Count + 1;
                ObjectList.Add(al);
            }
        }
        public override void Remove(DC dc)
        {
            ObjectList.Remove(dc);
        }
        public override void Save()
        {
            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(ObjectList, Formatting.Indented));
        }

        public override DC GetObject(string ip)
        {
            foreach (DC dc in ObjectList)
            {
                if (dc.IPAddress == ip)
                {
                    return dc;
                }
            }
            return null;
        }
        public DC GetDC(int id)
        {
            return ObjectList.Single(x => x.ID == id);
        }
    }
}
