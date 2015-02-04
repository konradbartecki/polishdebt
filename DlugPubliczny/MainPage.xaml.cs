using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DlugPubliczny.Resources;
using System.Windows.Threading;
using System.Globalization;

namespace DlugPubliczny
{
    public partial class MainPage : PhoneApplicationPage
    {
        const double DPS = 1988.40; //Debt Per Second 2013
        const double Population = 38538447; //Dane z 2011
        //double DPS_1_10 = DPS / 10;
        double DPS_75 = DPS / (1000 / 75);
        string liczoneOd = "2013.06.30";

        double dlug = 927500000000;
        //            XXXMMMTTTJJJ
        double dlugPerPerson = 0;
        double dlugAppStart = 0;
        double SecondsAgo = 0;

        


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            var plculture = new CultureInfo("pl-PL");

            DateTime startDate = DateTime.Parse(liczoneOd);
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now.Subtract(startDate);
            SecondsAgo = elapsed.TotalSeconds;

            dlug = dlug + (SecondsAgo * DPS);
            dlugPerPerson = dlug / Population;

            NationalDebtValue.Text = dlug.ToString("C", plculture);
            NationalDebtPerPersonValue.Text = dlugPerPerson.ToString("C", plculture);

            DispatcherTimer myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromMilliseconds(75);
            myTimer.Tick += myTimer_Tick;
            myTimer.Start();
        }

        public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void myTimer_Tick(object sender, EventArgs e)
        {
            var plculture = new CultureInfo("pl-PL");
            dlug = dlug + DPS_75;
            dlugPerPerson = dlug / Population;
            dlugAppStart = dlugAppStart + DPS_75;

            NationalDebtValue.Text = dlug.ToString("C", plculture);
            NationalDebtPerPersonValue.Text = dlugPerPerson.ToString("C", plculture);
            NationalDebtSinceAppStartValue.Text = dlugAppStart.ToString("C", plculture);
        }
    }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }