using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.LocalDb;

namespace Brainstormr.Droid.Services
{
    public class UIDispatcher : IUIDispatcher
    {
        public void dispatchToUI(Action codetorun)
        {

            using (var h = new Handler(Looper.MainLooper))
            {
                h.Post(codetorun);
            }

            //  GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(codetorun);
        }
    }
}