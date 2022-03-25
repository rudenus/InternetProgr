using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace _8_лаба
{
    internal class Program
    {
        private static string token = "5184157146:AAG5SuzC1ykNe76q2SO7TIjO8kuXJ_HzZXc";
        private static TelegramBotClient bot;
        private static Random random = new Random();
        public static async Task Main()
        {
            bot = new TelegramBotClient(token);
            var cts = new CancellationTokenSource();
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } 
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token);
            var client = await bot.GetMeAsync();
            Console.WriteLine(client.FirstName);
            Console.ReadLine();
        }

        private static Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        async static Task HandleUpdateAsync(ITelegramBotClient arg1, Update update, CancellationToken arg3)
        {
            if (update.Type != UpdateType.Message)
                return;
            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            if(update.Message.Type == MessageType.Voice)
            {
                using(var stream = new FileStream(@"../../../data/audio/repondVoice.mp3", FileMode.Open))
                {
                    await bot.SendAudioAsync(chatId, new InputOnlineFile(stream));
                }
                return;
                
            }
            if(update.Message.Type == MessageType.Photo)
            {
                await bot.SendTextMessageAsync(chatId, "Это лучшее фото!!!");
                return;
            }
            if (update.Message.Type != MessageType.Text)
            {
                await bot.SendTextMessageAsync(chatId, "И что ты хочешь этим сказать?" +
                       Environment.NewLine + "Введи /start чтобы посмотреть доступные команды");
                return;
            }
            if (messageText.Contains("/rand"))
            {
                if (Regex.IsMatch(messageText, "/rand [0-9]+ [0-9]+"))//пришлось так вынести тк через switch нельзя будет обработать параметры
                {
                    Console.WriteLine("random func");
                    int firstNum = Convert.ToInt32(messageText.Split(' ')[1]);
                    int secondNum = Convert.ToInt32(messageText.Split(' ')[2]);
                    if (secondNum < firstNum)
                    {
                        var temp = secondNum;
                        secondNum = firstNum;
                        firstNum = temp;
                    }
                    await bot.SendTextMessageAsync(chatId, random.Next(firstNum, secondNum).ToString());
                }
                else
                {
                    await bot.SendTextMessageAsync(chatId, "Введи команду согласно формату :/rand %i %j "
                        + Environment.NewLine + "Пример : /rand 5 10");
                }
                return;
            }
            
            switch (messageText) 
            {
                case "/start":
                    await bot.SendTextMessageAsync(chatId, "Этот бот написан в качестве учебного материала и может выполнять следующие команды: " +
                        Environment.NewLine + "/who - выводит ИД чата, ИД сообщения, ИД отправителя, имя отправителя и дату отправки сообщения." +
                        Environment.NewLine + "/rand %i %j - выводит случайное число между %i и %j. Или сообщение об ошибке, если %j меньше, чем %i.");
                    break;
                case "/who":
                    await bot.SendTextMessageAsync(chatId,
                        "Id чата - " + chatId+ Environment.NewLine
                        + "Id сообщения - "+update.Message.MessageId + Environment.NewLine
                        + "Имя пользователя - "+ update.Message.From.FirstName+Environment.NewLine
                        + "Дата отправки - " + update.Message.Date.ToString());
                    break;
                default:
                    await bot.SendTextMessageAsync(chatId, "И что ты хочешь этим сказать?" +
                        Environment.NewLine+"Введи /start чтобы посмотреть доступные команды");
                    break;
            }


        }
    }
}
