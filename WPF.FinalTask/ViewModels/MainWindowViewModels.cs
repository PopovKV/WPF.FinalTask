using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.FinalTask.Model;

namespace WPF.FinalTask.ViewModels
{
    class MainWindowViewModels : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }             

        private CalculationModel calculation;
       
        private string lastOperation;
        private string display;
        private bool newDisplayRequired = false;

        public string FirstOperand
        {
            get => calculation.FirstOperand;
            set { calculation.FirstOperand = value; }
        }

        public string SecondOperand
        {
            get => calculation.SecondOperand;
            set { calculation.SecondOperand = value; }
        }

        public string Operation
        {
            get => calculation.Operation;
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get => lastOperation;
            set { lastOperation = value; }
        }

        public string Result
        {
            get => calculation.Result;
        }

        public string ValidatedResult
        {
            get => calculation.ValidatedResult;
        }

        public string Display
        {
            get => display;
            set
            {
                display = value;
                OnPropertyChanged();
            }
        }

        //Команды для кнопок ввода текста

        public ICommand DigitButtonPressCommand { get; }
        private void OnDigitCommandExecute(object p)
        {
            string buttonContent = Convert.ToString(p);
            switch (buttonContent)
            {
                case ".":
                    if (Display.Contains(".") == false && Display.Length < 11)
                    {
                        Display += buttonContent;
                    }
                    if (newDisplayRequired)
                    {
                        Display = "0.";
                    }
                    break;
                case "+/-":
                    Display = Convert.ToString(-1 * Convert.ToDouble(Display));
                    break;
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    break;
                case "CE":
                    if (Display.Length > 1)
                    {
                        Display = Display.Remove(Display.Length - 1, 1);
                    }
                    else
                    {
                        Display = "0";
                    }
                    break;
                default:
                    if (Display == "0" || newDisplayRequired)
                    {
                        Display = buttonContent;
                    }
                    else
                    {
                        if (Display.Length < 11)
                        {
                            Display += buttonContent;
                        }
                    }
                    break;
            }
            if (LastOperation == null)
            {
                FirstOperand = Display;
            }
            else
            {
                SecondOperand = Display;
            }
            newDisplayRequired = false;
        }

        private bool CanDigitCommandExecuted(object p)
        {
            return true;
        }

        //Команды для кнопок с математическими действиями

        public ICommand OperationButtonPressCommand { get; }

        private void OnOperationCommandExecute(object p)
        {
            if (FirstOperand == string.Empty || LastOperation == "=")
            {
                FirstOperand = display;
                LastOperation = (string)p;
            }
            else
            {
                SecondOperand = display;
                Operation = lastOperation;
                calculation.Commands();

                LastOperation = (string)p;
                Display = Result;
                FirstOperand = display;
                Display = ValidatedResult;
            }
            newDisplayRequired = true;
        }

        private bool CanSingleOperationCommandExecuted(object p)
        {
            return true;
        }

        public ICommand SingleOperationButtonPressCommand { get; }

        private void OnSingleOperationCommandExecute(object p)
        {
            FirstOperand = Display;
            Operation = (string)p;
            calculation.Commands();

            LastOperation = "=";
            Display = Result;
            FirstOperand = display;
            Display = ValidatedResult;
            newDisplayRequired = true;
        }

        private bool CanOperationCommandExecuted(object p)
        {
            return true;
        }
        public MainWindowViewModels()
        {
            this.calculation = new CalculationModel();
            this.display = "0";
            this.FirstOperand = string.Empty;
            this.SecondOperand = string.Empty;
            this.Operation = string.Empty;
            this.lastOperation = string.Empty;
            DigitButtonPressCommand = new RelayCommand(OnDigitCommandExecute, CanDigitCommandExecuted);
            OperationButtonPressCommand = new RelayCommand(OnOperationCommandExecute, CanOperationCommandExecuted);
            SingleOperationButtonPressCommand = new RelayCommand(OnSingleOperationCommandExecute, CanSingleOperationCommandExecuted);
        }
    }
}
