using Collabrify_wp8.Collabrify;
using System;
using System.Collections.Generic;

class TestClass
{
  static void Main()
  {
    // Display the number of command line arguments:
    Debug 
    CollabrifyClient c = new CollabrifyClient("jessupjn@umich.edu", "JACK AND JILL", "wp8-collabrify@umich.edu", "82763BDBCA", true);

    List<string> l = new List<string>();
    l.Add("[none]");
    c.listSessions(l);
  }
}