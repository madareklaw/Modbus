namespace Modbus
{
    public enum ModbusControllerStatus
    {
        Invalid=0,
        Success=1,
        SerialPortIsNull=2,
        CannotOpenSerialPort=3,
        CannotCloseSerialPort=4,
    }
}