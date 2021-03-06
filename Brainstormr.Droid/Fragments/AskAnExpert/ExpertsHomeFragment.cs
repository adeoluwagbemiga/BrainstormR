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

namespace Brainstormr.Droid.Fragments.AskAnExpert
{
    public class ExpertsHomeFragment : Fragment
    {
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
            var view = inflater.Inflate(Resource.Layout.experthomelayout, container, false);
            var ft = ChildFragmentManager.BeginTransaction();
            var viewfragment = new ExpertsFragment();
            ft.Replace(Resource.Id.dry_expertframelayout, viewfragment);
            ft.Commit();

            //SetHasOptionsMenu(true);
            this.HasOptionsMenu = true;

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.expert_menu_search, menu);
            base.OnCreateOptionsMenu(menu, menuInflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_ebook_ebooks:
                        var expertsfragment = new ExpertsFragment();
                        var ft = ChildFragmentManager.BeginTransaction();
                        var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_expertframelayout);
                        if (fragment != null)
                            ft.Remove(fragment);
                        ft.Replace(Resource.Id.dry_expertframelayout, expertsfragment);
                        ft.Commit();
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