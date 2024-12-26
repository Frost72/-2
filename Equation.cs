using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slau
{
    public class Equation
    {
        public double[] Coefs { get; set; }
        public double Result { get; set; }
        public Equation(double[] Coefs, double result)
        {
            this.Coefs = Coefs;
            this.Result = result;
        }
    }
}
