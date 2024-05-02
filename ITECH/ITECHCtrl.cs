using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITECH
{
    public class ITECHCtrl : IDisposable
    {
        public event EventHandler OnPrepareEnd;
        public bool IsDemo { get; private set; }
        readonly VXIEquipments VXI = new();
        public int DeviceCount { get; private set; }
        public List<ITECHInstrument> Devices { get; private set; } = new();
        private int connected = 0;
        public ITECHCtrl(bool demo)
        {
            IsDemo = demo;
            VXI.OnSearchEnd += VXI_OnSearchEnd;
        }
        public void Prepare()
        {
            if(IsDemo)
            {
                SearchEquipmentDemo();
                return;
            }
            VXI.SearchEquipments();
        }
        private void SearchEquipmentDemo()
        {
            DeviceCount = 20;
            for(int i = 1; i < 21; i++)
            {
                string ip = "192.168.0." + (10 + i).ToString();
                ITECHInstrument device = new(ip, true);
                device.Init();
                Devices.Add(device);
            }
            OnPrepareEnd?.Invoke(this, new EventArgs());
        }
        private async void VXI_OnSearchEnd(object sender, EventArgs e)
        {
            var equips = VXI.Equipments.Where(x => !string.IsNullOrEmpty(x.adr)).OrderBy(x => x.adr);
            DeviceCount = VXI.EquipmentsCount;
            if(DeviceCount == 0)
            {
                await Task.Delay(2000);
                OnPrepareEnd?.Invoke(this, new EventArgs());
                return;
            }
            List<string> address = new();
            foreach(TmctlAPINet.DEVICELIST deviceList in VXI.Equipments)
            {
                address.Add(deviceList.adr);
            }
            address.Sort();
            Devices.Clear();
            for (int i = 0; i < VXI.EquipmentsCount; i++)
            {
                ITECHInstrument device = new(equips.ToArray()[i].adr, false);//  new(VXI.Equipments[i].adr);
                Devices.Add(device);
                device.OnConnectStatusChange += Device_OnConnected;
                device.Init();
            }
        }

        private void Device_OnConnected(object sender, EventArgs e)
        {
            connected += 1;
            if (connected == DeviceCount)
            {
                OnPrepareEnd?.Invoke(this, new EventArgs());
            }
        }

        public void Dispose()
        {
            if(IsDemo)
            {
                return;
            }
            for (int i = 0; i < DeviceCount; i++)
            {
                Devices[i].Dispose();
            }
        }
    }
}
