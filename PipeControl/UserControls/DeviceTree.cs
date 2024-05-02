using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IMP;

namespace ITECHTest
{
    public class DeviceTree : TreeView 
    {
        private readonly ImageList myImgIcon;
        private readonly ImageList myImgDrag;
        private TreeNode selectNode;
        public IMPControl IMP { get; set; }
        public List<DC> DCList { get; set; } = new();

        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public DeviceTree()
        {
            EnableDoubleBuffering();
            BackColor = Color.FromArgb(200,200,200);
            base.ForeColor = Color.White;
            base.Font = new Font("微软雅黑", 11F);
            base.AllowDrop = true;
            FullRowSelect = true;
            HideSelection = false;
            LineColor = Color.Gray;
            myImgIcon = new ImageList();

            myImgIcon.Images.Add(Properties.Resources.Imp);
            myImgIcon.Images.Add(Properties.Resources.ImpError);
            myImgIcon.Images.Add(Properties.Resources.yokogawa);
            myImgIcon.ImageSize = new Size(24, 24);

            ImageList = myImgIcon;
            myImgDrag = new ImageList();
            ShowNodeToolTips = true;
            ItemDrag += DevicesTree_ItemDrag;
            DragOver += DevicesTree_DragOver;
            DragEnter += DevicesTree_DragEnter;
            ItemHeight = 30;

            TreeNode impRoot = new TreeNode();
            impRoot.Name = "IMP_Root";
            impRoot.ImageIndex = 0;
            impRoot.SelectedImageIndex = 0;
            impRoot.Text = "IMP";

            TreeNode vxiRoot = new();
            vxiRoot.Name = "VXI_Root";
            vxiRoot.ImageIndex = 2;
            vxiRoot.SelectedImageIndex = 2;
            vxiRoot.Text = "直流电源";

            base.Nodes.Add(impRoot);
            base.Nodes.Add(vxiRoot);
        }
        public void ShowIMP()
        {
            TreeNode IMPRoot = Nodes.Find("IMP_Root", true)[0];
            IMPRoot.Nodes.Clear();
            IMPRoot.Text = "IMP";
            if (IMP.Channels.Count == 0)
            {
                IMPRoot.Text = "IMP(无设备)";
                return;
            }
            foreach (IMPDevice device in IMP.Devices)
            {
                TreeNode tmpDevice = new TreeNode();
                tmpDevice.Name = "IMP_Device_" + device.Address;
                tmpDevice.Text = device.Name;
                tmpDevice.ImageIndex = 0;
                tmpDevice.SelectedImageIndex = 0;
                foreach (IMPChannel c in device.Channels)
                {
                    TreeNode tmpChannel = new TreeNode();
                    tmpChannel.Name = "IMP_Channel_" + device.Address + "_" + c.Address;
                    tmpChannel.Text = c.UserName;
                    tmpChannel.ImageIndex = 0;
                    tmpChannel.SelectedImageIndex = 0;
                    tmpDevice.Nodes.Add(tmpChannel);
                }
                IMPRoot.Nodes.Add(tmpDevice);
            }
        }
        public void ShowVXI(VXIEquipments equipments)
        {
            TreeNode VXIRoot = Nodes.Find("VXI_Root", true)[0];
            VXIRoot.Nodes.Clear();
            VXIRoot.Name = "直流电源";
            if(equipments.EquipmentsCount == 0)
            {
                VXIRoot.Name = "直流电源(无设备)";
                return;
            }

            for (int i = 1; i <= equipments.EquipmentsCount; i++)
            {
                DC dc = new()
                {
                    Name = "DC" + i.ToString("000"),
                    IPAddress = equipments.Equipments[i - 1].adr
                };
                DCList.Add(dc);
                TreeNode tmpDevice = new TreeNode();
                tmpDevice.Name = "VXI_Device_" + dc.IPAddress;
                tmpDevice.Text = dc.Name + "(" + dc.IPAddress + ")";
                tmpDevice.ImageIndex = 2;
                tmpDevice.SelectedImageIndex = 2;
                VXIRoot.Nodes.Add(tmpDevice);
            }

        }
        private void DevicesTree_DragEnter(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void DevicesTree_DragOver(object sender, DragEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void DevicesTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
