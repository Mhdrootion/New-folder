using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JamiaNadwiyya.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class cloudPage : Page

    {
        private MobileServiceCollection<cloudData, cloudData> items;
        private IMobileServiceTable<cloudData> cloudatatable = App.MobileService.GetTable<cloudData>();
        public cloudPage()
        {
            this.InitializeComponent();
        }
       
        private async Task SendMessage(cloudData Clouddata)
        {
            // This code inserts a new TodoItem into the database. After the operation completes
            // and the mobile app backend has assigned an id, the item is added to the CollectionView.
            await cloudatatable.InsertAsync(Clouddata);
            items.Add(Clouddata);
        }

            private async Task Refreshclouddata()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems.
                items = await cloudatatable
                    .Where(Clouddata => Clouddata.Complete == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading messages").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.sendButton.IsEnabled = true;


            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {

            var Clouddata = new cloudData { Text = messagetextbx.Text };
            messagetextbx.Text = "";
            await SendMessage(Clouddata);
        }
    }
}
