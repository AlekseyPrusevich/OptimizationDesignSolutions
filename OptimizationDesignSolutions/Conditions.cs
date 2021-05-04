using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    public class Conditions
    {
        public double sugarWeight { get; set; }
        public double molassesWeight { get; set; }
        public double fruitPureeWeight { get; set; }

        public double sugarPrice { get; set; }
        public double molassesPrice { get; set; }
        public double fruitPureePrice { get; set; }

        public double a_sugarExpense { get; set; }
        public double a_molassesExpense { get; set; }
        public double a_fruitPureeExpense { get; set; }

        public double b_sugarExpense { get; set; }
        public double b_molassesExpense { get; set; }
        public double b_fruitPureeExpense { get; set; }

        public double c_sugarExpense { get; set; }
        public double c_molassesExpense { get; set; }
        public double c_fruitPureeExpense { get; set; }

        public double a_price { get; set; }
        public double b_price { get; set; }
        public double c_price { get; set; }

        public double otherExpenses { get; set; }
        public double a_request { get; set; }

        public Conditions()
        {
            sugarWeight = 0;
            molassesWeight = 0;
            fruitPureeWeight = 0;

            sugarPrice = 0;
            molassesPrice = 0;
            fruitPureePrice = 0;

            a_sugarExpense = 0;
            a_molassesExpense = 0;
            a_fruitPureeExpense = 0;

            b_sugarExpense = 0;
            b_molassesExpense = 0;
            b_fruitPureeExpense = 0;

            c_sugarExpense = 0;
            c_molassesExpense = 0;
            c_fruitPureeExpense = 0;

            a_price = 0;
            b_price = 0;
            c_price = 0;

            otherExpenses = 0;
            a_request = 0;
        }

        public Conditions(double _sugarWeight, double _molassesWeight, double _fruitPureeWeight, double _sugarPrice, double _molassesPrice,
            double _fruitPureePrice, double _a_sugarExpense, double _a_molassesExpense, double _a_fruitPureeExpense, double _b_sugarExpense, 
            double _b_molassesExpense, double _b_fruitPureeExpense, double _c_sugarExpense, double _c_molassesExpense, double _c_fruitPureeExpense,
            double _a_price, double _b_price, double _c_price, double _otherExpenses, double _a_request)
        {
            sugarWeight = _sugarWeight;
            molassesWeight = _molassesWeight;
            fruitPureeWeight = _fruitPureeWeight;

            sugarPrice = _sugarPrice;
            molassesPrice = _molassesPrice;
            fruitPureePrice = _fruitPureePrice;

            a_sugarExpense = _a_sugarExpense;
            a_molassesExpense = _a_molassesExpense;
            a_fruitPureeExpense = _a_fruitPureeExpense;

            b_sugarExpense = _b_sugarExpense;
            b_molassesExpense = _b_molassesExpense;
            b_fruitPureeExpense = _b_fruitPureeExpense;

            c_sugarExpense = _c_sugarExpense;
            c_molassesExpense = _c_molassesExpense;
            c_fruitPureeExpense = _c_fruitPureeExpense;

            a_price = _a_price;
            b_price = _b_price;
            c_price = _c_price;

            otherExpenses = _otherExpenses;
            a_request = _a_request;
        }
    }
}
