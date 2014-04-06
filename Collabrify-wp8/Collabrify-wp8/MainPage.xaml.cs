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
          this.InitializeComponent();

          CollabrifyParticipant p = new CollabrifyParticipant(1, "JACK AND JILL", "jessupjn@umich.edu", 123);
          c = new CollabrifyClient(p, "wp8-collabrify@umich.edu", "82763BDBCA", true);

          //c.ReturnInformation += new ChangedEventHander(updateInfo);
        }

        private void updateInfo(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, RoutedEventArgs e){
          if (RequestType.Name == "Warmup")
          {
            ResponseTextBlock.Text = "Warmup";

          }
          else if (RequestType.Name == "CreateSession")
          {
            ResponseTextBlock.Text = "Create Session";
            List<string> tags = new List<string>();
            tags.Add("[none]");
            Random rd = new Random();
            c.createSession(rd.Next(1, 9999999).ToString(), tags, "", true, delegate { });
          }
          else if (RequestType.Name == "ListSessions")
          {
            ResponseTextBlock.Text = "List Sessions";
            c.makeListSession();
          }
          else if (RequestType.Name == "LeaveSession")
          {
            ResponseTextBlock.Text = "Delete Session";
            c.leaveSession(true, delegate{});
          }
          else Debug.WriteLine(RequestType.Name);
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