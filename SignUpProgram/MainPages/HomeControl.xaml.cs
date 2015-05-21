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
    /// Interaction logic for HomeControl.xaml
    /// </summary>
    public partial class HomeControl : UserControl
    {
        //HomeControl is first page which users can sign in and go to sign up page and ID/PW search page
        //Login text box and buttons going to sign up page and ID/PW seach page
        
        SignUpControl signUpCtl = new SignUpControl();
        DBProcessor dbProcessor = DBProcessor.GetInstance();

        public HomeControl()
        {
            InitializeComponent();

        }

        public Boolean Login(SqlConnection con, string paramID, string paramPwd)
        {
            Boolean isConfirmed = false;

            if(dbProcessor.CheckUser(con, paramID, paramPwd) != 0)
            {
                MessageBox.Show("로그인 되었습니다.");
                isConfirmed = true;
            }
            else
            {
                MessageBox.Show("입력하신 정보가 일치하지 않습니다.");
            }

            return isConfirmed;
        }
    }
}
