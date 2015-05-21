using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SignUpProgram.OtherClasses
{
    public class GlobalClasses
    {
        private static GlobalClasses gClasses = new GlobalClasses();
        private GlobalClasses()
        {
        }

        public static GlobalClasses GetInstance()
        {
            return gClasses;
        }

        public void ClearTextBoxes(Grid grid)
        {
            foreach (UIElement control in grid.Children)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    TextBox txtBox = (TextBox)control;
                    txtBox.Text = String.Empty;
                }
                else if (control.GetType() == typeof(PasswordBox))
                {
                    PasswordBox pwdBox = (PasswordBox)control;
                    pwdBox.Password = String.Empty;
                }
            }
        }

    }
}
