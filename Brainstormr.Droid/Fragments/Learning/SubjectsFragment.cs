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
using GalaSoft.MvvmLight.Helpers;
using Brainstormr.Portable.Model.Learning;

namespace Brainstormr.Droid.Fragments.Learning
{
    public class SubjectsFragment : Fragment
    {
        public EbookSubjectViewModel Vm
        {
            get
            {
                return App.Locator.EbookSubjects;
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
            var view = inflater.Inflate(Resource.Layout.ebooksubjectlistviewlayout, container, false);
            var titlelabel = view.FindViewById<TextView>(Resource.Id.ebooksubjectlistlabel);
            titlelabel.Text = "Subjects";
            var ebookcategory_list = view.FindViewById<ListView>(Resource.Id.ebooksubject_listview);

            ebookcategory_list.Adapter = Vm.EbookSubjectList.GetAdapter(GetSubjectItemList);
            Vm.LoadEbookSubjectsCommand.Execute(null);

            return view;
        }

        private View GetSubjectItemList(int position, EbookSubjectItemModel model, View convertView)
        {
            convertView = this.Activity.LayoutInflater.Inflate(Resource.Layout.ebooksubjectlistviewitemlayout, null);
            convertView.FindViewById<TextView>(Resource.Id.ebooksubject_text_name).Text = model.Name;

            return convertView;

        }
    }
}