//---------------------------------------------------------------------------
//
// <copyright file="EventsDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>3/16/2017 2:35:33 PM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.WordPress;
using JamiaNadwiyya.Sections;
using JamiaNadwiyya.Navigation;
using JamiaNadwiyya.ViewModels;
using AppStudio.Uwp;

namespace JamiaNadwiyya.Pages
{
    public sealed partial class EventsDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public EventsDetailPage()
        {
            ViewModel = ViewModelFactory.NewDetail(new EventsSection());
            this.InitializeComponent();
			commandBar.DataContext = ViewModel;
        }

        public DetailViewModel ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadStateAsync(e.Parameter as NavDetailParameter);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }
    }
}
