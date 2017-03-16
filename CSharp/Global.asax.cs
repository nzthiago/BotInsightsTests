namespace AppInsightsBot
{
    using System;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static Microsoft.ApplicationInsights.TelemetryClient Telemetry { get; } = new Microsoft.ApplicationInsights.TelemetryClient();

        protected void Application_Start()
        {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY")))
                Telemetry.InstrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
