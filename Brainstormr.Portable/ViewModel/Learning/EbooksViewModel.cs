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
    public class EbooksViewModel : ViewModelBase
    {
        ILearningService _learningService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        public EbooksViewModel(ILearningService learningService, ILocalDbService localDbService, IDialogService dialogService)
        {
            _learningService = learningService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            EbooksList = new ObservableCollection<EbookItemModel>();
        }

        private RelayCommand _LoadEbooksCommand;

        private RelayCommand<EbookItemModel> _OpenEbooksDetailCommand;
        public ObservableCollection<EbookItemModel> EbooksList
        {
            get;
            private set;
        }
        public RelayCommand LoadEbooksCommand
        {
            get
            {
                return _LoadEbooksCommand
                    ?? (_LoadEbooksCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            EbooksList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var ebooklist = await _learningService.getAllEbooks(usertokenrec);
                                foreach (var item in ebooklist)
                                {
                                    EbooksList.Add(item);
                                }
                                RaisePropertyChanged(() => EbooksList);
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
        public RelayCommand<EbookItemModel> OpenEbooksDetailCommand
        {
            get
            {
                return _OpenEbooksDetailCommand
                    ?? (_OpenEbooksDetailCommand = new RelayCommand<EbookItemModel>(
                    async ebook =>
                    {
                        try
                        {
                            //var msg = new msg_EbookDetail(ebook) {
                            //    Amount = ebook.Amount,
                            //    Author = ebook.Author,
                            //    Category = ebook.Category,
                            //    Created = ebook.Created,
                            //    Description = ebook.Description,
                            //    Featured = ebook.Featured,
                            //    FileName = ebook.FileName,
                            //    FilePath = ebook.FilePath,
                            //    Id = ebook.Id,
                            //    ImageName = ebook.ImageName,
                            //    ImagePath = ebook.ImagePath,
                            //    Modified = ebook.Modified,
                            //    Name = ebook.Name,
                            //    PreviewFile = ebook.PreviewFile,
                            //    PreviewPath = ebook.PreviewPath,
                            //    Subject = ebook.Subject
                            //};
                            var msg = new msg_EbookDetail(ebook);
                            //msg.ebookdto.Amount = ebook.Amount;
                            //msg.ebookdto.Author = ebook.Author;
                            //msg.ebookdto.Category = ebook.Category;
                            //msg.ebookdto.Created = ebook.Created;
                            //msg.ebookdto.Description = ebook.Description;
                            //msg.ebookdto.Featured = ebook.Featured;
                            //msg.ebookdto.FileName = ebook.FileName;
                            //msg.ebookdto.FilePath = ebook.FilePath;
                            //msg.ebookdto.Id = ebook.Id;
                            //msg.ebookdto.ImageName = ebook.ImageName;
                            //msg.ebookdto.ImagePath = ebook.ImagePath;
                            //msg.ebookdto.Modified = ebook.Modified;
                            //msg.ebookdto.Name = ebook.Name;
                            //msg.ebookdto.PreviewFile = ebook.PreviewFile;
                            //msg.ebookdto.PreviewPath = ebook.PreviewPath;
                            //msg.ebookdto.Subject = ebook.Subject;
                            Messenger.Default.Send<msg_Transport<msg_EbookDetail>>(new msg_Transport<msg_EbookDetail>(msg));

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
