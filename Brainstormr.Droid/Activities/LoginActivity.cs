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
using Brainstormr.Portable.ViewModel.Login;
using Microsoft.Practices.ServiceLocation;
using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable;
using Brainstormr.Portable.Model.Login;
using GalaSoft.MvvmLight.Helpers;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

namespace Brainstormr.Droid.Activities
{
    [Activity(Label = "Login")]
    public class LoginActivity : BaseActivity
    {
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.loginlayout;
            }
        }

        public LoginViewModel Vm
        {
            get
            {
                return App.Locator.Login;
            }
        }

        private readonly List<Binding> _bindings = new List<Binding>();
        private TextView _msgtext;
        private EditText _txt_username;
        private EditText _txt_password;
        private Button _btn_login;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //App.currentactivity = this;
            SetContentView(Resource.Layout.loginlayout);
            
            SetSupportActionBar(Toolbar);
            
            ////SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            //SupportActionBar.SetDisplayShowTitleEnabled(true);
            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            _txt_username = FindViewById<EditText>(Resource.Id.txt_username);
            _txt_password = FindViewById<EditText>(Resource.Id.txt_password);
            _msgtext = FindViewById<TextView>(Resource.Id.msgText);
            _btn_login = FindViewById<Button>(Resource.Id.btn_login);

            _bindings.Add(this.SetBinding(() => Vm.UserName, _txt_username, () => _txt_username.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.Password, _txt_password, () => _txt_password.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.MsgText, _msgtext, () => _msgtext.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.AccessToken, BindingMode.TwoWay));

            _btn_login.Click += delegate {
                Vm.LoginCommand.Execute(null);

                //Snackbar.Make(view, "Your access token is: " + Vm.AccessToken, Snackbar.LengthLong)
                //    .Show();
            };
            //HasOptionsMenu = true;

            // Create your application here
            //try
            //{
            //    base.OnCreate(savedInstanceState);
            //    var vm_instantiate = Vm;
            //    var localdbservice = ServiceLocator.Current.GetInstance<ILocalDbService>();
            //    var loggedonuser = localdbservice.getUserTokenInfo();
            //    App.currentactivity = this;

            //    if (loggedonuser == null)
            //    {
            //        OnCreateEventForUserNotLoggedIn(savedInstanceState);
            //    }
            //    else
            //    {
            //        GlobalVal.LoginResult = loggedonuser;
            //        OnCreateEventForUserAlreadyLoggedIn(savedInstanceState, loggedonuser);
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private void OnCreateEventForUserAlreadyLoggedIn(Bundle savedInstanceState, TokenResponseModel loggedonuser)
        {
            //Load Home page if logged in
            SetContentView(Resource.Layout.main);
            
            //SupportActionBar.SetDisplayShowHomeEnabled(true);
            //SupportActionBar.SetHomeButtonEnabled(true);
        }

        private void OnCreateEventForUserNotLoggedIn(Bundle savedInstanceState)
        {
            SetContentView(Resource.Layout.loginlayout);
            _bindings.Add(this.SetBinding(() => Vm.UserName, () => _txt_username.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.Password, () => _txt_password.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.MsgText, () => _msgtext.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.AccessToken, BindingMode.TwoWay));
            //_btn_login.SetCommand("Click", Vm.LoginCommand);
            _btn_login.Click += _btn_login_Click;
            Vm.GetAccessTokenFromLocalDb.Execute(null);
        }

        private void _btn_login_Click(object sender, EventArgs e)
        {
            
        }
    }
}