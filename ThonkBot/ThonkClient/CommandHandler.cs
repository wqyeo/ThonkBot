using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace ThonkClient {
    public class CommandHandler {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly string prefix;

        // Retrieve client and CommandService instance via ctor
        public CommandHandler(DiscordSocketClient client, CommandService commands, string prefix) {
            this.commands = commands;
            this.client = client;
            this.prefix = prefix;
        }

        public async Task InstallCommandsAsync() {
            // Hook the MessageReceived event into our command handler
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam) {

            if (!TryGetUserMessage(messageParam, out SocketUserMessage message)) {
                return;
            }

            int argPos = 0;
            if (!(MessageHaveMentionOrPrefix(message, ref argPos) || message.Author.IsBot)){
                return;
            }

            var context = new SocketCommandContext(client, message);
            await commands.ExecuteAsync(context: context, argPos: argPos, services: null);
        }

        private bool MessageHaveMentionOrPrefix(SocketUserMessage message, ref int argPos) {
            return message.HasStringPrefix(prefix, ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos);
        }

        private bool TryGetUserMessage(SocketMessage messageParam, out SocketUserMessage message) {
            message = messageParam as SocketUserMessage;
            return message != null;
        }
    }
}
