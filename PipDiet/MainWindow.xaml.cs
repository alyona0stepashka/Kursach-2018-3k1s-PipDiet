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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WinForms;

namespace PipDiet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User us;
        private bool _isReportViewerLoaded;

        SolidColorBrush Mcolor = new SolidColorBrush();
        SolidColorBrush Mcolor2 = new SolidColorBrush();
        SolidColorBrush Mcolor3 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#4B4A43"));
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User user)
        {
            InitializeComponent();
            us = user;

            txthello.Content = "Hello, " + us.Login+" !";
            txtless.Content = "Kkal for less weight : " + us.DailyCaloriesNormEasy+" kkal";
            txtnorm.Content = "Kkal for current weight : " + us.DailyCaloriesNormNorm + " kkal";
            txtmore.Content = "Kkal for more weight : " + us.DailyCaloriesNormHard + " kkal";
            txtidealw.Content = "Ideal Weight : " + us.IdealWeight + " kg";

            Mcolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#C2CAD1")); //цвет выделения меню
            Mcolor2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#313937")); //возврат цвета меню
            if (us.Role == 1)
            {
                dockAdmin.Visibility = Visibility.Collapsed;
            }
        }
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
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor;
        }
        private void label1_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Label)sender).Foreground = Mcolor2;
        }

        #endregion

        private void Rep1_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
                //AdventureWorks2008R2DataSet dataset = new AdventureWorks2008R2DataSet();

                //dataset.BeginInit();

                //reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                //reportDataSource1.Value = dataset.SalesOrderDetail;
                //this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                //this._reportViewer.LocalReport.ReportEmbeddedResource = "<VSProjectName>.Report1.rdlc";

                //dataset.EndInit();

                ////fill data into adventureWorksDataSet
                //AdventureWorks2008R2DataSetTableAdapters.SalesOrderDetailTableAdapter salesOrderDetailTableAdapter = new AdventureWorks2008R2DataSetTableAdapters.SalesOrderDetailTableAdapter();
                //salesOrderDetailTableAdapter.ClearBeforeFill = true;
                //salesOrderDetailTableAdapter.Fill(dataset.SalesOrderDetail);

                //_reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }
    }
}
