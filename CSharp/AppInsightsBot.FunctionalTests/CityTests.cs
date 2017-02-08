using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AppInsightsBot.FunctionalTests
{
    [TestClass]
    public class CityTests
    {
        [TestMethod]
        public async Task ShouldChangeCity()
        {
            var step1 = new BotTestCase()
            {
                Action = "hi",
                ExpectedReply = "Welcome to the Search City bot. I'm currently configured to search for things in Seattle",
            };

            var step2 = new BotTestCase()
            {
                Action = "Thiago",
                ExpectedReply = "Welcome Thiago!",
            };

            var step3 = new BotTestCase()
            {
                Action = "change city to Portland",
                ExpectedReply = "All set Thiago. From now on, all my searches will be for things in Portland.",
            };

            var steps = new List<BotTestCase> { step1, step2, step3 };

            await TestRunner.RunTestCases(steps, null, 0);
        }

        [TestMethod]
        public async Task ShouldCheckCurrentCity()
        {
            var step1 = new BotTestCase()
            {
                Action = "hi",
                ExpectedReply = "Welcome to the Search City bot. I'm currently configured to search for things in Seattle",
            };

            var step2 = new BotTestCase()
            {
                Action = "Thiago",
                ExpectedReply = "Welcome Thiago!",
            };

            var step3 = new BotTestCase()
            {
                Action = "current city",
                ExpectedReply = "Hey Thiago, I'm currently configured to search for things in Seattle.",
            };

            var steps = new List<BotTestCase> { step1, step2, step3 };

            await TestRunner.RunTestCases(steps, null, 0);
        }

        [TestMethod]
        public async Task ShouldShowWelcome()
        {
            var testCase = new BotTestCase()
            {
                Action = "hi",
                ExpectedReply = "Welcome to the Search City bot. I'm currently configured to search for things in Seattle",
            };

            await TestRunner.RunTestCase(testCase);

        }

    }
}
