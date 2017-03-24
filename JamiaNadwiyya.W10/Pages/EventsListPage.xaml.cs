//---------------------------------------------------------------------------
//
// <copyright file="EventsListPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>3/16/2017 2:35:33 PM</createdOn>
//
//---------------------------------------------------------------------------

using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using AppStudio.DataProviders.WordPress;
using JamiaNadwiyya.Sections;
using JamiaNadwiyya.ViewModels;
using AppStudio.Uwp;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace JamiaNadwiyya.Pages
{
    
    public sealed partial class EventsListPage : Page
    {
        public ListViewModel ViewModel { get; set; }
        public EventsListPage()
        {
			ViewModel = ViewModelFactory.NewList(new EventsSection());

            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
			NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           
			ShellPage.Current.ShellControl.SelectItem("ef08e5f8-3798-4c67-a103-e803a41eab5b");
			ShellPage.Current.ShellControl.SetCommandBar(commandBar);
			if (e.NavigationMode == NavigationMode.New)
            {			
				await this.ViewModel.LoadDataAsync();
                this.ScrollToTop();
			}			
            base.OnNavigatedTo(e);
        }


        IMobileServiceTable<messagesTable> messgesTableObj = App.MobileService.GetTable<messagesTable>();


        private void gotom_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(cloudPage));
        }

        public  void refresh_Click(object sender, RoutedEventArgs e)
        {
            messagesTable obj = new messagesTable();
            ListItems.ItemsSource = obj.MyMessageCol;
            ListItems.Items.Add (messgesTableObj.ReadAsync(obj.MyMessageCol));
               
            
          }
    }
}
