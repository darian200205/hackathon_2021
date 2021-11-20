using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusApp
{
    public partial class MainPage : ContentPage
    {
        private readonly TrackCoordinates[] route1Coordinates;
        // bus' coordinates array

        private Pin pinRoute1 = new Pin()
        {
            Label = "Bus x" //random bus pin
        };

        public MainPage()
        {
            InitializeComponent();

            route1Coordinates = getCoordinates(); // gets coordinates
            myMap.Pins.Add(pinRoute1);

            var index = 0; // coordinates index
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
            pinRoute1.Position = new Xamarin.Forms.Maps.Position(
                    route1Coordinates[index].Position.LatitudeDegrees, route1Coordinates[index].Position.LongitudeDegrees);
                    );
            });

        }
    }
}
