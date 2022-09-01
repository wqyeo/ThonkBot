using Discord;
using Discord.Commands;
using Discord.WebSocket;

using ThonkBrain.Modules;

namespace ThonkClient.Modules {
    public class InfoModule : ModuleBase<SocketCommandContext> {
        [Command("userinfo")]
        [Summary("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null) {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }

        [Command("playing")]
        [Alias("p")]
        [RequireContext(ContextType.Guild)]
        public async Task GetUsersPlaying() {
            // NOTE: Wont work properly with large user count
            // See: https://discordnet.dev/api/Discord.WebSocket.SocketGuild.html#Discord_WebSocket_SocketGuild_Users
            var users = Context.Guild.Users;

            var embed = Info.GetUsersActivities(users, ActivityType.Playing);
            if (embed.Fields.Length == 0) {
                await ReplyAsync("Everyone seems to be either doing nothing or hiding their activities...");
            } else {
                await ReplyAsync(embed: embed);
            }
        }
    }
}
