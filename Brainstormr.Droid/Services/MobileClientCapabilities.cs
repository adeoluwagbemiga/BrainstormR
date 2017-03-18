using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Brainstormr.Portable.ViewModel;
using System.IO;
using Android.Util;

namespace Brainstormr.Droid.Services
{
    public class MobileClientCapabilities : IMobileClientCapabilities
    {
        public void deleteFile(string filepath)
        {
            File.Delete(filepath);
        }

        public DateTime getCurrentDate()
        {
            return DateTime.Now;
        }

        public string getDocumentStoragePath()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return documentsPath;
        }

        public byte[] getFileByteArray(string filepath)
        {
            return File.ReadAllBytes(filepath);
        }

        public void LogEvent(int logType, string message)
        {
            string ls_tag = "BRAINSTORMRLOG";
            string logmsg = message + " ++ " + DateTime.Now.ToString();
            if (logType == 1) Log.Info(ls_tag, logmsg);
            if (logType == 2) Log.Warn(ls_tag, logmsg);
            if (logType == 3) Log.Error(ls_tag, logmsg);
        }

        public void SaveFile(string filepath, byte[] fileBytes)
        {
            throw new NotImplementedException();
        }
    }
}