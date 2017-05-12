using Discord;
using Discord.Commands;

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;
        

        Random rand;

        string[] youDiedGifs = Directory.GetFiles(@"C:\Users\Wade\Documents\Visual Studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\YouDied");
        string[] smugPic = Directory.GetFiles(@"C:\Users\Wade\Documents\Visual Studio 2015\Projects\ConsoleApplication1\ConsoleApplication1\smug");

        string[] gameList;

        public MyBot()
        {
            rand = new Random();


            gameList = new string[]
            {
                "overwatch",
                "dontstarve",
                "darksouls",
                "gtav",
                "wd2",
                "hots"

            };
    

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;

            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            discord.UserJoined += async (s, e)=>
            {
                var role = e.Server.GetRole(303348412034711554);
                await e.User.AddRoles(role);
            };
            
            commands = discord.GetService<CommandService>();

            commands.CreateCommand("say").Parameter("message", ParameterType.Multiple)
            .Do(async (e) =>
            {
                await SendMessage(e);
            });

            commands.CreateCommand("agc").Parameter("channel", ParameterType.Multiple)
            .Do(async (e) =>
            {
                await AddGameChannel(e);
            });

            RegisterYouDiedCommand();
            RegisterSmugCommand();
            RegisterKindleCommand();
            RegisterJoinRoleCommand();

            

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzA0NzcxNTIyODQ4MDk2MjU2.C9rjSw.S8UU0MRgiu1HWOt2lR6e7DocAlg", TokenType.Bot);
            });
        }

        private void RegisterYouDiedCommand()
        {
            commands.CreateCommand("killme")
                .Do(async (e) =>
                {

                    int randomInt = rand.Next(youDiedGifs.Length);
                    string youDiedToPost = youDiedGifs[randomInt];
                    await e.Channel.SendFile(youDiedToPost);
                });

        }

        private void RegisterSmugCommand()
        {
            commands.CreateCommand("smug")
            .Do(async (e) =>
            {
                    int randomInt = rand.Next(smugPic.Length);
                    string imageToPost = smugPic[randomInt];
                    await e.Channel.SendFile(imageToPost);
            });
        }

        private void RegisterKindleCommand()
        {
            int counter = 6;

            commands.CreateCommand("kindle")
            .Do(async (e) =>
            {
                counter ++;
                await e.Channel.SendMessage("``` You kindled the Bonfire...\n  kindled " + counter + " times.```");
            });

        }


        private void RegisterJoinRoleCommand()
        {

            ///Bad Argument
            commands.CreateCommand("jc")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("```Missing parameter. Enter the name of the role you wish to join...\n      eg. \"!jc overwatch\"```");
                });

            commands.CreateCommand("lc")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("```Missing parameter. Enter the name of the role you wish to leave...\n      eg. \"!lc overwatch\"```");
                });

            ///OVERWATCH
            commands.CreateCommand("jc " + "overwatch")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755181822672911); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "overwatch")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755181822672911);
                    await e.User.RemoveRoles(role);
                });
            ///DARK SOULS
            commands.CreateCommand("jc " + "darksouls")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755365487050753); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "darksouls")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755365487050753);
                    await e.User.RemoveRoles(role);
                });
            ///DONT STARVE
            commands.CreateCommand("jc " + "dontstarve")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755276274335744); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "dontstarve")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304755276274335744);
                    await e.User.RemoveRoles(role);
                });
            ///GTA V
            commands.CreateCommand("jc " + "gtav")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304789186639167490); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "gtav")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304789186639167490);
                    await e.User.RemoveRoles(role);
                });
            ///WD 2
            commands.CreateCommand("jc " + "wd2")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304789271938596874); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "wd2")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(304789271938596874);
                    await e.User.RemoveRoles(role);
                });
            ///Heros of the storm
            commands.CreateCommand("jc " + "hots")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(309780598631825408); //get this argument from "\@<rolename>" in discord client
                    await e.User.AddRoles(role);
                });

            commands.CreateCommand("lc " + "hots")
                .Do(async (e) =>
                {
                    var role = e.Server.GetRole(309780598631825408);
                    await e.User.RemoveRoles(role);
                });

        }

        private async Task SendMessage(CommandEventArgs e)
        {
            var channel = e.Server.FindChannels(e.Args[0], ChannelType.Text).FirstOrDefault();

            var message = ConstructMessage(e, channel != null);

            var userRoles = e.User.Roles;

            if (channel == null)
            {
                await e.Channel.SendMessage("```" + message + "```");
            }
            else if (userRoles.Any(discord => discord.Name.ToUpper() == "FIREKEEPER"))
            {
                if (channel != null)
                {
                    await channel.SendMessage("```" + message + "```");
                }

            }
            else
            {
                await e.Channel.SendMessage("```You do not have permission to use this command...```");
            }
        }

        private string ConstructMessage(CommandEventArgs e, bool firstArgIsChannel)
        {
            string message = "";

            var name = e.User.Nickname != null ? e.User.Nickname : e.User.Name;

            var startIndex = firstArgIsChannel ? 1 : 0;

            for(int i = startIndex; i < e.Args.Length; i++)
            {
                message += e.Args[i].ToString() + " ";
            }

            var result = name + " says: " + message;
            return result;
        }

        private async Task AddGameChannel(CommandEventArgs e)
        {
            var userRoles = e.User.Roles;
            if (userRoles.Any(discord => discord.Name.ToUpper() == "FIREKEEPER"))
            {
                var channel = e.Server.FindChannels("botchan", ChannelType.Text).FirstOrDefault();
                string game = "";
                for (int i = 0; i < e.Args.Length; i++)
                {
                    game += e.Args[i].ToString() + " ";
                }
                await channel.SendMessage("test" + e.Args);
            }
            else
            {
                await e.Channel.SendMessage("```You do not have permission to use this command...```");
            }
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
