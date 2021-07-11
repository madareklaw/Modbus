using ModbusGui.MvvmHelpers;

namespace ModbusGui.Models
{
    public class ModbusControlModel : Notifier
    {
        private int _slaveAddress = 1;

        public int SlaveAddress
        {
            get => _slaveAddress;
            set
            {
                _slaveAddress = value;
                OnPropertyChanged(nameof(SlaveAddress));
            }
        }
        private int _interval = 250;

        public int Interval
        {
            get => _interval;
            set
            {
                _interval = value;
                OnPropertyChanged(nameof(Interval));
            }
        }
        private int _modbusAddress = 1;

        public int ModbusAddress
        {
            get => _modbusAddress;
            set
            {
                _modbusAddress = value;
                OnPropertyChanged(nameof(ModbusAddress));
            }
        }
        private int _length = 4;

        public int Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

    }
}