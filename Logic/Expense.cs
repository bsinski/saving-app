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

namespace SavingApp.Logic
{
    // because we are serializing the schedule we need expenses to be a public class
    public class Expense
    {
        // even if class is public we need getters and setter for the serialization
        public decimal money { get; set; }
        public DateTime date { get; set; }
        public String name { get; set; }
        public Expense(String name,decimal money,DateTime Date)
        {
            this.name = name;
            this.money = money;
            this.date = Date;
        }
        // we need default constructor for the serialization
        public Expense()
        {
            this.name = "newexpense";
            this.money = 10;
            this.date = DateTime.Today;
        }

    }
}