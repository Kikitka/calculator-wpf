using CalculatorWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CalculatorWPF.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private string _inputLine = "";

        public string InputLine
        {
            get => _inputLine;
            set
            {
                if (string.Equals(_inputLine, value))
                    return;

                _inputLine = value;
                OnPropertyChanged();
            }
        }

        private string _inputSymbol;

        public string InputSymbol
        {
            get => _inputSymbol;
            set
            {
                if (_inputSymbol == value)
                    return;

                _inputSymbol = value;
                OnPropertyChanged();
            }
        }

        private string _historyText = "";

        public string HistoryText
        {
            get => _historyText;
            set
            {
                if (string.Equals(_historyText, value))
                    return;

                _historyText = value;
                OnPropertyChanged();
            }
        }

        //
        private RelayCommand buttonPressCommand;
        public RelayCommand ButtonPressCommand
        {
            get
            {
                return buttonPressCommand ??
                  (buttonPressCommand = new RelayCommand(obj =>
                  {
                      string buttonValue = obj as string;

                      switch(buttonValue)
                      {
                          case "C": InputLine = "";
                              break;
                          case "<--":
                              if (InputLine.Length > 0)
                              {
                                  InputLine = InputLine.Remove(InputLine.Length - 1);
                              }
                              break;
                          case "=":
                              HistoryText += InputLine;

                              Calculator calculator = new Calculator();
                              InputLine = calculator.Calculate(InputLine).ToString();

                              HistoryText += "=" + InputLine + "\n";
                              break;
                          default: 
                              InputSymbol = buttonValue;
                              LineChecker lineChecker = new LineChecker();

                              InputLine = lineChecker.EditLine(InputLine, InputSymbol);
                              break;
                      }
                  }));
            }
        }
    }

}
