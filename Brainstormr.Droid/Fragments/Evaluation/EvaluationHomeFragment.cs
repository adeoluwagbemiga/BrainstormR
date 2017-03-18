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

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public partial class EvaluationHomeFragment : Fragment
    {
        public EvaluationHomeFragment()
        {
            RegisterMessageHandlers();
        }
        public override void OnResume()
        {
            base.OnResume();
            RegisterMessageHandlers();
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterMessageHandlers();
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            UnregisterMessageHandlers();
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
            var view = inflater.Inflate(Resource.Layout.evalhomelayout, container, false);
            var ft = ChildFragmentManager.BeginTransaction();
            var viewfragment = new EvaluationsFragment();
            ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
            ft.Commit();

            this.HasOptionsMenu = true;
            return view;
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.eval_menu_search, menu);
            base.OnCreateOptionsMenu(menu, menuInflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_eval_evals:
                        var evalsfragment = new EvaluationsFragment();
                        var ft = ChildFragmentManager.BeginTransaction();
                        var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_evalframelayout);
                        if (fragment != null)
                            ft.Remove(fragment);
                        ft.Replace(Resource.Id.dry_evalframelayout, evalsfragment);
                        ft.Commit();
                        return true;
                    case Resource.Id.menu_eval_evalcategories:
                        var evalcategsfragment = new EvaluationCategoriesFragment();
                        var ft1 = ChildFragmentManager.BeginTransaction();
                        var fragment1 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_evalframelayout);
                        if (fragment1 != null)
                            ft1.Remove(fragment1);
                        ft1.Replace(Resource.Id.dry_evalframelayout, evalcategsfragment);
                        ft1.Commit();
                        return true;
                    //case Resource.Id.menu_ebook_ebooksubjects:
                    //    var ebooksubjectsfragment = new SubjectsFragment();
                    //    var ft2 = ChildFragmentManager.BeginTransaction();
                    //    var fragment2 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                    //    if (fragment2 != null)
                    //        ft2.Remove(fragment2);
                    //    ft2.Replace(Resource.Id.dry_ebookframelayout, ebooksubjectsfragment);
                    //    ft2.Commit();
                    //    return true;
                }

            }
            catch (Exception ex)
            {
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}