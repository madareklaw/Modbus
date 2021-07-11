using System;
using System.IO.Ports;
using System.Windows;
using Modbus;
using ModbusGui.Models;
using ModbusGui.MvvmHelpers;
using ModbusGui.Views;

namespace ModbusGui.ViewModels
{
    public class SetupCommunicationsViewModel
    {
        public SetupCommunicationsModel Model { get; } = new SetupCommunicationsModel();

        public AppCommand<Window> OkCommand { get; }


        private SerialPortSetup SerialPortControl = new SerialPortSetup();

        public SetupCommunicationsViewModel()
        {
            Model.PropertyChanged += _model_PropertyChanged;
            OkCommand = new AppCommand<Window>(ExecuteOkCommand, CanExecuteOkCommand);
            ChangeUserControl();
        }

        private bool CanExecuteOkCommand(Window arg)
        {
            return !string.IsNullOrEmpty(_selectedItem);
        }

        private void ExecuteOkCommand(Window obj)
        {
            if (_selectedItem == Resources.Strings.AppStrings.ComboText_SerialPort)
            {
                if (SerialPortControl.DataContext is SerialPortSetupViewModel {Model: { } portModel})
                {
                    try
                    {
                        var port = new SerialPort(portModel.SelectedSerialPort, portModel.SelectedBaudRate,
                            portModel.SelectedParity, portModel.SelectedDataBit, portModel.SelectedStopBit);
                        ModbusHelper.Controller = new ModbusController(port);
                        obj?.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }

        private string _selectedItem = null;

        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.SelectedItem))
            {
                ChangeUserControl();
            }
        }

        private void ChangeUserControl()
        {
            _selectedItem = Model.SelectedItem;
            if (Model.SelectedItem == Resources.Strings.AppStrings.ComboText_SerialPort)
            {
                Model.UserControl = SerialPortControl;
            }
            //else if(_model.SelectedItem==Resources.Strings.AppStrings.ComboText_TCP)
            //{
            //TODO create TCP
            //}
            else
            {
                Model.UserControl = null;
            }
            OkCommand.RaiseCanExecuteChanged();
        }
    }
}