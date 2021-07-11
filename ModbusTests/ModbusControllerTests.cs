using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Modbus.Tests
{
    [TestFixture()]
    public class ModbusControllerTests
    {
        [Test]
        public void ModbusControllerTest()
        {
            var port = TestHelper.CreateTestSerialPort();
            Assert.IsNotNull(port);
            var controller = new ModbusController(port);
            Assert.IsNotNull(controller);

        }

        [Test()]
        public void OpenSerialPortCloseSerialTest()
        {
            var port = TestHelper.CreateTestSerialPort();
            Assert.IsNotNull(port);
            var controller = new ModbusController(port);
            Assert.IsNotNull(controller);
            var status = controller.OpenSerialPort();
            Assert.AreEqual(ModbusControllerStatus.Success, status);
            var isOpen = controller.IsSerialPortOpen();
            Assert.IsTrue(isOpen);
            status = controller.CloseSerialPort();
            Assert.AreEqual(ModbusControllerStatus.Success, status);
        }

        [Test]
        public void OpenSerialPortCloseSerialWithNullSerialPortTest()
        {
            Assert.Throws(typeof(ArgumentNullException), () => { var x = new ModbusController(null); });
        }



        [Test]
        public async Task ReadHoldingRegistersAsyncTest()
        {
            var port = TestHelper.CreateTestSerialPort();
            Assert.IsNotNull(port);
            var controller = new ModbusController(port);
            Assert.IsNotNull(controller);
            var status = controller.OpenSerialPort();
            Assert.AreEqual(ModbusControllerStatus.Success, status);
            var isOpen = controller.IsSerialPortOpen();
            Assert.IsTrue(isOpen);
            var registers = await controller.ReadHoldingRegistersAsync(1,1 , 4);
            Assert.IsNotNull(registers);
            Assert.AreEqual(4, registers.Length);
            status = controller.CloseSerialPort();
            Assert.AreEqual(ModbusControllerStatus.Success, status);
        }
    }
}