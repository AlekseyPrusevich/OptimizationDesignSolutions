using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OptimizationDesignSolutions
{
    public class MethodJeeves: MainWindow
    {
        public double pointOne { get; set; } 

        public double pointSecond { get; set; } 

        public double stepPointOne { get; set; }

        public double stepPointSecond { get; set; }

        public double Accuracy { get; set; } 
        public bool IsShowLog { get; set; }

        public MethodJeeves()
        {
            IsShowLog = false;
        }

        public MethodJeeves(double _pointOne, double _pointSecond, double _stepPointOne, double _stepPointSecond, double _accuracy, bool _isShowLog)
        {
            pointOne = _pointOne;
            pointSecond = _pointSecond;
            stepPointOne = _stepPointOne;
            stepPointSecond  = _stepPointSecond;
            Accuracy = _accuracy;
            IsShowLog = _isShowLog;
        }

        Paragraph logParagraph = new Paragraph();
        FlowDocument logFlowDocument = new FlowDocument();

        FlowDocument result { get; set; }

        private double calculateFun(double _x1, double _x2, double _x3)
        {
            return (Conditions.a_price - Conditions.otherExpenses) * _x1 - (Conditions.a_sugarExpense * Conditions.sugarPrice + Conditions.a_molassesExpense * Conditions.molassesPrice + Conditions.a_fruitPureeExpense * Conditions.fruitPureePrice) * _x1 +
                   (Conditions.b_price - Conditions.otherExpenses) * _x2 - (Conditions.b_sugarExpense * Conditions.sugarPrice + Conditions.b_molassesExpense * Conditions.molassesPrice + Conditions.b_fruitPureeExpense * Conditions.fruitPureePrice) * _x2 +
                   (Conditions.c_price - Conditions.otherExpenses) * _x3 - (Conditions.c_sugarExpense * Conditions.sugarPrice + Conditions.c_molassesExpense * Conditions.molassesPrice + Conditions.c_fruitPureeExpense * Conditions.fruitPureePrice) * _x3;
        }

        public FlowDocument JeevesCalculation()
        {
            double x1 = Conditions.a_request < 0 ? 0 : Conditions.a_request;
            double x2 = pointOne < 0 ? 0 : pointOne;
            double x3 = pointSecond < 0 ? 0 : pointSecond;

            double vx2 = stepPointOne;
            double vx3 = stepPointSecond;

            double accuracy = Accuracy;

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
            
            while(true)
            {
                function = calculateFun(x1, x2, x3);
                Console.WriteLine("Функция f= " + function);

                funArray[0] = calculateFun(x1, x2 + vx2, x3);
                funArray[1] = calculateFun(x1, x2, x3 + vx3);
                funArray[2] = calculateFun(x1, x2 - vx2, x3);
                funArray[3] = calculateFun(x1, x2, x3 - vx3);

                arrayx1[0] = x2 + vx2;
                arrayx1[1] = x2;
                arrayx1[2] = x2 - vx2;
                arrayx1[3] = x2;

                arrayx2[0] = x3;
                arrayx2[1] = x3 + vx3;
                arrayx2[2] = x3;
                arrayx2[3] = x3 - vx3;

                if (IsShowLog == true)
                {
                    logParagraph.Inlines.Add(new Run("__________________________________________________________________________________\r"));
                    logFlowDocument.Blocks.Add(logParagraph);
                }

                for (int i = 0; i < funArray.Length; i++)
                {
                    if (IsShowLog == true)
                    {
                        logParagraph.Inlines.Add(new Run("Функция f[" + arrayx1[i] + "," + arrayx2[i] + "] = " + funArray[i] + "\r"));
                        logFlowDocument.Blocks.Add(logParagraph);
                    }

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

                if (IsShowLog == true)
                {
                    logParagraph.Inlines.Add(new Run("Функция fmax[" + x2 + "," + x3 + "] = " + functionMax + "\r"));
                    logFlowDocument.Blocks.Add(logParagraph);
                }

                Calculation.Calculate(x1, x2, x3, out sugarWeight, out molassesWeight, out fructoseWeight);

                if (sugarWeight >= Conditions.sugarWeight || molassesWeight >= Conditions.molassesWeight || fructoseWeight >= Conditions.fruitPureeWeight)
                {
                    x2 = x2 - vx2;
                    x3 = x3 - vx3;
                    functionMax = calculateFun(x1, x2, x3);
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

            result = Report.GenerateReprot("МЕТОД ХУКФ-ДЖИВСА", functionMax, x1, x2, x3, sugarWeight, molassesWeight, fructoseWeight);

            if (IsShowLog == true)
            {
                foreach (var doc in result.Blocks.ToList())
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
    }
}
