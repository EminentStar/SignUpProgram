using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram.InfoClasses
{
    public class EMailInfo : PrivateInfo
    {
        private string info;

        public EMailInfo()
        {
        }
        public EMailInfo(string paramStr)
        {
            info = paramStr;
        }

        public override bool CheckRegEx()
        {
            Regex reg = new Regex(@"[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}");
            bool isMatched = false;
            isMatched = reg.IsMatch(this.info);

            if (!isMatched)
            {
                MessageBox.Show("이메일 주소 형식이 맞지 않습니다.");
            }

            return isMatched;
        }
        public override string GetInfo()
        {
            return this.info;
        }
    }
}
