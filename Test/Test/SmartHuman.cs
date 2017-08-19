using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Test
{
    class SmartHuman : Human
    {
        public override void haveMeal()
        {
            drink();
           // eat(); 
        }
    }

}
