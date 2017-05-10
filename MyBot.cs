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

        string[] testStr;

        public MyBot()
        {
            rand = new Random();


            testStr = new string[]
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

            
            commands = discord.GetService<CommandService>();

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
                    //await e.Channel.SendMessage("pong");
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

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}