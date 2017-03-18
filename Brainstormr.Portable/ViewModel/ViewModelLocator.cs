/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Columbus"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Evaluation;
using Brainstormr.Portable.Model.AskAnExpert;
using Brainstormr.Portable.Model.CareerCounselling;
using Brainstormr.Portable.Model.Evaluation;
using Brainstormr.Portable.Model.Learning;
using Brainstormr.Portable.Model.Login;
using Brainstormr.Portable.Model.MyAccount;
using Brainstormr.Portable.ViewModel.AskAnExpert;
using Brainstormr.Portable.ViewModel.CareerCounselling;
using Brainstormr.Portable.ViewModel.Evaluation;
using Brainstormr.Portable.ViewModel.Learning;
using Brainstormr.Portable.ViewModel.Login;
using Brainstormr.Portable.ViewModel.MyAccount;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace Brainstormr.Portable.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        /// 
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Register all services
            SimpleIoc.Default.Register<ILocalDbService, LocalDbService>();
            SimpleIoc.Default.Register<ILocalEvaluationService, LocalEvaluationService>();
            SimpleIoc.Default.Register<ILoginService, LoginService>();
            SimpleIoc.Default.Register<ILearningService, LearningService>();
            SimpleIoc.Default.Register<IEvaluationService, EvaluationService>();
            SimpleIoc.Default.Register<IMyAccountService, MyAccountService>();
            SimpleIoc.Default.Register<IAskAnExpertService, AskAnExpertService>();
            SimpleIoc.Default.Register<ICareerCounsellingService, CareerCounsellingService>();

            //SimpleIoc.Default.Register<IDialogService, DialogService>();
            //Register all viewmodels
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<EbooksViewModel>();
            SimpleIoc.Default.Register<EbookDetailViewModel>();
            SimpleIoc.Default.Register<EbookSubjectViewModel>();
            SimpleIoc.Default.Register<EbookCategoryViewModel>();
            SimpleIoc.Default.Register<EvaluationsViewModel>();
            SimpleIoc.Default.Register<EvaluationCategoriesViewModel>();
            SimpleIoc.Default.Register<EvaluationYearsViewModel>();
            SimpleIoc.Default.Register<EvaluationDetailViewModel>();
            SimpleIoc.Default.Register<TakeEvaluationViewModel>();
            SimpleIoc.Default.Register<StartEvaluationViewModel>();
            SimpleIoc.Default.Register<MyEbooksViewModel>();
            SimpleIoc.Default.Register<MyEvaluationsViewModel>();
            SimpleIoc.Default.Register<MySubscriptionsViewModel>();
            SimpleIoc.Default.Register<MyMessagesViewModel>();
            SimpleIoc.Default.Register<ExpertsViewModel>();
            SimpleIoc.Default.Register<CounsellorsViewModel>();
            SimpleIoc.Default.Register<ExpertDetailViewModel>();
            SimpleIoc.Default.Register<CounsellorDetailViewModel>();
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }
        public EbooksViewModel Ebooks
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EbooksViewModel>();
            }
        }
        public EbookDetailViewModel EbookDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EbookDetailViewModel>();
            }
        }
        public EbookSubjectViewModel EbookSubjects
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EbookSubjectViewModel>();
            }
        }
        public EbookCategoryViewModel EbookCategories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EbookCategoryViewModel>();
            }
        }
        public EvaluationsViewModel Evaluations
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EvaluationsViewModel>();
            }
        }
        public EvaluationDetailViewModel EvaluationDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EvaluationDetailViewModel>();
            }
        }
        public EvaluationCategoriesViewModel EvaluationCategories
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EvaluationCategoriesViewModel>();
            }
        }
        public EvaluationYearsViewModel EvaluationYears
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EvaluationYearsViewModel>();
            }
        }
        public TakeEvaluationViewModel TakeEvaluation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TakeEvaluationViewModel>();
            }
        }
        public StartEvaluationViewModel StartEvaluation
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartEvaluationViewModel>();
            }
        }
        public MyEbooksViewModel MyEbooks
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyEbooksViewModel>();
            }
        }
        public MyEvaluationsViewModel MyEvaluations
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyEvaluationsViewModel>();
            }
        }
        public MySubscriptionsViewModel MySubscriptions
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MySubscriptionsViewModel>();
            }
        }
        public MyMessagesViewModel MyMessages
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MyMessagesViewModel>();
            }
        }
        public ExpertsViewModel Experts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExpertsViewModel>();
            }
        }
        public CounsellorsViewModel Counsellors
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CounsellorsViewModel>();
            }
        }
        public ExpertDetailViewModel ExpertDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExpertDetailViewModel>();
            }
        }
        public CounsellorDetailViewModel CounsellorDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CounsellorDetailViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
