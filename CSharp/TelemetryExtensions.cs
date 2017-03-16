namespace AppInsightsBot
{
    using System.Collections.Generic;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.Bot.Builder.Dialogs;
    using Newtonsoft.Json;

    public static class TelemetryExtensions
    {
        public static TraceTelemetry CreateTraceTelemetry(this IDialogContext ctx, string message = null, IDictionary<string, string> properties = null)
        {
            var t = new TraceTelemetry(message);
            AddContextData(t.Properties, ctx);

            var m = ctx.MakeMessage();
            t.Properties.Add("ConversationId", m.Conversation.Id);
            t.Properties.Add("UserId", m.Recipient.Id);

            if (properties != null)
            {
                foreach (var p in properties)
                {
                    t.Properties.Add(p);
                }
            }

            return t;
        }

        public static EventTelemetry CreateEventTelemetry(this IDialogContext ctx, string message = null, IDictionary<string, string> properties = null)
        {
            var t = new EventTelemetry(message);
            AddContextData(t.Properties, ctx);

            var m = ctx.MakeMessage();
            t.Properties.Add("ConversationId", m.Conversation.Id);
            t.Properties.Add("UserId", m.Recipient.Id);

            if (properties != null)
            {
                foreach (var p in properties)
                {
                    t.Properties.Add(p);
                }
            }

            return t;
        }

        public static ExceptionTelemetry CreateExceptionTelemetry(this IDialogContext ctx, System.Exception ex, IDictionary<string, string> properties = null)
        {
            var t = new ExceptionTelemetry(ex);
            AddContextData(t.Properties, ctx);
            var m = ctx.MakeMessage();
            t.Properties.Add("ConversationId", m.Conversation.Id);
            t.Properties.Add("UserId", m.Recipient.Id);

            if (properties != null)
            {
                foreach (var p in properties)
                {
                    t.Properties.Add(p);
                }
            }

            return t;
        }

        private static void AddContextData(IDictionary<string,string> props, IDialogContext ctx)
        {
            props.Add("ConversationData", GetContextData(ctx.ConversationData, ContextConstants.CityKey));
            props.Add("PrivateConversationData", GetContextData(ctx.PrivateConversationData, ContextConstants.CityKey));
            props.Add("UserData", GetContextData(ctx.UserData, ContextConstants.UserNameKey));
        }

        private static string GetContextData(IBotDataBag data, string key)
        {
            string value;
            data.TryGetValue(key, out value);
            return (value != null) ? key + ": " + value : "";
        }
    }
}