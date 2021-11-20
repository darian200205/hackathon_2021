//using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Firebase.Database;
using Firebase.Database.Query;

namespace BusApp
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();


        bool isOnBus = false;
        //private readonly TrackCoordinates[] route1Coordinates;
        // bus' coordinates array

        //FirebaseClient firebaseClient = new FirebaseClient("https://xamarinfirebase-2bbb3-default-rtdb.europe-west1.firebasedatabase.app/");


        private Pin pinRoute1 = new Pin()
        {
            Label = "Bus x" //random bus pin
        };

        public MainPage()
        {
            InitializeComponent();

            //route1Coordinates = getCoordinates(); // gets coordinates
            //myMap.Pins.Add(pinRoute1);

            //var index = 0; // coordinates index
            //Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            //{
            //    pinRoute1.Position = new Xamarin.Forms.Maps.Position(
            //            route1Coordinates[index].Position.LatitudeDegrees, route1Coordinates[index].Position.LongitudeDegrees
            //            );

            //    if (index == 0)
            //    {
            //        myMap.MoveToRegion(MapSpan.FromCenterAndRadius(route1Coordinates.Position, Xamarin.Forms.Maps.Distance.FromKilometers(1)));
            //    }

            //    return route1Coordinates.Length >= index;

            //});

        }

        private async void updateBusLocation(String BusID, String Type, double longitude, double lat)
        {
            await firebaseHelper.UpdateBus(BusID, Type, longitude, lat);
        }

        private async void initializeBusLocation(String BusID, String Type, double longitude, double lat)
        {
            await firebaseHelper.AddBus(BusID, Type, longitude, lat);
        }


        async private void getUserCoordinates(object sender, EventArgs e)
        {
            isOnBus = true;
            String Type = "24B";

            var current_location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
                );

            initializeBusLocation("1e7", Type, current_location.Longitude, current_location.Latitude);

            while (isOnBus)
            {
                 current_location = await Geolocation.GetLocationAsync(
                    new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(30))
                );

                updateBusLocation("1e7", Type, current_location.Longitude, current_location.Latitude);

                await Task.Delay(1000);

            }
        }

        private void stopGettingCoordinates(object sender, EventArgs e)
        {
            isOnBus = false;
        }

    }
}
