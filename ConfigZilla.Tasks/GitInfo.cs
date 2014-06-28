using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConfigZilla.Tasks
{
    public class GitInfo : TaskWithProperties
    {
        string[] GitExeNames = { "git.exe" };
        string[] GitExeSearchPaths = { @"C:\Program Files (x86)\Git\bin" };

        /// <summary>
        /// Location of the GitExe. can be left blank, in which case "the usual suspects"
        /// will be searched. If git is not found then the task will still return success
        /// so as not to abort builds on boxes that don't have git.
        /// </summary>
        public string GitExe { get; set; }

        [Output]
        public string Branch { get; set; }

        [Output]
        public string LastCommitHash { get; set; }

        public override bool Execute()
        {
            if (GitExe != null)
                GitExe = GitExe.Trim();

            Branch = LastCommitHash = "";
            string pathToGitExe = FindGitExe();
            if (String.IsNullOrEmpty(pathToGitExe))
                return true;

            Log.LogMessage(MessageImportance.Normal, "Found git at '{0}'.", pathToGitExe);
            Branch = RunGit(pathToGitExe, "symbolic-ref --short head");
            LastCommitHash = RunGit(pathToGitExe, "show -s --pretty=format:%H");

            return true;
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

        string FindGitExe()
        {
            string result = null;

            // Try and find the one passed in. This must be a file.
            if (!String.IsNullOrWhiteSpace(GitExe))
            {
                result = FindGitExeImpl(GitExe);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    Log.LogWarning("Cannot find git executable at your specified MSBuild-property-specified path of '" + GitExe + 
                        "'. (It can be a list of possibilities, separated by semi-colons).");
                }
            }

            // Is it specified via an environment variable?
            string ev = Environment.GetEnvironmentVariable("CZ_GIT_EXE");
            if (!String.IsNullOrEmpty(ev))
            {
                result = FindGitExeImpl(ev);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    Log.LogWarning("Cannot find git executable at '" + ev + 
                        "' as specified in environment variable CZ_GIT_EXE. (It can be a list of possibilities, separated by semi-colons).");
                }
            }

            // Try "the usual suspects".
            foreach (string possiblePath in GitExeSearchPaths)
            {
                foreach (string exeName in GitExeNames)
                {
                    string p = Path.Combine(possiblePath, exeName);
                    if (FindGitExeImpl(p) != null)
                        return p;
                }
            }

            Log.LogWarning("Could not locate git.exe in the usual places. Testing to see if it is on your path.");
            foreach (string exe in GitExeNames)
            {
                string version = RunGit(exe, "--version", false);
                if (!String.IsNullOrEmpty(version))
                    return exe;
            }


            Log.LogWarning("Could not find git.exe on your path, ignoring.");
            return null;
        }

        /// <summary>
        /// Test to see whether any of the paths in <paramref name="possiblePaths"/> matches
        /// a file on disk. This method is guaranteed to not blow up. We don't want to
        /// fail due a build due to crappy environment variable settings, for example.
        /// </summary>
        /// <param name="possiblePaths">Semi-colon separated list of possible git exes.</param>
        /// <returns>First file path that exists, or null if none exist.</returns>
        string FindGitExeImpl(string possiblePaths)
        {
            try
            {
                if (String.IsNullOrEmpty(possiblePaths))
                    return null;

                string[] paths = possiblePaths.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (paths != null && paths.Length > 0)
                {
                    foreach (string path in paths)
                    {
                        if (File.Exists(path))
                            return path;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
