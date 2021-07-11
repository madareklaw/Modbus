using ModbusGui.MvvmHelpers;

namespace ModbusGui.ViewModels
{
    public class MainWindowViewModel
    {
        public AppCommand<bool> SetupCommunicationsCommand { get; }
        public AppCommand<bool> OpenCommunicationsCommand { get; }
        public AppCommand<bool> CloseCommunicationsCommand { get; }


        public MainWindowViewModel()
        {
            SetupCommunicationsCommand = new AppCommand<bool>(ExecuteSetupCommunicationsCommand);
            OpenCommunicationsCommand = new AppCommand<bool>(ExecuteOpenCommunicationsCommand, CanExecuteOpenCommunicationsCommand);
            CloseCommunicationsCommand = new AppCommand<bool>(ExecuteCloseCommunicationsCommand, CanExecuteCloseCommunicationsCommand);
        }

        private bool CanExecuteCloseCommunicationsCommand(bool arg)
        {
            return ModbusHelper.Controller?.IsSerialPortOpen() ?? false;
        }

        private bool CanExecuteOpenCommunicationsCommand(bool arg)
        {
            return !ModbusHelper.Controller?.IsSerialPortOpen()??false;
        }

        private void ExecuteCloseCommunicationsCommand(bool obj)
        {
            ModbusHelper.Controller.CloseSerialPort();
            OpenCommunicationsCommand.RaiseCanExecuteChanged();
            CloseCommunicationsCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteOpenCommunicationsCommand(bool obj)
        {
            ModbusHelper.Controller.OpenSerialPort();
            OpenCommunicationsCommand.RaiseCanExecuteChanged();
            CloseCommunicationsCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteSetupCommunicationsCommand(bool obj)
        {
            new ModbusGui.Views.SetupCommunications().ShowDialog();
            OpenCommunicationsCommand.RaiseCanExecuteChanged();
            CloseCommunicationsCommand.RaiseCanExecuteChanged();
        }
    }
}