namespace Keithley
{
    public enum ChannelType
    {
        无,
        交流电压,
        直流电压,
        温度,
        两线电阻,
        四线电阻
    }
    public enum TransducerType
    {
        无,
        热电偶,
        热敏电阻,
        RTD,
    }
    public enum ThermocoupleType
    {
        无,
        B,
        E,
        J,
        K,
        N,
        R,
        S,
        T
    }
    public enum ThermistorType : int
    {
        无 = 0,
        两千两百欧姆 = 2200,
        五千欧姆 = 5000,
        一万欧姆 = 10000
    }
    public enum FourWireRTDType
    {
        无,
        PT100,
        D100,
        F100,
        PT1000,
        PT385,
        PT3916
    }
}
