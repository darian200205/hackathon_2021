using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;



namespace BusApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainUserPage : ContentPage
    {
        bool isOnBus;
        bool isGettingOthersLocation;
        string selected = "24B";


        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainUserPage()
        {
            InitializeComponent();
            initializeLocation();

           
            Buses.Items.Add("1");
            Buses.Items.Add("2");
            Buses.Items.Add("3");
            Buses.Items.Add("4");
            Buses.Items.Add("5");
            Buses.Items.Add("6");
            Buses.Items.Add("7");
            Buses.Items.Add("8L");
            Buses.Items.Add("9");
            Buses.Items.Add("10");
            Buses.Items.Add("18");
            Buses.Items.Add("14");
            Buses.Items.Add("10");
            Buses.Items.Add("18");
            Buses.Items.Add("19");
            Buses.Items.Add("20");
            Buses.Items.Add("21");
            Buses.Items.Add("22");
            Buses.Items.Add("23");
            Buses.Items.Add("24");
            Buses.Items.Add("24B");
            Buses.Items.Add("25");
            Buses.Items.Add("25N");
            Buses.Items.Add("26");
            Buses.Items.Add("26L");
            Buses.Items.Add("27");
            Buses.Items.Add("28");
            Buses.Items.Add("28B");
            Buses.Items.Add("29");
            Buses.Items.Add("30");
            Buses.Items.Add("31");
            Buses.Items.Add("32");
            Buses.Items.Add("32B");
            Buses.Items.Add("33");
            Buses.Items.Add("34");
            Buses.Items.Add("35");
            Buses.Items.Add("36B");
            Buses.Items.Add("36L");
            Buses.Items.Add("37");
            Buses.Items.Add("38");
            Buses.Items.Add("39");
            Buses.Items.Add("39L");
            Buses.Items.Add("40");
            Buses.Items.Add("40S");
            Buses.Items.Add("41");
            Buses.Items.Add("42");
            Buses.Items.Add("43");
            Buses.Items.Add("43B");
            Buses.Items.Add("43P");
            Buses.Items.Add("44");
            Buses.Items.Add("45");
            Buses.Items.Add("46");
            Buses.Items.Add("46B");
            Buses.Items.Add("46L");
            Buses.Items.Add("47");
            Buses.Items.Add("48");
            Buses.Items.Add("48L");
            Buses.Items.Add("50");
            Buses.Items.Add("50L");
            Buses.Items.Add("52");
            Buses.Items.Add("52L");
            Buses.Items.Add("53");
            Buses.Items.Add("100");
            Buses.Items.Add("100L");
            Buses.Items.Add("101");
            Buses.Items.Add("102");
            Buses.Items.Add("102L");
        }


        async private void initializeLocation()
        {
            var current_location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
                );
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(current_location.Latitude, current_location.Longitude), Distance.FromKilometers(2)));
        }

        private void Buses_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = Buses.Items[Buses.SelectedIndex];
            //DisplayAlert("autobuzul", Buses.Items[Buses.SelectedIndex], "hide");
        }

        private async void initializeBusLocation(String BusID, String Type, double longitude, double lat)
        {
            await firebaseHelper.AddBus(BusID, Type, longitude, lat);
        }

        private async void updateBusLocation(String BusID, String Type, double longitude, double lat)
        {
            await firebaseHelper.UpdateBus(BusID, Type, longitude, lat);
        }



        public string generateID()
        {
            return Guid.NewGuid().ToString("N");
        }

        private void stopGettingCoordinates(object sender, EventArgs e)
        {
            isOnBus = false;
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            isOnBus = true;
            String Type = selected;

            var current_location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
                );

            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(current_location.Latitude, current_location.Longitude), Distance.FromMiles(1)).WithZoom(20));

            var driverID = generateID();
            initializeBusLocation(driverID, Type, current_location.Longitude, current_location.Latitude);


            while (isOnBus)
            {

                //await DisplayAlert("title", "s", "c");
                current_location = await Geolocation.GetLocationAsync(
                   new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
               );

                //await DisplayAlert("title", "s", current_location.Longitude.ToString());

                updateBusLocation(driverID, Type, current_location.Longitude, current_location.Latitude);


                await Task.Delay(1000);

            }
        }



        async private void startGettingOthersLocation(object sender, EventArgs e)
        {
            isGettingOthersLocation = true;
            String Type = selected;
            button12.Text = $"Tracking: Bus {Type}";

            var current_location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
                );

            //

            List<Bus> a = await firebaseHelper.GetAllBuses(Type);
            List<Pin> pins = new List<Pin>();

            for (int i = 0; i < a.Count(); i++)
            {
                Pin pin = new Pin()
                {
                    Label = Type
                };
                pins.Add(pin);
                if (a[i] != null)
                {
                    pins[i].Position = new Xamarin.Forms.Maps.Position(
                            a[i].Lat, a[i].Long
                    );
                }
                myMap.Pins.Add(pin);
            }

            while (isGettingOthersLocation)
            {
                a = await firebaseHelper.GetAllBuses(Type);

                for (int i = 0; i < a.Count(); i++)
                {
                    if (a[i] != null)
                    {
                        pins[i].Position = new Xamarin.Forms.Maps.Position(
                                a[i].Lat, a[i].Long
                        );
                    }
                }

                await Task.Delay(1000);

            }
        }

        private void stopGettingOthersLocation(object sender, EventArgs e)
        {
            button12.Text = "Start track";
            isGettingOthersLocation = false;
            myMap.Pins.Clear();
        }













        
    }
}