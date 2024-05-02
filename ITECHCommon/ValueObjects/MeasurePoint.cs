using Newtonsoft.Json;
namespace ITECHCommon
{
    [JsonObject(MemberSerialization.OptOut)]
    public class MeasurePoint
    {
        public int ID { get; set; }
        public string HardwareDeviceAddress { get; set; }
        public string HardwareChannelAddress { get; set; }
        public string Name { get; set; }
        public double? AlertValue { get; set; } = null;
        [JsonIgnore]
        public bool Selected { get; set; }
        [JsonIgnore]
        public double[] CurrentValue { get; set; } = new double[3];
        [JsonIgnore]
        public double LastValue { get; set; }
        [JsonIgnore]
        public int DataCount { get; set; } = 0;
        public MeasurePoint()
        {

        }
        public void AddData(double data)
        {
            LastValue = data;
            if (DataCount == 0)
            {
                CurrentValue[2] = data;
            }
            if (DataCount == 1)
            {
                CurrentValue[1] = CurrentValue[2];
                CurrentValue[2] = data;
            }
            if (DataCount >= 2)
            {
                CurrentValue[0] = CurrentValue[1];
                CurrentValue[1] = CurrentValue[2];
                CurrentValue[2] = data;
            }
            DataCount += 1;
        }
    }
}
