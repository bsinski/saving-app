using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SavingApp.Logic
{
    public static class ExpensesHandler
    {
        public static List<Expense> FilterbyDate(List<Expense> expenses,String month,String year)
        {
            List<Expense> results = new List<Expense>();
            foreach(Expense e in expenses)
            {
                DateTime date_e = e.date;
                String year_e = date_e.ToString("yyyy",CultureInfo.InvariantCulture);
                String month_e = date_e.ToString("MMMM", CultureInfo.InvariantCulture);
                if(year.CompareTo(year_e) + month.CompareTo(month_e) == 0)
                {
                    results.Add(e);
                }
            }
            return (results);
        }
    public static decimal SumExpenses(List<Expense> expenses)
        {
            decimal sum = 0;
            foreach(Expense e in expenses)
            {
                sum += e.money;
            }
            return (sum);
        }


    }
}