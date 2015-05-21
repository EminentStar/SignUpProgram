using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram.InfoClasses
{
    public class NameInfo : PrivateInfo
    {
        private string info;

        public NameInfo()
        {
        }
        public NameInfo(string paramStr)
        {
            info = paramStr;
        }

        public override bool CheckRegEx()
        {
            Regex reg = new Regex("[가-힣]{2,18}");
            bool isMatched = false;
            isMatched = reg.IsMatch(this.info);

            if (!isMatched)
            {
                MessageBox.Show("이름 형식이 맞지 않습니다.");
            }

            return isMatched;
        }

        public override string GetInfo()
        {
            return this.info;
        }
    }
}
