using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using ThonkClient;


namespace ThonkClient {
    public class Program {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal DiscordSocketClient Client { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private const string CONFIG_FILE_PATH = @"cred/config.json";

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync() {
            Client = new DiscordSocketClient();

            Client.Log += Log;

            // Check README.md in 'cred' folder
            string token = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(CONFIG_FILE_PATH)).Token;

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg) {
            // TODO: better logging
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}