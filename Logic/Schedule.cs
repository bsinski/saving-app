using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Java.Interop;
using Java.Util;

namespace SavingApp.Logic
{
    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Expense))]
    
    // schedule class is the equivalent of the saving plan in oue solution
    public class Schedule 
    {
        private decimal money;
        private DateTime date_start;
        private DateTime date_end;
        private string name;
        private List<Expense> expenses = new List<Expense>();

        // we have default constructor beacuse of the serialization
        public Schedule()
        {
            this.name = "newplan";
            this.money = 0;
            this.date_start =  new DateTime();
            this.date_end = DateTime.Today;     
        }
        public Schedule(string name,decimal money,DateTime date_start,DateTime date_end)
        {
            this.name = name;
            this.money = money;
            this.date_start = date_start;
            this.date_end = date_end;
        }


        public decimal Money
        {
            get => money;
            set => money = value;
        }

        public DateTime DateStart
        {
            get => date_start;
            set => date_start = value;
        }

        public DateTime DateEnd
        {
            get => date_end;
            set => date_end = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }


        public List<Expense> Expenses { get; set; }

        public void AddMoney(decimal credit)
        { 
            Money = Money + credit;
        }
        public void SubstractMoney(decimal credit)
        {
            Money = Money - credit;
        }

        // three following methods are for the divinding funds per given time frames
        public decimal PerDay()
        {
            TimeSpan value = date_end.Subtract(date_start);

            return (money / value.Days);
        }
        public decimal PerMonth()
        {
            TimeSpan value = date_end.Subtract(date_start);
            if(value.Days < 30.4)
            {
                return 0;
            }
            else
            {
                return ((decimal)(money * (decimal)30.5 / value.Days));
            }
            
        }
        public decimal PerYear()
        {
            TimeSpan value = date_end.Subtract(date_start);
            if (value.Days < 365.2)
            {
                return 0;
            }
            else
            {
                return ((decimal)(money * (decimal)365.250 / value.Days));
            }
            
        }

        public void AddExpense(Expense e)
        {
            
            Expenses.Add(e);
            SubstractMoney(e.money);
        }
        


    }
}