using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.Model.MyAccount;
using Java.Lang;
using Object = Java.Lang.Object;
using Brainstormr.Droid.Services;
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;

namespace Brainstormr.Droid.Fragments.MyAccount
{
    public class MyMessagesList_Adapter : BaseAdapter<MyMessageItemModel>, IFilterable
    {
        private List<MyMessageItemModel> _originalData;
        private List<MyMessageItemModel> _items;
        private readonly Activity _context;

        public MyMessagesList_Adapter(Activity activity, IEnumerable<MyMessageItemModel> MyMessageItemModelList)
        {
            _items = MyMessageItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;

            Filter = new Filter_MyMessageItemModel(this);
        }

        public override MyMessageItemModel this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public Filter Filter { get; private set; }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.mymessageslistviewitemlayout, null);

            var instructorname = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_instructorname);
            var message = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_message);
            var reply = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_reply);
            var subject = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_subject);
            var useremail = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_useremail);
            var read = convertView.FindViewById<TextView>(Resource.Id.mymessage_text_read);
            var image = convertView.FindViewById<ImageView>(Resource.Id.mymessage_imageView);
            var lineitem = _items[position];
            instructorname.Text = lineitem.InstructorName;
            message.Text = lineitem.Message;
            subject.Text = lineitem.Subject;
            reply.Text = lineitem.Reply;
            useremail.Text = lineitem.UserEmail;
            read.Text = lineitem.Read;
            //implement image loader for the image.
            var url = GlobalVal.webapibaseurl + lineitem.InstructorImage;
            ImageDownloader.AssignImageAsync(image, url, _context);
            return convertView;
        }
        private class Filter_MyMessageItemModel : Filter
        {
            private readonly MyMessagesList_Adapter _adapter;
            public Filter_MyMessageItemModel(MyMessagesList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<MyMessageItemModel>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(dto => dto.InstructorName.Contains(constraint.ToString()) || dto.Subject.Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter._items = values.ToArray<Object>()
                        .Select(r => r.ToNetObject<MyMessageItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }
    }
}