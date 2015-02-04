using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Globalization;
using Microsoft.Phone.Tasks;
using DlugPubliczny.Resources;
using Telerik.Windows.Controls;

namespace DlugPubliczny
{
    public partial class PanoramaPage : PhoneApplicationPage
    {
        const double DPS = 1988.40; //Debt Per Second 2013
        const double Population = 38483957; //Dane z 2011
        const double cenaXboxaOne = 1600;
        const double cenaBiurowca = 454004776;
        const double cenaSamochodu = 93827;
        const double cenaHighEndPC = 3500;
        
        string liczoneOd = "2013.06.30";
        double dlug = 927500000000;
        //            XXXMMMTTTJJJ
        private const double DPS_75 = 152.953846153846D; //1000/75
        //upgrade stuff

        double dlugPerPerson = 0;
        double dlugAppStart = 0;
        double SecondsAgo = 0;
        double xboxone = 0;
        double biurowcow = 0;
        double samochodow = 0;
        double highendpcs = 0;
        private readonly CultureInfo plculture = new CultureInfo("pl-PL");

        public PanoramaPage()
        {        

            DateTime startDate = DateTime.Parse(liczoneOd);
            DateTime now = DateTime.Now;
            TimeSpan elapsed = now.Subtract(startDate);
            SecondsAgo = elapsed.TotalSeconds;

            dlug = dlug + (SecondsAgo * DPS);

            InitializeComponent();
            MyTimer_Tick(this, new EventArgs());
            // <Repeatable tasks>

            //dlugPerPerson = dlug / Population;

            //xboxone = dlug / cenaXboxaOne;
            //biurowcow = dlug / cenaBiurowca;
            //samochodow = dlug / cenaSamochodu;
            //highendpcs = dlug / cenaHighEndPC;

            //InitializeComponent();

            //NationalDebtValue.Text = dlug.ToString("C", plculture);
            //NationalDebtPerPersonValue.Text = dlugPerPerson.ToString("C", plculture);
            //NationalDebtPerSecondValue.Text = DPS.ToString("C", plculture);

            //XboxOneValue.Text = Math.Floor(xboxone).ToString();
            //SkyscrapersValue.Text = Math.Floor(biurowcow).ToString();
            //CarsValue.Text = Math.Floor(samochodow).ToString();
            //HighEndPCValue.Text = Math.Floor(highendpcs).ToString();
            
            // <Repeatable tasks/>
            DispatcherTimer myTimer = new DispatcherTimer();
            myTimer.Interval = TimeSpan.FromMilliseconds(75);
            myTimer.Tick += MyTimer_Tick;
            myTimer.Start();
            
            #region Chart logic
            //(this.stockChart.HorizontalAxis as DateTimeCategoricalAxis).DateTimeComponent = Telerik.Charting.DateTimeComponent.Year;
            //(this.stockChart.HorizontalAxis as DateTimeCategoricalAxis).LabelFormat = "{0:yyyy}";

            //DateTime lastDate = DateTime.Now;
            //double lastVal = 20;

            //List<ChartDataObject> dataSouce = new List<ChartDataObject>();
            //for (int i = 0; i < 5;
            //++i)
            //{
            //    ChartDataObject obj = new ChartDataObject
            //    {
            //        Date =
            //            lastDate.AddMonths(1),
            //        Value = lastVal++
            //    };
            //    dataSouce.Add(obj);
            //    lastDate = obj.Date;
            //}
            //LineSeries series = (LineSeries)this.stockChart.Series[0];
            //series.CategoryBinding = new PropertyNameDataPointBinding()
            //{
            //    PropertyName =
            //        "Date"
            //};
            //series.ValueBinding = new PropertyNameDataPointBinding()
            //{
            //    PropertyName =
            //        "Value"
            //};

            //series.ItemsSource = dataSouce;
            #endregion
            //string chartsNow = AppResources.chartsNow;
            //NowDataPoint.Category = "Siema";
        }

        void MyTimer_Tick(object sender, EventArgs e)
        {            
            dlug = dlug + DPS_75;
            dlugPerPerson = dlug / Population;
            dlugAppStart = dlugAppStart + DPS_75;

            xboxone = dlug / cenaXboxaOne;
            biurowcow = dlug / cenaBiurowca;
            samochodow = dlug / cenaSamochodu;
            highendpcs = dlug / cenaHighEndPC;

            NationalDebtValue.Text = dlug.ToString("C", plculture);
            NationalDebtPerPersonValue.Text = dlugPerPerson.ToString("C", plculture);
            NationalDebtSinceAppStartValue.Text = dlugAppStart.ToString("C", plculture);

            XboxOneValue.Text = FormatString(Math.Floor(xboxone));
            SkyscrapersValue.Text = FormatString(Math.Floor(biurowcow));
            CarsValue.Text = FormatString(Math.Floor(samochodow));
            HighEndPCValue.Text = FormatString(Math.Floor(highendpcs));
        }

        private void HyperlinkButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();

            marketplaceReviewTask.Show();
        }

        private static String FormatString(double d)
        {
            return String.Format(CultureInfo.InvariantCulture, "{0:#,#}", d);
        }
    }
}