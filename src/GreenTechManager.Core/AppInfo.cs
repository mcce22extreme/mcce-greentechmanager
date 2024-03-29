﻿using System.Reflection;

namespace GreenTechManager.Core
{
    public class AppInfo
    {
        public string AppName { get; }

        public string AppVersion { get; }

        public static AppInfo Current { get; } = new AppInfo();

        private AppInfo()
        {
            var assemblyName = Assembly.GetEntryAssembly().GetName();
            AppName = assemblyName.Name;
            AppVersion = assemblyName.Version.ToString();
        }
    }
}
