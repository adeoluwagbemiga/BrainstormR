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
using Brainstormr.Portable.ViewModel.MyAccount;
using Android.Support.V4.View;
using Brainstormr.Droid.Services;

namespace Brainstormr.Droid.Fragments.MyAccount
{
    public class MyMessagesFragment : Fragment, AdapterView.IOnItemClickListener
    {
        private MyMessagesList_Adapter _adapter;
        private Android.Support.V7.Widget.SearchView _searchView;
        public MyMessagesViewModel Vm
        {
            get
            {
                return App.Locator.MyMessages;
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
            var view = inflater.Inflate(Resource.Layout.mymessageslistviewlayout, container, false);
            var titlelabel = view.FindViewById<TextView>(Resource.Id.mymessageslistlabel);
            titlelabel.Text = "My Messages";

            var mysub_list = view.FindViewById<ListView>(Resource.Id.mymessages_listview);

            _adapter = new MyMessagesList_Adapter(this.Activity, Vm.MyMessagesList);
            Vm.LoadMyMessagesCommand.Execute(null);
            mysub_list.Adapter = _adapter;
            mysub_list.OnItemClickListener = this;

            return view;
        }
        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var selecteditem = _adapter[position];
            Vm.OpenMyMessageDetailCommand.Execute(selecteditem);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.myaccount_menu_search, menu);

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
    }
}