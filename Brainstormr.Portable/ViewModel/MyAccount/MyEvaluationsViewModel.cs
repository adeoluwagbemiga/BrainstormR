using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.MyAccount;
using Brainstormr.Portable.ViewModel.MyAccount.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.MyAccount
{
    public class MyEvaluationsViewModel : ViewModelBase
    {
        IMyAccountService _myAccountService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;

        public MyEvaluationsViewModel(IMyAccountService myAccountService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _myAccountService = myAccountService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            MyEvaluationsList = new ObservableCollection<MyEvaluationItemModel>();
        }

        private RelayCommand _LoadMyEvaluationsCommand;
        private RelayCommand<MyEvaluationItemModel> _OpenMyEvaluationDetailCommand;
        public ObservableCollection<MyEvaluationItemModel> MyEvaluationsList
        {
            get;
            private set;
        }

        public RelayCommand LoadMyEvaluationsCommand
        {
            get
            {
                return _LoadMyEvaluationsCommand
                    ?? (_LoadMyEvaluationsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            MyEvaluationsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var evaluationlist = await _myAccountService.getMyEvaluations(usertokenrec);
                                foreach (var item in evaluationlist)
                                {
                                    MyEvaluationsList.Add(item);
                                }
                                RaisePropertyChanged(() => MyEvaluationsList);
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<MyEvaluationItemModel> OpenMyEvaluationDetailCommand
        {
            get
            {
                return _OpenMyEvaluationDetailCommand
                    ?? (_OpenMyEvaluationDetailCommand = new RelayCommand<MyEvaluationItemModel>(
                    async evaldetail =>
                    {
                        try
                        {
                            //var msg = new msg_MyEvaluationDetail()
                            //{
                            //    AvailableTotal = evaldetail.AvailableTotal,
                            //    Created = evaldetail.Created,
                            //    Subject = evaldetail.Subject,
                            //    Duration = evaldetail.Duration,
                            //    Id = evaldetail.Id,
                            //    Modified = evaldetail.Modified,
                            //    NoOfQuestions = evaldetail.NoOfQuestions,
                            //    PassPercentage = evaldetail.PassPercentage,
                            //    QuestionCategory = evaldetail.QuestionCategory,
                            //    QuestionId = evaldetail.QuestionId,
                            //    TotalScore = evaldetail.TotalScore,
                            //    UserEmail = evaldetail.UserEmail,
                            //    Year = evaldetail.Year
                            //};
                            var msg = new msg_MyEvaluationDetail(evaldetail);
                            Messenger.Default.Send<msg_Transport<msg_MyEvaluationDetail>>(new msg_Transport<msg_MyEvaluationDetail>(msg));

                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }

    }
}
