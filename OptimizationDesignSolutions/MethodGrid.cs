using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    class MethodGrid
    {
        public FlowDocument calculate(Conditions _conditions, bool _isShowLog)
        {
            Conditions conditions = _conditions;

            Paragraph logParagraph = new Paragraph();
            FlowDocument logFlowDocument = new FlowDocument();

            double x1 = conditions.a_request;
            double x2 = 0;
            double x3 = 0;

            double sugarWeight = 0;
            double molassesWeight = 0;
            double fructoseWeight = 0;

            double fun = 0;
            double funMAX = 0;

            for (int i = 0; i <= 4000; i++)
            {
                for (int j = 0; j <= 4000; j++)
                {
                    if ((conditions.a_sugarExpense * x1 + conditions.b_sugarExpense * i + conditions.c_sugarExpense * j) <= conditions.sugarWeight &&
                        (conditions.a_molassesExpense * x1 + conditions.b_molassesExpense * i + conditions.c_molassesExpense * j) <= conditions.molassesWeight &&
                        (conditions.a_fruitPureeExpense * x1 + conditions.b_fruitPureeExpense * i + conditions.c_fruitPureeExpense * j) <= conditions.fruitPureeWeight)
                    {

                        fun = (conditions.a_price - conditions.otherExpenses) * x1 - (conditions.a_sugarExpense * conditions.sugarPrice + conditions.a_molassesExpense * conditions.molassesPrice + conditions.a_fruitPureeExpense * conditions.fruitPureePrice) * x1 +
                              (conditions.b_price - conditions.otherExpenses) * i - (conditions.b_sugarExpense * conditions.sugarPrice + conditions.b_molassesExpense * conditions.molassesPrice + conditions.b_fruitPureeExpense * conditions.fruitPureePrice) * i +
                              (conditions.c_price - conditions.otherExpenses) * j - (conditions.c_sugarExpense * conditions.sugarPrice + conditions.c_molassesExpense * conditions.molassesPrice + conditions.c_fruitPureeExpense * conditions.fruitPureePrice) * j;

                        if (fun > funMAX)
                        {
                            funMAX = fun;

                            x2 = i;
                            x3 = j;

                            if (_isShowLog == true)
                            {
                                logParagraph.Inlines.Add(new Run("прибыль - " + funMAX + "$ при объёмах производства карамели А = " + x1 + " т., В = " + x2 + " т., С = " + x3 + " т.\r"));
                                logFlowDocument.Blocks.Add(logParagraph);
                            }
                        }
                    }
                }
            }

            sugarWeight = conditions.a_sugarExpense * x1 + conditions.b_sugarExpense * x2 + conditions.c_sugarExpense * x3;
            molassesWeight = conditions.a_molassesExpense * x1 + conditions.b_molassesExpense * x2 + conditions.c_molassesExpense * x3;
            fructoseWeight = conditions.a_fruitPureeExpense * x1 + conditions.b_fruitPureeExpense * x2 + conditions.c_fruitPureeExpense * x3;

            logParagraph.Inlines.Add(new Run("МЕТОД ПЕРЕБОРА ПО СЕТКЕ\r" +
                "Максимальная прибыль - " + funMAX + "$ при объёмах производства карамели А = " + x1 + " т., В = " + x2 + " т., С = " + x3 + " т.\r" +
                "Масса сахара - " + sugarWeight + " тонн(ы) \r" +
                "Масса патоки - " + molassesWeight + "  тонн(ы) \r" +
                "Масса фруктозы - " + fructoseWeight + "  тонн(ы)\r"));
            logFlowDocument.Blocks.Add(logParagraph);

            return logFlowDocument;
        }
    }
}
