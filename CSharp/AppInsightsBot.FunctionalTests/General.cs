namespace AppInsightsBot.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class General
    {
        private static BotHelper botHelper;
        public static TestContext testContext { get; set; }

        internal static BotHelper BotHelper
        {
            get { return botHelper; }
        }

        // Will run once before all of the tests in the project. We start assuming the user is already logged in to Azure,
        // which should  be done separately via the AzureBot.ConsoleConversation or some other means. 
        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            testContext = context;
            string directLineToken = Environment.GetEnvironmentVariable("DirectLineToken");
            if (string.IsNullOrEmpty(directLineToken))
                directLineToken = context.Properties["DirectLineToken"].ToString();
            string microsoftAppId = Environment.GetEnvironmentVariable("MicrosoftAppId");
            if (string.IsNullOrEmpty(microsoftAppId))
                microsoftAppId = context.Properties["MicrosoftAppId"].ToString();
            string botId = Environment.GetEnvironmentVariable("BotId");
            if (string.IsNullOrEmpty(botId))
                botId = context.Properties["BotId"].ToString();

            botHelper = new BotHelper(directLineToken, microsoftAppId, botId);
        }

        // Will run after all the tests have finished
        [AssemblyCleanup]
        public static void CleanUp()
        {
            if (botHelper != null)
            {
                botHelper.Dispose();
            }
        }
    }
}
