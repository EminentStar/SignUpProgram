using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram.InfoClasses
{
    public class PhoneInfo : PrivateInfo
    {
        private string info;

        public PhoneInfo()
        {
        }
        public PhoneInfo(string paramStr)
        {
            info = paramStr;
        }

        public override bool CheckRegEx()
        {
            Regex reg = new Regex(@"\d{3}-\d{3,4}-\d{4}");
            bool isMatched = false;
            isMatched = reg.IsMatch(this.info);

            if (!isMatched)
            {
                MessageBox.Show("휴대폰 번호 형식이 맞지 않습니다.");
            }

            return isMatched;
        }
        public override string GetInfo()
        {
            return this.info;
        }
    }
}
