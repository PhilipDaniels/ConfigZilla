using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Globalization;

namespace ConfigZilla.Tasks
{
    /// <summary>
    /// Simple task to get the time as a string to a high precision, so that it can be passed
    /// into the TimeTaken task.
    /// </summary>
    public class GetTime : Task
    {
        public const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

        [Output]
        public string CurrentTime { get; set; }

        public override bool Execute()
        {
            CurrentTime = DateTime.Now.ToString(TIME_FORMAT, CultureInfo.InvariantCulture);
            return true;
        }
    }
}
