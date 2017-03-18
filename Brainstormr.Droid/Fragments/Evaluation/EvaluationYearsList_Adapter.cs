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
using Java.Lang;
using Object = Java.Lang.Object;
using Brainstormr.Droid.Services;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationYearsList_Adapter : BaseAdapter<EvaluationYearItemModel>, IFilterable
    {

        private List<EvaluationYearItemModel> _originalData;
        private List<EvaluationYearItemModel> _items;
        private readonly Activity _context;
        public EvaluationYearsList_Adapter(Activity activity, IEnumerable<EvaluationYearItemModel> EvaluationYearItemModelList)
        {
            _items = EvaluationYearItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;

            Filter = new Filter_EvaluationYearItemModel(this);

        }
        public override EvaluationYearItemModel this[int position]
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
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.evalyearlistviewitemlayout, null);

            var year = convertView.FindViewById<TextView>(Resource.Id.evalyear_text_year);
            var lineitem = _items[position];
            year.Text = lineitem.Year.ToString();

            return convertView;
        }
        private class Filter_EvaluationYearItemModel : Filter
        {
            private readonly EvaluationYearsList_Adapter _adapter;
            public Filter_EvaluationYearItemModel(EvaluationYearsList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<EvaluationYearItemModel>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(dto => dto.Year.ToString().Contains(constraint.ToString())));
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
                        .Select(r => r.ToNetObject<EvaluationYearItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }
    }

}