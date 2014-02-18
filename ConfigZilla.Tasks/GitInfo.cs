using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConfigZilla.Tasks
{
    public class GitInfo : TaskWithProperties
    {
        const string GitExeName = "git.exe";

        /// <summary>
        /// Location of the GitExe. can be left blank, in which case "the usual suspects"
        /// will be searched.
        /// </summary>
        public string GitExe { get; set; }

        [Output]
        public string Branch { get; set; }

        [Output]
        public string LastCommitHash { get; set; }

        public override bool Execute()
        {
            Branch = LastCommitHash = "";
            string pathToGitExe = FindGitExe();
            if (String.IsNullOrEmpty(pathToGitExe))
                return false;

            Branch = RunGit(pathToGitExe, "symbolic-ref --short head");
            LastCommitHash = RunGit(pathToGitExe, "show -s --pretty=format:%H");

            return true;
        }

        string FindGitExe()
        {
            // Try and find the one passed in.
            if (!String.IsNullOrWhiteSpace(GitExe))
            {
                if (GitExistsAtPath(GitExe))
                {
                    return MakeFullPath(GitExe);
                }
                else
                {
                    Log.LogWarning("Cannot find git.exe at your specified path of " + GitExe + ", trying default locations.");
                }
            }

            // Try the usual suspects.
            foreach (string possiblePath in GitExeSearchPaths)
            {
                if (GitExistsAtPath(possiblePath))
                {
                    return MakeFullPath(possiblePath);
                }
            }

            Log.LogWarning("Could not locate git.exe in the usual places. Testing to see if it is on your path.");
            string version = RunGit(GitExe, "--version", false);
            if (String.IsNullOrEmpty(version))
            {
                Log.LogError("Could not find git.exe on your path.");
                return "";
            }
            else
            {
                return GitExe;
            }
        }

        string RunGit(string gitExe, string arguments, bool logErrors = true)
        {
            try
            {
                ProcessStartInfo gitInfo = new ProcessStartInfo();
                gitInfo.CreateNoWindow = true;
                gitInfo.RedirectStandardError = true;
                gitInfo.RedirectStandardOutput = true;
                gitInfo.FileName = gitExe;
                gitInfo.UseShellExecute = false;

                Process gitProcess = new Process();
                gitInfo.Arguments = arguments;
                gitInfo.WorkingDirectory = Properties["MSBuildProjectDirectory"];
                gitProcess.StartInfo = gitInfo;
                gitProcess.Start();
                string stderr_str = gitProcess.StandardError.ReadToEnd();
                string stdout_str = gitProcess.StandardOutput.ReadToEnd();
                gitProcess.WaitForExit();
                gitProcess.Close();
                string gitOutput = stdout_str.Replace("\r", "").Replace("\n", "");
                return gitOutput;
            }
            catch (Exception ex)
            {
                if (logErrors)
                {
                    Log.LogError("Error running git.exe: " + ex.ToString());
                }
                return "";
            }
        }

        IEnumerable<string> GitExeSearchPaths
        {
            get
            {
                yield return @"C:\Program Files (x86)\Git\bin";
                yield return @"C:\PublicHome\OtherApps\PortableGit\bin";
            }
        }

        bool GitExistsAtPath(string path)
        {
            path = MakeFullPath(path);
            return File.Exists(path);
        }

        string MakeFullPath(string path)
        {
            path = (path ?? "").Trim();

            if (path.EndsWith(GitExeName, StringComparison.OrdinalIgnoreCase))
                return path;
            else
            {
                return Path.Combine(path, GitExeName);
            }
        }
    }

}
