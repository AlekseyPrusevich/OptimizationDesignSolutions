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
        HookJeevesParameters hookJeevesParameters;

        public Conditions conditions { get; set; }

        public bool isShowLog { get; set; }

        public MethodHookLeevesWindow()
        {
            InitializeComponent();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hookJeevesParameters = new HookJeevesParameters(Convert.ToDouble(pointOneTB.Text),
                    Convert.ToDouble(pointSecondTB.Text),
                    Convert.ToDouble(stepPointOneTB.Text),
                    Convert.ToDouble(stepPointSecondTB.Text),
                    Convert.ToDouble(accuracyTB.Text));
            }
            catch
            {
                MessageBox.Show("Необходимо ввести число", "ODS", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MethodHookJeeves methodHookJeeves = new MethodHookJeeves();
            methodHookJeeves.calculate(conditions, hookJeevesParameters,  isShowLog);

            this.Visibility = Visibility.Hidden;
        }
    }
}
