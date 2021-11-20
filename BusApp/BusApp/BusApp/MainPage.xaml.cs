using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database;
using Firebase.Database.Query;

namespace BusApp
{
    public partial class MainPage : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();

  

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allBuses = await firebaseHelper.GetAllBuses("24B");
            lstPersons.ItemsSource = allBuses;
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            //var person = await firebaseHelper.GetPerson(Convert.ToInt32(txtId.Text));
            //if (person != null)
            //{
            //    txtId.Text = person.PersonId.ToString();
            //    txtName.Text = person.Name;
            //    await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            //}
            //else
            //{
            //    await DisplayAlert("Success", "No Person Available", "OK");
            //}

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdateBus(txtId.Text, "24B", -Double.Parse(txtName.Text), Double.Parse(txtName.Text));
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Updated Successfully", "OK");
            var allPersons = await firebaseHelper.GetAllBuses("24B");
            lstPersons.ItemsSource = allPersons;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            //await firebaseHelper.DeletePerson(Convert.ToInt32(txtId.Text));
            //await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            //var allPersons = await firebaseHelper.GetAllPersons();
            //lstPersons.ItemsSource = allPersons;
        }

        int k = 0;
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            k++;
            await firebaseHelper.AddBus(k.ToString(), "24B", 12324.5345, -124334.545);

            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            await DisplayAlert("Success", "Person Added Successfully", "OK");
            var allPersons = await firebaseHelper.GetAllBuses("24B");
            lstPersons.ItemsSource = allPersons;
        }
    
    }
}   

     