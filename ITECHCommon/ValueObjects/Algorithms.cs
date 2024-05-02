using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PipeControl.Common
{
    public class Algorithms : BaseObject<Algorithm>
    {
        public override string ConfigFile { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "\\config\\算法.json";
        public override List<Algorithm> ObjectList { get; set; }

        public Algorithms() : base("算法")
        {

        }
        public override void Add(Algorithm al)
        {
            al.ID = ObjectList.Count + 1;
            ObjectList.Add(al);
        }
        public override void Remove(Algorithm dc)
        {
            ObjectList.Remove(dc);
        }
        public override void Save()
        {
            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(ObjectList, Formatting.Indented));
        }

        public override Algorithm GetObject(string name)
        {
            return ObjectList.Single(x => x.Name == name);
        }
        public Algorithm GetAlgorithm(int id)
        {
            return ObjectList.Single(x => x.ID == id);
        }
    }
}
