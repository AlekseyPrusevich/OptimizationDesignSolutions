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
        Conditions conditions = new Conditions();

        Paragraph logParagraph = new Paragraph();
        FlowDocument logFlowDocument = new FlowDocument();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void calculateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                conditions.sugarWeight = Convert.ToDouble(sugarWeightTB.Text);
                conditions.molassesWeight = Convert.ToDouble(molassesWeightTB.Text);
                conditions.fruitPureeWeight = Convert.ToDouble(fruitPureeWeightTB.Text);

                conditions.sugarPrice = Convert.ToDouble(sugarPriceTB.Text);
                conditions.molassesPrice = Convert.ToDouble(molassesPriceTB.Text);
                conditions.fruitPureePrice = Convert.ToDouble(fruitPureePriceTB.Text);

                conditions.a_sugarExpense = Convert.ToDouble(a_sugarExpenseTB.Text);
                conditions.a_molassesExpense = Convert.ToDouble(a_molassesExpenseTB.Text);
                conditions.a_fruitPureeExpense = Convert.ToDouble(a_fruitPureeExpenseTB.Text);

                conditions.b_sugarExpense = Convert.ToDouble(b_sugarExpenseTB.Text);
                conditions.b_molassesExpense = Convert.ToDouble(b_molassesExpenseTB.Text);
                conditions.b_fruitPureeExpense = Convert.ToDouble(b_fruitPureeExpenseTB.Text);

                conditions.c_sugarExpense = Convert.ToDouble(c_sugarExpenseTB.Text);
                conditions.c_molassesExpense = Convert.ToDouble(c_molassesExpenseTB.Text);
                conditions.c_fruitPureeExpense = Convert.ToDouble(c_fruitPureeExpenseTB.Text);

                conditions.a_price = Convert.ToDouble(a_priceTB.Text);
                conditions.b_price = Convert.ToDouble(b_priceTB.Text);
                conditions.c_price = Convert.ToDouble(c_priceTB.Text);

                conditions.otherExpenses = Convert.ToDouble(otherExpensesTB.Text);
                conditions.a_request = Convert.ToDouble(a_requestTB.Text);
            }
            catch
            {
                MessageBox.Show("Необходимо ввести число", "ODS", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            calculate();
        }

        public void calculate()
        {
            double x1 = conditions.a_request;
            double x2 = 0;
            double x3 = 0;

            double sugarWeight = 0;
            double molassesWeight = 0;
            double fructoseWeight = 0;

            double fun = 0;
            double funMAX = 0;


            logRTB.Document.Blocks.Clear();

            for (int i = 0; i <= 4000; i++)
            {
                for (int j = 0; j <= 4000; j++)
                {
                    if ((conditions.a_sugarExpense * x1 +      conditions.b_sugarExpense * i +      conditions.c_sugarExpense * j) <= conditions.sugarWeight && 
                        (conditions.a_molassesExpense * x1 +   conditions.b_molassesExpense * i +   conditions.c_molassesExpense * j) <= conditions.molassesWeight && 
                        (conditions.a_fruitPureeExpense * x1 + conditions.b_fruitPureeExpense * i + conditions.c_fruitPureeExpense * j) <= conditions.fruitPureeWeight)
                    {

                        fun = (conditions.a_price - conditions.otherExpenses) * x1 - (conditions.a_sugarExpense * conditions.sugarPrice + conditions.a_molassesExpense * conditions.molassesPrice + conditions.a_fruitPureeExpense * conditions.fruitPureePrice) * x1 +
                              (conditions.b_price - conditions.otherExpenses) *  i - (conditions.b_sugarExpense * conditions.sugarPrice + conditions.b_molassesExpense * conditions.molassesPrice + conditions.b_fruitPureeExpense * conditions.fruitPureePrice) * i +
                              (conditions.c_price - conditions.otherExpenses) *  j - (conditions.c_sugarExpense * conditions.sugarPrice + conditions.c_molassesExpense * conditions.molassesPrice + conditions.c_fruitPureeExpense * conditions.fruitPureePrice) * j;

                        if (fun > funMAX)
                        {
                            funMAX = fun;

                            x2 = i;
                            x3 = j;


                            if (logVisCB.IsChecked == true)
                            {
                                Console.WriteLine("Максимальное значение функции равно {0} при х1 = 40, х2 = {1}, х3 = {2}\n", funMAX, x2, x3);
                                logParagraph.Inlines.Add(new Run("Fmax = " + funMAX + " при х1 = " + x1 + ", х2 = " + x2 + ", х3 = " + x3 + "\r"));
                                logFlowDocument.Blocks.Add(logParagraph);
                                logRTB.Document = logFlowDocument;
                            }
                        }
                    }
                }  
            }

            logRTB.Document = logFlowDocument;

            sugarWeight = conditions.a_sugarExpense * x1 + conditions.b_sugarExpense * x2 + conditions.c_sugarExpense * x3;
            molassesWeight = conditions.a_molassesExpense * x1 + conditions.b_molassesExpense * x2 + conditions.c_molassesExpense * x3;
            fructoseWeight = conditions.a_fruitPureeExpense * x1 + conditions.b_fruitPureeExpense * x2 + conditions.c_fruitPureeExpense * x3;

            logRTB.AppendText("МЕТОД ПЕРЕБОРА ПО СЕТКЕ\r");
            logRTB.AppendText("Fmax = " + funMAX + " при х1 = " + x1 + ", х2 = " + x2 + ", х3 = " + x3 + "\r" +
                    "Масса сахара - " + sugarWeight + " тонн(ы) \r" +
                    "Масса патоки - " + molassesWeight + "  тонн(ы) \r" +
                    "Масса фруктозы - " + fructoseWeight + "  тонн(ы)\r");

            Console.WriteLine("МЕТОД ПЕРЕБОРА ПО СЕТКЕ\n");
            Console.WriteLine("Максимальное значение функции равно {0} при х1 = 40, х2 = {1}, х3 = {2}\n" +
                "Масса сахара - {3} тонн(ы) \n" +
                "Масса патоки - {4} тонн(ы) \n" +
                "Масса фруктозы - {5} тонн(ы)\n\n", funMAX, x2, x3, sugarWeight, molassesWeight, fructoseWeight);
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
