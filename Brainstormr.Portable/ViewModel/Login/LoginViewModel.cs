using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable.Model.Login;
using Brainstormr.Portable.ViewModel.Login.msg;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel.Login
{
    public class LoginViewModel : ViewModelBase
    {
        private string _UserName;
        private string _Password;
        private string _MsgText;
        private string _AccessToken;
        private RelayCommand _LoginCommand;
        private RelayCommand<input_externalLoginCmd> _ExternalLoginCommand;
        private RelayCommand _LoadHomePageIfLoggedInCommand;
        private RelayCommand _GetAccessTokenFromLocalDbCommand;

        ILocalDbService _LocalDbService;
        ILoginService _LoginService;
        IDialogService _dialogService;
        public LoginViewModel(ILocalDbService LocalDbService, ILoginService LoginService, IDialogService dialogService)
        {
            _LocalDbService = LocalDbService;
            _LoginService = LoginService;
            _dialogService = dialogService;
        }
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                Set(() => UserName, ref _UserName, value);
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                Set(() => Password, ref _Password, value);
            }
        }

        public string AccessToken
        {
            get
            {
                return _AccessToken;
            }
            set
            {
                Set(() => AccessToken, ref _AccessToken, value);
            }
        }

        public string MsgText
        {
            get
            {
                return _MsgText;
            }
            set
            {
                Set(() => MsgText, ref _MsgText, value);
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _LoginCommand
                    ?? (_LoginCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            //if (CrossConnectivity.Current.IsConnected == false) throw new InvalidOperationException("No Network Available");
                            var loginvalidationresult = await _LoginService.ValidateUser(UserName, Password);
                            if (loginvalidationresult.AccessToken == "")
                            {
                                MsgText = "Login not successful!";
                                return;
                            }
                            _LocalDbService.saveToken(loginvalidationresult.AccessToken, loginvalidationresult.Username);
                            GlobalVal.LoginResult = loginvalidationresult;
                            var getuserinforesult = await _LoginService.getUserProfileinfo(loginvalidationresult.AccessToken);
                            var user = new UserInfo()
                            {
                                AvatarUrl = getuserinforesult.AvatarUrl,
                                City = getuserinforesult.City,
                                Country = getuserinforesult.Country,
                                Course = getuserinforesult.Course,
                                HasRegistered = getuserinforesult.HasRegistered,
                                Institution = getuserinforesult.Institution,
                                LoginProvider = getuserinforesult.LoginProvider,
                                Name = getuserinforesult.Name,
                                Profession = getuserinforesult.Profession,
                                UserName = getuserinforesult.UserName
                            };


                            _LocalDbService.updateUserInfo(user);
                            var msg = new msg_userloggedinAlready { loginresult = loginvalidationresult };
                            Messenger.Default.Send(new msg_Transport<msg_userloggedinAlready>(msg));
                            //MsgText = "Login Sucessful!";
                            var loggeduser = _LocalDbService.getUserInfo();
                            MsgText = "Welcome " + loggeduser.Name + " You are logged in as " + loggeduser.UserName;
                            RaisePropertyChanged(() => MsgText);
                        }
                        catch (System.Exception ex)
                        {

                            //var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                            await _dialogService.ShowMessage(ex.Message, "Message");
                            //MsgText = ex.Message;
                            //RaisePropertyChanged(() => MsgText);
                        }
                        //------------------------------------------------------------

                        //-----------------------------------------------------------------------
                    }));
            }
        }

        public RelayCommand<input_externalLoginCmd> ExternalLoginCommand
        {
            get
            {
                return _ExternalLoginCommand
                    ?? (_ExternalLoginCommand = new RelayCommand<input_externalLoginCmd>(
                        async externallogin =>
                        {
                            try
                            {
                                var loginvalidationresult = await _LoginService.ValidateExternalLogin(externallogin.userName, externallogin.provider, externallogin.name, externallogin.picture, externallogin.externalAccessToken);
                                if (loginvalidationresult.AccessToken == "")
                                {
                                    MsgText = "Login not successful!";
                                    return;
                                }
                                var inputdetails = new ExternalLoginInfo()
                                {
                                    ExternalAccessToken = externallogin.externalAccessToken,
                                    Name = externallogin.name,
                                    Picture = externallogin.picture,
                                    Provider = externallogin.provider,
                                    UserName = externallogin.userName
                                };
                                _LocalDbService.saveExternalLoginDetails(inputdetails);
                                GlobalVal.LoginResult = loginvalidationresult;
                                var getuserinforesult = await _LoginService.getUserProfileinfo(loginvalidationresult.AccessToken);
                                var user = new UserInfo()
                                {
                                    AvatarUrl = getuserinforesult.AvatarUrl,
                                    City = getuserinforesult.City,
                                    Country = getuserinforesult.Country,
                                    Course = getuserinforesult.Course,
                                    HasRegistered = getuserinforesult.HasRegistered,
                                    Institution = getuserinforesult.Institution,
                                    LoginProvider = getuserinforesult.LoginProvider,
                                    Name = getuserinforesult.Name,
                                    Profession = getuserinforesult.Profession,
                                    UserName = getuserinforesult.UserName
                                };
                                _LocalDbService.updateUserInfo(user);
                                var msg = new msg_userloggedinAlready { loginresult = loginvalidationresult };
                                Messenger.Default.Send(new msg_Transport<msg_userloggedinAlready>(msg));
                                //MsgText = "Login Sucessful!";
                                var loggeduser = _LocalDbService.getUserInfo();
                                MsgText = "Welcome " + loggeduser.Name + " You are logged in as " + loggeduser.UserName;
                                RaisePropertyChanged(() => MsgText);

                            }
                            catch (Exception ex)
                            {
                                //MsgText = ex.Message;
                                //RaisePropertyChanged(() => MsgText);
                                var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                                await dialog.ShowError(ex, "Error when refreshing", "OK", null);
                            }
                        }
                        ));
            }
        }

        public RelayCommand GetAccessTokenFromLocalDb
        {
            get
            {
                return _GetAccessTokenFromLocalDbCommand
                    ?? (_GetAccessTokenFromLocalDbCommand = new RelayCommand(
                     () =>
                     {
                         //------------------------------------------------------------
                         AccessToken = _LocalDbService.getAccessToken();
                         RaisePropertyChanged(() => AccessToken);
                         //-----------------------------------------------------------------------
                     }));
            }
        }

        public RelayCommand LoadHomePageIfLoggedCommand
        {
            get
            {
                return _LoadHomePageIfLoggedInCommand
                    ?? (_LoadHomePageIfLoggedInCommand = new RelayCommand(
                    async () =>
                    {
                        try
                        {
                            var returnresult = _LocalDbService.getUserTokenInfo();
                            if (returnresult != null)
                            {
                                Messenger.Default.Send<msg_userloggedinAlready>(new msg_userloggedinAlready { loginresult = returnresult });
                            }
                        }
                        catch (System.Exception ex)
                        {

                            var dialog = ServiceLocator.Current.GetInstance<IDialogService>();
                            await dialog.ShowError(ex, "Error when refreshing", "OK", null);
                        }
                    }));
            }
        }
    }
}
