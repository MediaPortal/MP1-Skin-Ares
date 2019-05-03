// Decompiled with JetBrains decompiler
// Type: Update_Ares.Program
// Assembly: Update_Ares, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9DA556C2-859F-4341-9272-72EA75C0651F
// Assembly location: E:\SvnRoot\1.MP\MediaPortal-1\skin\Ares\Areschecker.exe

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace Update_Ares
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      int num1 = 0;
      int num2 = 0;
      string path1 = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate";
      string str1 = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\Ares.zip";
      string str2 = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate";
      string path2 = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate\\Ares-for-MP1-master\\Themes";
      string sourceDirName = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate\\Ares-for-MP1-master\\";
      string destDirName = "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\skin\\Ares\\";
      try
      {
        Directory.CreateDirectory(path1);
      }
      catch (Exception ex)
      {
        Console.WriteLine("The process failed: {0}", (object) ex.Message);
      }
      WebClient webClient = new WebClient();
      try
      {
        webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36";
        webClient.DownloadFile("https://raw.githubusercontent.com/Wizard123/Ares-for-MP1/master/Version.txt", "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate\\Version.txt");
        using (TextReader textReader = (TextReader) System.IO.File.OpenText("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresUpdate\\Version.txt"))
        {
          num1 = int.Parse(textReader.ReadLine());
          Console.WriteLine("\r\n Remote version is " + (object) num1);
        }
        using (TextReader textReader = (TextReader) System.IO.File.OpenText("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\skin\\Ares\\Version.txt"))
        {
          num2 = int.Parse(textReader.ReadLine());
          Console.WriteLine("\r\n Local version is " + (object) num2);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("The process failed: {0}", (object) ex.Message);
      }
      if (num1 <= num2)
        return;
      Console.Write("\r\n Downloading latest Ares files please wait as this may take a while depending on your internet connection");
      webClient.DownloadFile("https://github.com/Wizard123/Ares-for-MP1/archive/master.zip", "C:\\ProgramData\\Team MediaPortal\\MediaPortal\\Ares.zip");
      Console.Write("\r\n Unzipping downloaded files ready for install");
      ZipFile.ExtractToDirectory(str1, str2);
      try
      {
        Directory.Delete(path2, true);
        Console.WriteLine("\r\n Excluding themes folder: " + Directory.Exists(path2).ToString());
      }
      catch (Exception ex)
      {
        Console.WriteLine("The process failed: {0}", (object) ex.Message);
      }
      Program.DirectoryCopy(sourceDirName, destDirName, true);
      Thread.Sleep(5000);
      try
      {
        Directory.Delete(str2, true);
        Directory.Exists(str2);
        Console.WriteLine("Removing update files: Done");
        Thread.Sleep(2000);
        System.IO.File.Delete(str1);
        Console.WriteLine("Removing update package: Done");
        Thread.Sleep(2000);
        Directory.Delete("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\Cache\\Ares", true);
        Console.WriteLine("Clearing cache folder and restoring settings: Done");
        Thread.Sleep(5000);
      }
      catch (Exception ex)
      {
        Console.WriteLine("The process failed: {0}", (object) ex.Message);
      }
      Process.GetProcessById(Process.Start("C:\\ProgramData\\Team MediaPortal\\MediaPortal\\AresBackupRestore.exe").Id).WaitForExit();
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirName);
      if (!directoryInfo1.Exists)
        throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
      DirectoryInfo[] directories = directoryInfo1.GetDirectories();
      if (!Directory.Exists(destDirName))
        Directory.CreateDirectory(destDirName);
      foreach (FileInfo file in directoryInfo1.GetFiles())
      {
        string destFileName = Path.Combine(destDirName, file.Name);
        file.CopyTo(destFileName, true);
        Console.WriteLine(file.Name);
      }
      if (!copySubDirs)
        return;
      foreach (DirectoryInfo directoryInfo2 in directories)
      {
        string destDirName1 = Path.Combine(destDirName, directoryInfo2.Name);
        Program.DirectoryCopy(directoryInfo2.FullName, destDirName1, copySubDirs);
        Console.WriteLine(directoryInfo2.Name);
      }
    }
  }
}
