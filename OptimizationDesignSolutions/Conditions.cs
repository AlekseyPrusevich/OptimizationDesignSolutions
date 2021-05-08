using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    public static class Conditions
    {
        public static double sugarWeight { get; set; } = 0;
        public static double molassesWeight { get; set; } = 0;
        public static double fruitPureeWeight { get; set; } = 0;

        public static double sugarPrice { get; set; } = 0;
        public static double molassesPrice { get; set; } = 0;
        public static double fruitPureePrice { get; set; } = 0;

        public static double a_sugarExpense { get; set; } = 0;
        public static double a_molassesExpense { get; set; } = 0;
        public static double a_fruitPureeExpense { get; set; } = 0;

        public static double b_sugarExpense { get; set; } = 0;
        public static double b_molassesExpense { get; set; } = 0;
        public static double b_fruitPureeExpense { get; set; } = 0;

        public static double c_sugarExpense { get; set; } = 0;
        public static double c_molassesExpense { get; set; } = 0;
        public static double c_fruitPureeExpense { get; set; } = 0;

        public static double a_price { get; set; } = 0;
        public static double b_price { get; set; } = 0;
        public static double c_price { get; set; } = 0;

        public static double otherExpenses { get; set; } = 0;
        public static double a_request { get; set; } = 0;

    }
}
