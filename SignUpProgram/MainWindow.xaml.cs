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
using Microsoft.VisualBasic;
using SignUpProgram.OtherClasses;


namespace SignUpProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //NavigationService service;
        HomeControl homeCtl = new HomeControl();
        SignUpControl signUpCtl = new SignUpControl();
        MainControl mainCtl = new MainControl();
        DBProcessor dbProcessor = DBProcessor.GetInstance();
        ModifyControl modifyCtl = new ModifyControl();
        PWCheckControl pwdCheckCtl = new PWCheckControl();
        PWModfyControl pwdModifyCtl = new PWModfyControl();
        GlobalClasses gClasses = GlobalClasses.GetInstance();

        SqlConnection con;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = @"server=novak.sejong.ac.kr;database=Junkyu;uid=Junkyu;pwd=qkrwnsrb1";
            con = new SqlConnection(connectionString);
            con.Open();

            mainGrid.Children.Add(homeCtl);

            //HomeControl's handler;
            homeCtl.sign_up.Click += sign_up_Click;
            homeCtl.login_btn.Click += login_btn_Click;
            homeCtl.pwd.KeyDown += pwd_KeyDown;

            //SignUpControl's Handlers
            signUpCtl.back_btn.Click += back_btn_Click;
            signUpCtl.signUp_btn.Click += signUp_btn_Click;

            //MainControl's Handlers
            mainCtl.logout_btn.Click += logout_btn_Click;
            mainCtl.change_btn.Click += change_btn_Click;

            //PwdCheckControl's Handlers
            pwdCheckCtl.pwdCheck_btn.Click += pwdCheck_btn_Click;
            pwdCheckCtl.pwdCancel_btn.Click += pwdCancel_btn_Click;
            pwdCheckCtl.pwd_check.KeyDown += pwd_check_KeyDown;

            //ModifyControl's Handlers
            modifyCtl.modify_btn.Click += modify_btn_Click;
            modifyCtl.modifyCancel_btn.Click += modifyCancel_btn_Click;
            modifyCtl.pwdChange_btn.Click += pwdChange_btn_Click;
            modifyCtl.remove_btn.Click += remove_btn_Click;


            //PWModifyControl's Handlers
            pwdModifyCtl.pwdChangeSuccess_btn.Click += pwdChangeSuccess_btn_Click;
            pwdModifyCtl.pwdChangeCancel_btn.Click += pwdChangeCancel_btn_Click;


            Closed += MainWindow_Closed;
        }


        void MainWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("This window's being closing");
            con.Close();
        }

        //HomeControl's Handlers***********************************
        void sign_up_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(signUpCtl);
        }

        void login_btn_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        public void Login()
        {
            if (homeCtl.Login(con, homeCtl.id.Text, homeCtl.pwd.Password)) // logined
            {
                mainCtl.setID(con, homeCtl.id.Text);
                dbProcessor.ChangeUserStateASLoggedIn(con, homeCtl.id.Text);
                gClasses.ClearTextBoxes(homeCtl.grid);

                mainGrid.Children.Clear();
                mainGrid.Children.Add(mainCtl);
            }
            else //failed to login
            {
            }
        }

        public void pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        //SignUpControl's Handlers***********************************
        void signUp_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("signUp button is clicked");

            if (signUpCtl.SignUpProcess(con))//Success creating a user
            {
                //you need to login in
                MessageBox.Show("회원가입에 성공하였습니다.");
                mainCtl.setID(con, signUpCtl.id.Text);
                dbProcessor.ChangeUserStateASLoggedIn(con, mainCtl.UserID);
                gClasses.ClearTextBoxes(signUpCtl.grid);

                mainGrid.Children.Clear();
                mainGrid.Children.Add(mainCtl);
            }
        }

        void back_btn_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Goback();
            MessageBox.Show("back button is clicked");
            mainGrid.Children.Clear();
            mainGrid.Children.Add(homeCtl);
        }

        //MainControl's Handlers***********************************
        void logout_btn_Click(object sender, RoutedEventArgs e)
        {
            dbProcessor.ChangeUserStateASLoggedOut(con, mainCtl.UserID);
            MessageBox.Show(mainCtl.UserID + " 님 감사합니다. 좋은하루 되세요. :)");
            mainCtl.idTitle.Text = String.Empty;

            mainGrid.Children.Clear();
            mainGrid.Children.Add(homeCtl);
        }

        public void change_btn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            pwdCheckCtl.Id = mainCtl.UserID;
            mainGrid.Children.Add(pwdCheckCtl);
        }

        //PWCheckControl's Handlers
        public void pwdCheck_btn_Click(object sender, RoutedEventArgs e)
        {
            PwdCheck();
        }

        public void pwdCancel_btn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(mainCtl);
        }

        public void pwd_check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                PwdCheck();
        }

        public void PwdCheck()
        {
            if (pwdCheckCtl.PwdCheckProcess(con, mainCtl.UserID, pwdCheckCtl.pwd_check.Password)) //correct pasword
            {
                mainGrid.Children.Clear();
                modifyCtl.Id = mainCtl.UserID;
                modifyCtl.SetInformation(con, mainCtl.UserID);
                mainGrid.Children.Add(modifyCtl);
            }
        }

        //ModifyControl's Handlers
        public void modify_btn_Click(object sender, RoutedEventArgs e)
        {
            if (modifyCtl.UpdateProcess(con, mainCtl.UserID)) //Success modifying information
            {
                mainGrid.Children.Clear();
                MessageBox.Show("회원정보수정에 성공하였습니다.");
                mainGrid.Children.Add(mainCtl);
            }
        }

        public void modifyCancel_btn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(mainCtl);
        }
        public void pwdChange_btn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            pwdModifyCtl.Id = mainCtl.UserID;
            mainGrid.Children.Add(pwdModifyCtl);
        }
        public void remove_btn_Click(object sender, RoutedEventArgs e)
        {
            string password = Microsoft.VisualBasic.Interaction.InputBox("비밀번호를 입력해주세요.", "계정을 삭제하시겠습니까?");
            Boolean isCorrect = dbProcessor.CheckPassword(con, mainCtl.UserID, password);

            if (isCorrect)
            {
                //remove the id
                dbProcessor.removeID(con, mainCtl.UserID);
                MessageBox.Show("계정탈퇴에 성공하였습니다.");
                mainGrid.Children.Clear();
                mainGrid.Children.Add(homeCtl);
            }
            else
            {
                MessageBox.Show("비밀번호가 틀립니다.");
            }
        }

        //PWModifyControl's Handlers

        public void pwdChangeSuccess_btn_Click(object sender, RoutedEventArgs e)
        {
            if (pwdModifyCtl.ModifyProcess(con, mainCtl.UserID))//Success modifying password
            {
                mainGrid.Children.Clear();
                mainGrid.Children.Add(modifyCtl);
            }
        }
        public void pwdChangeCancel_btn_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(modifyCtl);

        }

    }
}
