using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using CustomerManager.DB;
using CustomerManager.Model;
using CustomerManager.Common;
using DevExpress.Xpf.Dialogs;
using Microsoft.Win32;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using CustomerManager.ViewModels;
using System.IO;
namespace CustomerManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
       // public Access<Customer> Cus = new Access<Customer>();
        public MainWindow()
        {
            InitializeComponent();
            //for (int i = 0; i < 2500; i++)
            //{
            //    Cus.Insert(new Customer() { Name = "test", Sid =GeneratorID.generate()});
            //}
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            //for (int i = 0; i < 100; i++)
            //{
            //    DXSplashScreen.Progress(i);
            //    DXSplashScreen.SetState(string.Format("{0} %", (i + 1)));
            //    System.Threading.Thread.Sleep(40);
            //}

        }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DXSplashScreen.Close();
            this.Activate();
        }
        private void grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e)
        {
            string sid = e.GetListSourceFieldValue("Sid").ToString();
            if (e.IsGetData)
            {
                if (e.Column.FieldName=="Gender")
                {
                     e.Value = Convert.ToInt32(sid.Substring(16, 1)) % 2 == 0 ? "女" : "男";
                    //e.Value = "女";
                }else if (e.Column.FieldName == "Age")
                {

                int year = Convert.ToInt32(sid.Substring(6, 4));
                   //int month = Convert.ToInt16( sid.Substring(10, 2));
                   // int day = Convert.ToInt16(sid.Substring(12, 2));
                   // Debug.WriteLine(year);
                   // Debug.WriteLine(month);
                   // Debug.WriteLine(day);
                  e.Value = DateTime.Now.Year - year;
                    // DateTime dt = new DateTime(year, month, day);
                    //Debug.write( dt.ToShortDateString();
                }
               
            }
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
           //int x= DB.DB.Content.Delete<Customer>(Customer._.Sid.Len()==17);
           // MessageBox.Show(x.ToString());
            var fileDialog = new SaveFileDialog ()
            {
                Filter = "Excel|*.xls"
            };


            var result = fileDialog.ShowDialog();
            if (result == true)
            {
                int counts = (dataView.DataContext as MainWindowViewModel).Helper.newList.Count;
                DXMessageBox.Show(string.Format("将导出{0}条记录",counts), "重要提示", MessageBoxButton.OK, MessageBoxImage.Information);
                dataView.ExportToXls(fileDialog.FileName);
                
                DXMessageBox.Show( "成功导出", "重要提示",MessageBoxButton.OK,MessageBoxImage.Information);
                
            }

                
        }
        private void Backup_Click(object sender, RoutedEventArgs e)
        {

            var fileDialog = new SaveFileDialog()
            {
                Filter = "Access数据库（2003）|*.mdb"
            };

            var result = fileDialog.ShowDialog();
            if(result == true)
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                DotNet.Utilities.FileOperate.FileCoppy(System.IO.Path.Combine(basePath,"Test.mdb"),fileDialog.FileName);
                DXMessageBox.Show(caption:"信息提示",messageBoxText:"数据库备份成功",icon:MessageBoxImage.Information,button:MessageBoxButton.OK);
            }

        }
    }
}
