using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Android.Widget;
using AndroidX.AppCompat.App;
using SavingApp.Logic;

namespace SavingApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]

    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Button newplan_button = FindViewById<Button>(Resource.Id.newplan_button);
            Button browseplans_button = FindViewById<Button>(Resource.Id.browseplans_button);
            Button summary_button = FindViewById<Button>(Resource.Id.summary_button);

            newplan_button.Click += newplan_button_Click;
            browseplans_button.Click += browseplan_Click;
            summary_button.Click += summary_Click;
        }

        protected void newplan_button_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(NewPlanActivity));
            StartActivity(intent);
        }
        protected void browseplan_Click(object sender,EventArgs e)
        {
            var intent = new Intent(this, typeof(ListPlanActivity));
            StartActivity(intent);
        }
        protected void summary_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(StatsActivity));
            StartActivity(intent);
        }
    }
}