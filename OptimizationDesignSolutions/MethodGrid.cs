using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    class Grid
    {
        Paragraph logParagraph = new Paragraph();
        FlowDocument logFlowDocument = new FlowDocument();

        public bool IsShowLog { get; set; }

        FlowDocument result { get; set; }

        public Grid(bool _isShowLog)
        {
            IsShowLog = _isShowLog;
        }

        public FlowDocument GridCalculation()
        {
            double x1 = Conditions.a_request;
            double x2 = 0;
            double x3 = 0;

            double sugarWeight = 0;
            double molassesWeight = 0;
            double fructoseWeight = 0;

            double fun = 0;
            double funMAX = 0;

            DoCalculation(ref x1, ref x2, ref x3, ref fun, ref funMAX, ref sugarWeight, ref molassesWeight, ref fructoseWeight);

            result = Report.GenerateReprot("МЕТОД ПЕРЕБОРА ПО СЕТКЕ", funMAX, x1, x2, x3, sugarWeight, molassesWeight, fructoseWeight);

            if (IsShowLog == true)
            {
                foreach(var doc in result.Blocks.ToList())
                {
                    logFlowDocument.Blocks.Add(doc);
                }

                return logFlowDocument;
            }
            else
            {
                return result;
            }
        }

        private void DoCalculation(ref double x1, ref double x2, ref double x3, 
                                   ref double fun, ref double funMAX, 
                                   ref double sugarWeight, ref double molassesWeight, ref double fructoseWeight)
        {
            for (int i = 0; i <= 4000; i++)
            {
                for (int j = 0; j <= 4000; j++)
                {
                    if ((Conditions.a_sugarExpense * x1 + Conditions.b_sugarExpense * i + Conditions.c_sugarExpense * j) <= Conditions.sugarWeight &&
                        (Conditions.a_molassesExpense * x1 + Conditions.b_molassesExpense * i + Conditions.c_molassesExpense * j) <= Conditions.molassesWeight &&
                        (Conditions.a_fruitPureeExpense * x1 + Conditions.b_fruitPureeExpense * i + Conditions.c_fruitPureeExpense * j) <= Conditions.fruitPureeWeight)
                    {

                        fun = (Conditions.a_price - Conditions.otherExpenses) * x1 - (Conditions.a_sugarExpense * Conditions.sugarPrice + Conditions.a_molassesExpense * Conditions.molassesPrice + Conditions.a_fruitPureeExpense * Conditions.fruitPureePrice) * x1 +
                              (Conditions.b_price - Conditions.otherExpenses) * i - (Conditions.b_sugarExpense * Conditions.sugarPrice + Conditions.b_molassesExpense * Conditions.molassesPrice + Conditions.b_fruitPureeExpense * Conditions.fruitPureePrice) * i +
                              (Conditions.c_price - Conditions.otherExpenses) * j - (Conditions.c_sugarExpense * Conditions.sugarPrice + Conditions.c_molassesExpense * Conditions.molassesPrice + Conditions.c_fruitPureeExpense * Conditions.fruitPureePrice) * j;

                        if (fun > funMAX)
                        {
                            funMAX = fun;

                            x2 = i;
                            x3 = j;

                            if(IsShowLog == true)
                            {
                                logParagraph.Inlines.Add(new Run("Прибыль - " + funMAX + "$ при объёмах производства карамели А = " + x1 + " т., В = " + i + " т., С = " + j + " т.\r"));
                                logFlowDocument.Blocks.Add(logParagraph);
                            }
                        }
                    }
                }
            }

            Calculation.Calculate(x1, x2, x3, out sugarWeight, out molassesWeight, out fructoseWeight);
        }
    }
}
