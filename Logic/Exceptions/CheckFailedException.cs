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

namespace SavingApp.Logic.Exceptions
{
    [Serializable]
    class CheckFailedException : Exception
    {
        public CheckFailedException() : base() { }
        public CheckFailedException(string message) : base(message) { }
        protected CheckFailedException(System.Runtime.Serialization.SerializationInfo info,
       System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}