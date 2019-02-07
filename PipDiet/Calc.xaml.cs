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
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;


namespace PipDiet
{

    public partial class Calc : Window
    {
        public Calc() { }
        #region var declaration
        User us;
        List<Product> Products = new List<Product>(); 
        private System.Windows.Forms.NotifyIcon MyNotifyIcon; 
        Label l1, l2, l3, l5, l6, l8, txtcalc;
        TextBox txtamount;
        SolidColorBrush Mcolor, Mcolor2; 
        DataGrid products;
        List<string> lr = new List<string>();
        #endregion
        public Calc(User user)
        { 
            InitializeComponent();
            us = user;
            if (us.Role == 1)
            {
                dockAdmin.Visibility = Visibility.Collapsed;
            }

            Resource.Get_BG(canv);

            #region FindName
            l1 = (Label)FindName("label1");
            l2 = (Label)FindName("label2");
            l3 = (Label)FindName("label3"); 
            l5 = (Label)FindName("label5");
            l6 = (Label)FindName("label6"); 
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
         
        #endregion
        #endregion

        public double GetKoef(string prod,int iduser)// get koef 
        {
            double kk = 0;
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SelectKkal", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@userid", iduser);
                    cmd.Parameters.AddWithValue("@product", prod);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kk = (int)reader["Kkal"];
                        };
                    }
                }
            }
                    return kk;
        }


        private void btncalc_Click(object sender, RoutedEventArgs e) //calc kkal
        {
            if (txtamount.Text == "")
            {
                MessageBox.Show("Заполните поле количества в граммах.");
            }
            else
            {
                float ii = 0;
                double kk = GetKoef((string)txtprod.Text, us.UserId); 
                float p = (float)Convert.ToDouble(amount.Text);
                ii = ((float)p * ((float)kk / (float)100));
                txtcalc.Content = Convert.ToString(ii);
            }
        }




        private void datagrid_products_Loaded(object sender, RoutedEventArgs e) //load products
        {
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
                    }
                    datagrid_products.ItemsSource = Products;
                    conn.Close();
                }
            }

        }

        private void datagrid_products_MouseDoubleClick(object sender, MouseButtonEventArgs e) //choose product
        {
            if (datagrid_products.SelectedItem == null) return;
            Product pr = datagrid_products.SelectedItem as Product;
            txtprod.Text = pr.Name;
        }
    }
}
