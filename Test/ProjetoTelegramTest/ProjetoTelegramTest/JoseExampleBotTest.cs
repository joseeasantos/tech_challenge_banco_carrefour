using System;
using Telegram.Bot.Args;
using Xunit;

namespace ProjetoTelegramTest
{
	public class JoseExampleBotTest
	{
		static string URLTelegram = @"https://api.telegram.org";
		static string TokenIdTelegramValido = "1364969608:AAF2KahU8xSsMcFvtQlhNgUIVv1OCXzkEtY";
		static string ProjectIdDialogflow = "dio-bot-fnss";
		static string LocalChaveDialogflow = @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json";
		

		public ProjetoDialogflow.Dialogflow instanciarDialogFlow() {
			return  new ProjetoDialogflow.Dialogflow(ProjectIdDialogflow, LocalChaveDialogflow);			
		}

		[Fact]
		public void URLTelegramGetFile()
		{
			string token = "123456";

			ProjetoDialogflow.Dialogflow df = new ProjetoDialogflow.Dialogflow(ProjectIdDialogflow, LocalChaveDialogflow);

			ProjetoTelegram.JoseExampleBot bot = new ProjetoTelegram.JoseExampleBot(token, df);

			//verificar se url de GetFile está correta	
			var url = $"{URLTelegram}/bot{token}/getFile";
			Assert.Contains(url, bot.URLTelegramGetFile );
		}
		
		[Fact]
		public void URLTelegramGetVoice()
		{
			string token = "123456";

			ProjetoTelegram.JoseExampleBot bot = new ProjetoTelegram.JoseExampleBot(token, instanciarDialogFlow());

			//verificar se url de GetVoice está correta
			Assert.Contains($"{URLTelegram}/file/bot{token}", bot.URLTelegramGetVoice);
		}

		[Fact]
		public void EfetuarOperacoesDeTexto()
		{
		}
	}
}
