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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace PipDiet
{
    public partial class AddProduct : Window
    {
        #region var declaration
        User us;
        List<Product> Products = new List<Product>();
        Product curProduct;

        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush(); 
        #endregion
        public AddProduct(User user)
        {
            InitializeComponent();
            us = user;
            if (us.Role == 1)
            {
                dockAdmin.Visibility = Visibility.Collapsed;
            }

            txtost.Content = (us.DailyCaloriesNormNorm - GetOst()) + " kkal";

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
            #endregion 

            Fill();
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
        #region small logica
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboProducts.SelectedItem == null || txtamount.Text == "")
            {
                new NotificationWindow("!!!--Заполните все поля--!!!").Show();
                comboProducts.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNewMealS", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@userid", us.UserId);
                        cmd.Parameters.AddWithValue("@prodid", curProduct.ProductID);
                        cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(txtamount.Text));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            { }
                            reader.Close();
                        }
                        conn.Close();
                    }
                    new NotificationWindow("В базу данных добавлена запись о приеме пищи: " + curProduct.Name + " " + txtamount.Text + " г").Show();
                comboProducts.SelectedItem = null;
                txtamount.Text = "";
                txtadd2.Content = "♥";
                    txtost.Content = (us.DailyCaloriesNormNorm-GetOst())+" kkal";
                }

            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtname.Text == "" || txtkkal1.Text == "")
            {
                new NotificationWindow("!!!--Заполните все поля--!!!").Show();
                txtname.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNewProduct", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@userid", us.UserId);
                        cmd.Parameters.AddWithValue("@name", txtname.Text);
                        cmd.Parameters.AddWithValue("@kkal", Convert.ToInt32(txtkkal1.Text));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            { }
                            reader.Close();
                        }
                        conn.Close();
                    }
                new NotificationWindow("В базу данных добавлено: " + txtname.Text + " " + txtkkal1.Text + " kkal/100g").Show(); 
                txtname.Text = "";
                txtkkal1.Text = "";
                txtadd.Content = "♥";
                }


                Fill();
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            comboProducts.SelectedItem = null;
            txtamount.Text = ""; 
            txtkkal1.Text = "";
            txtname.Text = ""; 
            txtsize1.Text = "";
            txtsize2.Text = "";
            txtsize3.Text = "";
            txtsize4.Text = "";
            txtweight.Text = ""; 
            txtadd.Content = "";
            txtadd1.Content = "";
            txtadd12.Content = "";
            txtadd2.Content = "";

        }  
        public void Fill()
        {
            //load product
            comboProducts.SelectedItem = null;
            Products.Clear();
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SelectUsersProduct", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@userid", us.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product prod = new Product();

                            prod.ProductID = (int)reader["ProductId"];
                            prod.Name = reader["Name"].ToString();
                            prod.Kkal = (int)reader["Kkal"];
                            prod.UserID = (int)reader["UserID"];

                            Products.Add(prod);
                        };
                        conn.Close();
                    }
                    comboProducts.ItemsSource = Products;
                }
            }
        }
        #endregion

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

        private void comboProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboProducts.SelectedItem == null) return;
            curProduct = comboProducts.SelectedItem as Product;
        }

        private void btnWeight_Click(object sender, RoutedEventArgs e)
        {
            if (txtweight.Text == "")
            {
                new NotificationWindow("!!!--Заполните все поля--!!!").Show();
                txtweight.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNewWeightS", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@userid", us.UserId); 
                        cmd.Parameters.AddWithValue("@weight", Convert.ToInt32(txtweight.Text));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            { }
                            reader.Close();
                        }
                        conn.Close();
                    }
                }

                new NotificationWindow("В базу данных добавлена запись о весе: " + txtweight.Text + " " + " кг").Show();
                txtweight.Text = ""; 
                txtadd1.Content = "♥";
            }
        }
        public int GetOst()
        {
            int i = 0;
            string sqlstr2 = "select [dbo].[SelectOstKkalUser](" + us.UserId + ");";

            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlstr2, conn);
                i = Convert.ToInt32(command.ExecuteScalar());
            }
            return i;

        }


        private void btnSize_Click(object sender, RoutedEventArgs e)
        {
            if (txtsize1.Text == "" || txtsize2.Text == "" || txtsize3.Text == "" || txtsize4.Text == "" )
            {
                new NotificationWindow("!!!--Заполните все поля--!!!").Show();
                txtsize1.Focus();
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                {
                    using (SqlCommand cmd = new SqlCommand("AddNewSizeS", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@userid", us.UserId);
                        cmd.Parameters.AddWithValue("@size1", Convert.ToInt32(txtsize1.Text));
                        cmd.Parameters.AddWithValue("@size2", Convert.ToInt32(txtsize2.Text));
                        cmd.Parameters.AddWithValue("@size3", Convert.ToInt32(txtsize3.Text));
                        cmd.Parameters.AddWithValue("@size4", Convert.ToInt32(txtsize4.Text));
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            { }
                            reader.Close();
                        }
                        conn.Close();
                    }
                }

                new NotificationWindow("В базу данных добавлена запись об измерениях").Show();
                txtsize1.Text = "";
                txtsize2.Text = "";
                txtsize3.Text = "";
                txtsize4.Text = "";
                txtadd1.Content = "♥";
            }
        }

        private void label4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //AddRecipe Menu = new AddRecipe();
            //Menu.Show();
            this.Close();
        }
        #endregion  

        private void Quest(object sender, MouseButtonEventArgs e)
        {
            //Quest objModal = new Quest();
            //objModal.Owner = this;
            //ApplyEffect(this);

            //objModal.ShowDialog();

            //ClearEffect(this);
        }
        private void ApplyEffect(Window win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 4;
            win.Effect = objBlur;
        }
        private void ClearEffect(Window win)
        {
            win.Effect = null;
        }
        private void About_p(object sender, MouseButtonEventArgs e)
        {
            //AboutBtn objModal = new AboutBtn();
            //objModal.Owner = this;
            //ApplyEffect(this);

            //objModal.ShowDialog();

            //ClearEffect(this);
        }

        private void txtamount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double val;
            if (!Double.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        } 

        #region methods_mouse_leave
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
    }
}
