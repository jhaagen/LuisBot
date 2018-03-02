using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Sample.SimpleEchoBot.Tasks;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace SimpleEchoBot.Dialogs {
    [Serializable]
    public class DevOpsDialog : LuisDialog<object> {
        public DevOpsDialog() : base(CreateLuisService()) {

        }

        private static ILuisService CreateLuisService() {
            string appId = ConfigurationManager.AppSettings["LuisAppId"];
            string apiKey = ConfigurationManager.AppSettings["LuisAPIKey"];
            var luisModel = new LuisModelAttribute(appId, apiKey);
            return new LuisService(luisModel);
        }

        [LuisIntent("ListBuilds")]
        public async Task ListBuildsIntent(IDialogContext context, LuisResult result) {
            await context.PostAsync("Let me list those builds for you.");

            new ListBuilds(context.Activity.ToConversationReference()).Start();

            context.Wait(MessageReceived);

        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result) {
            await context.PostAsync($"You said: {result.Query}. I don't know how to do that.");
            context.Wait(MessageReceived); }
    }
}