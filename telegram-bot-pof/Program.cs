using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace telegrambotpof
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Run ().Wait ();
		}

		static async Task Run()
		{
			var Bot = new Api(ConfigurationManager.AppSettings ["telegramAccessToken"]);
			var me = await Bot.GetMe();
			Console.WriteLine("Hello my name is {0}", me.Username);
			var offset = 0;

			while (true)
			{
				var updates = await Bot.GetUpdates(offset);
				foreach (var update in updates)
				{
					if (update.Message.Type == MessageType.TextMessage)
					{
						await Bot.SendChatAction(update.Message.Chat.Id, ChatAction.Typing);
						await Task.Delay(2000);
						var t = await Bot.SendTextMessage(update.Message.Chat.Id, update.Message.Text);
						Console.WriteLine("Echo Message: {0}", update.Message.Text);
					}
						
					offset = update.Id + 1;
				}

				await Task.Delay(1000);
			}
		}
	}
}
