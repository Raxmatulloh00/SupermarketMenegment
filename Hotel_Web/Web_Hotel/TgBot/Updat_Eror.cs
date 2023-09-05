using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Threading;
using System.Diagnostics.Metrics;
using TelegramBot;
using About;
using UseCases.Actions;

namespace TelegramBot
{
    public class UpdatEror
    {
        private List<Hotel> hotels;
        private readonly IHotelActions HotelActions;
        public UpdatEror()
        {
            //this.HotelActions = HotelActions;
        }
        public void Work()
        {
            var client = new TelegramBotClient("6506031107:AAGdDKfGIWgJRfrl5xJE4syJplTNlKEEQ_w");
            client.StartReceiving(Update, Error);

        }
        public async Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message?.Text != null)
            {
                await Console.Out.WriteLineAsync($"{message.Chat.Username} | {message.Text}");

                hotels = HotelActions.View().ToList();
                if (message.Text.ToLower().Contains("/start"))
                {


                    await botclient.SendTextMessageAsync(message.Chat.Id, $"Assalomu alaykum hurmatli foydalanuvchi: {message.Chat.FirstName}" +
                        $"     Bizning: Travel To Country botimizga Xush kelibsiz!!!! ");

                    var buttonRows = new List<List<InlineKeyboardButton>>();

                    foreach (var item in hotels)
                    {
                        var callbackData = $"hotel_{item.HotelId}"; // Assuming each hotel has an ID

                        var button = InlineKeyboardButton.WithCallbackData(item.HotelName, callbackData);

                        // Add each button to a row
                        buttonRows.Add(new List<InlineKeyboardButton> { button });
                    }

                    var replyMarkup = new InlineKeyboardMarkup(buttonRows);
                    await botclient.SendTextMessageAsync(message.Chat.Id, "Choose an option:", replyMarkup: replyMarkup);
                }
                return;
            }
            else if(update.CallbackQuery != null)
            {
                
            }

        }
        public static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
