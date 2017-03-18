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
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Brainstormr.Droid.Services;
using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.ViewModel;
using Brainstormr.Portable.ViewModel.Login;
using Brainstormr.Portable.ViewModel.Evaluation;

namespace Brainstormr.Droid
{
    public static class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    //var nav = new NavigationService();

                    SimpleIoc.Default.Register<INavigationService, NavigationService>();
                    SimpleIoc.Default.Register<IDialogService, BrainstormrDialog>();
                    SimpleIoc.Default.Register<ISQLiteConnectionService, SQLiteConnectionService>();
                    SimpleIoc.Default.Register<IUIDispatcher, UIDispatcher>();
                    SimpleIoc.Default.Register<LoginViewModel>();
                    SimpleIoc.Default.Register<EvaluationsViewModel>();
                    SimpleIoc.Default.Register<EvaluationDetailViewModel>();
                    SimpleIoc.Default.Register<TakeEvaluationViewModel>();
                    SimpleIoc.Default.Register<StartEvaluationViewModel>();
                    _locator = new ViewModelLocator();
                }

                return _locator;
            }
        }
        public static Android.Support.V7.App.AppCompatActivity currentactivity { get; set; }

    }
}