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
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;
using Brainstormr.Portable.ViewModel.Learning.msg;

namespace Brainstormr.Droid.Fragments.Learning
{
    public class EbookDetailFragment : Fragment
    {
        public EbookDetailViewModel Vm
        {
            get
            {
                return App.Locator.EbookDetail;
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
            var view = inflater.Inflate(Resource.Layout.ebookdetaillayout, container, false);

            var titlelabel = view.FindViewById<TextView>(Resource.Id.ebookdetail_titlelabel);
            titlelabel.Text = "Ebook Detail";
            var name = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_name);
            var subject = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_subject);
            var featured = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_featured);
            var description = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_description);
            var author = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_author);
            var amount = view.FindViewById<TextView>(Resource.Id.ebookdetail_text_amount);
            var image = view.FindViewById<ImageView>(Resource.Id.ebookdetail_imageView);
            var btnpreview = view.FindViewById<Button>(Resource.Id.btn_ebookdetail_previewebook);

            name.Text = Vm.Name;
            subject.Text = Vm.Subject;
            featured.Text = Vm.Featured.ToString();
            description.Text = Vm.Description;
            author.Text = Vm.Author;
            amount.Text = Vm.Amount.ToString();
            var url = GlobalVal.webapibaseurl + Vm.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, this.Activity);

            btnpreview.Click += Btnpreview_Click;

            return view;
        }

        private void Btnpreview_Click(object sender, EventArgs e)
        {
            Vm.NavigateToEbookPreviewCommand.Execute(null);
        }
    }
}