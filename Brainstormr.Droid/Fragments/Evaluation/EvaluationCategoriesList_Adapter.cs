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
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Droid.Helpers;
using Java.Lang;
using Object = Java.Lang.Object;
using Brainstormr.Droid.Services;
using Brainstormr.Portable;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationCategoriesList_Adapter : BaseAdapter<EvaluationCategoryItemModel>, IFilterable
    {

        private List<EvaluationCategoryItemModel> _originalData;
        private List<EvaluationCategoryItemModel> _items;
        private readonly Activity _context;
        public EvaluationCategoriesList_Adapter(Activity activity, IEnumerable<EvaluationCategoryItemModel> EvaluationCatgoryItemModelList)
        {
            _items = EvaluationCatgoryItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;

            Filter = new Filter_EvaluationCategoryItemModel(this);

        }
        public override EvaluationCategoryItemModel this[int position]
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
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.evalcategorylistviewitemlayout, null);

            var code = convertView.FindViewById<TextView>(Resource.Id.evalcateg_text_code);
            var name = convertView.FindViewById<TextView>(Resource.Id.evalcateg_text_name);
            var image = convertView.FindViewById<ImageView>(Resource.Id.evalcateg_imageView);
            var lineitem = _items[position];
            code.Text = lineitem.Code;
            name.Text = lineitem.Name;
            //implement image loader for the image.
            var url = GlobalVal.webapibaseurl + lineitem.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, _context);
            return convertView;
        }
        private class Filter_EvaluationCategoryItemModel : Filter
        {
            private readonly EvaluationCategoriesList_Adapter _adapter;
            public Filter_EvaluationCategoryItemModel(EvaluationCategoriesList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<EvaluationCategoryItemModel>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(dto => dto.Code.Contains(constraint.ToString()) || dto.Name.Contains(constraint.ToString())));
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
                        .Select(r => r.ToNetObject<EvaluationCategoryItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }

    }
}