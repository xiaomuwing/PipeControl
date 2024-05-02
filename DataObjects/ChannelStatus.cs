namespace DataObjects
{
    public enum ChannelStatus : byte
    {
        未知 = 0,
        正在连接 = 1,
        正常 = 2,
        断开连接 = 3,
        报警 = 4,
        无设备 = 5
    }
}
