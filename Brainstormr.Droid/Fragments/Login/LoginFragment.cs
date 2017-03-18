using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.ViewModel.Login;
using Microsoft.Practices.ServiceLocation;
using Brainstormr.Portable.LocalDb;
using Brainstormr.Portable;
using GalaSoft.MvvmLight.Helpers;
using Brainstormr.Portable.Model.Login;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Brainstormr.Droid.Activities;

namespace Brainstormr.Droid.Fragments.Login
{
    public class LoginFragment : Fragment
    {
        private readonly List<Binding> _bindings = new List<Binding>();
        private TextView _msgtext;
        private EditText _txt_username;
        private EditText _txt_password;
        private Button _btn_login;
        private ProgressBar _login_progressbar;
        public LoginViewModel Vm
        {
            get
            {
                return App.Locator.Login;
            }
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var intent = new Intent(Activity, typeof(LoginActivity));
            //StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.loginlayout, container, false);

            _txt_username = view.FindViewById<EditText>(Resource.Id.txt_username);
            _txt_password = view.FindViewById<EditText>(Resource.Id.txt_password);
            _msgtext = view.FindViewById<TextView>(Resource.Id.msgText);
            _btn_login = view.FindViewById<Button>(Resource.Id.btn_login);
            // _login_progressbar = view.FindViewById<ProgressBar>(Resource.Id.loginprogressBar);

            _bindings.Add(this.SetBinding(() => Vm.UserName, _txt_username, () => _txt_username.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.Password, _txt_password, () => _txt_password.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.MsgText, _msgtext, () => _msgtext.Text, BindingMode.TwoWay));
            _bindings.Add(this.SetBinding(() => Vm.AccessToken, BindingMode.TwoWay));

            _btn_login.Click += delegate
            {
                Vm.LoginCommand.Execute(null);

                Snackbar.Make(view, "Your access token is: " + Vm.AccessToken, Snackbar.LengthLong)
                    .Show();
            };
            HasOptionsMenu = true;
            //Vm.GetAccessTokenFromLocalDb.Execute(null);
            return view;
        }
    }
}