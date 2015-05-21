using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram.InfoClasses
{
    public class PwdInfo : PrivateInfo
    {
        private string info1;
        private string info2;
        public PwdInfo()
        {
        }
        public PwdInfo(string paramStr1, string paramStr2)
        {
            info1 = paramStr1;
            info2 = paramStr2;
        }

        public override bool CheckRegEx()
        {
            Regex reg = new Regex("[a-zA-Z0-9]{8,16}");
            bool checkPwd = info1.Equals(info2);

            if (!checkPwd)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
            }
            else if (!(checkPwd = (reg.IsMatch(info1) && checkPwd) ? true : false))
            {
                MessageBox.Show("비밀번호 형식이 맞지 않습니다.");
            }

            return checkPwd;
        }
        public override string GetInfo()
        {
            return this.info1;
        }
    }
}
