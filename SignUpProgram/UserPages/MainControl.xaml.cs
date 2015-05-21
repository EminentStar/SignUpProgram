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
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        //MainControl is the page after you sign-in
        //you can go to the user information modifying page
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        private string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public MainControl()
        {
            InitializeComponent();
        }

        public void setID(SqlConnection con, string paramID)
        {
            this.UserID = paramID;
            idTitle.Text = this.UserID + " 님";
        }

    }
}
