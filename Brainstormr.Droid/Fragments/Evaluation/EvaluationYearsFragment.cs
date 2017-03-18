using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Brainstormr.Portable.ViewModel.Evaluation;
using Android.Support.V4.View;
using Brainstormr.Droid.Services;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationYearsFragment : Fragment
    {
        private EvaluationYearsList_Adapter _adapter;
        private Android.Support.V7.Widget.SearchView _searchView;
        public EvaluationYearsViewModel Vm
        {
            get
            {
                return App.Locator.EvaluationYears;
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.listviewlayout, container, false);
            var titlelabel = view.FindViewById<TextView>(Resource.Id.txtlistlabel);
            titlelabel.Text = "Evaluation Years";

            var evalyear_list = view.FindViewById<ListView>(Resource.Id.dry_listview);

            _adapter = new EvaluationYearsList_Adapter(this.Activity, Vm.EvaluationYearsList);
            Vm.LoadEvaluationsYearsCommand.Execute(null);
            evalyear_list.Adapter = _adapter;



            //this.HasOptionsMenu = true;
            return view;
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.eval_menu_search, menu);

            var item = menu.FindItem(Resource.Id.action_search);
            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

            _searchView.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                //  Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(_adapter));
        }
    }
}