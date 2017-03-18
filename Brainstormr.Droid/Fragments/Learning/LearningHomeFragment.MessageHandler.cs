using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.ViewModel.Learning.msg;

namespace Brainstormr.Droid.Fragments.Learning
{
    public partial class LearningHomeFragment 
    {
        private void RegisterMessageHandlers()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_EbookDetail>(this, processmsg_EbookDetail);
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
        }


        private void UnregisterMessageHandlers()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_EbookDetail>(this, processmsg_EbookDetail);
            //GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<msg_TakeEvaluation>(this, processmsg_TakeEvaluation);
        }
        private void processmsg_EbookDetail(msg_EbookDetail msg)
        {
            try
            {
                //if (this.Activity == null || this.Activity.IsFinishing) return;
                //var viewfragment = new EvaluationDetailFragment(msg.evaluation_dto);
                //var ft = ChildFragmentManager.BeginTransaction();
                //ft.Replace(Resource.Id.dry_evalframelayout, viewfragment);
                //ft.Commit();

                var ebookfragment = new EbookDetailFragment();
                var ft = ChildFragmentManager.BeginTransaction();
                var fragment = this.ChildFragmentManager.FindFragmentById(Resource.Id.dry_ebookframelayout);
                if (fragment != null)
                    ft.Remove(fragment);
                ft.Replace(Resource.Id.dry_ebookframelayout, ebookfragment);
                ft.Commit();
            }
            catch (Exception ex)
            {
            }
        }

    }
}