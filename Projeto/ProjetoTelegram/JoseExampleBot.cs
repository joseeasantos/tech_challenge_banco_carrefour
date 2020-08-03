using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace ProjetoTelegram
{
    public class JoseExampleBot : Telegram
	{
		public ProjetoDialogflow.Dialogflow df { get; set; }

		public JoseExampleBot(string tokenTelegram, ProjetoDialogflow.Dialogflow dialogflow)
		{
			this.TokenId = tokenTelegram;
			this.df = dialogflow;

		}


		public override string EfetuarOperacoesDeTexto(MessageEventArgs e)
		{
			if (e.Message?.Text != String.Empty)
			{				
				try
				{					
					string mensagemRetorno = this.df.IntentText(e.Message.Text, "pt-BR", e.Message.MessageId.ToString());
					Console.WriteLine("***");
					Console.WriteLine($"Mensagem recebida do telegram: {e.Message.Text}");
					Console.WriteLine($"Mensagem processada e enviada ao telegram: {mensagemRetorno}");
					Console.WriteLine("***");
					return mensagemRetorno;
				}
				catch 
				{
					throw;
				}

			}
			else 
			{
				return "";
			}

		}

		public override string EfetuarOperacoesDeVoz(MessageEventArgs e)
		{

			//string retorno = SpeechToText(TokenId, e.Message.Voice.FileId);
			//RetornoGetFileInstagram r = JsonConvert.DeserializeObject<RetornoGetFileInstagram>(retorno);
			//Console.WriteLine(JsonConvert.SerializeObject(r));
			Console.WriteLine($"Audio: {e.Message.Voice.ToString()}");
			return "";
		}

	}
}
