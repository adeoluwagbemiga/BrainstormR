using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.AskAnExpert;
using Brainstormr.Portable.ViewModel.AskAnExpert.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.AskAnExpert
{
    public class ExpertDetailViewModel :  ViewModelBase
    {
        IAskAnExpertService _expertService;
        ILocalDbService _localDbService;
        IDialogService _dialogService;
        msg_ExpertDetail _msg_Detail;
        public ExpertDetailViewModel(IAskAnExpertService expertService, ILocalDbService localDbService, 
            IDialogService dialogService, msg_ExpertDetail msg_Detail)
        {
            _expertService = expertService;
            _localDbService = localDbService;
            _dialogService = dialogService;
            _msg_Detail = msg_Detail;
            InstructorSubjectsList = new ObservableCollection<InstructorSubjectItemModel>();
            InstructorSubjectReviewsList = new ObservableCollection<InstructorSubjectReviewItemModel>();
            if (msg_Detail.expert_dto != null)
            {
                Id = msg_Detail.expert_dto.Id; RaisePropertyChanged(() => Id);
                ImagePath = msg_Detail.expert_dto.ImagePath; RaisePropertyChanged(() => ImagePath);
                Name = msg_Detail.expert_dto.Name; RaisePropertyChanged(() => Name);
                Email = msg_Detail.expert_dto.Email; RaisePropertyChanged(() => Email);
                Phone = msg_Detail.expert_dto.Phone; RaisePropertyChanged(() => Phone);
                About = msg_Detail.expert_dto.About; RaisePropertyChanged(() => About);
                Portfolio = msg_Detail.expert_dto.Portfolio; RaisePropertyChanged(() => Portfolio);
                University = msg_Detail.expert_dto.University; RaisePropertyChanged(() => University);
                Degree = msg_Detail.expert_dto.Degree; RaisePropertyChanged(() => Degree);
                //IsCounsellor = msg_Detail.expert_dto.IsCounsellor; RaisePropertyChanged(() => IsCounsellor);
                IsExpert = msg_Detail.expert_dto.IsExpert; RaisePropertyChanged(() => IsExpert);
                IsTutor = msg_Detail.expert_dto.IsTutor; RaisePropertyChanged(() => IsTutor);
            }

        }
        public ObservableCollection<InstructorSubjectItemModel> InstructorSubjectsList
        {
            get;
            private set;
        }
        public ObservableCollection<InstructorSubjectReviewItemModel> InstructorSubjectReviewsList
        {
            get;
            private set;
        }
        private RelayCommand _LoadExpertDetailCommand;

        private RelayCommand _SendMessageCommand;

        private int _id;
        private string _imagePath;
        private string _name;
        private string _email;
        private string _phone;
        private string _about;
        private string _pictureName;
        private string _portfolio;
        private string _university;
        private string _degree;
        private bool _isCounsellor;
        private bool _isTutor;
        private bool _isExpert;
        private string _created;
        private string _modified;
        private string _message;
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
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Set(() => Email, ref _email, value);
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                Set(() => Phone, ref _phone, value);
            }
        }
        public string About
        {
            get
            {
                return _about;
            }
            set
            {
                Set(() => About, ref _about, value);
            }
        }
        public string PictureName
        {
            get
            {
                return _pictureName;
            }
            set
            {
                Set(() => PictureName, ref _pictureName, value);
            }
        }
        public string Portfolio
        {
            get
            {
                return _portfolio;
            }
            set
            {
                Set(() => Portfolio, ref _portfolio, value);
            }
        }
        public string University
        {
            get
            {
                return _university;
            }
            set
            {
                Set(() => University, ref _university, value);
            }
        }
        public string Degree
        {
            get
            {
                return _degree;
            }
            set
            {
                Set(() => Degree, ref _degree, value);
            }
        }
        public bool IsCounsellor
        {
            get
            {
                return _isCounsellor;
            }
            set
            {
                Set(() => IsCounsellor, ref _isCounsellor, value);
            }
        }
        public bool IsTutor
        {
            get
            {
                return _isTutor;
            }
            set
            {
                Set(() => IsTutor, ref _isTutor, value);
            }
        }
        public bool IsExpert
        {
            get
            {
                return _isExpert;
            }
            set
            {
                Set(() => IsExpert, ref _isExpert, value);
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
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                Set(() => Message, ref _message, value);
            }
        }
        public RelayCommand LoadExpertDetailCommand
        {
            get
            {
                return _LoadExpertDetailCommand
                    ?? (_LoadExpertDetailCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            InstructorSubjectsList.Clear();
                            InstructorSubjectReviewsList.Clear();
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                var instructorsubjectlist = await _expertService.getInstructorSubjects(usertokenrec, Id);
                                foreach (var item in instructorsubjectlist)
                                {
                                    InstructorSubjectsList.Add(item);
                                }
                                RaisePropertyChanged(() => InstructorSubjectsList);
                                var instructorsubjectreviewlist = await _expertService.getInstructorSubjectReviews(usertokenrec, Id);
                                foreach (var item in instructorsubjectreviewlist)
                                {
                                    InstructorSubjectReviewsList.Add(item);
                                }
                                RaisePropertyChanged(() => InstructorSubjectReviewsList);
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
        public RelayCommand SendMessageCommand
        {
            get
            {
                return _SendMessageCommand
                    ?? (_SendMessageCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            //var msg = new msg_CounsellorDetail(counsellor);
                            //Messenger.Default.Send<msg_Transport<msg_CounsellorDetail>>(new msg_Transport<msg_CounsellorDetail>(msg));
                            var usertokenrec = _localDbService.getAccessToken();
                            if (usertokenrec != null)
                            {
                                string category = "";
                                if (IsExpert == true) { category = "Expert"; } else { category = "Tutor"; }
                                string subject = "";
                                var subjectrevlist = await _expertService.getInstructorSubjectReviews(usertokenrec, Id);
                                subject = subjectrevlist.FirstOrDefault(a => a.InstructorId == Id).Subject;
                                var sendmessage = await _expertService.sendMessage(usertokenrec, Id, subject, Message, category);
                            }
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
