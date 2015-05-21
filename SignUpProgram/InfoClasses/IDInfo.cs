using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram.InfoClasses
{
    public class IDInfo : PrivateInfo
    {
        private string info;

        public IDInfo()
        {
        }
        public IDInfo(string paramStr)
        {
            info = paramStr;
        }

        public override bool CheckRegEx()
        {
            Regex reg = new Regex("[a-z0-9]{4,16}");
            bool isMatched = false;
            isMatched = reg.IsMatch(this.info);

            if (!isMatched)
            {
                MessageBox.Show("아이디 형식이 맞지 않습니다.");
            }

            return isMatched;
        }
        public override string GetInfo()
        {
            return this.info;
        }
    }
}
