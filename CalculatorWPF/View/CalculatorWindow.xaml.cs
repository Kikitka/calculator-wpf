using System;
using System.Windows;
using CalculatorWPF.ViewModel;

namespace CalculatorWPF.View
{
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
            DataContext = new CalculatorViewModel();
        }
    }
}
