using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace ProjetoTelegram
{

    public abstract class Telegram
    {
        protected const string URLTelegram = @"https://api.telegram.org";
        protected string TokenId { get; set; }

        public string URLTelegramGetFile { get {
                return $"{URLTelegram}/bot{TokenId}/getFile";
            }
        }

        public string URLTelegramGetVoice { get {
                return $"{URLTelegram}/file/bot{TokenId}";
            }
        }

        protected TelegramBotClient TelegramClient { get; set; }

        public abstract string EfetuarOperacoesDeVoz(MessageEventArgs e);
        public abstract string EfetuarOperacoesDeTexto(MessageEventArgs e);
        private void OnMessage(object sender, MessageEventArgs e)
        {

			if (e.Message.Type == MessageType.Voice /* || e.Message.Type == MessageType.Audio*/)
			{
                this.EfetuarOperacoesDeVoz(e);				
			}
			if (e.Message.Type == MessageType.Text)
			{
                this.EfetuarOperacoesDeTexto(e);				
			}

		}


		public void IniciarTelegram() {
            this.TelegramClient = new TelegramBotClient(this.TokenId);

            TelegramClient.OnMessage += OnMessage;

            this.TelegramClient.StartReceiving();

            Thread.Sleep(Timeout.Infinite);
            TelegramClient.StopReceiving();

        }
        
    }
}
