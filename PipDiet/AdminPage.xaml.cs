using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace PipDiet
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        #region var declaration
        User us;
        List<ProductInfo> Products=new List<ProductInfo>();
        List<User> Users=new List<User>();
        User curUser;
        ProductInfo curProdInfo;


        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8, txtcalc;
        TextBox txtamount;
        SolidColorBrush Mcolor, Mcolor2;
        DataGrid products;
        List<string> lr = new List<string>();
        #endregion
        public AdminPage(User user)
        {
            InitializeComponent();
            us = user;
            LoadDG();

            Resource.Get_BG(canv);

            #region FindName
            l1 = (Label)FindName("label1");
            l2 = (Label)FindName("label2");
            l3 = (Label)FindName("label3");
            l4 = (Label)FindName("label4");
            l5 = (Label)FindName("label5");
            l6 = (Label)FindName("label6");
            l7 = (Label)FindName("label7");
            l8 = (Label)FindName("label8");
            txtcalc = (Label)FindName("calc");

            txtamount = (TextBox)FindName("amount");

            products = (DataGrid)FindName("datagrid_products");
            #endregion


            #region service
            label1.ToolTip = "Главная страница";
            label2.ToolTip = "Подобрать рецепт";
            label3.ToolTip = "Добавить продукт";  
            label5.ToolTip = "Калькулятор калорийности";
            label6.ToolTip = "Настройки"; 
            label8.ToolTip = "Выход";

            Mcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C2CAD1"));
            Mcolor2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#313937"));

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }
        #region methods_menu
        #region methods_click_mouse
        private void Label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow(us).Show();
            this.Close();
        }

 
        private void Label8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Login().Show();
            this.Close();
        }
        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new AddDiet(us).Show();
            this.Close();
        }

        private void label3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new AddProduct(us).Show();
            this.Close();
        }
        private void label177_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new AdminPage(us).Show();
            this.Close();
        }


        private void label6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Settings Menu = new Settings(us);
            Menu.Show();
            this.Close();
        }

        private void label5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Calc(us).Show();
            this.Close();
        }

        #endregion

         
        #region methods_mouse_leave

         

        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double val;
            if (!Double.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }
         
        private void btndelus_Click(object sender, RoutedEventArgs e)
        {
            if (curUser.UserId == us.UserId)
            {
                new NotificationWindow("!!!--Нельзя удалять самого себя--!!!").Show();

            }
            else { 
                using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        cmd.Parameters.AddWithValue("@userid", curUser.UserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                            };
                        }
                    }
                }
                LoadDG();
                ClearF();
                new NotificationWindow("Удален пользователь: " + curUser.Login).Show();
            }
        }

        private void btndelprod_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteKkalByID", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@prodid", curProdInfo.ProductID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        };
                    }
                }
            }
            LoadDG();
            ClearF();
            new NotificationWindow("Удален продукт: "+curProdInfo.NameProduct).Show();
        }

        private void btndelusprod_Click(object sender, RoutedEventArgs e)
        {
            if (curUser.UserId == us.UserId)
            {
                new NotificationWindow("!!!--Нельзя удалять системные продукты--!!!").Show();

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteKkal", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        cmd.Parameters.AddWithValue("@userid", curUser.UserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                            };
                        }
                    }
                }
                new NotificationWindow("Удалены все продукты пользователя: " + curUser.Login).Show();
                LoadDG();
                ClearF();
            }
        }

        private void btndelusdiet_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteDiet", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@userid", curUser.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        };
                    }
                }
                using (SqlCommand cmd = new SqlCommand("DeleteMealS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@userid", curUser.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        };
                    }
                }
            }
            LoadDG();
            ClearF();
            new NotificationWindow("Удалена диета пользователя: "+ curUser.Login).Show();
        }

        private void datagrid_users_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (datagrid_users.SelectedItem == null) return;
            curUser = datagrid_users.SelectedItem as User;
            txtdelusprod.Text = curUser.Login;
            txtdelusdiet.Text = curUser.Login;
            txtdelus.Text = curUser.Login;
        }

        private void btnxmlw1_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ImportW", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                { 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        };
                    }
                }
                new NotificationWindow("Import Succes").Show();
            }

        }

        private void btnxmls1_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("ImportS", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                { 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        };
                    }
                }
                new NotificationWindow("Import Succes").Show();
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
        }
        #endregion

        #region methods_control_window
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
        #endregion
          

        private void datagrid_products_MouseDoubleClick(object sender, MouseButtonEventArgs e) //choose product
        {
            if (datagrid_products.SelectedItem == null) return;
            curProdInfo = datagrid_products.SelectedItem as ProductInfo;
            txtdelprod.Text = curProdInfo.NameProduct; 
        }
        public void LoadDG()
        {
            datagrid_products.ItemsSource = null;
            datagrid_users.ItemsSource = null;
            if(Products!=null) Products.Clear();
            if (Users != null) Users.Clear();

            //load products
            string sqlstr = "select * from [dbo].[KkalView] order by [UserID] asc, [ProductID] asc;";

            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlstr, conn);
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductInfo d = new ProductInfo();
                            d.ProductID = (int)reader["ProductID"];
                            d.UserID = (int)reader["UserID"];
                            d.Username = (string)reader["Username"];
                            d.NameProduct = (string)reader["ProductName"];
                            d.Kkal = (int)reader["Kkal"];
                            Products.Add(d);
                        };
                    }
                    datagrid_products.ItemsSource = Products;
                }
            }

            //load users
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SelectAllUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                { 
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User d = new User();
                            d.UserId = (int)reader["UserID"];
                            d.Role = (int)reader["Admin"];
                            d.Login = (string)reader["Username"]; 

                            Users.Add(d);
                        };
                    }
                    datagrid_users.ItemsSource = Users; 
                }
            }
        }
        public void ClearF()
        { 
            txtdelus.Text = "";
            txtdelprod.Text = "";
            txtdelusdiet.Text = "";
            txtdelusprod.Text = "";
        }
    }
}
