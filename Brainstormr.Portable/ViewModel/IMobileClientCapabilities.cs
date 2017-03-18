using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.ViewModel
{
    public interface IMobileClientCapabilities
    {
        string getDocumentStoragePath();
        byte[] getFileByteArray(string filepath);
        void deleteFile(string filepath);
        void LogEvent(int logType, string message);
        DateTime getCurrentDate();
        void SaveFile(string filepath, byte[] fileBytes);
    }
}
