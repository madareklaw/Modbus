using System.IO.Ports;

namespace Modbus.Tests
{
    public class TestHelper
    {
        public static SerialPort CreateTestSerialPort(string serialPort = "COM4", int baudRate = 115200)
        {
            return new SerialPort(serialPort, baudRate);
        }
    }
}