using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Threading;

namespace QuanLyThuChi.Config
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "QuanLyThuChi.Resx.Resources";
        private static readonly Lazy<ResourceManager> resMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;
            
            var ci = Thread.CurrentThread.CurrentCulture;
            var translate = resMgr.Value.GetString(Text, ci);
            if (translate == null)
            {
                return Text;
            }
            return translate;
        }
    }
}
