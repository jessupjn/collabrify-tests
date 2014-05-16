using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using System.IO;
using System.Windows;
using System.Reflection;
using System.Runtime.InteropServices;

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
      browser.IsGeolocationEnabled = true;
      browser.ScriptNotify += ScriptCallback;
      browser.Navigated += delegate
      {
        Debug.WriteLine("FIRST NAVIGATION COMPLETED");
        //browser.NavigateToString("javascript:connectToServer('" + mToken + "');");
      };

      string html = null;
      using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("Collabrify_wp8.Resources.home.html"))
      using (var r = new StreamReader(s))
      html = r.ReadToEnd();

      browser.NavigateToString(html);
    }//

    public WebBrowser getBrowser()
    {
      return browser;
    }

    private void ScriptCallback(object sender, NotifyEventArgs e)
    {
      switch( e.Value.Substring(0,8) )
      {
        case "onLoad::":
          pageLoaded();
          break;
        case "onOpen::":
          channelOpen();
          break;
        case "onClose:":
          channelClosed();
          break;
        case "errors::":
          channelError("","");
          break;
        case "message:":
          channelMessageReceived(e.Value.Substring(8));
          break;
      }

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
