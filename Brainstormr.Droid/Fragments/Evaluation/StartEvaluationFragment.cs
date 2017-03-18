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
using Brainstormr.Portable.ViewModel.Evaluation;
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;
using GalaSoft.MvvmLight.Helpers;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Brainstormr.Portable.LocalDb.Evaluation;
using Brainstormr.Portable.LocalDb;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class StartEvaluationFragment : Fragment, AdapterView.IOnItemClickListener
    {
        msg_StartEvaluation msg;
        StartEvaluationViewModel viewmodel;
        private readonly List<Binding> _bindings = new List<Binding>();
        public StartEvaluationFragment(msg_StartEvaluation _msg)
        {
            msg = _msg;
            var dialogservice = ServiceLocator.Current.GetInstance<IDialogService>();
            var evalservice = ServiceLocator.Current.GetInstance<IEvaluationService>();
            var localevalservice = ServiceLocator.Current.GetInstance<ILocalEvaluationService>();
            var localdbservice = ServiceLocator.Current.GetInstance<ILocalDbService>();
            viewmodel = new StartEvaluationViewModel(evalservice, localdbservice, dialogservice, localevalservice, msg);
        }
        public StartEvaluationViewModel Vm
        {
            get
            {
                return viewmodel;
                //return App.Locator.StartEvaluation;
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
            var view = inflater.Inflate(Resource.Layout.evalstartevallayout, container, false);
            var category = view.FindViewById<TextView>(Resource.Id.starteval_text_category);
            var subject = view.FindViewById<TextView>(Resource.Id.starteval_text_subject);
            var noofquestions = view.FindViewById<TextView>(Resource.Id.starteval_text_noofquestions);
            var totalscore = view.FindViewById<TextView>(Resource.Id.starteval_text_totalscore);
            var textlabel = view.FindViewById<TextView>(Resource.Id.starteval_titlelabel);
            textlabel.Text = "Start Exam";
            var btnnext = view.FindViewById<Button>(Resource.Id.btn_starteval_next);
            var btnprevious = view.FindViewById<Button>(Resource.Id.btn_starteval_previous);
            var btnsubmit = view.FindViewById<Button>(Resource.Id.btn_starteval_submit);
            var qimage = view.FindViewById<ImageView>(Resource.Id.starteval_imageView);
            var optionlist = view.FindViewById<ListView>(Resource.Id.starteval_optionslistview);
            var questiontext = view.FindViewById<TextView>(Resource.Id.starteval_text_questiontext);

            //_bindings.Add(this.SetBinding(() => Vm.Category, category, () => category.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.NoOfQuestions, noofquestions, () => noofquestions.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.Subject, subject, () => subject.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.TotalScore, totalscore, () => totalscore.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.QuestionText, questiontext, () => questiontext.Text, BindingMode.TwoWay));


            Vm.EvalId = msg._evalId;

            category.Text = Vm.Category;
            subject.Text = Vm.Subject;
            noofquestions.Text = Vm.NoOfQuestions.ToString();
            totalscore.Text = Vm.TotalScore.ToString();
            questiontext.Text = Vm.QuestionText;

            if (Vm.ImagePath == "")
            {
                qimage.Visibility = ViewStates.Gone;
            }
            else
            {
                qimage.Visibility = ViewStates.Visible;
                var url = GlobalVal.webapibaseurl + Vm.ImagePath;
                ImageDownloader.AssignImageAsync(qimage, url, this.Activity);
            }
            _bindings.Add(this.SetBinding(() => Vm.QuestionOptionsList).WhenSourceChanges(() =>
            {
                optionlist.Adapter = Vm.QuestionOptionsList.GetAdapter(GetOptionsList);
                optionlist.OnItemClickListener = this;
            }));
            
            btnnext.Click += Btnnext_Click;
            btnprevious.Click += Btnprevious_Click;
            btnsubmit.Click += Btnsubmit_Click;

            Vm.LoadStartEvaluationCommand.Execute(msg._evalId);
            return view;
        }

        private View GetOptionsList(int position, QuestionOptionItemModel model, View convertView)
        {
            convertView = this.Activity.LayoutInflater.Inflate(Resource.Layout.evalstartevaloptionlistviewitemlayout, null);
            convertView.FindViewById<TextView>(Resource.Id.starteval_text_questionoption).Text = model.OptionText;

            return convertView;
        }

        private void Btnsubmit_Click(object sender, EventArgs e)
        {
            Vm.SubmitEvaluationCommand.Execute(null);
        }

        private void Btnprevious_Click(object sender, EventArgs e)
        {
            Vm.PreviousQuestionCommand.Execute(null);
        }

        private void Btnnext_Click(object sender, EventArgs e)
        {
            Vm.NextQuestionCommand.Execute(null);
        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            var selecteditem = Vm.QuestionOptionsList[position];
            Vm.AnswerSelectedCommand.Execute(selecteditem);
        }
    }
}