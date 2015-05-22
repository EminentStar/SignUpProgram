using SignUpProgram.InfoClasses;
using SignUpProgram.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for PWModfyControl.xaml
    /// </summary>
    public partial class PWModfyControl : UserControl
    {
        //you can change your password
        private string id;
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        GlobalClasses gClasses = GlobalClasses.GetInstance();

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public PWModfyControl()
        {
            InitializeComponent();
        }

        public Boolean ModifyProcess(SqlConnection con, string paramID)
        {
            Boolean rv = false;
            PwdInfo pwdInfo = new PwdInfo(pwd_new.Password, pwd_new_re.Password);

            //Original password and new password are both correct
            rv = (dbProcessor.CheckPassword(con, paramID, pwd_origin.Password) && pwdInfo.CheckRegEx()) ? true : false;

            if (rv && pwd_origin.Password.Equals(pwd_new.Password))
            {
                MessageBox.Show("같은 비밀번호로 변경할 수 없습니다.");
            }
            else if (rv) // if orginal password was correct
            {
                MessageBox.Show("비밀번호변경에 성공하였습니다.");
                dbProcessor.UpdatePassword(con, paramID, pwdInfo.GetInfo());
            }
            else
            {
                MessageBox.Show("비밀번호변경에 실패하였습니다.");
            }

            gClasses.ClearTextBoxes(this.grid);

            return rv;
        }
    }
}