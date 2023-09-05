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
using UseCases.DataStore;
using DataStore.SQL;
using System.Data;
using System.Data.SqlClient;
using TelegramBot.Modal;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using System.Globalization;
using Telegram.Bot.Types.InputFiles;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System;
//using Telegram.Bot.Types.InputFiles;

namespace TelegramBot
{
    public class UpdatEror
    {
        public void Work()
        {

            var client = new TelegramBotClient("6506031107:AAGdDKfGIWgJRfrl5xJE4syJplTNlKEEQ_w");
            client.StartReceiving(Update, Error);

        }
        private static Message sms = new();
        private static BackBtnHelper backBtnHelper = new BackBtnHelper();
        private static Message[] photo;

        private static int PrevuisStepId = 1;
        public static async Task Update(ITelegramBotClient botclient, Update update, CancellationToken token)
        {
            //Message sms = new();
            SqlConnection sqlCon = new SqlConnection(@"Data Source=RAXMATULLOH;Initial Catalog=Travel;Integrated Security=True;");
            DataTable dtlb = new DataTable();
            string Sqlquery = string.Empty;
            string DisplayName = string.Empty;
            string CallBackColumn = string.Empty;
            string textMessage = string.Empty;
            var message = update.Message;
            var query = update.CallbackQuery;
            if (message?.Text != null)
            {
                await Console.Out.WriteLineAsync($"{message.Chat.Username} | {message.Text}");

                if (message.Text.ToLower().Contains("/start"))
                {

                    await botclient.SendTextMessageAsync(message.Chat.Id, $"Assalomu alaykum hurmatli foydalanuvchi: {message.Chat.FirstName}" + "\n" +
                        $"Bizning 🌍 @Travel_to_country_bot 🌎 ga" + "\n" + "\n" +
                        $"\t\t\t\t\t\t 👋 Xush kelibsiz 👋 ");

                    Sqlquery = $"Select * from country";
                    dtlb = ReturnTables(sqlCon, Sqlquery);
                    DisplayName = "CountryName";
                    CallBackColumn = "CountryId";
                    var inlineKeyboard = ButtonMaker(dtlb, UserStepNextStep.City, DisplayName, CallBackColumn, false);

                    //await botclient.EditMessageTextAsync(
                    //    chatId: query.From.Id,
                    //    messageId: ((int)query.From.Id),
                    //    text: "Choose an Country:",
                    //    replyMarkup: inlineKeyboard
                    //    );


                    sms = await botclient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Choose an Country:",
                        replyMarkup: inlineKeyboard
                    );
                }
                return;
            }
            else if (query?.Data != null)
            {
                ControlModal modal = new();
                modal = JsonConvert.DeserializeObject<ControlModal>(query.Data)!;
                int backSearchId = modal.step == UserStepNextStep.City ? backBtnHelper.SelectedCountryId : backBtnHelper.SelectedCityId;
                int searchId = modal.isBackBtnClicked ? backSearchId : int.Parse(modal.message);
                switch (modal.step)
                {

                    case UserStepNextStep.Country:
                        Sqlquery = $"Select * from country";
                        dtlb = ReturnTables(sqlCon, Sqlquery);
                        textMessage = dtlb?.Rows?.Count == 0 ? "‼️ Sorry we dont have countrys yet ‼️" : "Choose an Country:";
                        DisplayName = "CountryName";
                        CallBackColumn = "CountryId";
                        var inlineKeyboar = ButtonMaker(dtlb, UserStepNextStep.City, DisplayName, CallBackColumn, false);
                        if (sms.MessageId > 0)
                        {
                            sms = await botclient.EditMessageTextAsync(
                                chatId: query.From.Id,
                                messageId: sms.MessageId,
                                 text: textMessage,
                                 replyMarkup: inlineKeyboar
                                );
                        }

                        break;
                    case UserStepNextStep.City:
                        Sqlquery = $"Select * from Citiy where CountryConnectId={searchId}";
                        backBtnHelper.SelectedCountryId = searchId;
                        dtlb = ReturnTables(sqlCon, Sqlquery);
                        textMessage = dtlb?.Rows?.Count == 0 ? "‼️ Sorry we dont have citys in this country yet ‼️" : "Choose an City:";
                        DisplayName = "CityName";
                        CallBackColumn = "CityId";
                        var inlineKeyboard = ButtonMaker(dtlb, UserStepNextStep.HotelList, DisplayName, CallBackColumn,
                                                backBtnCallBackData: UserStepNextStep.Country);
                        if (sms.MessageId > 0)
                        {
                            sms = await botclient.EditMessageTextAsync(
                                chatId: query.From.Id,
                                messageId: sms.MessageId,
                                 text: textMessage,
                                 replyMarkup: inlineKeyboard
                                );
                        }

                        break;
                    case UserStepNextStep.HotelList:
                        if (photo != null && photo.Count() > 0)
                        {
                            foreach (var item in photo)
                            {
                                if (item.MessageId > 0)
                                {
                                    await botclient.DeleteMessageAsync(query.From.Id, item.MessageId);
                                }
                            }
                            photo = new Message[0];
                        }
                        Sqlquery = $"Select * from Hotel where CityConnctId={searchId}";
                        backBtnHelper.SelectedCityId = searchId;
                        dtlb = ReturnTables(sqlCon, Sqlquery);
                        textMessage = dtlb?.Rows?.Count == 0 ? "‼️ Sorry we dont have hotels in this city yet ‼️" : "Choose an hotel:";
                        DisplayName = "HotelName";
                        CallBackColumn = "HotelId";
                        var nlineKeyboard = ButtonMaker(dtlb, UserStepNextStep.HotelsDesc, DisplayName, CallBackColumn,
                            backBtnCallBackData: UserStepNextStep.City);
                        if (sms.MessageId > 0)
                        {
                            sms = await botclient.EditMessageTextAsync(
                                chatId: query.From.Id,
                                messageId: sms.MessageId,
                                 text: textMessage,
                                 replyMarkup: nlineKeyboard
                                );
                        }
                        else
                        {
                            sms = await botclient.SendTextMessageAsync(
                                chatId: query.From.Id,
                                 text: textMessage,
                                 replyMarkup: nlineKeyboard
                                );
                        }

                        //if (callbackQuery.Data == CallBackColumn)
                        //{
                        //    // Open Google webpage
                        //    var url = "https://www.google.com";
                        //    await botclient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                        //    await Task.Delay(1000); // Simulate typing
                        //    await botclient.SendTextMessageAsync(message.Chat.Id, $"Opening Google: {url}");
                        //}

                        Console.WriteLine((UserStepNextStep.HotelList));
                        break;

                    case UserStepNextStep.HotelsDesc:

                        Sqlquery = $"Select * from Hotel where HotelId={searchId}";
                        string imageQury = $"Select * from Image where HotelConnctId={searchId}";
                        var imgTbl = ReturnTables(sqlCon, imageQury);
                        PrevuisStepId = searchId;
                        dtlb = ReturnTables(sqlCon, Sqlquery);
                        var data = dtlb.Rows[0];
                        string? hotelStar = data["HotelStar"].ToString();
                        string stars = string.Empty;
                        int hotelStarCount = hotelStar == null ? 0 : int.Parse(hotelStar);
                        if (hotelStarCount > 0)
                        {
                            for (int i = 0; i < hotelStarCount; i++)
                            {
                                stars += "⭐️";
                            }
                        }
                        var backbtn = BackButtonMaker(DisplayName: " 🔙 Orqaga", UserStepNextStep.HotelList);
                        List<InputMediaPhoto> photos = new List<InputMediaPhoto>();
                        FileStream? photoStream = null;
                        InputMediaPhoto media = null;
                        try
                        {
                            if (sms.MessageId > 0)
                            {
                                await botclient.DeleteMessageAsync(query.From.Id, sms.MessageId);
                            }
                            foreach (DataRow row in imgTbl.Rows)
                            {
                                string photPath = row["ImageUrl"].ToString()!;
                                string hotelImg = $"C:\\Users\\raxma\\Csharp_Project\\Hotel_Web\\Web_Hotel\\Web_Hotel\\wwwroot{photPath}";
                                if (System.IO.File.Exists(hotelImg))
                                {
                                    photoStream = new FileStream(hotelImg, FileMode.Open, FileAccess.Read);
                                    string fileName = Path.GetFileName(hotelImg);
                                    media = new InputMediaPhoto(new InputMedia(photoStream, fileName));
                                    photos.Add(media);
                                }
                            }
                         
                        }

                        finally
                        {
                            photo = await botclient.SendMediaGroupAsync(
                                chatId: query.From.Id,
                                media: photos);
                            photoStream?.Close();
                            photoStream?.Dispose();
                            //if (sms.MessageId > 0)
                            //{
                            //}
                            var buttonRows = new List<List<InlineKeyboardButton>>();
                            ControlModal modadsadsal = new()
                            {
                                step = UserStepNextStep.HotelList,
                                isBackBtnClicked = true,
                            };
                            var callBackData = JsonConvert.SerializeObject(modadsadsal);
                            var button = InlineKeyboardButton.WithCallbackData("🔙 Orqaga", callBackData);
                            buttonRows.Add(new List<InlineKeyboardButton> { button });
                            var inlineKeyboardds = new InlineKeyboardMarkup(buttonRows);
                            sms = await botclient.SendTextMessageAsync(
                                chatId: query.From.Id,
                                text: $"Hotel Name:\t\t{data["HotelName"]}\n" +
                                 $"HotelPrice:\t\t${data["HotelPrice"]}\n" +
                                 $"Hotel Star:\t\t{data["HotelStar"]}{stars}\n" +
                                 $"Hotel desc:\t\t{data["HotelDescription"]}",
                                replyMarkup: inlineKeyboardds);
                        }
                        //List<string> imagePaths = new List<string>();

                        break;
                    default:
                        break;
                }
            }


        }

        private static DataTable ReturnTables(SqlConnection sql, string query)
        {

            SqlDataAdapter sqlda = new SqlDataAdapter(query, sql);
            DataTable dtlb = new DataTable();
            sqlda.Fill(dtlb);
            return dtlb;
        }



        private static InlineKeyboardButton BackButtonMaker(string DisplayName, UserStepNextStep CallBackColumn)
        {
            ControlModal modal = new()
            {
                step = CallBackColumn,
                isBackBtnClicked = true,
            };
            var callBackData = JsonConvert.SerializeObject(modal);
            var backbutton = InlineKeyboardButton.WithCallbackData(DisplayName, callBackData);
            return backbutton;
        }

        private static InlineKeyboardMarkup ButtonMaker(DataTable dtlb, UserStepNextStep nextStep, string DisplayName,
                            string CallBackColumn, bool needbackBtn = true, string backBtnDisplayName = " 🔙 Orqaga",
                            UserStepNextStep backBtnCallBackData = UserStepNextStep.Country)
        {
            var buttonRows = new List<List<InlineKeyboardButton>>();
            ControlModal modal = new ControlModal();
            foreach (DataRow row in dtlb.Rows)
            {
                modal = new()
                {
                    message = row[CallBackColumn].ToString()!,
                    step = nextStep
                };
                string CallData = JsonConvert.SerializeObject(modal);
                var button = InlineKeyboardButton.WithCallbackData(row[DisplayName].ToString()!, CallData);
                buttonRows.Add(new List<InlineKeyboardButton> { button });
            }
            if (needbackBtn)
            {
                var BackButton = BackButtonMaker(backBtnDisplayName, backBtnCallBackData);
                buttonRows.Add(new List<InlineKeyboardButton> { BackButton });
            }
            var inlineKeyboard = new InlineKeyboardMarkup(buttonRows);
            return inlineKeyboard;
        }

        public static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }

}
