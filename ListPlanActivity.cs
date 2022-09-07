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
using Java.Util;
using System.Xml.Serialization;
using System.IO;
using static Android.Widget.AdapterView;

namespace SavingApp
{
    [Activity(Label = "ListPlanActivity")]
    [Obsolete]
    public class ListPlanActivity : Activity, ListView.IOnItemClickListener
    {
        ListView planlist;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.list_view);
            planlist = FindViewById<ListView>(Resource.Id.planlist);
            Button back = FindViewById<Button>(Resource.Id.backtomain2_button);
            createPlanList();
            planlist.OnItemClickListener = this;
            back.Click += back_Click;

        }
        protected void back_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        protected void createPlanList()
        {
            // method is filling and displaying list of plans
            List<Schedule> s = SchedulesHandler.readPlans();
            string[] plans = new string[s.Count];
            int i = 0;
            foreach (Schedule plan in s)
            {
                plans[i] = plan.Name;
                i++;
            }
            ArrayAdapter adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, plans);
            planlist.SetAdapter(adapter);
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            // method is used to start acitivty and passes serialized plan that was chosen on the list
            List<Schedule> list_s = SchedulesHandler.readPlans();
            Schedule s = list_s[position];
            var intent = new Intent(this, typeof(PlanActivity));
            string s_text = SchedulesHandler.WriteToString(s);
            intent.PutExtra("schedule", s_text);
            StartActivity(intent);
        }




    }
}