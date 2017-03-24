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
using SQLite.Net.Attributes;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace JamiaNadwiyya.Pages
{
   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class cloudPage : Page

    {
        
        

        public cloudPage()
        {
            this.InitializeComponent();
        }
        IMobileServiceTable<messagesTable> messgesTableObj = App.MobileService.GetTable<messagesTable>();

        
 public async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                messagesTable obj = new messagesTable();
             obj.MyMessageCol = messagetextbx.Text;
             await messgesTableObj.InsertAsync(obj);
               jjj.Text = obj.MyMessageCol;
                

            }
           
           catch (Exception ex)
            {
                dialog.Text = "error" + ex;
                
            }
            
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            jjj.Text = "";

        }
    }
        }
    

