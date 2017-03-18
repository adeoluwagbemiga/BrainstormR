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
using Brainstormr.Portable.Model.Evaluation;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationsFragment : Fragment, AdapterView.IOnItemClickListener
    {
        private EvaluationsList_Adapter _adapter;
        private Android.Support.V7.Widget.SearchView _searchView;

        public EvaluationsViewModel Vm
        {
            get
            {
                return App.Locator.Evaluations;
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
            var view = inflater.Inflate(Resource.Layout.evallistviewlayout, container, false);

            // Create your fragment here
            var titlelabel = view.FindViewById<TextView>(Resource.Id.evaltxtlistlabel);
            titlelabel.Text = "Evaluations";

            var eval_list = view.FindViewById<ListView>(Resource.Id.dry_evalslistview);

            _adapter = new EvaluationsList_Adapter(this.Activity, Vm.EvaluationsList);
            Vm.LoadEvaluationsCommand.Execute(null);
            eval_list.Adapter = _adapter;
            eval_list.OnItemClickListener = this;

            //this.HasOptionsMenu = true;
            return view;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var selecteditem = _adapter[position];
            Vm.OpenEvaluationDetailCommand.Execute(selecteditem);
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
            //base.OnCreateOptionsMenu(menu, menuInflater);
        }
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    try
        //    {
        //        switch (item.ItemId)
        //        {
        //            case Resource.Id.menu_eval_evals:
        //                var evaluationsfragment = new EvaluationsFragment();
        //                var ft = ChildFragmentManager.BeginTransaction();
        //                var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.home_framelayout);
        //                if (fragment != null)
        //                    ft.Remove(fragment);
        //                ft.Replace(Resource.Id.home_framelayout, evaluationsfragment);
        //                ft.Commit();
        //                //var evaluationsfragment = new EvaluationsFragment();
        //                //var ft = ChildFragmentManager.BeginTransaction();
        //                //ft.Replace(Resource.Id.home_framelayout, evaluationsfragment);
        //                //ft.Commit();
        //                return true;
        //            case Resource.Id.menu_eval_evalcategories:
        //                var evaluationcategoriesfragment = new EvaluationCategoriesFragment();
        //                var ft1 = ChildFragmentManager.BeginTransaction();
        //                var fragment1 = this.ChildFragmentManager.FindFragmentById(Resource.Id.home_framelayout);
        //                if (fragment1 != null)
        //                    ft1.Remove(fragment1);
        //                ft1.Replace(Resource.Id.home_framelayout, evaluationcategoriesfragment);
        //                ft1.Commit();
        //                //var evaluationcategoriesfragment = new EvaluationCategoriesFragment();
        //                //var ft1 = ChildFragmentManager.BeginTransaction();
        //                //ft1.Replace(Resource.Id.home_framelayout, evaluationcategoriesfragment);
        //                //ft1.Commit();
        //                return true;
        //            case Resource.Id.menu_eval_evalyears:
        //                var evaluationyearsfragment = new EvaluationYearsFragment();
        //                var ft2 = ChildFragmentManager.BeginTransaction();
        //                var fragment2 = this.ChildFragmentManager.FindFragmentById(Resource.Id.home_framelayout);
        //                if (fragment2 != null)
        //                    ft2.Remove(fragment2);
        //                ft2.Replace(Resource.Id.home_framelayout, evaluationyearsfragment);
        //                ft2.Commit();
        //                //var evaluationyearsfragment = new EvaluationCategoriesFragment();
        //                //var ft2 = ChildFragmentManager.BeginTransaction();
        //                //ft2.Replace(Resource.Id.home_framelayout, evaluationyearsfragment);
        //                //ft2.Commit();
        //                return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return base.OnOptionsItemSelected(item);
        //}

    }
}