 using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SavingApp.Logic;
using SavingApp.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SavingApp
{
    [Activity(Label = "NewExpenseAcitivty")]
    public class NewExpenseActivity : Activity
    {
        private String money;
        private String name;
        private String date;
        private Schedule schedule;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.new_expense);
            EditText expensemoneyin = FindViewById<EditText>(Resource.Id.expense_input);
            EditText expensenamein = FindViewById<EditText>(Resource.Id.expensename_input);
            EditText expensedatein = FindViewById<EditText>(Resource.Id.expensedate_input);
            Button acceptexpense = FindViewById<Button>(Resource.Id.acceptexpense_button);
            Button back_button = FindViewById<Button>(Resource.Id.backtoplan2_button);
            schedule = SchedulesHandler.ReadFromString(this.Intent.Extras.GetString("schedule"));
            date = DateTime.Now.ToString("dd/MM/yyyy");
            expensedatein.Text = date.Replace(".","/");
            money = "100.00";
            expensemoneyin.Text = money;
            name = "newexpense";
            expensenamein.Text = name;

            expensemoneyin.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                money = e.Text.ToString();
            };
            expensenamein.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                name = e.Text.ToString();
            };
            expensedatein.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                date = e.Text.ToString();
            };
            acceptexpense.Click += acceptexpense_Click;
            back_button.Click += back_Click;
        }
        protected void acceptexpense_Click(object sender, EventArgs e)
        {
            bool valid_data = true;
            try
            {
                // checking the corectness of the input
                InputCheck.checkExpenseInput(date, name, money, this);
            }
            catch (CheckFailedException exc)
            {
                valid_data = false;
            }
            if (valid_data == true)
            {
                Expense expen = new Expense(name, Convert.ToDecimal(money, CultureInfo.InvariantCulture), Convert.ToDateTime(date));
                schedule.AddExpense(expen);
                Toast.MakeText(Application, "expense added ", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(PlanActivity));
                String s_text = SchedulesHandler.WriteToString(schedule);
                // updating the plan with new expense
                SchedulesHandler.UpdatePlans(schedule);
                intent.PutExtra("schedule", s_text);
                StartActivity(intent);
            }
        }
        protected void back_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(PlanActivity));
            String s_text = SchedulesHandler.WriteToString(schedule);
            intent.PutExtra("schedule", s_text);
            StartActivity(intent);
        }
    }
}