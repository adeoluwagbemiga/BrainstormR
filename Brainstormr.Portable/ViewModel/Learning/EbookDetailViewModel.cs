using Brainstormr.Portable.ViewModel.Learning.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Learning
{
    public class EbookDetailViewModel : ViewModelBase
    {
        msg_EbookDetail _dto;
        public EbookDetailViewModel(msg_EbookDetail dto)
        {
            if (dto != null)
            {
                Id = dto.ebookdto.Id; RaisePropertyChanged(() => Id);
                Name = dto.ebookdto.Name; RaisePropertyChanged(() => Name);
                Category = dto.ebookdto.Category; RaisePropertyChanged(() => Category);
                Subject = dto.ebookdto.Subject; RaisePropertyChanged(() => Subject);
                Description = dto.ebookdto.Description; RaisePropertyChanged(() => Description);
                Author = dto.ebookdto.Author; RaisePropertyChanged(() => Author);
                Featured = dto.ebookdto.Featured; RaisePropertyChanged(() => Featured);
                Amount = dto.ebookdto.Amount; RaisePropertyChanged(() => Amount);
                FileName = dto.ebookdto.FileName; RaisePropertyChanged(() => FileName);
                PreviewFile = dto.ebookdto.PreviewFile; RaisePropertyChanged(() => PreviewFile);
                ImageName = dto.ebookdto.ImageName; RaisePropertyChanged(() => ImageName);
                ImagePath = dto.ebookdto.ImagePath; RaisePropertyChanged(() => ImagePath);
                PreviewPath = dto.ebookdto.PreviewPath; RaisePropertyChanged(() => PreviewPath);
                FilePath = dto.ebookdto.FilePath; RaisePropertyChanged(() => FilePath);
                Created = dto.ebookdto.Created; RaisePropertyChanged(() => Created);
                Modified = dto.ebookdto.Modified; RaisePropertyChanged(() => Modified);
                DTO = dto; RaisePropertyChanged(() => DTO);
            }
        }
        private int _id;
        private string _name;
        private string _category;
        private string _subject;
        private string _description;
        private string _author;
        private bool _featured;
        private decimal _amount;
        private string _fileName;
        private string _previewFile;
        private string _imageName;
        private string _imagePath;
        private string _previewPath;
        private string _filePath;
        private string _created;
        private string _modified;
        public msg_EbookDetail DTO
        {
            get
            {
                return _dto;
            }
            set
            {
                Set(() => DTO, ref _dto, value);
            }
        }
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(() => Id, ref _id, value);
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(() => Name, ref _name, value);
            }
        }
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                Set(() => Category, ref _category, value);
            }
        }
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                Set(() => Subject, ref _subject, value);
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                Set(() => Description, ref _description, value);
            }
        }
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                Set(() => Author, ref _author, value);
            }
        }
        public bool Featured
        {
            get
            {
                return _featured;
            }
            set
            {
                Set(() => Featured, ref _featured, value);
            }
        }
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                Set(() => Amount, ref _amount, value);
            }
        }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                Set(() => FileName, ref _fileName, value);
            }
        }
        public string PreviewFile
        {
            get
            {
                return _previewFile;
            }
            set
            {
                Set(() => PreviewFile, ref _previewFile, value);
            }
        }
        public string ImageName
        {
            get
            {
                return _imageName;
            }
            set
            {
                Set(() => ImageName, ref _imageName, value);
            }
        }
        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                Set(() => ImagePath, ref _imagePath, value);
            }
        }
        public string PreviewPath
        {
            get
            {
                return _previewPath;
            }
            set
            {
                Set(() => PreviewPath, ref _previewPath, value);
            }
        }
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                Set(() => FilePath, ref _filePath, value);
            }
        }
        public string Created
        {
            get
            {
                return _created;
            }
            set
            {
                Set(() => Created, ref _created, value);
            }
        }
        public string Modified
        {
            get
            {
                return _modified;
            }
            set
            {
                Set(() => Modified, ref _modified, value);
            }
        }
        private RelayCommand _navigateToEbookPreviewCommand;
        public RelayCommand NavigateToEbookPreviewCommand
        {
            get
            {
                return _navigateToEbookPreviewCommand
                    ?? (_navigateToEbookPreviewCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {

                            Messenger.Default.Send<msg_EbookPreview>(new msg_EbookPreview { ebookdto = DTO });


                        }
                        catch (Exception ex)
                        {
                            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                            await dialog.ShowError(ex, "Error when refreshing", "OK", null);
                        }

                    }));
            }
        }

    }
}
