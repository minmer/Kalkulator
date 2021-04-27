using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IOperator ourOperator;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Result_Click(object sender, RoutedEventArgs e)
        {
            ourOperator = FindOperator(TextBox_Entry.Text);
            TextBlock_Result.Text = ourOperator.Calculate().ToString();
        }
        private IOperator FindOperator(string entry)
        {
            if (entry.Contains("("))
            {
                return Paranthese(entry);
            }
            else if (entry.Contains("+"))
            {
                return Add(entry);
            }
            else if (entry[0] == '-')
            {
                return Negative(entry);
            }
            else if (entry.Contains("-"))
            {
                return Subtract(entry);
            }
            else if (entry.Contains("*"))
            {
                return Multiply(entry);
            }
            else if (entry.Contains("/"))
            {
                return Divide(entry);
            }
            else
            {
                return new CalcNumber { Number = double.Parse(entry) };
            }
        }
        private IOperator Add(string entry)
        {
            int index = entry.LastIndexOf('+');
            string leftString = entry.Remove(index);
            string rigthString = entry.Remove(0, index + 1);
            return new CalcAdd { LeftOperator = FindOperator(leftString), RigthOperator = FindOperator(rigthString) };
        }

        private IOperator Negative(string entry)
        {
            return new CalcNegative { Number = FindOperator(entry.Substring(1)) };
        }

        private IOperator Subtract(string entry)
        {
            int index = entry.LastIndexOf('-');
            string leftString = entry.Remove(index);
            string rigthString = entry.Remove(0, index + 1);
            return new CalcAdd { LeftOperator = FindOperator(leftString), RigthOperator = new CalcNegative { Number = FindOperator(rigthString) } };
        }
        private IOperator Multiply(string entry)
        {
            int index = entry.LastIndexOf('*');
            string leftString = entry.Remove(index);
            string rigthString = entry.Remove(0, index + 1);
            return new CalcMultiply { LeftOperator = FindOperator(leftString), RigthOperator = FindOperator(rigthString) };
        }
        private IOperator Divide(string entry)
        {
            int index = entry.LastIndexOf('/');
            string leftString = entry.Remove(index);
            string rigthString = entry.Remove(0, index + 1);
            return new CalcDivide { LeftOperator = FindOperator(leftString), RigthOperator = FindOperator(rigthString) };
        }
        private IOperator Paranthese(string entry)
        {
            int startIndex = entry.IndexOf('(');
            int endIndex = entry.LastIndexOf(')');
            string leftString = entry.Remove(startIndex);
            string innerString = entry.Substring(startIndex +1, endIndex - startIndex -1);
            string rigthString = entry.Remove(0, endIndex + 1);
            return FindOperator(leftString + FindOperator(innerString).Calculate() + rigthString);
        }
    }
}