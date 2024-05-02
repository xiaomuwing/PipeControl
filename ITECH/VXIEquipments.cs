using System;
using System.Threading.Tasks;
using TmctlAPINet;

namespace ITECH
{
    public class VXIEquipments
    {
        public event EventHandler OnSearchEnd;
        private const int MAX_EQUIPMENTS = 128;
        private TMCTL myWTCtrl;
        public DEVICELIST[] Equipments { get; private set; } = new DEVICELIST[MAX_EQUIPMENTS];
        int equipmentsCount = -1;
        public int EquipmentsCount { get { return equipmentsCount; } }

        int result = -1;
        public VXIEquipments()
        {
            myWTCtrl = new TMCTL();
        }
        public void SearchEquipments()
        {
            Task.Run(() =>
            {
                Search();
                //IntervalTime();
            });
        }
        private void Search()
        {
            result = myWTCtrl.SearchDevices(TMCTL.TM_CTL_VXI11, Equipments, 128, ref equipmentsCount, null);
            OnSearchEnd?.Invoke(this, new EventArgs());
            searchEnd = true;
        }
        int cnt = 0;
        bool searchEnd = false;
        private async void IntervalTime()
        {
            while(true)
            {
                if (!searchEnd)
                {
                    await Task.Delay(1000);
                    cnt += 1;
                    if (cnt == 5)
                    {
                        OnSearchEnd?.Invoke(this, new EventArgs());
                        searchEnd = true;
                        return;
                    }
                }
                else
                {
                    OnSearchEnd?.Invoke(this, new EventArgs());
                    searchEnd = true;
                    return;
                }
            }
        }
    }
}
