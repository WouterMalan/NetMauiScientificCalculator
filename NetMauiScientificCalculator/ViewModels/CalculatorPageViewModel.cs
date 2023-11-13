using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMauiScientificCalculator.ViewModels
{
    [INotifyPropertyChanged]
    public partial class CalculatorPageViewModel
    {

        #region Constructor

        public CalculatorPageViewModel()
        {
          
        }

        #endregion

        #region Instance Methods

        [RelayCommand]
        private void Reset()
        {
            this.CalculatedResult = "0";
            this.InputText = "";
            this.isSciOpWaiting = false;
        }

        [RelayCommand]
        private void Calculate()
        {
            if (this.InputText.Length == 0)
            {
                return;
            }

            if (isSciOpWaiting)
            {
                this.InputText += ")";
                isSciOpWaiting = false;
            }

            try
            {
                var inputString = NormalizeInputString();
                var expression = new NCalc.Expression(inputString);// this is to be replaced with a custom parser to support scientific operations
                var result = expression.Evaluate();

                this.CalculatedResult = result.ToString();
            }
            catch
            {
               this.CalculatedResult = "NaN";
            }
        }

        private string NormalizeInputString()
        {
            Dictionary<string, string> _opMapper = new()
        {
            {"×", "*"},
            {"÷", "/"},
            {"SIN", "Sin"},
            {"COS", "Cos"},
            {"TAN", "Tan"},
            {"ASIN", "Asin"},
            {"ACOS", "Acos"},
            {"ATAN", "Atan"},
            {"LOG", "Log"},
            {"EXP", "Exp"},
            {"LOG10", "Log10"},
            {"POW", "Pow"},
            {"SQRT", "Sqrt"},
            {"ABS", "Abs"},
        };

            var inputString = this.InputText.ToUpper();

            foreach (var item in _opMapper)
            {
                inputString = inputString.Replace(item.Key, item.Value);
            }

            return inputString;
        }

        [RelayCommand]
        private void Backspace()
        {
            if (this.InputText.Length > 0)
            {
                this.InputText = this.InputText.Remove(this.InputText.Length - 1);
            }
        }

        [RelayCommand]
        private void NumberInput(string input)
        {
            if (this.InputText == "0")
            {
                this.InputText = input;
            }
            else
            {
                this.InputText += input;
            }
        }

        [RelayCommand]
        private void MathOperatorInput(string input)
        {
            if (this.InputText.Length == 0)
            {
                return;
            }

            if (isSciOpWaiting)
            {
                this.InputText += ")";
                isSciOpWaiting = false;
            }

            this.InputText += input;
        }

        [RelayCommand]
        private void RegionOperatorInput(string input)
        {
            if (this.InputText.Length == 0)
            {
                return;
            }

            if (input == ")")
            {
                this.isSciOpWaiting = false;
            }

            this.InputText += input;
        }

        [RelayCommand]
        private void ScientificOperatorInput(string input)
        {
            if (this.InputText.Length == 0)
            {
                return;
            }

            if (isSciOpWaiting)
            {
                this.InputText += ")";
            }

            this.InputText += $"{input}(";
            isSciOpWaiting = true;
        }


        #endregion


        #region Instance Properties

        [ObservableProperty]
        private string inputText = "";

        private bool isSciOpWaiting = false;

        [ObservableProperty]
        private string calculatedResult = "0";

        #endregion

    }
}
