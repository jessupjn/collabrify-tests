using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Phone.Controls;

namespace Collabrify_wp8.Collabrify
{
  class ChannelAPI
  {
    ChannelAPIEventListener listener;

    WebBrowser browser;
    
    public ChannelAPI(string mToken)
    {
      browser = new WebBrowser();
      browser.IsScriptEnabled = true;
      browser.Navigated += delegate
      {
        Debug.WriteLine("FIRST NAVIGATION COMPLETED");
        //browser.NavigateToString("javascript:connectToServer('" + mToken + "');");
      };
      browser.Navigate(new Uri("home.html", UriKind.Relative));

    }//

    public WebBrowser getBrowser()
    {
      return browser;
    }

    public void pageLoaded()
    {
      Debug.WriteLine("ChannelAPI - pageLoaded");

    }

    public void channelOpen()
    {
      Debug.WriteLine("ChannelAPI - channelOpen");

      listener.onChannelAPIOpen();


    }

    public void channelClosed()
    {
      Debug.WriteLine("ChannelAPI - channelClosed");

      listener.onChannelAPIClosed();


    }

    public void channelMessageReceived(string message)
    {
      Debug.WriteLine("ChannelAPI - channelMessageReceived");

      listener.onChannelAPIUpdateEvent();

    }

    public void channelError(string code, string description)
    {
      Debug.WriteLine("ChannelAPI - channelError");

      listener.onChannelAPIError();
    }
  }

  class ChannelAPIEventListener
  {
    public void onChannelAPIOpen()
    {

    }

    public void onChannelAPIClosed()
    {

    }

    public void onChannelAPIError()
    {

    }

    public void onChannelAPIUpdateEvent()
    {

    }

  }
}
