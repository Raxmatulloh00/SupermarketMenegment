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
using System.Data;
using System.Data.SqlClient;

namespace Telgram
{
    class Program
    {

        //UpdatEror updatEror;
        
        static void Main(string[] args)
        {
            UpdatEror updatEror = new UpdatEror();

            Console.WriteLine("Hello World");

            updatEror.Work();
            Console.ReadKey();
        }

    }
}

#region

//InlineKeyboardButton succses = new InlineKeyboardButton("");
//InlineKeyboardButton errror = new InlineKeyboardButton("");
//succses.Text = "joylashuv tog'rimi";
//errror.Text = "Joylashuv notog'rimi";

#endregion