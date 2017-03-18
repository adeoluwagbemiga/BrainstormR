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
using Brainstormr.Portable.ViewModel.Evaluation.msg;

namespace Brainstormr.Droid.Fragments.Evaluation
{
    public partial class EvaluationHomeFragment
    {

        private void RegisterMessageHandlers()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_EvaluationDetail>(this, processmsg_EvaluationDetail);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_StartEvaluation>(this, processmsg_StartEvaluation);
        }

        private void UnregisterMessageHandlers()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_EvaluationDetail>(this, processmsg_EvaluationDetail);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_StartEvaluation>(this, processmsg_StartEvaluation);
        }
        private void processmsg_EvaluationDetail(msg_EvaluationDetail msg)
        {
            try
            {
                if (this.Activity == null || this.Activity.IsFinishing) return;
                var viewfragment = new EvaluationDetailFragment(msg);
                var ft = ChildFragmentManager.BeginTransaction();
                ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                ft.Commit();

                //var evalsfragment = new EvaluationDetailFragment(msg.evaluation_dto);
                //var ft = ChildFragmentManager.BeginTransaction();
                //var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_evaldetailframe);
                //if (fragment != null)
                //    ft.Remove(fragment);
                //ft.Replace(Resource.Id.dry_evaldetailframe, evalsfragment);
                //ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
        private void processmsg_TakeEvaluation(msg_TakeEvaluation msg)
        {
            try
            {
                if (this.Activity == null || this.Activity.IsFinishing) return;
                //var viewfragment = new TakeEvaluationFragment(msg);
                //var ft = ChildFragmentManager.BeginTransaction();
                //ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                //ft.Commit();
                var evalsfragment = new TakeEvaluationFragment(msg);
                var ft = ChildFragmentManager.BeginTransaction();
                var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_evalframelayout);
                if (fragment != null)
                    ft.Remove(fragment);
                ft.Replace(Resource.Id.dry_evalframelayout, evalsfragment);
                ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }
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

    }
}