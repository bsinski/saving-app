using Android.App;
using Android.OS;
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SavingApp.Logic;
using System.Threading.Tasks;
using System.Globalization;
using SavingApp.Logic.Exceptions;

namespace SavingApp
{
    [Activity(Label = "NewPlanActivity")]
    public class NewPlanActivity : Activity
    {
        private String dateS;
        private String dateE;
        private String money;
        private String name;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        SetContentView(Resource.Layout.new_plan_first);
        EditText dateStart = FindViewById<EditText>(Resource.Id.dateStart_input);
        EditText dateEnd = FindViewById<EditText>(Resource.Id.dateEnd_input);
        EditText moneyIn = FindViewById<EditText>(Resource.Id.money_input);
        EditText nameIn = FindViewById<EditText>(Resource.Id.name_input);
        Button acceptPlan_button = FindViewById<Button>(Resource.Id.acceptPlan_button);
        Button back_button = FindViewById<Button>(Resource.Id.backtomain_button);
            money = "100.00";
            moneyIn.Text = money;
            name = "newplan";
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
        acceptPlan_button.Click += acceptPlan_Click;
        back_button.Click += back_Click;
    }

    protected void acceptPlan_Click(object sender, EventArgs e)
    {
            bool valid_data = true;
            try
            {
                // checking the correctnes of the input
                InputCheck.checkPlanInput(dateS, dateE, name, money, this,true);
            }catch(CheckFailedException exc)
            {
                valid_data = false;
            }
            if (valid_data == true)
            {
                Schedule saving_plan = new Schedule(name, Convert.ToDecimal(money, CultureInfo.InvariantCulture), Convert.ToDateTime(dateS), Convert.ToDateTime(dateE));
               // saving new plan
                SchedulesHandler.savePlan(saving_plan);
                Toast.MakeText(Application, "New plan created", ToastLength.Short).Show();
                Recreate();
            }
        
    }
    protected void back_Click(object sender, EventArgs e)
    {
         var intent = new Intent(this, typeof(MainActivity));
         StartActivity(intent);
    }

    }
    
}