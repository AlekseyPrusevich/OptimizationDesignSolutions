using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    class Jeeves
    {
        public double pointOne { get; set; }

        public double poinSecond { get; set; }

        public double stepPointOne { get; set; }

        public double stepPointSecond { get; set; }

        public double accuracy { get; set; }

        public Conditions MyProperty { get; set; }

        public Jeeves()
        {
            pointOne = 0;
            poinSecond = 0;
            stepPointOne = 0;
            stepPointSecond = 0;
            accuracy = 0;
        }

        public Jeeves(double _pointOne, double _poinSecond, double _stepPointOne, double _stepPointSecond, double _accuracy)
        {
            pointOne = _pointOne;
            poinSecond = _poinSecond;
            stepPointOne = _stepPointOne;
            stepPointSecond = _stepPointSecond;
            accuracy = _accuracy;
        }

        private double calculateFun(Conditions Conditions, double _x1, double _x2, double _x3)
        {
            return (Conditions.a_price - Conditions.otherExpenses) * _x1 - (Conditions.a_sugarExpense * Conditions.sugarPrice + Conditions.a_molassesExpense * Conditions.molassesPrice + Conditions.a_fruitPureeExpense * Conditions.fruitPureePrice) * _x1 +
                   (Conditions.b_price - Conditions.otherExpenses) * _x2 - (Conditions.b_sugarExpense * Conditions.sugarPrice + Conditions.b_molassesExpense * Conditions.molassesPrice + Conditions.b_fruitPureeExpense * Conditions.fruitPureePrice) * _x2 +
                   (Conditions.c_price - Conditions.otherExpenses) * _x3 - (Conditions.c_sugarExpense * Conditions.sugarPrice + Conditions.c_molassesExpense * Conditions.molassesPrice + Conditions.c_fruitPureeExpense * Conditions.fruitPureePrice) * _x3;
        }

        public void JeevesMethod(Conditions _conditions, bool _isShowLog)
        {
            Conditions Conditions = _conditions;

            Paragraph logParagraph = new Paragraph();
            FlowDocument logFlowDocument = new FlowDocument();

            double x1 = Conditions.a_request;

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
                function = calculateFun(Conditions, x1, x2, x3);
                Console.WriteLine("Функция f= " + function);

                funArray[0] = calculateFun(Conditions, x1, x2 + vx2, x3);
                funArray[1] = calculateFun(Conditions, x1, x2, x3 + vx3);
                funArray[2] = calculateFun(Conditions, x1, x2 - vx2, x3);
                funArray[3] = calculateFun(Conditions, x1, x2, x3 - vx3);

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

                sugarWeight = Conditions.a_sugarExpense * x1 + Conditions.b_sugarExpense * x2 + Conditions.c_sugarExpense * x3;
                molassesWeight = Conditions.a_molassesExpense * x1 + Conditions.b_molassesExpense * x2 + Conditions.c_molassesExpense * x3;
                fructoseWeight = Conditions.a_fruitPureeExpense * x1 + Conditions.b_fruitPureeExpense * x2 + Conditions.c_fruitPureeExpense * x3;

                if (sugarWeight >= Conditions.sugarWeight || molassesWeight >= Conditions.molassesWeight || fructoseWeight >= Conditions.fruitPureeWeight)
                {
                    x2 = x2 - vx2;
                    x3 = x3 - vx3;
                    functionMax = calculateFun(Conditions, x1, x2, x3);
                    xflag = false;
                }

                if (sugarWeight <= Conditions.sugarWeight && molassesWeight <= Conditions.molassesWeight && fructoseWeight <= Conditions.fruitPureeWeight)
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

            Result.Returned = true;
            Result.FlowDocument = logFlowDocument;
        }
    }
}
