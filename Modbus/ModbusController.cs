using System;
using System.IO.Ports;
using System.Threading.Tasks;
using NModbus;
using NModbus.Serial;

namespace Modbus
{
    public class ModbusController
    {
        /// <summary>
        /// Create instance of Modbus Controller associated with provided Serial Port
        /// </summary>
        /// <param name="serialPort"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ModbusController(SerialPort serialPort)
        {
            // Make sure the serial port is valid
            ModbusSerialPort = serialPort ?? throw new ArgumentNullException(nameof(serialPort), "Serial Port Cannot Be Null");
            
            // Wrap the serial port
            var adapter = new SerialPortAdapter(ModbusSerialPort);
            // Create the factory
            var factory = new ModbusFactory();
            // Create Modbus Master
            Master = factory.CreateRtuMaster(adapter);

        }

        /// <summary>
        /// Dispose of objects
        /// </summary>
        ~ModbusController()
        {
            //close the serial port
            ModbusSerialPort?.Close();
            Master?.Dispose();
            ModbusSerialPort?.Dispose();
        }

        private SerialPort ModbusSerialPort { get; set; }

        private IModbusMaster Master { get; set; }

        #region SerialPort handler

        /// <summary>
        /// Open SerialPort
        /// </summary>
        /// <returns>Controller status</returns>
        public ModbusControllerStatus OpenSerialPort()
        {
            if (ModbusSerialPort == null)
            {
                return ModbusControllerStatus.SerialPortIsNull;
            }
            if (ModbusSerialPort.IsOpen)
            {
                // already open
                return ModbusControllerStatus.Success;
            }

            try
            {
                ModbusSerialPort.Open();
                if (ModbusSerialPort.IsOpen)
                {
                    return ModbusControllerStatus.Success;
                }
            }
            catch
            {
                // Cannot open serialPort. Might already be in use?
            }
            return ModbusControllerStatus.CannotOpenSerialPort;
        }
        /// <summary>
        /// Close serialPort
        /// </summary>
        /// <returns>Controller status</returns>
        public ModbusControllerStatus CloseSerialPort()
        {
            if (ModbusSerialPort == null)
            {
                return ModbusControllerStatus.SerialPortIsNull;
            }
            ModbusSerialPort.Close();
            return ModbusSerialPort.IsOpen ? ModbusControllerStatus.CannotCloseSerialPort : ModbusControllerStatus.Success;
        }
        /// <summary>
        /// Is the serial port open?
        /// </summary>
        /// <returns>Is open?</returns>
        public bool IsSerialPortOpen()
        {
            return ModbusSerialPort?.IsOpen ?? false;
        }

        #endregion


        #region Modbus Handler
        /// <summary>
        ///  Read holding registers (Function Code 3)
        /// </summary>
        /// <param name="slaveAddress">Address of slave</param>
        /// <param name="startAddress">Starting address to read from</param>
        /// <param name="numberOfValuesToRead">number of registers to read</param>
        /// <returns></returns>
        public async Task<ushort[]> ReadHoldingRegistersAsync(byte slaveAddress, ushort startAddress, ushort numberOfValuesToRead)
        {
            return await Master.ReadHoldingRegistersAsync(slaveAddress, startAddress, numberOfValuesToRead);
        }
        /// <summary>
        /// Write Single Register (Function code 6)
        /// </summary>
        /// <param name="slaveAddress">Address of slave</param>
        /// <param name="registerAddress">Register address</param>
        /// <param name="valueToWrite">value to write</param>
        /// <returns></returns>
        public async Task WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort valueToWrite)
        {
            await Master.WriteSingleRegisterAsync(slaveAddress, registerAddress, valueToWrite);
        }

        #endregion
    }
}
