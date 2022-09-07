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
    [Activity(Label = "ModifyPlan")]
    public class ModifyPlan : Activity
    {
        private String dateS;
        private String dateE;
        private String money;
        private String name;
        private Schedule schedule;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            schedule = SchedulesHandler.ReadFromString(this.Intent.Extras.GetString("schedule"));
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.modify_plan);
            EditText dateStart = FindViewById<EditText>(Resource.Id.dateStartnew_input);
            EditText dateEnd = FindViewById<EditText>(Resource.Id.dateEndnew_input);
            EditText moneyIn = FindViewById<EditText>(Resource.Id.moneynew_input);
            EditText nameIn = FindViewById<EditText>(Resource.Id.namenew_input);
            Button modify_button = FindViewById<Button>(Resource.Id.modifyplan_button);
            Button back_button = FindViewById<Button>(Resource.Id.backtoplan_button);

            // seting the edittext string to the values of plan from the intent
            dateS = schedule.DateStart.ToString("dd/MM/yyyy").Replace(".", "/");
            dateStart.Text = dateS;
            dateE = schedule.DateEnd.ToString("dd/MM/yyyy").Replace(".", "/");
            dateEnd.Text = dateE;
            money = schedule.Money.ToString();
            moneyIn.Text = money;
            name = schedule.Name;
            nameIn.Text = name;

            dateStart.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                dateS = e.Text.ToString();
            };
            dateEnd.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                dateE = e.Text.ToString();
            };
            moneyIn.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                money = e.Text.ToString();
            };
            nameIn.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
                name = e.Text.ToString();
            };
            modify_button.Click += modifyplan_Click;
            back_button.Click += back_Click;
        }
        protected void modifyplan_Click(object sender,EventArgs e)
        {
            bool valid_data = true;
            try
            {
                //checking the corectness of the input
                InputCheck.checkPlanInput(dateS, dateE, name, money, this,false);
            }
            catch (CheckFailedException exc)
            {
                valid_data = false;
            }
            if (valid_data == true)
            {
                Schedule plan = new Schedule(name, Convert.ToDecimal(money, CultureInfo.InvariantCulture), Convert.ToDateTime(dateS), Convert.ToDateTime(dateE));
                plan.Expenses = schedule.Expenses;
                // if we have we are changing the name of the plan we have to delete plan with previous name
                if (schedule.Name.CompareTo(plan.Name) != 0)
                {
                    SchedulesHandler.DeletePlan(schedule);
                    SchedulesHandler.savePlan(plan);
                }
                else
                {
                    SchedulesHandler.UpdatePlans(plan);
                }
                Toast.MakeText(Application, "Plan Modified!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(PlanActivity));
                String s_text = SchedulesHandler.WriteToString(plan);
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