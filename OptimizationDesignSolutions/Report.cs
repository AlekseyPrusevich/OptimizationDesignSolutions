using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    static class Report
    {
        
        public static FlowDocument GenerateReprot(string name, double funMAX, double x1, double x2, double x3, double sugarWeight, double molassesWeight, double fructoseWeight)
        {
            Paragraph logParagraph = new Paragraph();
            FlowDocument logFlowDocument = new FlowDocument();

            logParagraph.Inlines.Add(new Run($"{name}\r" +
                "Максимальная прибыль - " + funMAX + "$ при объёмах производства карамели А = " + x1 + " т., В = " + x2 + " т., С = " + x3 + " т.\r" +
                "Масса сахара - " + sugarWeight + " тонн(ы) \r" +
                "Масса патоки - " + molassesWeight + "  тонн(ы) \r" +
                "Масса фруктозы - " + fructoseWeight + "  тонн(ы)\r"));
            logFlowDocument.Blocks.Add(logParagraph);

            return logFlowDocument;
        }
    }

    static class Calculation
    {
        public static void Calculate(double x1, double x2, double x3, out double sugarWeight, out double molassesWeight, out double fructoseWeight)
        {
            sugarWeight = Conditions.a_sugarExpense * x1 + Conditions.b_sugarExpense * x2 + Conditions.c_sugarExpense * x3;
            molassesWeight = Conditions.a_molassesExpense * x1 + Conditions.b_molassesExpense * x2 + Conditions.c_molassesExpense * x3;
            fructoseWeight = Conditions.a_fruitPureeExpense * x1 + Conditions.b_fruitPureeExpense * x2 + Conditions.c_fruitPureeExpense * x3;
        }
    }
}
