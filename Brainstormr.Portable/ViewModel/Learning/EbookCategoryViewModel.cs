using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.LocalDb.Learning;
using Brainstormr.Portable.Model.Learning;
using Brainstormr.Portable.ViewModel.Learning.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Learning
{
    public class EbookCategoryViewModel : ViewModelBase
    {
        ILearningService _learningService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        ILocalDbLearningService _locallearningService;
        public EbookCategoryViewModel(ILearningService learningService, ILocalDbService localDbService, IDialogService dialogService,
            ILocalDbLearningService locallearningService)
        {
            _learningService = learningService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            _locallearningService = locallearningService;
            EbookCategoryList = new ObservableCollection<EbookCategoryItemModel>();
        }

        private RelayCommand _LoadEbookCategoriesCommand;
        private RelayCommand<EbookCategoryItemModel> _OpenEbookCategoryDetailCommand;

        public ObservableCollection<EbookCategoryItemModel> EbookCategoryList
        {
            get;
            private set;
        }
        public RelayCommand LoadEbookCategoriesCommand
        {
            get
            {
                return _LoadEbookCategoriesCommand
                    ?? (_LoadEbookCategoriesCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EbookCategoryList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var categorylist = await _learningService.getAllEbookCategories(usertokenrec);
                                foreach (var item in categorylist)
                                {
                                    EbookCategoryList.Add(item);
                                }
                                RaisePropertyChanged(() => EbookCategoryList);
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
        public RelayCommand<EbookCategoryItemModel> OpenEbookCategoryDetailCommand
        {
            get
            {
                return _OpenEbookCategoryDetailCommand
                    ?? (_OpenEbookCategoryDetailCommand = new RelayCommand<EbookCategoryItemModel>(
                    async ebookcategory =>
                    {
                        try
                        {
                            msg_EbookCategoryDetail msg = new msg_EbookCategoryDetail();
                            //msg.memberid = GlobalVal.loginresult.memberid;
                            //msg.commiteedetails = param_committeeitem.Description; msg.commiteeid = param_committeeitem.Id;
                            msg.Id = ebookcategory.Id; msg.Name = ebookcategory.Name; msg.Created = ebookcategory.Created; msg.Modified = ebookcategory.Modified;
                            Messenger.Default.Send<msg_Transport<msg_EbookCategoryDetail>>(new msg_Transport<msg_EbookCategoryDetail>(msg));

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
