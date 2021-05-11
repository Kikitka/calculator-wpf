using CalculatorWPF.Model;
using System.Linq;
using System.Text.RegularExpressions;

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
        private RelayCommand _buttonPressCommand;
        public RelayCommand ButtonPressCommand
        {
            get
            {
                return _buttonPressCommand ??
                  (_buttonPressCommand = new RelayCommand(obj =>
                  {
                      var buttonValue = obj as string;

                      switch(buttonValue)
                      {
                          case "C": 
                              InputLine = string.Empty;
                              break;
                          case "<--":
                              if (InputLine.Length > 0)
                              {
                                  InputLine = InputLine.Remove(InputLine.Length - 1);
                              }
                              break;
                          case "=":
                              var num = new Regex(@"\d$");
                              if (InputLine.EndsWith(")") || num.IsMatch(InputLine.Last().ToString()))
                              {
                                  HistoryText += InputLine;

                                  var calculator = new Calculator();
                                  InputLine = calculator.Calculate(InputLine).ToString();

                                  HistoryText += "=" + InputLine + "\n";
                              }
                              break;
                          default: 
                              InputSymbol = buttonValue;
                              var lineChecker = new LineChecker();

                              InputLine = lineChecker.EditLine(InputLine, InputSymbol);
                              break;
                      }
                  }));
            }
        }
    }

}
