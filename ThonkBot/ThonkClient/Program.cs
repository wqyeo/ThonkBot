using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using ThonkClient;


namespace ThonkClient {
    public class Program {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal DiscordSocketClient Client { get; private set; }
        internal CommandHandler CommandHandler { get; private set; }
        internal CommandService CommandService { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private const string CONFIG_FILE_PATH = @"cred/config.json";

        public static Task Main(string[] args) => new Program().MainAsync();

        public async Task MainAsync() {
            Client = new DiscordSocketClient();

            Client.Log += Log;

            BotConfig config;
            try {
                // Check README.md in 'cred' folder
                config = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText(CONFIG_FILE_PATH));
            } catch (Exception e) {
                Console.WriteLine("Error Occured trying to load config file...\r\nError:\r\n");
                Console.WriteLine($"{e}\r\n");
                Console.WriteLine("Press anything to exit...");
                Console.ReadKey();
                return;
            }

            await Client.LoginAsync(TokenType.Bot, config.Token);
            await Client.StartAsync();

            CommandService = new CommandService();
            CommandHandler = new CommandHandler(Client, CommandService, config.Prefix);
            await CommandHandler.InstallCommandsAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg) {
            // TODO: better logging
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}