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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace PipDiet
{
    public partial class Register : Window
    {
        #region var declaration
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        #endregion
        public Register()
        {
            InitializeComponent();
            #region service
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }

        private void btnGoRegister_Click(object sender, RoutedEventArgs e) //add new user
        {

            if (txtusername.Text.Length == 0)
            {
                new NotificationWindow("!!!--Введите логин--!!!");
                txtusername.Focus();
            }
            else
            {
                if (txtpassword.Password.Length == 0)
                {
                    new NotificationWindow("!!!--Введите пароль--!!!");
                    txtpassword.Focus();
                }
                else
                {
                    if (!((bool)rbfem.IsChecked) && !((bool)rbmale.IsChecked))
                    {
                        new NotificationWindow("!!!--Выберите пол--!!!");
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                        {
                            using (SqlCommand cmd = new SqlCommand("AddNewUser", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            })
                            {
                                double bmr = 1500;
                                conn.Open();
                                cmd.Parameters.AddWithValue("@username", txtusername.Text);
                                cmd.Parameters.AddWithValue("@password", Convert.ToString(txtpassword.Password));
                                if ((bool)rbfem.IsChecked)
                                {
                                    bmr = (9.99 * (Convert.ToInt32(txtweight.Text)) + 6.25 * (Convert.ToInt32(txtgrowth.Text)) - 4.92 * Convert.ToInt32(txtage.Text)) - 161;
                                    cmd.Parameters.AddWithValue("@sex", "Female");
                                    cmd.Parameters.AddWithValue("@idealweight", (((Convert.ToInt32(txtgrowth.Text)) * 3.5 / 2.54) - 108) * 0.453);
                                }
                                else if ((bool)rbmale.IsChecked)
                                {
                                    bmr = (9.99 * (Convert.ToInt32(txtweight.Text)) + 6.25 * (Convert.ToInt32(txtgrowth.Text)) - 4.92 * Convert.ToInt32(txtage.Text)) + 5;
                                    cmd.Parameters.AddWithValue("@sex", "Male");
                                    cmd.Parameters.AddWithValue("@idealweight", (((Convert.ToInt32(txtgrowth.Text)) * 4 / 2.54) - 128) * 0.453);
                                }
                                cmd.Parameters.AddWithValue("@calories1", bmr * 1.2);
                                cmd.Parameters.AddWithValue("@calories2", bmr * 1.5);
                                cmd.Parameters.AddWithValue("@calories3", bmr * 1.9);
                                cmd.Parameters.AddWithValue("@birth", Convert.ToInt32(txtage.Text));
                                cmd.Parameters.AddWithValue("@growth", Convert.ToInt32(txtgrowth.Text));

                                cmd.Parameters.AddWithValue("@admin", 1);


                                using (var reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        new Login().Show();
                                        this.Close();
                                    }
                                    reader.Close();
                                    new NotificationWindow("Вы успешно зарегистрировались!").Show();
                                    new Login().Show();
                                    this.Close();
                                }
                            }

                        }
                    }
                }
                //Reset();
            }

        }

        #region methods_control_window
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           //clsDB.Close_DB_Connection();
            Close();
            //Environment.Exit(0);
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Minimize Sucessful";
                MyNotifyIcon.BalloonTipText = "Minimized the app ";
                MyNotifyIcon.ShowBalloonTip(400);
                MyNotifyIcon.Visible = true;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
        #endregion

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
        private void txtfrostid_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double val;
            if (!Double.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
    }
}
