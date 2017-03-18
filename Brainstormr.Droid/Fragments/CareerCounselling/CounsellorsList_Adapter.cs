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
//using Brainstormr.Portable.Model.AskAnExpert;
using Java.Lang;
using Brainstormr.Droid.Services;
using Object = Java.Lang.Object;
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;
using Brainstormr.Portable.Model.CareerCounselling;

namespace Brainstormr.Droid.Fragments.CareerCounselling
{
    public class CounsellorsList_Adapter : BaseAdapter<InstructingExpertItemModel>, IFilterable
    {
        private List<InstructingExpertItemModel> _originalData;
        private List<InstructingExpertItemModel> _items;
        private readonly Activity _context;
        public CounsellorsList_Adapter(Activity activity, IEnumerable<InstructingExpertItemModel> CounsellorsItemModelList)
        {
            _items = CounsellorsItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;
            Filter = new Filter_CounsellorItemModel(this);
        }


        public override InstructingExpertItemModel this[int position]
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
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.expertslistviewitemlayout, null);

            var name = convertView.FindViewById<TextView>(Resource.Id.expert_text_name);
            var university = convertView.FindViewById<TextView>(Resource.Id.expert_text_university);
            var isexpert = convertView.FindViewById<TextView>(Resource.Id.expert_text_isexpert);
            var email = convertView.FindViewById<TextView>(Resource.Id.expert_text_email);
            var degree = convertView.FindViewById<TextView>(Resource.Id.expert_text_degree);
            var image = convertView.FindViewById<ImageView>(Resource.Id.expert_imageView);
            var lineitem = _items[position];
            name.Text = lineitem.Name;
            university.Text = lineitem.University;
            if (lineitem.IsCounsellor == true)
            {
                isexpert.Text = "Counsellor";
            }
            email.Text = lineitem.Email;
            degree.Text = lineitem.Degree;
            ////implement image loader for the image.
            var url = GlobalVal.webapibaseurl + lineitem.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, _context);
            return convertView;
        }
        private class Filter_CounsellorItemModel : Filter
        {
            private readonly CounsellorsList_Adapter _adapter;
            public Filter_CounsellorItemModel(CounsellorsList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<InstructingExpertItemModel>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(dto => dto.Name.Contains(constraint.ToString()) || dto.Name.Contains(constraint.ToString())));
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
                        .Select(r => r.ToNetObject<InstructingExpertItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }
        //public TextView Title { get; set; }
    }
}