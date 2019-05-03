// Decompiled with JetBrains decompiler
// Type: AresPlugin.ImagePlugin
// Assembly: AresPlugin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7FD261EF-3851-4E6C-BF90-2BA317CE9E7F
// Assembly location: E:\SvnRoot\1.MP\MediaPortal-1\skin\Ares\Plugin\AresPlugin.dll

using MediaPortal.GUI.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Xml;

namespace AresPlugin
{
  public class ImagePlugin : IPlugin, ISetupForm
  {
    private System.Threading.Timer fancyTimer = new System.Threading.Timer((TimerCallback) (state =>
    {
      string tagvalue1 = (string) null;
      string tagvalue2 = (string) null;
      string tagvalue3 = (string) null;
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      try
      {
        string inputUri = "http://movieweb.com/rss/movie-news/";
        List<string> stringList = new List<string>();
        using (XmlReader reader = XmlReader.Create(inputUri))
        {
          SyndicationItem syndicationItem = SyndicationFeed.Load(reader).Items.FirstOrDefault<SyndicationItem>();
          tagvalue1 = syndicationItem.Title.Text;
          tagvalue2 = syndicationItem.Summary.Text;
          tagvalue3 = syndicationItem.Links[0].Uri.ToString();
        }
      }
      catch
      {
        DateTime utcNow = DateTime.UtcNow;
        string str = "\r\nError pulling latest movie news from Movieweb.com.\r\n";
        StreamWriter streamWriter = new StreamWriter(folderPath + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine((object) utcNow);
        streamWriter.WriteLine(str);
        streamWriter.Close();
      }
      try
      {
        GUIPropertyManager.SetProperty("#LatestMovieNewsTitle", tagvalue1);
        GUIPropertyManager.SetProperty("#LatestMovieNewsDesc", tagvalue2);
        GUIPropertyManager.SetProperty("#LatestMovieNewsImg", tagvalue3);
      }
      catch
      {
        DateTime utcNow = DateTime.UtcNow;
        StreamWriter streamWriter = new StreamWriter(folderPath + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + "    There was a problem with setting the latest movie news properties!..\r\n");
        streamWriter.Close();
      }
    }), (object) null, 0, 300000);

    public string Author()
    {
      return "Wizard123";
    }

    public bool CanEnable()
    {
      return true;
    }

    public bool DefaultEnabled()
    {
      return true;
    }

    public string Description()
    {
      return "AresBackup";
    }

    public bool GetHome(
      out string strButtonText,
      out string strButtonImage,
      out string strButtonImageFocus,
      out string strPictureImage)
    {
      strButtonText = this.PluginName();
      strButtonImage = string.Empty;
      strButtonImageFocus = string.Empty;
      strPictureImage = string.Empty;
      return false;
    }

    public bool HasSetup()
    {
      return false;
    }

    public string PluginName()
    {
      return "AresBackup";
    }

    public void ShowPlugin()
    {
    }

    public int GetWindowId()
    {
      return 0;
    }

    void IPlugin.Start()
    {
      if (GUIWindowManager.ActiveWindow <= 0)
        return;
      GUIPropertyManager.SetProperty("#media.user", Environment.UserName);
      string folderPath1 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      string path1 = folderPath1 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\media\\btn\\unfocus";
      string str1 = (string) null;
      foreach (string file in Directory.GetFiles(path1))
        str1 = str1 + "," + Path.GetFileNameWithoutExtension(file);
      char[] chArray1 = new char[2]{ ',', ' ' };
      string str2 = str1.TrimStart(chArray1);
      XmlDocument xmlDocument1 = new XmlDocument();
      xmlDocument1.Load(folderPath1 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\PluginDefines.xml");
      try
      {
        xmlDocument1.SelectSingleNode("//window/define[contains(.,'#PluginTileList:')]").InnerText = "#PluginTileList:" + str2;
        DateTime utcNow = DateTime.UtcNow;
        string str3 = "    Tile image list updated and written to xml.\r\n";
        StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + str3);
        streamWriter.Close();
      }
      catch
      {
        DateTime utcNow = DateTime.UtcNow;
        StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + "    There was a problem with the xml parsing during tile list update or define does not exist !..\r\n");
        streamWriter.Close();
      }
      xmlDocument1.Save(folderPath1 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\PluginDefines.xml");
      string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      string path2 = folderPath2 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\Themes\\Apollo\\Media\\btn\\unfocus";
      string str4 = (string) null;
      foreach (string file in Directory.GetFiles(path2))
        str4 = str4 + "," + Path.GetFileNameWithoutExtension(file);
      char[] chArray2 = new char[2]{ ',', ' ' };
      string str5 = str4.TrimStart(chArray2);
      XmlDocument xmlDocument2 = new XmlDocument();
      xmlDocument2.Load(folderPath2 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\PluginDefines.xml");
      try
      {
        xmlDocument2.SelectSingleNode("//window/define[contains(.,'#PluginTileListApollo:')]").InnerText = "#PluginTileListApollo:" + str5;
        DateTime utcNow = DateTime.UtcNow;
        string str3 = "    Tile image list for Apollo theme updated and written to xml.\r\n";
        StreamWriter streamWriter = new StreamWriter(folderPath2 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + str3);
        streamWriter.Close();
      }
      catch
      {
        DateTime utcNow = DateTime.UtcNow;
        StreamWriter streamWriter = new StreamWriter(folderPath2 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + "    There was a problem with the xml parsing during tile list update for Apollo or define does not exist !..\r\n");
        streamWriter.Close();
      }
      xmlDocument2.Save(folderPath2 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\PluginDefines.xml");
      int num1 = 0;
      int num2 = 0;
      WebClient webClient = new WebClient();
      try
      {
        webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
        webClient.DownloadFile("https://raw.githubusercontent.com/Wizard123/Ares-for-MP1/master/Version.txt", folderPath1 + "\\Team MediaPortal\\MediaPortal\\Version.txt");
        using (TextReader textReader = (TextReader) System.IO.File.OpenText(folderPath1 + "\\Team MediaPortal\\MediaPortal\\Version.txt"))
        {
          num1 = int.Parse(textReader.ReadLine());
          StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
          streamWriter.WriteLine("Remote Ares version: " + (object) num1);
          streamWriter.Close();
        }
        using (TextReader textReader = (TextReader) System.IO.File.OpenText(folderPath1 + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\Version.txt"))
        {
          num2 = int.Parse(textReader.ReadLine());
          StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
          streamWriter.WriteLine("Local Ares version: " + (object) num2);
          streamWriter.Close();
        }
      }
      catch (Exception ex)
      {
        StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine("The process failed: {0}", (object) ex.Message);
        streamWriter.Close();
      }
      if (num1 > num2)
      {
        try
        {
          GUIPropertyManager.SetProperty("#skin.updates.available", "True");
          StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
          streamWriter.WriteLine("Updates are available, setting property to : True");
          streamWriter.Close();
        }
        catch (Exception ex)
        {
          StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
          streamWriter.WriteLine("The process failed: {0}", (object) ex.Message);
          streamWriter.Close();
        }
      }
      else
      {
        GUIPropertyManager.SetProperty("#skin.updates.available", "False");
        StreamWriter streamWriter = new StreamWriter(folderPath1 + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine("No updates available");
        streamWriter.Close();
      }
    }

    void IPlugin.Stop()
    {
      string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      try
      {
        System.IO.File.Copy(folderPath + "\\Team MediaPortal\\MediaPortal\\skin\\Ares\\SkinSettings.xml", folderPath + "\\Team MediaPortal\\MediaPortal\\AresSettingsBak.xml", true);
        DateTime utcNow = DateTime.UtcNow;
        string str = "    SkinSettings.xml backed up.\r\n";
        StreamWriter streamWriter = new StreamWriter(folderPath + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + str);
        streamWriter.Close();
        System.IO.File.Delete(folderPath + "\\Team MediaPortal\\MediaPortal\\Version.txt");
      }
      catch
      {
        DateTime utcNow = DateTime.UtcNow;
        string str = "    Failed to backup skinsettings.xml please check file exists and write pesrmissions are available to \\Team MediaPortal\\MediaPortal.\r\n";
        StreamWriter streamWriter = new StreamWriter(folderPath + "\\Team MediaPortal\\MediaPortal\\log\\AresLog.txt", true);
        streamWriter.WriteLine(utcNow.ToString() + str);
        streamWriter.Close();
      }
    }
  }
}
