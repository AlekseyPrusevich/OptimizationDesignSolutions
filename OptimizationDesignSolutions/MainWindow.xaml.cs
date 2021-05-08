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

namespace OptimizationDesignSolutions
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public bool isShowLog = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Conditions.sugarWeight = Convert.ToDouble(sugarWeightTB.Text);
                Conditions.molassesWeight = Convert.ToDouble(molassesWeightTB.Text);
                Conditions.fruitPureeWeight = Convert.ToDouble(fruitPureeWeightTB.Text);

                Conditions.sugarPrice = Convert.ToDouble(sugarPriceTB.Text);
                Conditions.molassesPrice = Convert.ToDouble(molassesPriceTB.Text);
                Conditions.fruitPureePrice = Convert.ToDouble(fruitPureePriceTB.Text);

                Conditions.a_sugarExpense = Convert.ToDouble(a_sugarExpenseTB.Text);
                Conditions.a_molassesExpense = Convert.ToDouble(a_molassesExpenseTB.Text);
                Conditions.a_fruitPureeExpense = Convert.ToDouble(a_fruitPureeExpenseTB.Text);

                Conditions.b_sugarExpense = Convert.ToDouble(b_sugarExpenseTB.Text);
                Conditions.b_molassesExpense = Convert.ToDouble(b_molassesExpenseTB.Text);
                Conditions.b_fruitPureeExpense = Convert.ToDouble(b_fruitPureeExpenseTB.Text);

                Conditions.c_sugarExpense = Convert.ToDouble(c_sugarExpenseTB.Text);
                Conditions.c_molassesExpense = Convert.ToDouble(c_molassesExpenseTB.Text);
                Conditions.c_fruitPureeExpense = Convert.ToDouble(c_fruitPureeExpenseTB.Text);

                Conditions.a_price = Convert.ToDouble(a_priceTB.Text);
                Conditions.b_price = Convert.ToDouble(b_priceTB.Text);
                Conditions.c_price = Convert.ToDouble(c_priceTB.Text);

                Conditions.otherExpenses = Convert.ToDouble(otherExpensesTB.Text);
                Conditions.a_request = Convert.ToDouble(a_requestTB.Text);

            }
            catch
            {
                MessageBox.Show("Необходимо ввести число", "ODS", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Conditions.a_request >= 0)
            {
                isShowLog = logVisCB.IsChecked == true ? true : false;

                if (MethodGridRB.IsChecked == true)
                {
                    Grid methodGrid = new Grid(isShowLog);
                    logRTB.Document = methodGrid.GridCalculation();
                }
                if (MethodHookJeevesRB.IsChecked == true)
                {
                    MethodHookLeevesWindow methodHookLeevesWindow = new MethodHookLeevesWindow(this);
                    methodHookLeevesWindow.Show();
                    methodHookLeevesWindow.isShowLog = isShowLog;
                }
            }
            else
            {
                MessageBox.Show("Запрос карамели А не может быть ниже 0 тонн", "ODS", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void GetChildResult(FlowDocument flowDocument)
        {
            
        }

        private void updateDataBtn_Click(object sender, RoutedEventArgs e)
        {
            sugarWeightTB.Text = "800";
            molassesWeightTB.Text = "600";
            fruitPureeWeightTB.Text = "120";

            sugarPriceTB.Text = "1220";
            molassesPriceTB.Text = "1500";
            fruitPureePriceTB.Text = "2100";

            a_sugarExpenseTB.Text = "0,8";
            a_molassesExpenseTB.Text = "0,4";
            a_fruitPureeExpenseTB.Text = "0";

            b_sugarExpenseTB.Text = "0,5";
            b_molassesExpenseTB.Text = "0,4";
            b_fruitPureeExpenseTB.Text = "0,1";

            c_sugarExpenseTB.Text = "0,6";
            c_molassesExpenseTB.Text = "0,3";
            c_fruitPureeExpenseTB.Text = "0,1";

            a_priceTB.Text = "2040";
            b_priceTB.Text = "1990";
            c_priceTB.Text = "1970";

            otherExpensesTB.Text = "450";
            a_requestTB.Text = "40";

        }

        private void clearLogBtn_Click(object sender, RoutedEventArgs e)
        {
            logRTB.Document.Blocks.Clear();
        }
    }
}
