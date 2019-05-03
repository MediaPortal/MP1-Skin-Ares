// Decompiled with JetBrains decompiler
// Type: Backup.Program
// Assembly: AresBackupRestore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEEA91EA-1079-4AAC-9007-7F4F4829E35B
// Assembly location: E:\SvnRoot\1.MP\MediaPortal-1\skin\Ares\Backup\AresBackupRestore.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Backup
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      FileInfo fileInfo1 = new FileInfo("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\BackupSkin\\Ares\\SkinSettings.xml");
      FileInfo fileInfo2 = new FileInfo("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\skin\\Ares\\SkinSettings.xml");
      FileInfo fileInfo3A = new FileInfo("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresSettingsBak.xml");
      FileInfo fileInfo3B = new FileInfo("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\BackupSkin\\Ares\\AresSettingsBak.xml");
      string LogFile = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\log\\AresBackupRestore.log";

      // Copy old backup to new backup folder, if doesn't exist
      if (fileInfo3A.Exists && !fileInfo1.Exists)
      {
        File.Copy(Convert.ToString(fileInfo3A), Convert.ToString(fileInfo1));
        File.Move(Convert.ToString(fileInfo3A), Convert.ToString(fileInfo3B));
        DateTime utcNow = DateTime.UtcNow;
        string str1 = "     No Found skinsettings backup file in 'C:\\ProgramData\\Team MediaPortal\\MediaPortal\\BackupSkin\\Ares\' ";
        string str2 = "     Found old skin backup to 'C:\\ProgramData\\Team MediaPortal\\MediaPortal\\' - File copied and renamed to BackupSkin folder ";
        string str3 = "    --------------------------------------------------------------------------------------------------------------------------------\r\n";
        StreamWriter streamWriter1 = new StreamWriter(LogFile, true);
        streamWriter1.WriteLine("\r\n" + (object)utcNow + str3 + "\r\n" + (object)utcNow + str1 + (object)utcNow + str2 + (object)utcNow + str3);
        streamWriter1.Close();
      }

      // Update Skinsetting value to match backup, but allow to use new skinsetting file with new settings.  
      if (fileInfo1.Exists && fileInfo2.Exists)
      {
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.Load(Convert.ToString(fileInfo1));
        XmlDocument xmlDocument2 = new XmlDocument();
        xmlDocument2.Load(Convert.ToString(fileInfo2));
        DateTime utcNow = DateTime.UtcNow;
        string str1 = "    Found skinsettings backup file in 'C:\\ProgramData\\Team MediaPortal\\MediaPortal\\BackupSkin\\Ares\' so will use that to restore settings from.\r\n\r\n";
        string str2 = "    Start Ares skin settings restore from backed up file..\r\n\r\n";
        string str3 = "    --------------------------------------------------------------------------------------------------------------------------------\r\n";
        StreamWriter streamWriter1 = new StreamWriter(LogFile, true);
        streamWriter1.WriteLine("\r\n" + (object) utcNow + str3 + "\r\n" + (object) utcNow + str2 + (object) utcNow + str1 + (object) utcNow + str3);
        streamWriter1.Close();
        List<string> stringList = new List<string>();
        foreach (XmlNode xmlNode in xmlDocument1.GetElementsByTagName("entry"))
        stringList.Add(xmlNode.Attributes["name"].Value);
        stringList.Remove("guidecolorchannelbutton");
        stringList.Remove("guidecolorchannelbuttonselected");
        stringList.Remove("guidecolorgroupbutton");
        stringList.Remove("guidecolorgroupbuttonselected");
        stringList.Remove("guidecolorprogramselected");
        stringList.Remove("guidecolorprogramended");
        stringList.Remove("guidecolorborderhighlight");
        stringList.Remove("defaultgenre");
        stringList.Remove("genre0");
        stringList.Remove("genre1");
        stringList.Remove("genre2");
        stringList.Remove("genre3");
        stringList.Remove("genre4");
        stringList.Remove("genre5");
        stringList.Remove("genre6");
        foreach (string str4 in stringList)
        {
          try
          {
            XmlNode xmlNode = xmlDocument1.SelectSingleNode("//entry[@name='" + str4 + "']");
            xmlDocument2.SelectSingleNode("//entry[@name='" + str4 + "']").InnerText = xmlNode.InnerText;
            StreamWriter streamWriter2 = new StreamWriter(LogFile, true);
            streamWriter2.WriteLine(utcNow.ToString() + "    " + str4 + " Updated with " + xmlNode.InnerText + "\r\n");
            streamWriter2.Close();
          }
          catch
          {
            StreamWriter streamWriter2 = new StreamWriter(LogFile, true);
            streamWriter2.WriteLine(utcNow.ToString() + "    " + str4 + " does not exist skipping and using default !..\r\n");
            streamWriter2.Close();
          }
        }
        xmlDocument2.Save(Convert.ToString(fileInfo2));
      }
      else
      {
        DateTime utcNow = DateTime.UtcNow;
        string str = "    Unable to find a backup file to restore from, default skin settings will be used.\r\n";
        StreamWriter streamWriter = new StreamWriter(LogFile, true);
        streamWriter.WriteLine(utcNow.ToString() + str);
        streamWriter.Close();
        Environment.Exit(0);
      }
    }
  }
}
