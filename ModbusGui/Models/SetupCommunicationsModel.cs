using System.Configuration;
using System.DirectoryServices;
using System.Windows.Controls;
using ModbusGui.MvvmHelpers;

namespace ModbusGui.Models
{
    public class SetupCommunicationsModel : Notifier
    {
        public string[] AvailableCommsTypes => new[]
        {
            Resources.Strings.AppStrings.ComboText_SerialPort,
            Resources.Strings.AppStrings.ComboText_TCP
        };

        private string _selectedItem = Resources.Strings.AppStrings.ComboText_SerialPort;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private UserControl _userControl = null;

        public UserControl UserControl
        {
            get => _userControl;
            set
            {
                _userControl = value;
                OnPropertyChanged(nameof(UserControl));
            }
        }

    }
}