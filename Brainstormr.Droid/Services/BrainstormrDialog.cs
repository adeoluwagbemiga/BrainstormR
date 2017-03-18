using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Views;

namespace Brainstormr.Droid.Services
{
    public class BrainstormrDialog : IDialogService
    {
        public async Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() => {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(error.Message);
                        alert.SetPositiveButton("Ok", (senderAlert, args) => {
                            //  Toast.MakeText(Application.Context, "Deleted!", ToastLength.Short).Show();
                            afterHideCallback();
                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    });
                }

            });

            // alert.
            //alert.SetNegativeButton("Cancel", (senderAlert, args) => {
            //    Toast.MakeText(this, "Cancelled!", ToastLength.Short).Show();
            //});


        }

        public async Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() => {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(message);
                        alert.SetPositiveButton("Ok", (senderAlert, args) => {

                            afterHideCallback();
                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    });
                }

            });
        }

        public async Task ShowMessage(string message, string title)
        {

            await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() => {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(message);

                        Dialog dialog = alert.Create();
                        dialog.Show();
                    });
                }

            });
        }


        public async Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() => {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(message);
                        alert.SetPositiveButton("Ok", (senderAlert, args) => {

                            afterHideCallback();
                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();
                    });
                }

            });
        }

        public async Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            return await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() =>
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(message);

                        alert.SetPositiveButton(buttonConfirmText, (senderAlert, args) => {

                            afterHideCallback(true);

                        });
                        alert.SetNegativeButton(buttonCancelText, (senderAlert, args) => {

                            afterHideCallback(false);

                        });
                        Dialog dialog = alert.Create();
                        dialog.Show();

                    });
                    return true;
                }

            });
        }

        public async Task ShowMessageBox(string message, string title)
        {

            await Task.Run(() =>
            {
                using (var h = new Handler(Looper.MainLooper))
                {
                    h.Post(() => {
                        AlertDialog.Builder alert = new AlertDialog.Builder(App.currentactivity);
                        alert.SetTitle(title);
                        alert.SetMessage(message);

                        Dialog dialog = alert.Create();
                        dialog.Show();
                    });
                }

            });
        }

    }
}