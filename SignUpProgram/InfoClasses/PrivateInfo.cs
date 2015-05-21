using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpProgram.InfoClasses
{
    public abstract class PrivateInfo
    {
        public abstract bool CheckRegEx();
        public abstract string GetInfo();
    }
}
