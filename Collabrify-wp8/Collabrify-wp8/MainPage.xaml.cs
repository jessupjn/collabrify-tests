using Collabrify_wp8.Collabrify;
using Microsoft.Phone.Controls;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

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

          //c = new CollabrifyClient("jessupjn@umich.edu", "JACK AND JILL", "wp8-collabrify@umich.edu", "82763BDBCA", true);
          c = new CollabrifyClient("haha@umich.edu", "jacky", "wp8-collabrify@umich.edu", "82763BDBCA", true);

          //c.ReturnInformation += new ChangedEventHander(updateInfo);
        }

        private void updateInfo(object sender, EventArgs e)
        {

        } 

        private void Button1_Click(object sender, RoutedEventArgs e){
          if (RequestType.Name == "JoinSession")
          {
            //ResponseTextBlock.Text = "Warmup";
            c.joinSession(Convert.ToInt64(textfield.Text),"", delegate { });
          }
          else if (RequestType.Name == "CreateSession")
          {
            //ResponseTextBlock.Text = "Create Session";
            List<string> tags = new List<string>();
            tags.Add("[none]");
            Random rd = new Random();
            c.createSession(rd.Next(1, 9999999).ToString(), tags, "", false, delegate {
              Deployment.Current.Dispatcher.BeginInvoke(delegate
              {
                textfield.Text = c.getSession().getId().ToString();
              });
            });
          }
          else if (RequestType.Name == "ListSessions")
          {
            //ResponseTextBlock.Text = "List Sessions";
            List<string> l = new List<string>();
            l.Add("TagTesting");
            c.listSessions(l);
          }
          else if (RequestType.Name == "LeaveSession")
          {
            //ResponseTextBlock.Text = "Delete Session";
            c.leaveSession(true, delegate{});
          }
          else if (RequestType.Name == "Broadcast")
          {
            //ResponseTextBlock.Text = "Delete Session";
            Debug.WriteLine("Broadcast: '" + textfield.Text + "'");

            MemoryStream ms = new MemoryStream();
            Serializer.SerializeWithLengthPrefix<string>(ms, textfield.Text, PrefixStyle.Base128);
            byte[] byteArray = ms.ToArray();

            c.broadcast(byteArray, "coolEvent");
          }
          else Debug.WriteLine(RequestType.Name);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OptionSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
            RequestType = lpi;
        }

    }
}