using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SavingApp.Logic;
using System.Xml.Serialization;
using System.IO;


namespace SavingApp
{
    [Activity(Label = "PlanAcitivity")]
    public class PlanActivity : Activity
    {
        Schedule schedule;
        ListView expenseslist;
        protected override void OnCreate(Bundle savedInstanceState)
        { 
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.chosen_plan);
            schedule = SchedulesHandler.ReadFromString(this.Intent.Extras.GetString("schedule"));
            TextView planname = FindViewById<TextView>(Resource.Id.planname_output);
            TextView datestart = FindViewById<TextView>(Resource.Id.datestart_output);
            TextView dateend = FindViewById<TextView>(Resource.Id.dateend_output);
            TextView total = FindViewById<TextView>(Resource.Id.total_output);
            TextView permonth = FindViewById<TextView>(Resource.Id.permonth_output);
            TextView perday = FindViewById<TextView>(Resource.Id.perday_output);
            TextView peryear = FindViewById<TextView>(Resource.Id.peryear_output);
            Button addexpense = FindViewById<Button>(Resource.Id.addexpense_button);
            Button deleteplan = FindViewById<Button>(Resource.Id.deleteplan_button);
            Button modifyplan = FindViewById<Button>(Resource.Id.modifyplan_button);
            Button back = FindViewById<Button>(Resource.Id.backtolist_button);

            expenseslist = FindViewById<ListView>(Resource.Id.expenses_list);
            createExpenseList();
            planname.Text = schedule.Name;
            datestart.Text = schedule.DateStart.ToShortDateString();
            dateend.Text = schedule.DateEnd.ToShortDateString();
            total.Text = schedule.Money.ToString("0.00");

            // not showing anyvvalue if we have shorter period in the plan 
            if(schedule.PerDay() != 0)
            {
                perday.Text = schedule.PerDay().ToString("0.00");
            }
            if(schedule.PerMonth() != 0)
            {
                permonth.Text = schedule.PerMonth().ToString("0.00");
            }
            if (schedule.PerYear() != 0)
            {
                peryear.Text = schedule.PerYear().ToString("0.00");
            }
            addexpense.Click += addexpense_Click;
            deleteplan.Click += deleteplan_Click;
            modifyplan.Click += modifyplan_Click;
            back.Click += back_Click;

        }
       
        protected void addexpense_Click(object sender, EventArgs e)
        {
           // starting the new activity and passing the serialixed plan object with the intent 
            var intent = new Intent(this, typeof(NewExpenseActivity));
            String s_text = SchedulesHandler.WriteToString(schedule);
            intent.PutExtra("schedule", s_text);
            StartActivity(intent);  
        }
        protected void createExpenseList()
        {
            // creating and filling up the list with the plan expenses
            List<Expense> expenses = schedule.Expenses;
            string[] expenses_string = new string[expenses.Count];
            int i = 0;
            foreach(Expense exp in expenses)
            {
                expenses_string[i] = exp.name;
                i++;
            }

            ArrayAdapter adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, expenses_string);
            expenseslist.SetAdapter(adapter);
        } 
        protected void deleteplan_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Are you sure?");
            alert.SetMessage("That you want to delete this plan?");

            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                SchedulesHandler.DeletePlan(schedule);
                var intent = new Intent(this, typeof(ListPlanActivity));
                Toast.MakeText(Application, "plan deleted ", ToastLength.Short).Show();
                StartActivity(intent);

            });
            alert.SetNegativeButton("No", (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }
        protected void modifyplan_Click(object sender,EventArgs e)
        {
            var intent = new Intent(this, typeof(ModifyPlan));
            String s_text = SchedulesHandler.WriteToString(schedule);
            SchedulesHandler.UpdatePlans(schedule);
            intent.PutExtra("schedule", s_text);
            StartActivity(intent);
        }
        protected void back_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ListPlanActivity));
            StartActivity(intent);
        }

    }

}