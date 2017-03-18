using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.Learning;
using Brainstormr.Portable.ViewModel.Learning.msg;
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

namespace Brainstormr.Portable.ViewModel.Learning
{
    public class EbookSubjectViewModel : ViewModelBase
    {
        ILearningService _learningService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public EbookSubjectViewModel(ILearningService learningService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _learningService = learningService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            EbookSubjectList = new ObservableCollection<EbookSubjectItemModel>();
        }

        private RelayCommand _LoadEbookSubjectsCommand;

        private RelayCommand<EbookSubjectItemModel> _OpenEbookSubjectDetailCommand;
        public ObservableCollection<EbookSubjectItemModel> EbookSubjectList
        {
            get;
            private set;
        }
        public RelayCommand LoadEbookSubjectsCommand
        {
            get
            {
                return _LoadEbookSubjectsCommand
                    ?? (_LoadEbookSubjectsCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EbookSubjectList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var categorylist = await _learningService.getAllEbookSubjects(usertokenrec);
                                foreach (var item in categorylist)
                                {
                                    EbookSubjectList.Add(item);
                                }
                                RaisePropertyChanged(() => EbookSubjectList);
                                //Messenger.Default.Send<msg_userloggedinAlready>(new msg_userloggedinAlready { loginresult = returnresult });
                            }
                        }
                        catch (System.Exception ex)
                        {

                            await _dialogService.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
        public RelayCommand<EbookSubjectItemModel> OpenEbookSubjectDetailCommand
        {
            get
            {
                return _OpenEbookSubjectDetailCommand
                    ?? (_OpenEbookSubjectDetailCommand = new RelayCommand<EbookSubjectItemModel>(
                    async ebooksubject =>
                    {
                        try
                        {
                            msg_EbookSubjectDetail msg = new msg_EbookSubjectDetail();
                            //msg.memberid = GlobalVal.loginresult.memberid;
                            //msg.commiteedetails = param_committeeitem.Description; msg.commiteeid = param_committeeitem.Id;
                            msg.Id = ebooksubject.Id; msg.Name = ebooksubject.Name; msg.Created = ebooksubject.Created; msg.Modified = ebooksubject.Modified;
                            Messenger.Default.Send<msg_Transport<msg_EbookSubjectDetail>>(new msg_Transport<msg_EbookSubjectDetail>(msg));

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
