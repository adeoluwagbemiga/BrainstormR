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

namespace Brainstormr.Droid.Fragments.MyAccount
{
    public class MyAccountHomeFragment : Fragment
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
            var view = inflater.Inflate(Resource.Layout.myaccounthomelayout, container, false);
            var ft = ChildFragmentManager.BeginTransaction();
            var viewfragment = new MyEbooksFragment();
            ft.Replace(Resource.Id.dry_myaccountframelayout, viewfragment);
            ft.Commit();

            //SetHasOptionsMenu(true);
            this.HasOptionsMenu = true;

            return view;
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.myaccount_menu_search, menu);
            base.OnCreateOptionsMenu(menu, menuInflater);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            try
            {
                switch (item.ItemId)
                {
                    case Resource.Id.menu_myaccount_myebooks:
                        var myebooksfragment = new MyEbooksFragment();
                        var ft = ChildFragmentManager.BeginTransaction();
                        var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_myaccountframelayout);
                        if (fragment != null)
                            ft.Remove(fragment);
                        ft.Replace(Resource.Id.dry_myaccountframelayout, myebooksfragment);
                        ft.Commit();
                        return true;
                    case Resource.Id.menu_myaccount_myevals:
                        var ebookcategsfragment = new MyEvalsFragment();
                        var ft1 = ChildFragmentManager.BeginTransaction();
                        var fragment1 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_myaccountframelayout);
                        if (fragment1 != null)
                            ft1.Remove(fragment1);
                        ft1.Replace(Resource.Id.dry_myaccountframelayout, ebookcategsfragment);
                        ft1.Commit();
                        return true;
                    case Resource.Id.menu_myaccount_mymessages:
                        var ebooksubjectsfragment = new MyMessagesFragment();
                        var ft2 = ChildFragmentManager.BeginTransaction();
                        var fragment2 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_myaccountframelayout);
                        if (fragment2 != null)
                            ft2.Remove(fragment2);
                        ft2.Replace(Resource.Id.dry_myaccountframelayout, ebooksubjectsfragment);
                        ft2.Commit();
                        return true;
                    case Resource.Id.menu_myaccount_mysubscriptions:
                        var mysubscriptionsfragment = new MySubscriptionsFragment();
                        var ft3 = ChildFragmentManager.BeginTransaction();
                        var fragment3 = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_myaccountframelayout);
                        if (fragment3 != null)
                            ft3.Remove(fragment3);
                        ft3.Replace(Resource.Id.dry_myaccountframelayout, mysubscriptionsfragment);
                        ft3.Commit();
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