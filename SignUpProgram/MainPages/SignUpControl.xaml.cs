using SignUpProgram.InfoClasses;
using SignUpProgram.OtherClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SignUpControl.xaml
    /// </summary>
    public partial class SignUpControl : UserControl
    {
        //u can input your information and get your ID
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        GlobalClasses gClasses = GlobalClasses.GetInstance();
        public SignUpControl()
        {
            InitializeComponent();

            clear_btn.Click += clear_btn_Click;
        }

        void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            gClasses.ClearTextBoxes(this.grid);
        }

        public bool SignUpProcess(SqlConnection con)
        {
            ArrayList arrList = new ArrayList();
            bool isMatched = false;

            arrList.Add(new NameInfo(name.Text));
            arrList.Add(new IDInfo(id.Text));
            arrList.Add(new PwdInfo(pwd.Password, repwd.Password));
            arrList.Add(new PhoneInfo(phone.Text));
            arrList.Add(new EMailInfo(email.Text));

            foreach (PrivateInfo element in arrList)
            {
                isMatched = element.CheckRegEx();
                if (!isMatched)
                    break;
            }
            if(isMatched)
            {
                EntireInfo Infos = new EntireInfo(id.Text, name.Text, pwd.Password, phone.Text, email.Text);
                isMatched = (dbProcessor.CreateUser(con, Infos) != 0) ? true : false;
            }

            return isMatched;
        }




        

        

        

       

        

        
    }
}
