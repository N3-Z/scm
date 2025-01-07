﻿using System;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ScmBackup.Tests.Integration
{
    /// <summary>
    /// helper class to create unique temp directories for integration tests
    /// </summary>
    public class DirectoryHelper
    {
        public static string CreateTempDirectory()
        {
            return DirectoryHelper.CreateTempDirectory(string.Empty);
        }

        public static string CreateTempDirectory(string suffix)
        {
            string tempDir = Path.GetTempPath();
            string subDir = "_scm-backup-tests";
            string newDir =  DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);

            if (!string.IsNullOrWhiteSpace(suffix))
            {
                newDir += '-' + suffix;
            }

            string finalDir = Path.Combine(tempDir, subDir, newDir);

            if (Directory.CreateDirectory(finalDir) != null)
            {
                return finalDir;
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the directory of the current test assembly (usually bin/debug)
        /// </summary>
        public static string TestAssemblyDirectory()
        {
            string unc = typeof(DirectoryHelper).GetTypeInfo().Assembly.Location;

            // convert from UNC path to "real" path
            var uri = new Uri(unc);
            string file = uri.LocalPath;

            return Path.GetDirectoryName(file);
        }
    }
}
