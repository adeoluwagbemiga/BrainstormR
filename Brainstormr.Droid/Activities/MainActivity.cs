using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using Brainstormr.Droid.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Brainstormr.Droid.Fragments.Login;
using Brainstormr.Droid.Fragments.Evaluation;
using Brainstormr.Droid.Fragments.Learning;
using Brainstormr.Droid.Fragments.MyAccount;
using Brainstormr.Droid.Fragments.AskAnExpert;
using Brainstormr.Droid.Fragments.CareerCounselling;

namespace Brainstormr.Droid.Activities
{
    [Activity(Label = "Brainstormr", MainLauncher = false, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : BaseActivity
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.main;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            App.currentactivity = this;

            drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayShowTitleEnabled(true);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home_1:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_home_2:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_home_3:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_home_4:
                        ListItemClicked(3);
                        break;
                    case Resource.Id.nav_home_5:
                        ListItemClicked(4);
                        break;
                    case Resource.Id.nav_home_6:
                        ListItemClicked(5);
                        break;
                    case Resource.Id.nav_home_7:
                        ListItemClicked(6);
                        break;
                }

                Snackbar.Make(drawerLayout, "You selected: " + e.MenuItem.TitleFormatted, Snackbar.LengthLong)
                    .Show();

                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked(0);
            }
        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = Fragment1.NewInstance();
                    break;
                case 1:
                    //fragment = Fragment2.NewInstance();
                    fragment = new LearningHomeFragment();
                    break;
                case 2:
                    //fragment = Fragment2.NewInstance();
                    fragment = new EvaluationHomeFragment();
                    break;
                case 3:
                    //fragment = Fragment2.NewInstance();
                    fragment = new ExpertsHomeFragment();
                    break;
                case 4:
                    //fragment = Fragment2.NewInstance();
                    fragment = new CounsellingHomeFragment();
                    break;
                case 5:
                    //fragment = Fragment2.NewInstance();
                    fragment = new MyAccountHomeFragment();
                    break;
                case 6:
                    fragment = new LoginFragment();
                    //StartActivity(typeof(LoginActivity));
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment).AddToBackStack(null)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                //case Android.Resource.Id.nav_home_3:
                //    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                //    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }
    }
}

