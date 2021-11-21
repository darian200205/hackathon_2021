using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusApp
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-2bbb3-default-rtdb.europe-west1.firebasedatabase.app/");

        public async Task<List<Bus>> GetAllBuses(string Type)
        {
            return (await firebase
              .Child("Buses").Child(Type)
              .OnceAsync<Bus>()).Select(item => new Bus
              {
                  Lat = item.Object.Lat,
                  Long = item.Object.Long,
                  BusId = item.Object.BusId
              }).ToList();
        }

        public async Task AddBus(string BusId, string Type, double Latitude, double Longitude)
        {

            await firebase
              .Child("Buses").Child(Type)
              .PostAsync(new Bus() { BusId = BusId, Lat = Latitude, Long = Longitude });
        }



        public async Task<Bus> GetBusesbyID(string BusID, string Type)
        {
            var allBuses = await GetAllBuses(Type);
            await firebase
              .Child("Buses").Child(Type)
              .OnceAsync<Bus>();
            return allBuses.Where(a => a.BusId == BusID).FirstOrDefault();
        }


        public async Task UpdateBus(string BusId, string Type, double NewLongitude, double NewLatitude)
        {
            var toUpdatePerson = (await firebase
              .Child("Buses").Child(Type)
              .OnceAsync<Bus>()).Where(a => a.Object.BusId == BusId).FirstOrDefault();

            await firebase
              .Child("Buses").Child(Type)
              .Child(toUpdatePerson.Key)
              .PutAsync(new Bus() { BusId = BusId, Lat = NewLatitude, Long = NewLongitude });
        }


        //public async Task DeleteBus(string Type, string BusId)
        //{
        //    var toDeletePerson = (await firebase
        //      .Child("Buses").Child(Type)
        //      .OnceAsync<Bus>()).Where(a => a.Object.BusId == BusId).FirstOrDefault();
        //    await firebase.Child("Buses").Child(toDeletePerson.Key).DeleteAsync();

        //}

    }

}