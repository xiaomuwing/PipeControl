using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataObjects
{
    public abstract class BaseObject<T> where T : class
    {
        public abstract List<T> ObjectList { get; set; }
        public abstract string ConfigFile { get; set; }
        public BaseObject(string configFileName)
        {
            ConfigFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + configFileName + ".json";
            if (!File.Exists(ConfigFile))
            {
                using FileStream filestream = new(ConfigFile, FileMode.Create, FileAccess.ReadWrite);
            }
            string json = File.ReadAllText(ConfigFile);
            ObjectList = new List<T>();
            ObjectList = JsonConvert.DeserializeObject<List<T>>(json);
            if (ObjectList == null)
            {
                ObjectList = new();
            }
        }
        public abstract void Add(T t);

        public abstract void Remove(T t);

        public abstract void Save();
        public abstract T GetObject(string str);

    }
}
