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

namespace OptimizationDesignSolutions
{
    /// <summary>
    /// Логика взаимодействия для MethodHookLeevesWindow.xaml
    /// </summary>
    public partial class MethodHookLeevesWindow : Window
    {
        MethodJeeves methodJeeves;

        public bool isShowLog { get; set; }
        public MainWindow MainWindow { get; }

        public MethodHookLeevesWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                methodJeeves = new MethodJeeves(Convert.ToDouble(pointOneTB.Text),
                    Convert.ToDouble(pointSecondTB.Text),
                    Convert.ToDouble(stepPointOneTB.Text),
                    Convert.ToDouble(stepPointSecondTB.Text),
                    Convert.ToDouble(accuracyTB.Text),
                    isShowLog);
                MainWindow.logRTB.Document = methodJeeves.JeevesCalculation();
            }
            catch
            {
                MessageBox.Show("Необходимо ввести число", "ODS", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                        
            this.Visibility = Visibility.Hidden;
        }
    }
}
