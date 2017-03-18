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

namespace Brainstormr.Droid.Fragments.Learning
{
    public partial class LearningHomeFragment : Fragment
    {
        public LearningHomeFragment()
        {
            RegisterMessageHandlers();
        }
        public override void OnDestroy()
        {
            UnregisterMessageHandlers();
            base.OnDestroy();
        }
        public override void OnDestroyView()
        {
            UnregisterMessageHandlers();
            base.OnDestroyView();   
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
            var view = inflater.Inflate(Resource.Layout.ebookshomelayout, container, false);
            var ft = ChildFragmentManager.BeginTransaction();
            var viewfragment = new EbooksFragment();
            ft.Replace(Resource.Id.dry_ebookframelayout, viewfragment);
            ft.Commit();

            //SetHasOptionsMenu(true);
            this.HasOptionsMenu = true;

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.learning_menu_search, menu);
            base.OnCreateOptionsMenu(menu, menuInflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_ebook_ebooks:
                        var ebooksfragment = new EbooksFragment();
                        var ft = ChildFragmentManager.BeginTransaction();
                        var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                        if (fragment != null)
                            ft.Remove(fragment);
                        ft.Replace(Resource.Id.dry_ebookframelayout, ebooksfragment);
                        ft.Commit();
                        return true;
                    case Resource.Id.menu_ebook_ebookcategories:
                        var ebookcategsfragment = new EbookCategoriesFragment();
                        var ft1 = ChildFragmentManager.BeginTransaction();
                        var fragment1 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                        if (fragment1 != null)
                            ft1.Remove(fragment1);
                        ft1.Replace(Resource.Id.dry_ebookframelayout, ebookcategsfragment);
                        ft1.Commit();
                        return true;
                    case Resource.Id.menu_ebook_ebooksubjects:
                        var ebooksubjectsfragment = new SubjectsFragment();
                        var ft2 = ChildFragmentManager.BeginTransaction();
                        var fragment2 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                        if (fragment2 != null)
                            ft2.Remove(fragment2);
                        ft2.Replace(Resource.Id.dry_ebookframelayout, ebooksubjectsfragment);
                        ft2.Commit();
                        return true;
                }

            }
            catch (Exception ex)
            {
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}