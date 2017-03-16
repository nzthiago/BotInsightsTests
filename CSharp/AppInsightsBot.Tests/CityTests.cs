using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Autofac;
using Microsoft.Bot.Builder.Dialogs.Internals;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Tests;
using System.Linq;
using System.Threading;

namespace AppInsightsBot.Tests
{
    [TestClass]
    public class CityTests : DialogTestBase
    {
        [TestMethod]
        public async Task ShouldChangeCity()
        {
            IDialog<object> stateDialog = new StateDialog();

            var toBot = DialogTestBase.MakeTestMessage();
            toBot.From.Id = Guid.NewGuid().ToString();
            toBot.Text = "hi!";
            Func<IDialog<object>> MakeRoot = () => stateDialog;

            using (new FiberTestBase.ResolveMoqAssembly(stateDialog))
            using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, stateDialog))
            {
                // act: sending the message
                IMessageActivity toUser = await GetResponse(container, MakeRoot, toBot);

                // assert: check if the dialog returned the right response
                Assert.IsTrue(toUser.Text.Equals("Welcome to the Search City bot. I'm currently configured to search for things in Seattle"));

                toBot.Text = "Thiago";
                toUser = await GetResponse(container, MakeRoot, toBot);
                Assert.IsTrue(toUser.Text.StartsWith("Welcome Thiago!"));

                toBot.Text = "change city to Portland";
                toUser = await GetResponse(container, MakeRoot, toBot);
                Assert.IsTrue(toUser.Text.Equals("All set Thiago. From now on, all my searches will be for things in Portland."));
            }
        }

        [TestMethod]
        public async Task ShouldCheckCurrentCity()
        {
            IDialog<object> stateDialog = new StateDialog();

            var toBot = DialogTestBase.MakeTestMessage();
            toBot.From.Id = Guid.NewGuid().ToString();
            toBot.Text = "hi";
            Func<IDialog<object>> MakeRoot = () => stateDialog;

            using (new FiberTestBase.ResolveMoqAssembly(stateDialog))
            using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, stateDialog))
            {
                // act: sending the message
                IMessageActivity toUser = await GetResponse(container, MakeRoot, toBot);

                // assert: check if the dialog returned the right response
                Assert.IsTrue(toUser.Text.Equals("Welcome to the Search City bot. I'm currently configured to search for things in Seattle"));

                toBot.Text = "Thiago";
                toUser = await GetResponse(container, MakeRoot, toBot);
                Assert.IsTrue(toUser.Text.StartsWith("Welcome Thiago!"));

                toBot.Text = "current city";
                toUser = await GetResponse(container, MakeRoot, toBot);
                Assert.IsTrue(toUser.Text.Equals("Hey Thiago, I'm currently configured to search for things in Seattle."));
            }
        }

        [TestMethod]
        public async Task ShouldShowWelcome()
        {
            IDialog<object> stateDialog = new StateDialog();

            var toBot = DialogTestBase.MakeTestMessage();
            toBot.From.Id = Guid.NewGuid().ToString();
            toBot.Text = "hi";
            Func<IDialog<object>> MakeRoot = () => stateDialog;

            using (new FiberTestBase.ResolveMoqAssembly(stateDialog))
            using (var container = Build(Options.MockConnectorFactory | Options.ScopedQueue, stateDialog))
            {
                // act: sending the message
                IMessageActivity toUser = await GetResponse(container, MakeRoot, toBot);

                // assert: check if the dialog returned the right response
                Assert.IsTrue(toUser.Text.Equals("Welcome to the Search City bot. I'm currently configured to search for things in Seattle"));
            }
        }

        private async Task<IMessageActivity> GetResponse(IContainer container, Func<IDialog<object>> makeRoot, IMessageActivity toBot)
        {
            using (var scope = DialogModule.BeginLifetimeScope(container, toBot))
            {
                DialogModule_MakeRoot.Register(scope, makeRoot);

                // act: sending the message
                using (new LocalizedScope(toBot.Locale))
                {
                    var task = scope.Resolve<IPostToBot>();
                    await task.PostAsync(toBot, CancellationToken.None);
                }
                //await Conversation.SendAsync(toBot, makeRoot, CancellationToken.None);
                return scope.Resolve<Queue<IMessageActivity>>().Dequeue();
            }
        }
        private IMessageActivity GetResponse(IContainer container, Func<IDialog<object>> makeRoot)
        {

            using (var scope = DialogModule.BeginLifetimeScope(container, DialogTestBase.MakeTestMessage()))
            {
                DialogModule_MakeRoot.Register(scope, makeRoot);

                return scope.Resolve<Queue<IMessageActivity>>().Dequeue();
            }
        }
    }
}
