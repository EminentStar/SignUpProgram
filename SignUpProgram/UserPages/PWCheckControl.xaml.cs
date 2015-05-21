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
    /// Interaction logic for PWCheckControlxaml.xaml
    /// </summary>
    public partial class PWCheckControl : UserControl
    {
        //PW checking requires for security

        private string id;
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        GlobalClasses gClasses = GlobalClasses.GetInstance();

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public PWCheckControl()
        {
            InitializeComponent();
        }

        public Boolean PwdCheckProcess(SqlConnection con, string paramID, string paramPwd)
        {
            Boolean rv = false;
            if(dbProcessor.CheckPassword(con, paramID, paramPwd)) //typed correct password
            {
                MessageBox.Show("비밀번호가 일치합니다.");
                rv = true;
            }
            else
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
            }
            gClasses.ClearTextBoxes(this.grid);
            return rv;
        }
    }
}
