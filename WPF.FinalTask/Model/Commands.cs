using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.FinalTask.Model
{
    public class CalculationModel
    {
        private string result;

        public string ValidateResultLength(string result)
        {
            string hlp="";
            int i = 0;
            if (result.Length >= 11)
            {
                if (result.Contains(".") && result.IndexOf(".") <= 10 && result.Contains("E")==false)
                {
                    while (result.Length > 11)
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                }
                else
                {
                    while (hlp.Length < (11-result.Length + result.IndexOf("E")) || result[i].Equals("E"))
                    {
                        hlp += result[i];
                        i++;
                    }
                    result = hlp + result.Substring(result.IndexOf("E"), (result.Length - result.IndexOf("E")));
                }                
            }
            return result;
        }

        public CalculationModel(string firstOperand, string secondOperand, string operation)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = string.Empty;
        }

        public CalculationModel(string firstOperand, string operation)
        {
            FirstOperand = firstOperand;
            SecondOperand = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public CalculationModel()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }
        public string ValidatedResult { get; set; }

        public void Commands()
        {
            try
            {
                switch (Operation)
                {
                    case "+":
                        result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case "-":
                        result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case "x":
                        result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case "/":
                        result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case "^":
                        result = Math.Pow(Convert.ToDouble(FirstOperand), Convert.ToDouble(SecondOperand)).ToString();
                        break;

                    case "%":
                        result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand) * 100).ToString();
                        break;

                    case "1/x":
                        result = (1 / Convert.ToDouble(FirstOperand)).ToString();
                        break;

                    case "x^2":
                        result = Math.Pow(Convert.ToDouble(FirstOperand), 2).ToString();
                        break;

                    case "vx":
                        result = Math.Sqrt(Convert.ToDouble(FirstOperand)).ToString();
                        break;

                    case "=":
                        result = Result;
                        break;
                }
               ValidatedResult = ValidateResultLength(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}