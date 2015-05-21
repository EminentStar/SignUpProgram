using SignUpProgram.InfoClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for ModifyControl.xaml
    /// </summary>
    public partial class ModifyControl : UserControl
    {
        private string id;
        private EntireInfo entireInfo;
        DBProcessor dbProcessor = DBProcessor.GetInstance();

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public ModifyControl()
        {
            //you can change your information
            //you can go to the withdrawal page
            InitializeComponent();
        }

        public void SetInformation(SqlConnection con, string paramID)
        {
            entireInfo = dbProcessor.FetchInfo(con, paramID);

            user_id.Text = entireInfo.Id;
            name.Text = entireInfo.Name;
            phone.Text = entireInfo.Phone;
            email.Text = entireInfo.Email;
        }

        public Boolean UpdateProcess(SqlConnection con, string paramID)
        {
            ArrayList infoList = new ArrayList();
            Boolean isMatched = false;

            infoList.Add(new IDInfo(user_id.Text));
            infoList.Add(new NameInfo(name.Text));
            infoList.Add(new PhoneInfo(phone.Text));
            infoList.Add(new EMailInfo(email.Text));

            foreach(PrivateInfo element in infoList )
            {
                if (element.CheckRegEx())
                    isMatched = true;
                else
                    break;
            }
            if(isMatched)
            {
                dbProcessor.UpdateInformation(con, infoList);
            }

            return isMatched;

        }

       

    }
}
