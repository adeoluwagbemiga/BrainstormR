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
using Brainstormr.Portable.Model.Learning;
using Java.Lang;
using Object = Java.Lang.Object;
using Brainstormr.Droid.Services;
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;

namespace Brainstormr.Droid.Fragments.Learning
{
    class EbooksList_Adapter : BaseAdapter<EbookItemModel>, IFilterable
    {

        private List<EbookItemModel> _originalData;
        private List<EbookItemModel> _items;
        private readonly Activity _context;
        public EbooksList_Adapter(Activity activity, IEnumerable<EbookItemModel> EbookItemModelList)
        {
            _items = EbookItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;

            Filter = new Filter_EbookItemModel(this);

        }
        public override EbookItemModel this[int position]
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
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.ebooklistviewitemlayout, null);

            var name = convertView.FindViewById<TextView>(Resource.Id.ebook_text_name);
            var category = convertView.FindViewById<TextView>(Resource.Id.ebook_text_category);
            var subject = convertView.FindViewById<TextView>(Resource.Id.ebook_text_subject);
            var description = convertView.FindViewById<TextView>(Resource.Id.ebook_text_description);
            var author = convertView.FindViewById<TextView>(Resource.Id.ebook_text_author);
            var amount = convertView.FindViewById<TextView>(Resource.Id.ebook_text_amount);
            var image = convertView.FindViewById<ImageView>(Resource.Id.ebook_imageView);
            var lineitem = _items[position];
            name.Text = lineitem.Name;
            category.Text = lineitem.Category;
            subject.Text = lineitem.Subject;
            description.Text = lineitem.Description;
            author.Text = lineitem.Author;
            amount.Text = lineitem.Amount.ToString();
            //implement image loader for the image.
            var url = GlobalVal.webapibaseurl + lineitem.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, _context);
            return convertView;
        }
        private class Filter_EbookItemModel : Filter
        {
            private readonly EbooksList_Adapter _adapter;
            public Filter_EbookItemModel(EbooksList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<EbookItemModel>();
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
                        .Select(r => r.ToNetObject<EbookItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }
    }
}