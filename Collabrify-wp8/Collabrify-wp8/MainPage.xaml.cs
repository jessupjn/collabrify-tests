using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Collabrify_wp8.Resources;
using System.Threading;
using Collabrify_v2.CollabrifyProtocolBuffer;
using System.Runtime.Serialization;
using System.IO;
using ProtoBuf;
using Collabrify_wp8.Http_Requests;
using Collabrify_wp8.Collabrify;
using System.Diagnostics;

namespace Collabrify_wp8
{
    public partial class MainPage : PhoneApplicationPage
    {

        public ListPickerItem RequestType = new ListPickerItem();
        CollabrifyClient c;


        // Constructor
        public MainPage()
        {
          InitializeComponent();

          c = new CollabrifyClient("A", "A", "A", true);
          c.Changed += new ChangedEventHander(updateInfo);
        }

        private void updateInfo(object sender, EventArgs e)
        {
            ResponseTextBlock.Text = "Success";
        }

        private void Button1_Click(object sender, RoutedEventArgs e){
            if (RequestType.Name == "Warmup")
            {
                ResponseTextBlock.Text = "Warmup";
                c.makeWarmup(); 
            }
            else if (RequestType.Name == "CreateSession")
            {
                ResponseTextBlock.Text = "Create Session";
                c.makeCreateSession(); 
            }
            else if (RequestType.Name == "ListSessions")
            {
                ResponseTextBlock.Text = "List Sessions";
                c.makeListSession(); 
            }
            else if (RequestType.Name == "DeleteSessions")
            {
                ResponseTextBlock.Text = "Delete Session";
                c.makeDeleteSession(); 
            }
        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            // clear response text
            ResponseTextBlock.Text = "Foo";
        }

        private void OptionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
            RequestType = lpi;
        }

    }
}