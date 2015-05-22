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
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        //u can find your ID or password
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        GlobalClasses gClasses = GlobalClasses.GetInstance();


        public SearchControl()
        {
            InitializeComponent();
        }

        public Boolean SearchIDProcess(SqlConnection con)
        {
            return dbProcessor.SearchID(con, name.Text, email.Text );
        }
    }
}
