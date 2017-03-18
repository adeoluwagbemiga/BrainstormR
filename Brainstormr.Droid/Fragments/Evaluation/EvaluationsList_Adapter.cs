using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.Model.Evaluation;
using Object = Java.Lang.Object;
using Java.Lang;
using Brainstormr.Droid.Helpers;
using Brainstormr.Portable;
using Brainstormr.Droid.Services;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationsList_Adapter : BaseAdapter<EvaluationItemModel>, IFilterable
    {
        private List<EvaluationItemModel> _originalData;
        private List<EvaluationItemModel> _items;
        private readonly Activity _context;
        public EvaluationsList_Adapter(Activity activity, IEnumerable<EvaluationItemModel> EvaluationItemModelList)
        {
            _items = EvaluationItemModelList.OrderBy(s => s.Id).ToList();
            _context = activity;

            Filter = new Filter_EvaluationItemModel(this);

        }
        public override EvaluationItemModel this[int position]
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
                convertView = this._context.LayoutInflater.Inflate(Resource.Layout.evallistviewitemlayout, null);

            var code = convertView.FindViewById<TextView>(Resource.Id.eval_text_code);
            var name = convertView.FindViewById<TextView>(Resource.Id.eval_text_name);
            var subject = convertView.FindViewById<TextView>(Resource.Id.eval_text_subject);
            var year = convertView.FindViewById<TextView>(Resource.Id.eval_text_year);
            //var category = convertView.FindViewById<TextView>(Resource.Id.eval_text_category);
            var duration = convertView.FindViewById<TextView>(Resource.Id.eval_text_duration);
            var noofquestions = convertView.FindViewById<TextView>(Resource.Id.eval_text_noofquestions);
            var image = convertView.FindViewById<ImageView>(Resource.Id.eval_imageView);
            var lineitem = _items[position];
            code.Text = lineitem.Code;
            name.Text = lineitem.Name;
            subject.Text = lineitem.Subject;
            year.Text = lineitem.Year.ToString();
            //category.Text = lineitem.CategoryId.ToString();
            duration.Text = lineitem.Duration.ToString();
            noofquestions.Text = lineitem.NoOfQuestions.ToString();
            //implement image loader for the image.
            var url = GlobalVal.webapibaseurl + lineitem.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, _context);
            return convertView;
        }
        private class Filter_EvaluationItemModel : Filter
        {
            private readonly EvaluationsList_Adapter _adapter;
            public Filter_EvaluationItemModel(EvaluationsList_Adapter adapter)
            {
                _adapter = adapter;
            }
            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<EvaluationItemModel>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter._items;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter._originalData.Where(dto => dto.Code.Contains(constraint.ToString()) || dto.Name.Contains(constraint.ToString()) ||
                        dto.Subject.Contains(constraint.ToString()) || dto.Year.ToString().Contains(constraint.ToString())));
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
                        .Select(r => r.ToNetObject<EvaluationItemModel>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }

        }


    }
}