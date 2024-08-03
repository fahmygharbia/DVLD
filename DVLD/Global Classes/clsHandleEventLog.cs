using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DVLD.Global_Classes
{
    internal static class clsHandleEventLog
    {
        public static void SaveEventLog(string message, EventLogEntryType eventLogEntryType, string sourceName = "DVLD")
        {
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application"); 
            }

            EventLog.WriteEntry(sourceName, message, eventLogEntryType);

        }

    }
}
