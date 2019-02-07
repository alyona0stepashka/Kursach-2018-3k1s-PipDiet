using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PipDiet
{
    public partial class Settings : Window
    {
        #region var declaration
        User us;
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;
        Label l1, l2, l3, l4, l5, l6, l7, l8;
        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        SolidColorBrush Mcolor3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4B4A43"));
         
        #endregion
        public Settings(User user)
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
            l4 = (Label)FindName("label4");
            l5 = (Label)FindName("label5");
            l6 = (Label)FindName("label6");
            l7 = (Label)FindName("label7");
            l8 = (Label)FindName("label8");
            #endregion

            #region service
            label1.ToolTip = "Главная страница";
            label2.ToolTip = "Подобрать рецепт";
            label3.ToolTip = "Добавить продукт"; 
            label5.ToolTip = "Калькулятор калорийности";
            label6.ToolTip = "Настройки"; 
            label8.ToolTip = "Выход";

            Mcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C2CAD1")); //цвет выделения меню
            Mcolor2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#313937")); //возврат цвета меню

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = new System.Drawing.Icon("Pusheen_Love.ico");
            MyNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(MyNotifyIcon_MouseDoubleClick);
            #endregion
        }


        #region methods_menu
        #region methods_click_mouse
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
        }
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

        #region methods_move_mouse

        #endregion

        #region methods_mouse_leave


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BGBuilder builder = new ImageBuilder();
            builder.BuildImage();
            builder.SetBG(canv);
            Resource.bg = builder.GetResult();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BGBuilder builder = new ColorBuilder();
            builder.BuildColor();
            builder.SetBG(canv);
            Resource.bg = builder.GetResult();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            BGBuilder builder = new StandartBuilder();
            builder.BuildStandarTheme();
            builder.SetBG(canv);
            Resource.bg = builder.GetResult();
        }
          
        #endregion
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
    }
}