using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Globalization;

namespace ConfigZilla.Tasks
{
    /// <summary>
    /// Simple task to get output the difference between two times, in seconds.
    /// </summary>
    public class GetElapsedTime : Task
    {
        [Required]
        public string StartTime { get; set; }

        public string EndTime { get; set; }

        [Output]
        public string TotalSecs { get; set; }

        public override bool Execute()
        {
            DateTime start;
            if (!DateTime.TryParseExact(StartTime, new string[] { GetTime.TIME_FORMAT }, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                if (!DateTime.TryParse(StartTime, out start))
                {
                    Log.LogError("Cannot parse StartTime to a time.");
                    return false;
                }
            }

            DateTime end = DateTime.Now;
            if (!String.IsNullOrWhiteSpace(EndTime))
            {
                if (!DateTime.TryParseExact(StartTime, new string[] { GetTime.TIME_FORMAT }, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
                {
                    if (!DateTime.TryParse(StartTime, out start))
                    {
                        Log.LogError("Cannot parse EndTime to a time.");
                        return false;
                    }
                }
            }

            var delta = end - start;
            TotalSecs = delta.TotalSeconds.ToString("0.00", CultureInfo.InvariantCulture);
            return true;
        }
    }
}
