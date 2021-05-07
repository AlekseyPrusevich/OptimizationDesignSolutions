using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    class MethodHookJeeves : MainWindow
    {
        /*
        public double x1 { get; set; }
        public double x2 { get; set; }
        public double x3 { get; set; }
        public double vx2 { get; set; }
        public double vx3 { get; set; }
        public double accuracy { get; set; }
        */

        public double calculateFun(Conditions conditions, double _x1, double _x2, double _x3)
        {
            return (conditions.a_price - conditions.otherExpenses) * _x1 - (conditions.a_sugarExpense * conditions.sugarPrice + conditions.a_molassesExpense * conditions.molassesPrice + conditions.a_fruitPureeExpense * conditions.fruitPureePrice) * _x1 +
                   (conditions.b_price - conditions.otherExpenses) * _x2 - (conditions.b_sugarExpense * conditions.sugarPrice + conditions.b_molassesExpense * conditions.molassesPrice + conditions.b_fruitPureeExpense * conditions.fruitPureePrice) * _x2 +
                   (conditions.c_price - conditions.otherExpenses) * _x3 - (conditions.c_sugarExpense * conditions.sugarPrice + conditions.c_molassesExpense * conditions.molassesPrice + conditions.c_fruitPureeExpense * conditions.fruitPureePrice) * _x3;
        }

        public void calculate (Conditions _conditions, HookJeevesParameters _hookJeevesParameters, bool _isShowLog)
        {
            Conditions conditions = _conditions;
            HookJeevesParameters hookJeevesParameters = _hookJeevesParameters;

            Paragraph logParagraph = new Paragraph();
            FlowDocument logFlowDocument = new FlowDocument();

            double x1 = conditions.a_request;
            double x2 = hookJeevesParameters.pointOne;
            double x3 = hookJeevesParameters.poinSecond;

            double vx2 = hookJeevesParameters.stepPointOne;
            double vx3 = hookJeevesParameters.stepPointSecond;

            double accuracy = hookJeevesParameters.accuracy;

            double function;
            double functionMax = -100000000000;
            double functionOld = functionMax;

            double sugarWeight;
            double molassesWeight;
            double fructoseWeight;

            bool xflag = true;

            double[] funArray = new double[4];
            double[] arrayx1 = new double[1000];
            double[] arrayx2 = new double[1000];

            if (x1 < 0)
                x1 = 0;
            if (x2 < 0)
                x2 = 0;
            if (x3 < 0)
                x3 = 0;

            for (; ; )
            {
                function = calculateFun(conditions, x1, x2, x3);
                Console.WriteLine("Функция f= " + function);

                funArray[0] = calculateFun(conditions, x1, x2 + vx2, x3);
                funArray[1] = calculateFun(conditions, x1, x2, x3 + vx3);
                funArray[2] = calculateFun(conditions, x1, x2 - vx2, x3);
                funArray[3] = calculateFun(conditions, x1, x2, x3 - vx3);

                arrayx1[0] = x2 + vx2;
                arrayx1[1] = x2;
                arrayx1[2] = x2 - vx2;
                arrayx1[3] = x2;

                arrayx2[0] = x3;
                arrayx2[1] = x3 + vx3;
                arrayx2[2] = x3;
                arrayx2[3] = x3 - vx3;

                Console.WriteLine("__________________________________________________________________________________");
                for (int i = 0; i < funArray.Length; i++)
                {
                    Console.WriteLine("Функция f[{0},{1}] = {2} ", arrayx1[i], arrayx2[i], funArray[i]);

                    if (functionMax < funArray[i] && xflag)
                    {
                        functionMax = funArray[i];
                        x2 = arrayx1[i];
                        x3 = arrayx2[i];
                    }

                    if (arrayx1[i] < 0)
                        x2 = x2 + vx2;

                    if (arrayx2[i] < 0)
                        x3 = x3 + vx3;
                }

                Console.WriteLine("Функция fmax[{0},{1}] = {2}", x2, x3, functionMax);

                sugarWeight = conditions.a_sugarExpense * x1 + conditions.b_sugarExpense * x2 + conditions.c_sugarExpense * x3;
                molassesWeight = conditions.a_molassesExpense * x1 + conditions.b_molassesExpense * x2 + conditions.c_molassesExpense * x3;
                fructoseWeight = conditions.a_fruitPureeExpense * x1 + conditions.b_fruitPureeExpense * x2 + conditions.c_fruitPureeExpense * x3;

                if (sugarWeight >= conditions.sugarWeight || molassesWeight >= conditions.molassesWeight || fructoseWeight >= conditions.fruitPureeWeight)
                {
                    x2 = x2 - vx2;
                    x3 = x3 - vx3;
                    functionMax = calculateFun(conditions, x1, x2, x3);
                    xflag = false;
                }

                if (sugarWeight <= conditions.sugarWeight && molassesWeight <= conditions.molassesWeight && fructoseWeight <= conditions.fruitPureeWeight)
                {
                    xflag = true;

                    if (functionOld == functionMax)
                    {
                        vx2 = vx2 / 2;
                        vx3 = vx3 / 2;

                        if (vx2 <= accuracy || vx3 <= accuracy)
                            break;
                    }
                }

                functionOld = functionMax;
            }

            Console.WriteLine("Сахар = " + sugarWeight);
            Console.WriteLine("Патока = " + molassesWeight);
            Console.WriteLine("Фруктоза = " + fructoseWeight);
            Console.ReadLine();

            logParagraph.Inlines.Add(new Run("МЕТОД ХУКФ-ДЖИВСА\r" +
                    "Максимальная прибыль - " + functionMax + "$ при объёмах производства карамели А = " + x1 + " т., В = " + x2 + " т., С = " + x3 + " т.\r" +
                    "Масса сахара - " + sugarWeight + " тонн(ы) \r" +
                    "Масса патоки - " + molassesWeight + "  тонн(ы) \r" +
                    "Масса фруктозы - " + fructoseWeight + "  тонн(ы)\r"));
            logFlowDocument.Blocks.Add(logParagraph);
            logParagraph.Inlines.Add(new Run("прибыль - " + functionMax + "$ при объёмах производства карамели А = " + x1 + " т., В = " + x2 + " т., С = " + x3 + " т.\r"));
            logFlowDocument.Blocks.Add(logParagraph);

            //logRTB.AppendText("afaggjk");

            hookJeevesReturn(logFlowDocument);
            logRTB.Document = logFlowDocument;
        }
    }
}
