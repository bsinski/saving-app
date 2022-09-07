using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SavingApp.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SavingApp.Logic
{
    static class InputCheck
    {
        public static void checkPlanInput(String dateS,String dateE,String name,String money,Activity a,bool newplan)
        {
            // we are using two different exceptions and CheckFailedException is used to unify all other exceptions
            List<Schedule> schedules = SchedulesHandler.readPlans();
            try
            {
                DateTime ds = Convert.ToDateTime(dateS);
                DateTime de = Convert.ToDateTime(dateE);
                String nm = name;
                decimal mn = Convert.ToDecimal(money, CultureInfo.InvariantCulture);
                if (DateTime.Compare(ds, de) >= 0)
                {
                    throw new InvalidInputException("End of plan should be after start of the plan");
                }
                if (mn < 0)
                {
                    throw new InvalidInputException("Plan's credit sould be greater than zero");
                }
                int index = schedules.FindIndex(sch => sch.Name == nm);
                if (index > -1 && newplan)
                {
                    throw new InvalidInputException("Plan with this name already exists. Plans should have unique names");
                }
            }
            catch (InvalidCastException e)
            {
                Toast.MakeText(a.Application, "Wrong input type!", ToastLength.Short).Show();
                throw new CheckFailedException();
            }
            catch (InvalidInputException e)
            {
                Toast.MakeText(a.Application, e.Message, ToastLength.Short).Show();
                throw new CheckFailedException();
            }
            catch(FormatException e)
            {
                Toast.MakeText(a.Application, "Wrong input type!", ToastLength.Short).Show();
                throw new CheckFailedException();
            }
            

        }

        public static void checkExpenseInput(String date,String name, String money,Activity a)
        {
            try
            {
                DateTime d = Convert.ToDateTime(date);
                String nm = name;
                decimal mn = Convert.ToDecimal(money, CultureInfo.InvariantCulture);
                if (mn < 0)
                {
                    throw new InvalidInputException("Expense credit sould be greater than zero");
                }
            }
            catch (InvalidCastException e)
            {
                Toast.MakeText(a.Application, "Wrong input type!", ToastLength.Short).Show();
                throw new CheckFailedException();
            }
            catch (InvalidInputException e)
            {
                Toast.MakeText(a.Application, e.Message, ToastLength.Short).Show();
                throw new CheckFailedException();
            }

        }
    }
}