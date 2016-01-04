using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using StructureMap;

namespace Wicresoft.Solution.Domain
{
    public class ScreenAdapter : IScreenAdapter
    {
        const string ANDROID_DEFAULT_VIEWPORT = "width=device-width, target-densityDpi={0}dpi, initial-scale=1.0, maximum-scale=1.0, user-scalable=no";
        const string IPHONE_DEFAULT_VIEWPORT = "width=480, maximum-scale=1.0, user-scalable=no";
        const string HEADER_KEY_USERAGENT = "User-Agent";
        const string HEADER_KEY_RESOLUTION = "Resolution";
        const char RESOLUTION_SPLIT = '*';
        const string HEADER_KEY_OS = "OS";
        const string ANDROID = "Android";
        const string HIGH_DPI_PREFIX = "high-";
        const string DEVICE_DPI_PREFIX = "device-";

        public void SetViewport(Action<string> setViewPort)
        {
            var viewport = string.Empty;
            if (IsAndroid())
            {
                //viewport = GetAndroidViewport();
                viewport = string.Format(ANDROID_DEFAULT_VIEWPORT, HIGH_DPI_PREFIX);
            }
            else
            {
                viewport = IPHONE_DEFAULT_VIEWPORT;
            }

            if (setViewPort != null) setViewPort(viewport);

        }

        internal bool IsAndroid()
        {
            var isAndroid = false;
            var headerCollection = HttpContext.Current.Request.Headers;

            var os = headerCollection[HEADER_KEY_OS];
            if (!string.IsNullOrWhiteSpace(os))
            {
                isAndroid = os.IndexOf(ANDROID, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            if (!isAndroid)
            {
                var userAgent = HttpContext.Current.Request.Headers[HEADER_KEY_USERAGENT];
                if (!string.IsNullOrWhiteSpace(userAgent))
                {
                    isAndroid |= userAgent.IndexOf(ANDROID, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }

            return isAndroid;
        }

        private string GetAndroidViewport()
        {
            var viewPort = string.Empty;
            var dpi = HIGH_DPI_PREFIX;
            var sessionRepository = ObjectFactory.GetInstance<ISessionRepository>();

            var resolution = HttpContext.Current.Request.Headers[HEADER_KEY_RESOLUTION];
            if (string.IsNullOrWhiteSpace(resolution))
            {
                resolution = sessionRepository.TryGet(HEADER_KEY_RESOLUTION);

            }

            if (!string.IsNullOrWhiteSpace(resolution))
            {
                sessionRepository[HEADER_KEY_RESOLUTION] = resolution;
                var resolutionParts = resolution.Split(RESOLUTION_SPLIT);
                if (resolutionParts.Length > 0)
                {
                    var wide = double.Parse(resolutionParts[0]);
                    dpi = CalculateAndroidDpi(wide);
                }
            }

            viewPort = string.Format(ANDROID_DEFAULT_VIEWPORT, dpi);


            return viewPort;
        }

        internal string CalculateAndroidDpi(double wide)
        {
            //if (wide <= 480) return HIGH_DPI_PREFIX;
            if (wide >= 800) return DEVICE_DPI_PREFIX;
            return HIGH_DPI_PREFIX;
        }
    }
}
