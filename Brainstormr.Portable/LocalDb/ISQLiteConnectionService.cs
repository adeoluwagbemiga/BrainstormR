using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainstormr.Portable.LocalDb
{
    public interface ISQLiteConnectionService
    {
        SQLiteConnection getConnection();
        DateTime getTodaysDate();
    }
}
