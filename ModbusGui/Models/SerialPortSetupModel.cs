using ModbusGui.MvvmHelpers;
using System.IO.Ports;

namespace ModbusGui.Models
{
    public class SerialPortSetupModel : Notifier
    {
        private string[] _serialPorts = SerialPort.GetPortNames();

        public string[] SerialPorts
        {
            get => _serialPorts;
            set
            {
                _serialPorts = value;
                OnPropertyChanged(nameof(SerialPorts));
            }
        }

        private string _selectedSerialPort = "COM4";

        public string SelectedSerialPort
        {
            get => _selectedSerialPort;
            set
            {
                _selectedSerialPort = value;
                OnPropertyChanged(nameof(SelectedSerialPort));
            }
        }
        public int[] DataBits => new[] { 5, 6, 7, 8 };

        private int _selectedDataBit = 8;

        public int SelectedDataBit
        {
            get => _selectedDataBit;
            set
            {
                _selectedDataBit = value;
                OnPropertyChanged(nameof(SelectedDataBit));
            }
        }


        public StopBits[] StopBits => new[]
        {
            System.IO.Ports.StopBits.None,
            System.IO.Ports.StopBits.One,
            System.IO.Ports.StopBits.OnePointFive,
            System.IO.Ports.StopBits.Two
        };

        private StopBits _selectedStopBit = System.IO.Ports.StopBits.One;

        public StopBits SelectedStopBit
        {
            get => _selectedStopBit;
            set
            {
                _selectedStopBit = value;
                OnPropertyChanged(nameof(SelectedStopBit));
            }
        }


        public Parity[] Paritys => new[]
        {
            Parity.None, Parity.Odd, Parity.Even, Parity.Mark, Parity.Space
        };

        private Parity _selectedParity = Parity.None;

        public Parity SelectedParity
        {
            get => _selectedParity;
            set
            {
                _selectedParity = value;
                OnPropertyChanged(nameof(SelectedParity));
            }
        }


        public int[] BaudRates
        {
            get
            {
                return new[]
                {
                    110,300,600,1200,2400,4800,9600,14400,19200,28800,38400,56000,57600,115200,230400
                };
            }
        }

        private int _selectedBaudRate = 115200;

        public int SelectedBaudRate
        {
            get => _selectedBaudRate;
            set
            {
                _selectedBaudRate = value;
                OnPropertyChanged(nameof(SelectedBaudRate));
            }
        }

    }
}