using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignUpProgram.InfoClasses
{
    public class EntireInfo
    {
        private string id;
        private string name;
        private string pwd;
        private string phone;
        private string email;
        private int isAdmin;
        private int checkSignedIn;

        public EntireInfo()
        {
        }

        public EntireInfo(string paramID, string paramName, string paramPwd, string paramPhone, string paramEmail)
        {
            this.id = paramID;
            this.name = paramName;
            this.pwd = paramPwd;
            this.phone = paramPhone;
            this.email = paramEmail;
            this.isAdmin = 0;
            this.checkSignedIn = 0;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public int IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
        public int CheckSignedIn
        {
            get { return checkSignedIn; }
            set { checkSignedIn = value; }
        }
    }
}
