using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace QuanLyThuChi.Config
{
    public class LocalizationService
    {
        private readonly ResourceManager _resourceManager;

        public LocalizationService()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var assemblyName = new AssemblyName(assembly.FullName);
            _resourceManager = new ResourceManager($"{assemblyName.Name}.Resx", assembly);
        }

        public string GetString(string key)
        {
            var cultureInfo = CultureInfo.CurrentUICulture;
            var localizedString = _resourceManager.GetString(key, cultureInfo);

            if (string.IsNullOrEmpty(localizedString))
            {
                localizedString = key;
            }

            return localizedString;
        }
    }
}
