using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SavingApp.Logic
{
    class Plotter
    {
        private List<Expense> expenses;
        private String month;
        private String year;
        private int[] days;
        private decimal[] daysvalues;
        public Plotter(List<Expense> expenses,String month, String year)
        {
            this.expenses = expenses;
            this.month = month;
            this.year = year;
            SetExpensesAndDays();
        }

        public LineChart PlotDays()
        {
            // method for making the plot object
            var entries = new List<ChartEntry>();
            int i;
            foreach (int d in days)
            {
                i = d - 1;
                ChartEntry newentry = new ChartEntry((float)daysvalues[i])
                {
                    Label = days[i].ToString(),
                    ValueLabel = daysvalues[i].ToString(),
                    Color = SKColor.Parse("#5AAFFE")

                };
                entries.Add(newentry);

            }
            LineChart chart = new LineChart { Entries = entries, LineSize = 8,LabelTextSize = 25,LabelOrientation = Microcharts.Orientation.Horizontal };
            return (chart);
        }

        public List<Expense> Expenses
        {
            get => expenses;
            set => expenses = value;
        }

        // this method is used for setting the values per days while constructing the plotter object
        protected void SetExpensesAndDays()
        {
            int m = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
            int y = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture).Year;
            int n = DateTime.DaysInMonth(y, m);
            days = Enumerable.Range(1, n).ToArray();
            daysvalues = new decimal[n];
            foreach(int d in days)
            {
                List<Expense> filtered = expenses.FindAll(exp => exp.date.Day == d);
                decimal sum_month = ExpensesHandler.SumExpenses(filtered);
                daysvalues[d - 1] = sum_month;
            }

        }



        //protected List<decimal> GetValues()
        //{
        //    List<decimal> results = new List<decimal>();
        //    foreach(String d in dates)
        //    {
        //        List<Expense> filtered = expenses.FindAll(exp => exp.date.ToString("MMMM").CompareTo(d) == 0);
        //        decimal sum_month = ExpensesHandler.SumExpenses(filtered);
        //        results.Add()
        //    }
        //}


    }
}