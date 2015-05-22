using SignUpProgram.InfoClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SignUpProgram
{
    public class DBProcessor
    {
        private static DBProcessor dbProcessor = new DBProcessor();

        private DBProcessor()
        {
        }

        public static DBProcessor GetInstance()
        {
            return dbProcessor;
        }

        public int IsExistsID(SqlConnection con, string paramID)
        {
            int idCnt = 0;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE user_id = '" + paramID + "'";
                idCnt = (Int32)cmd.ExecuteScalar();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return idCnt;
        }

        public int CreateUser(SqlConnection con, EntireInfo paramInfo)
        {
            int rv = 0, isExistsID = 0;

            isExistsID = IsExistsID(con, paramInfo.Id);

            if (isExistsID == 0)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    cmd.CommandText = "INSERT INTO Users VALUES('" + paramInfo.Id + "', '"
                                                                   + paramInfo.Name + "', '"
                                                                   + paramInfo.Pwd + "', '"
                                                                   + paramInfo.Phone + "', '"
                                                                   + paramInfo.Email + "',"
                                                                   + paramInfo.IsAdmin + ","
                                                                   + paramInfo.CheckSignedIn + ")";
                    rv = cmd.ExecuteNonQuery();
                }
                catch (SystemException ex)
                {
                    MessageBox.Show("중복된 아이디입니다.");
                }
            }

            return rv;
        }

        public int CheckUser(SqlConnection con, string paramID, string paramPwd)
        {
            int idCnt = 0;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE user_id = '" + paramID + "' and user_passwd = '" + paramPwd + "'";
                idCnt = (Int32)cmd.ExecuteScalar();

                if (idCnt != 0)
                    ChangeUserStateASLoggedIn(con, paramID);

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return idCnt;
        }

        public Boolean CheckPassword(SqlConnection con, string paramID, string paramPwd)
        {
            int idCnt = 0;
            Boolean isCorrect = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE user_id = '" + paramID + "' and user_passwd = '" + paramPwd + "'";
                idCnt = (Int32)cmd.ExecuteScalar();

                if (idCnt != 0)
                    isCorrect = true;

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return isCorrect;
        }

        public void ChangeUserStateASLoggedIn(SqlConnection con, string paramID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "UPDATE Users SET check_signed_in = 1 WHERE user_id = '" + paramID + "'";
                cmd.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ChangeUserStateASLoggedOut(SqlConnection con, string paramID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "UPDATE Users SET check_signed_in = 0 WHERE user_id = '" + paramID + "'";
                cmd.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string GetNameOfID(SqlConnection con, string paramID)
        {
            string name = String.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT user_name FROM Users WHERE user_id = '" + paramID + "'";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    name = reader["user_name"].ToString();
                }
                reader.Close();
            }
            catch (SystemException ex)
            {
            }
            return name;
        }

        public EntireInfo FetchInfo(SqlConnection con, string paramID)
        {
            EntireInfo entireInfo = new EntireInfo();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT * FROM Users WHERE user_id = '" + paramID + "'";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    entireInfo.Name = reader["user_name"].ToString();
                    entireInfo.Id = reader["user_id"].ToString();
                    entireInfo.Pwd = reader["user_passwd"].ToString();
                    entireInfo.Phone = reader["user_phone"].ToString();
                    entireInfo.Email = reader["user_email"].ToString();
                }
                reader.Close();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return entireInfo;
        }

        public void UpdateInformation(SqlConnection con, ArrayList infoList)
        {
            const int ID = 0, NAME = 1, PHONE = 2, EMAIL = 3;
            int rv = 0;

            NameInfo nameInfo = (NameInfo)infoList[NAME];
            IDInfo idInfo = (IDInfo)infoList[ID];
            PhoneInfo phoneInfo = (PhoneInfo)infoList[PHONE];
            EMailInfo emailInfo = (EMailInfo)infoList[EMAIL];

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "UPDATE Users SET user_name = '" + nameInfo.GetInfo() + "', user_phone = '" + phoneInfo.GetInfo() + "', user_email = '" + emailInfo.GetInfo() + "' WHERE user_id = '" + idInfo.GetInfo() + "'";
                rv = cmd.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void UpdatePassword(SqlConnection con, string paramID, string paramPwd)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "UPDATE Users SET user_passwd = '" + paramPwd + "' WHERE user_id = '" + paramID + "'";
                cmd.ExecuteNonQuery();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void removeID(SqlConnection con, string paramID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "DELETE Users WHERE user_id = '" + paramID + "'";
                cmd.ExecuteNonQuery();

            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        public Boolean SearchID(SqlConnection con, string paramName, string paramEmail)
        {
            string id = null;
            Boolean rv = false;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                cmd.CommandText = "SELECT user_id FROM Users WHERE user_name = '" + paramName + "' and user_email = '" + paramEmail + "'";
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("해당 아이디는 \"" + reader["user_id"].ToString() + "\" 입니다.");
                    rv = true;
                }
                else
                {
                    MessageBox.Show("아이디를 찾을 수 없습니다.");
                }
                reader.Close();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return rv;
        }
    }
}
