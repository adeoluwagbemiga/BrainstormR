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
using Brainstormr.Portable.ViewModel.Learning;
using Android.Support.V4.View;
using Brainstormr.Droid.Services;
using Brainstormr.Portable.ViewModel.Learning.msg;

namespace Brainstormr.Droid.Fragments.Learning
{
    public class EbooksFragment : Fragment, AdapterView.IOnItemClickListener
    {
       
        private EbooksList_Adapter _adapter;
        private Android.Support.V7.Widget.SearchView _searchView;
        public EbooksFragment()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_EbookDetail>(this, processmsg_EbookDetail);
        }
        private void processmsg_EbookDetail(msg_EbookDetail msg)
        {
            try
            {
                //if (this.Activity == null || this.Activity.IsFinishing) return;
                //var viewfragment = new EvaluationDetailFragment(msg.evaluation_dto);
                //var ft = ChildFragmentManager.BeginTransaction();
                //ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                //ft.Commit();

                var ebookfragment = new EbookDetailFragment();
                var ft = ChildFragmentManager.BeginTransaction();
                var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                if (fragment != null)
                    ft.Remove(fragment);
                ft.Replace(Resource.Id.dry_ebookframelayout, ebookfragment);
                ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
        public EbooksViewModel Vm
        {
            get
            {
                return App.Locator.Ebooks;
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
            var view = inflater.Inflate(Resource.Layout.ebooklistviewlayout, container, false);
            var titlelabel = view.FindViewById<TextView>(Resource.Id.ebooklistlabel);
            titlelabel.Text = "Ebooks";

            var ebook_list = view.FindViewById<ListView>(Resource.Id.ebooks_listview);

            _adapter = new EbooksList_Adapter(this.Activity, Vm.EbooksList);
            Vm.LoadEbooksCommand.Execute(null);
            ebook_list.Adapter = _adapter;
            ebook_list.OnItemClickListener = this;

            //this.HasOptionsMenu = true;
            return view;
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var selecteditem = _adapter[position];
            Vm.OpenEbooksDetailCommand.Execute(selecteditem);
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater menuInflater)
        {
            menuInflater.Inflate(Resource.Menu.learning_menu_search, menu);

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