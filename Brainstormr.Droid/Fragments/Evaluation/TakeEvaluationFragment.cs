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
using Brainstormr.Portable.ViewModel.Evaluation.msg;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Evaluation;
using GalaSoft.MvvmLight.Helpers;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class TakeEvaluationFragment : Fragment
    {
        private void processmsg_StartEvaluation(msg_StartEvaluation msg)
        {
            try
            {
                if (this.Activity == null || this.Activity.IsFinishing) return;
                //var viewfragment = new TakeEvaluationFragment(msg);
                //var ft = ChildFragmentManager.BeginTransaction();
                //ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                //ft.Commit();
                var ft = this.FragmentManager.BeginTransaction();
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.dry_evalframelayout);
                if (fragment != null)
                    ft.Remove(fragment);
                ft.Replace(Resource.Id.dry_evalframelayout, new StartEvaluationFragment(msg));
                ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_StartEvaluation>(this, processmsg_StartEvaluation);
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_StartEvaluation>(this, processmsg_StartEvaluation);
        }
        msg_TakeEvaluation msg;
        TakeEvaluationViewModel viewmodel;
        private readonly List<Binding> _bindings = new List<Binding>();         public TakeEvaluationFragment(msg_TakeEvaluation _msg)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_StartEvaluation>(this, processmsg_StartEvaluation);
            msg = _msg;
            var dialogservice = ServiceLocator.Current.GetInstance<IDialogService>();
            var evalservice = ServiceLocator.Current.GetInstance<IEvaluationService>();
            var localevalservice = ServiceLocator.Current.GetInstance<ILocalEvaluationService>();
            var localdbservice = ServiceLocator.Current.GetInstance<ILocalDbService>();
            viewmodel = new TakeEvaluationViewModel(evalservice, localdbservice, dialogservice, localevalservice, msg);
        }
        public TakeEvaluationViewModel Vm
        {
            get
            {
                return viewmodel;
                //return App.Locator.TakeEvaluation;
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
            var view = inflater.Inflate(Resource.Layout.evaltakeevallayout, container, false);
            var category = view.FindViewById<TextView>(Resource.Id.takeeval_text_category);
            var subject = view.FindViewById<TextView>(Resource.Id.takeeval_text_subject);
            var noofquestions = view.FindViewById<TextView>(Resource.Id.takeeval_text_noofquestions);
            var totalscore = view.FindViewById<TextView>(Resource.Id.takeeval_text_totalscore);
            var textlabel = view.FindViewById<TextView>(Resource.Id.takeeval_titlelabel);
            textlabel.Text = "Take Exam";
            var btncontinue = view.FindViewById<Button>(Resource.Id.btn_takeeval_continue);

            //_bindings.Add(this.SetBinding(() => Vm.Category, category, () => category.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.NoOfQuestions, noofquestions, () => noofquestions.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.Subject, subject, () => subject.Text, BindingMode.TwoWay));
            //_bindings.Add(this.SetBinding(() => Vm.TotalScore, totalscore, () => totalscore.Text, BindingMode.TwoWay));
            // _bindings.Add(this.SetBinding(() => Vm.TotalScore, totalscore, () => totalscore.Text, BindingMode.TwoWay));
            category.Text = Vm.Category;
            subject.Text = Vm.Subject;
            noofquestions.Text = Vm.NoOfQuestions.ToString();
            totalscore.Text = Vm.TotalScore.ToString();
            btncontinue.Click += (s, e) => {
                Vm.StartEvaluationCommand.Execute(null);
            };
            //int EvalId = Arguments.GetInt("EvalID", 0);
            Vm.EvalId = msg.evaluation_dto.Id;
            //Vm.Category = msg.evaluation_dto.CategoryId.ToString();
            //Vm.TotalScore = msg.evaluation_dto.
            Vm.LoadTakeEvaluationCommand.Execute(msg.evaluation_dto.Id);

            return view;
        }
    }
}