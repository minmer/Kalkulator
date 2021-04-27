using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    public class CalcNumber : IOperator
    {
        public double Number { get; set; }
        double IOperator.Calculate()
        {
            return Number;
        }
    }
}
