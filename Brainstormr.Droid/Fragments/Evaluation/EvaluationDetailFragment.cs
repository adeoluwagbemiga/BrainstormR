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
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.ViewModel.Evaluation;
using Brainstormr.Portable;
using Brainstormr.Droid.Helpers;
using Brainstormr.Portable.ViewModel.Evaluation.msg;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Views;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public class EvaluationDetailFragment : Fragment
    {
        private void processmsg_TakeEvaluation(msg_TakeEvaluation msg)
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
                ft.Replace(Resource.Id.dry_evalframelayout, new TakeEvaluationFragment(msg));
                ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
        EvaluationDetailViewModel viewmodel;
        public EvaluationDetailFragment(msg_EvaluationDetail _msg_dto)
        {
            msg_dto = _msg_dto;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
            //viewmodel = ServiceLocator.Current.GetInstance<EvaluationDetailViewModel>();
            var dialogservice = ServiceLocator.Current.GetInstance<IDialogService>();
            viewmodel = new EvaluationDetailViewModel(_msg_dto, dialogservice);
        }
        //EvaluationItemModel msg_dto;
        msg_EvaluationDetail msg_dto;
        public override void OnDestroy()
        {
            base.OnDestroy();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
        }
        public override void OnDestroyView()
        {
            base.OnDestroyView();
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
        }
        public EvaluationDetailViewModel Vm
        {
            get
            {
                return viewmodel;
                //return App.Locator.EvaluationDetail;
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
            var view = inflater.Inflate(Resource.Layout.evaldetaillayout, container, false);
            var titlelabel = view.FindViewById<TextView>(Resource.Id.evaldetail_titlelabel);
            titlelabel.Text = "Evaluation Details";

            var code = view.FindViewById<TextView>(Resource.Id.evaldetail_text_code);
            var name = view.FindViewById<TextView>(Resource.Id.evaldetail_text_name);
            var subject = view.FindViewById<TextView>(Resource.Id.evaldetail_text_subject);
            var year = view.FindViewById<TextView>(Resource.Id.evaldetail_text_year);
            var category = view.FindViewById<TextView>(Resource.Id.evaldetail_text_category);
            var duration = view.FindViewById<TextView>(Resource.Id.evaldetail_text_duration);
            var noofquestions = view.FindViewById<TextView>(Resource.Id.evaldetail_text_noofquestions);
            var image = view.FindViewById<ImageView>(Resource.Id.evaldetail_imageView);
            var btntaketest = view.FindViewById<Button>(Resource.Id.btn_evaldetail_taketest);

            code.Text = msg_dto.evaluation_dto.Code;
            name.Text = msg_dto.evaluation_dto.Name;
            subject.Text = msg_dto.evaluation_dto.Subject;
            year.Text = msg_dto.evaluation_dto.Year.ToString();
            category.Text = msg_dto.evaluation_dto.CategoryId.ToString();
            duration.Text = msg_dto.evaluation_dto.Duration.ToString();
            noofquestions.Text = msg_dto.evaluation_dto.NoOfQuestions.ToString();
            //implement image loader for the image.
            var url = GlobalVal.webapibaseurl + msg_dto.evaluation_dto.ImagePath;
            ImageDownloader.AssignImageAsync(image, url, this.Activity);

            Bundle args = new Bundle();
            args.PutInt("EvalID", msg_dto.evaluation_dto.Id);
            Vm.EvalId = msg_dto.evaluation_dto.Id;
            //btntaketest.Click += delegate{
            //    Bundle args = new Bundle();
            //    args.PutInt("EvalID", msg_dto.Id);
            //    try
            //    {
            //        if (this.Activity == null || this.Activity.IsFinishing) return;
            //        var viewfragment = new TakeEvaluationFragment();
            //        var ft = ChildFragmentManager.BeginTransaction();
            //        ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
            //        ft.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //    //Vm.OpenTakeEvaluationCommand.Execute(msg_dto.Id);
            //};
            //btntaketest.Click += Btntaketest_Click;
            btntaketest.Click += delegate {
                Vm.EvalId = msg_dto.evaluation_dto.Id;
                Vm.OpenTakeEvaluationCommand.Execute(null);
            };

            return view;
        }

        private void Btntaketest_Click(object sender, EventArgs e)
        {
            Bundle args = new Bundle();
            args.PutInt("EvalID", msg_dto.evaluation_dto.Id);
            Vm.EvalId = msg_dto.evaluation_dto.Id;
            //Vm.OpenTakeEvaluationCommand.Execute(null);
            var newmsg = new msg_TakeEvaluation(msg_dto.evaluation_dto);
            try
            {
                if (this.Activity == null || this.Activity.IsFinishing) return;
                if (this.ChildFragmentManager == null) return;

                TakeEvaluationFragment frg = new TakeEvaluationFragment(newmsg);

                var ft2 = ChildFragmentManager.BeginTransaction();
                var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_evalframelayout);
                if (fragment != null)
                    ft2.Remove(fragment);
                ft2.Replace(Resource.Id.dry_evalframelayout, frg);
                ft2.Commit();
                //if (this.Activity == null || this.Activity.IsFinishing) return;
                //var viewfragment = new TakeEvaluationFragment();
                //var ft = ChildFragmentManager.BeginTransaction();
                //ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                //ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
    }
}