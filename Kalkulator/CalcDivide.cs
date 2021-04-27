using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalkulator
{
    public class CalcDivide : IOperator
    {
        public IOperator LeftOperator { get; set; }
        public IOperator RigthOperator { get; set; }
        double IOperator.Calculate()
        {
            return LeftOperator.Calculate() / RigthOperator.Calculate();
        }
    }
}
