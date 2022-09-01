using Discord;
using Discord.WebSocket;
namespace ThonkBrain.Modules {
    public static class Info {

        /// <summary>
        /// Collects info about how many users are doing a certain activity.
        /// Then output it into an embed.
        /// </summary>
        /// <param name="users">The collection of users,</param>
        /// <param name="activityType">The type of activity to track,</param>
        /// <returns>The embed.</returns>
        public static Embed GetUsersActivities(IReadOnlyCollection<SocketGuildUser> users, ActivityType activityType) {
            Dictionary<string, int> activityCounts = GetActivityCounts();

            var embed = new EmbedBuilder { };

            foreach (var activityCount in activityCounts) {
                embed.AddField(activityCount.Key, activityCount.Value.ToString());
            }

            // TODO: Maybe return an embedbuilder instead?
            // or accept embed config input.
            return embed.Build();

            #region Local_Function

            // Get the amount of users playing a certain activity.
            Dictionary<string, int> GetActivityCounts() {
                Dictionary<string, int> res = new Dictionary<string, int>();

                foreach (var user in users) {
                    foreach (var activity in user.Activities) {

                        if (activity.Type == activityType) {
                            res.IncrementValueByID(activity.Name);
                        }
                    }
                }

                return res;
            }

            #endregion
        }
    }
}
