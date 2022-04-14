using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.CompilerServices;

namespace My_First_Discord_NET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var bot = new BotDiscord();
            await bot.Run();
        }
    }

    class BotDiscord
    {
        private DiscordSocketClient _client;
        private CommandService _command;

        public BotDiscord()
        {
            _client = new DiscordSocketClient();
            _command = new CommandService();

            _client.Log += LogAsync;
            _client.MessageUpdated += MessageUpdated;
            _client.MessageReceived += MessageReceived;

            _command.Log += LogAsync;

        }

        public async Task Run()
        {
            // _client.Log += Log;
            var token = "Nzc3ODcyNTAyNjkzMTY3MTA1.............";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task<Task> MessageReceived(SocketMessage message) 
        {
            var msg = message as SocketUserMessage;

            char prefix = '?';
            int arg = 0;

            if((msg.HasCharPrefix(prefix, ref arg)))
            {
                // ค้างอยู่ตรงนี้้ 555+
            }


            // Console.WriteLine("{0} : {1}", message.Author, message);
            // ArrayList data = CheckPrefix((string)"?", (string)"e", (string)message.Content);
            // Console.WriteLine(data);

            return Task.CompletedTask;
        }
        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }

        private async Task<Task> LogAsync(LogMessage message)
        {
            // Console.WriteLine(message);
            if(message.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}"
                + $" failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException);
            }
            else
            {
                Console.WriteLine($"[General/{message.Severity}] {message}");
            }

            return Task.FromResult(Task.CompletedTask);
        }
    }

    public class RegisterCommandAttribute : CommandAttribute 
    {
        public RegisterCommandAttribute([CallerMemberName] string name = "") 
            : base(name) { }
    }

    class Commands
    {
        [RegisterCommand]
        public string PingPong()
        {  

            return "OK";
        }

        private ArrayList IsCommmand(string prefix, string command, string message)
        {
            var arr = new ArrayList();

            if ((prefix + command) == message)
            {
                arr.Add(true);
                arr.Add(command);
                arr.Add(message.Split(' '));
            }
            else
            {
                arr.Add(false);
                arr.Add(null);
                arr.Add(null);
            }

            return arr;
        }
    }
}
