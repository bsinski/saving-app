using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using SavingApp.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SavingApp
{
    [Activity(Label = "StatsActivity")]
    public class StatsActivity : Activity
    {
        Spinner months;
        Spinner years;
        String chosen_year;
        String chosen_month;
        TextView total_output;
        ChartView chartView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.summary);
            months =  FindViewById<Spinner>(Resource.Id.month_spinner);
            years = FindViewById<Spinner>(Resource.Id.year_spinner);
            Button accept_button = FindViewById<Button>(Resource.Id.acceptdate_button);
            Button back_button = FindViewById<Button>(Resource.Id.backtomain3_button);
            total_output = FindViewById<TextView>(Resource.Id.total_output);
            chartView = FindViewById<ChartView>(Resource.Id.chart);
            CreateSpinnerMonths();
            CreateSpinnerYears();
            accept_button.Click += AcceptDateClick;
            back_button.Click += BackClick;
            months.ItemSelected += SelectMonth;
            years.ItemSelected += SelectYear;
        }
        protected void CreateSpinnerMonths()
        {
            //creating the spinner with months
            string[] m = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, m);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            months.Adapter = adapter;
        }
        protected void CreateSpinnerYears()
        {
            //creating the spinner with years tho choose
            string[] y = { "2021","2020","2019","2018" };
            var adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSpinnerItem, y);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            years.Adapter = adapter;

        }
        protected void AcceptDateClick(object sender,EventArgs  e)
        {
            List<Schedule> schedules = SchedulesHandler.readPlans();
            List<Expense> expenses = new List<Expense>();
            // getting expenses from all of the plans
            foreach(Schedule s in schedules)
            {
                expenses.AddRange(s.Expenses);
            }
            // filtering and summing the expenses in given months
            List<Expense> filtered = ExpensesHandler.FilterbyDate(expenses, chosen_month, chosen_year);
            total_output.Text = ExpensesHandler.SumExpenses(filtered).ToString(CultureInfo.InvariantCulture);
            Plotter pl = new Plotter(filtered, chosen_month, chosen_year);
            LineChart ch = pl.PlotDays();
            // showing the plot
            chartView.Chart = ch;
        }
        protected void SelectMonth(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            chosen_month =(String) months.GetItemAtPosition(e.Position);
        }
        protected void SelectYear(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            chosen_year = (String)years.GetItemAtPosition(e.Position);

        }
        protected void BackClick(object sender,EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }




    }
}