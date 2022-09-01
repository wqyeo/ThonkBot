using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThonkClient.Modules {
    public class AdminModule : ModuleBase<SocketCommandContext> {
        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public Task BanAsync(IGuildUser user) => Context.Guild.AddBanAsync(user);
    }
}
