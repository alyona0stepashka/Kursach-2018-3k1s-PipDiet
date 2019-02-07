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
    /// <summary>
    /// Логика взаимодействия для AddDiet.xaml
    /// </summary>
    public partial class AddDiet : Window
    {
        public AddDiet() { }
        #region var declaration
        User us;
        List<Product> Products = new List<Product>();
        List<DietInfo> DietInf = new List<DietInfo>();

        int curDay;
        Product curProduct; 
        List<Meal> Meals = new List<Meal>();
        List<Meal> Meals2 = new List<Meal>();
        List<Diet> Diets = new List<Diet>();


        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8, txtcalc;
        TextBox txtamount;
        SolidColorBrush Mcolor, Mcolor2;
        DataGrid products;
        List<string> lr = new List<string>();
        #endregion
        public AddDiet(User user)
        {
            InitializeComponent();
            us = user;
            if (us.Role==1)
            { 
                dockAdmin.Visibility=Visibility.Collapsed;
            }

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


      
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }



        #region methods_mouse_leave

         

        private void amount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double val;
            if (!Double.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

  

        private void comboDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboDay.SelectedItem == null) return;
            curDay = (int)comboDay.SelectedItem;
        }

        private void btnAddMeal_Click(object sender, RoutedEventArgs e)
        {
            if (Meals.Count == 0)
            {
                new NotificationWindow("!!!--Выберите продукты--!!!").Show();
                txtAmount.Text = "";
                txtMeal.Text = "";
                comboProd.SelectedItem = null;
                comboDay.SelectedItem = null;
                comboProd.Focus();
            }
            else if (txtMeal.Text == "")
            {
                new NotificationWindow("!!!--Выберите номер приема пищи --!!!").Show();
                txtAmount.Text = "";
                txtMeal.Text = "";
                comboProd.SelectedItem = null;
                comboDay.SelectedItem = null;
                txtMeal.Focus();
            }
            else
            {

                foreach (var prods in Meals)
                {
                    Meal m = new Meal(Convert.ToInt32(txtMeal.Text), prods.Product, prods.Kkal, prods.Amount);
                    m.MealId = prods.MealId;
                    Meals2.Add(m);
                }
                txtMeal.Text = "";
                datagrid_prods.ItemsSource = null;
                Meals.Clear();
                datagrid_prods.ItemsSource = Meals2;
            }
            LoadDG();
        }

        private void btnAddDay_Click(object sender, RoutedEventArgs e)
        {
            if (Meals2.Count == 0)
            {
                new NotificationWindow("!!!--Выберите продукты--!!!").Show();
                txtAmount.Text = "";
                txtMeal.Text = "";
                comboProd.SelectedItem = null;
                comboDay.SelectedItem = null;
                comboProd.Focus();
            }
            else if (curDay == 0)
            {
                new NotificationWindow("!!!--Выберите номер дня--!!!").Show();
                txtAmount.Text = "";
                txtMeal.Text = "";
                comboProd.SelectedItem = null;
                comboDay.SelectedItem = null;
                comboDay.Focus();
            }
            else
            {
                curDay = (int)comboDay.SelectedItem;
                foreach (var prods in Meals2)
                {
                    Diet d = new Diet();
                    d.DayNum = curDay;
                    d.MealN = prods;

                    string sqlstr = "select [dbo].[SelectKkalRes](" + prods.Product.ProductID + "," + prods.Amount + ");";
                    string sqlstr2 = "select [dbo].[SelectDietId](" + us.UserId + "," + prods.MealId + ");";

                    using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(sqlstr2, conn);
                        int is_empty = Convert.ToInt32(command.ExecuteScalar());
                        d.DietId = is_empty;

                        double kk = GetKoef(prods.Product.Name, us.UserId);
                        float p = (float)Convert.ToDouble(prods.Amount);
                        d.Kkal = (int)(p * (kk / 100));
                        using (SqlCommand cmd = new SqlCommand("AddNewDietRecord", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            cmd.Parameters.AddWithValue("@userid", us.UserId);
                            cmd.Parameters.AddWithValue("@mealid", prods.MealId);
                            cmd.Parameters.AddWithValue("@kkal", d.Kkal);
                            cmd.Parameters.AddWithValue("@mealnum", d.MealN.MealN);
                            cmd.Parameters.AddWithValue("@daynum", d.DayNum);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                };
                            }
                        }
                    }
                    Diets.Add(d);

                }
                comboDay.SelectedItem = null;
                datagrid_prods.ItemsSource = null;
                Meals2.Clear();


            }
            LoadDG();
        }
         

        private void comboProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboProd.SelectedItem == null) return;
            curProduct = comboProd.SelectedItem as Product;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //load day
            List<int> l = new List<int>();
            for (int i = 1; i <= 7; i++)
            {
                l.Add(i);
            }
            comboDay.ItemsSource = l;
            //load product
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
                    comboProd.ItemsSource = Products;
                }
            }
            //load datagrid
            LoadDG();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteDiet", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@userid", us.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        { };
                    }
                }
                using (SqlCommand cmd = new SqlCommand("DeleteMeal", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@userid", us.UserId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        { };
                    }
                    conn.Close();
                }
            }
            Diets.Clear();
            LoadDG();
        }

        private void btnAddProd_Click(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text.Length == 0)
            {
                new NotificationWindow("!!!--Введите кол-во в граммах--!!!");
                txtAmount.Text = "";
                txtMeal.Text = "";
                comboProd.SelectedItem = null;
                comboDay.SelectedItem = null;
                txtAmount.Focus();
            }
            else
            {
                if (curProduct == null)
                {
                    new NotificationWindow("!!!--Выберите продукт--!!!");
                    txtAmount.Text = "";
                    txtMeal.Text = "";
                    comboProd.SelectedItem = null;
                    comboDay.SelectedItem = null;
                    comboProd.Focus();
                }
                else
                {
                    curProduct = comboProd.SelectedItem as Product;
                    using (SqlConnection conn = new SqlConnection(Metadata.ConnectionString.defaultString))
                    {
                        using (SqlCommand cmd = new SqlCommand("AddNewMeal", conn)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                        {
                            conn.Open();
                            cmd.Parameters.AddWithValue("@prodid", curProduct.ProductID);
                            cmd.Parameters.AddWithValue("@amount", Convert.ToInt32(txtAmount.Text));
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                }
                                reader.Close();
                            }
                        }
                    }
                    Meal m = new Meal(0, curProduct, Convert.ToInt32(Convert.ToInt32(txtAmount.Text)/100 * GetKoef(curProduct.Name, us.UserId)), Convert.ToInt32(txtAmount.Text));


                    string sqlstr = "select [dbo].[SelectMealId](" + curProduct.ProductID + "," + txtAmount.Text + ");";

                    using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
                    {
                        conn.Open();
                        SqlCommand command = new SqlCommand(sqlstr, conn);
                        int is_empty = Convert.ToInt32(command.ExecuteScalar());
                        m.MealId = is_empty;
                        conn.Close();
                    }
                    Meals.Add(m);
                    txtAmount.Text = "";
                    comboProd.SelectedItem = null;
                }
            }
            datagrid_prods.ItemsSource = null;
            datagrid_prods.ItemsSource = Meals;
            LoadDG();
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

        public double GetKoef(string prod, int iduser)// get koef 
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
         
        
        public void LoadDG()
        {
            datagrid_products.ItemsSource = null;
            DietInf.Clear();

            string sqlstr = "select * from [dbo].[DietView] order by [DietNum] asc, [MealNum] asc;";

            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlstr, conn);
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if ((int)reader["UserID"] == us.UserId)
                            {
                                DietInfo d = new DietInfo();
                                d.DayN = (int)reader["DietNum"];
                                d.MealN = (int)reader["MealNum"];
                                d.ProductName = (string)reader["NameProd"];
                                d.Amount = (decimal)reader["Amount"];
                                d.Kkal = (int)reader["Kkal"];
                                DietInf.Add(d);
                            }
                        };
                    }
                    datagrid_products.ItemsSource = DietInf;
                }
            }

            sqlstr = "select [dbo].[SelectDietKkalAv](" + us.UserId + ");";

            using (SqlConnection conn = new SqlConnection(Metadata.CurrentConnectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(sqlstr, conn);
                txtSumKkal.Content = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
            }
            LoadAdvice();
        }
        public void LoadAdvice()
        {
            int kkal = Convert.ToInt32(txtSumKkal.Content);
            if (kkal>us.DailyCaloriesNormHard || kkal>(us.DailyCaloriesNormHard - (us.DailyCaloriesNormHard - us.DailyCaloriesNormNorm)/2))
            {
                txtadvice.Text = "Текущая диета не безопасна для вашего здоровья, калорийность пищи сильно превышена, Вы будете толстеть"; 
            }
            else if (kkal < us.DailyCaloriesNormEasy || kkal < (us.DailyCaloriesNormEasy + (us.DailyCaloriesNormNorm - us.DailyCaloriesNormEasy) / 2))
            {
                txtadvice.Text = "Текущая диета не безопасна для вашего здоровья, калорийность пищи недостаточна, Вы будете худеть";
            }
            else
            {
                txtadvice.Text = "Текущая диета безопасна для вашего здоровья, калорийность пищи близка к Вашей норме " + us.DailyCaloriesNormNorm;
            }
        }
         
    }
}
