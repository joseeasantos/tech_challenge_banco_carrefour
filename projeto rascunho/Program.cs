using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Google.Cloud.Dialogflow.V2;
using Google.Cloud.Speech.V1;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace consoleAppDialogFlow
{

	class RetornoGetFileInstagram_Result
	{
		public string file_id { get; set; }
		public string file_unique_id { get; set; }
		public long file_size { get; set; }
		public string file_path { get; set; }
	}
   class RetornoGetFileInstagram
	{
		public string ok { get; set; }
		public RetornoGetFileInstagram_Result result { get; set; }		
	}



	class Program
	{
		private static string TokenId = "136496960***********tY";
		private static string URLTelegram = "https://api.telegram.org";
		private static string URLTelegramGetFile { get {
				return URLTelegram + "/bot" + TokenId + " / getFile";
			} }
		private static string URLTelegramGetVoice { get {
				return URLTelegram + "/file/bot" + TokenId;
			}
		}

		
		private static TelegramBotClient botClient =
					   new TelegramBotClient(TokenId);

		
		private static string GetAudioFileInstagram(string url)
		{
			return @"https://api.telegram.org/file/bot136496960***********tY/voice/file_0.oga";
		}
		private static string PostGetFileInstagram(string url, NameValueCollection parametros)
		{
			using (var client = new WebClient())
			{
				//var values = new NameValueCollection();
				//values["thing1"] = "hello";
				//values["thing2"] = "world";
				string responseString = "";
				try
				{
					client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

					var response = client.UploadValues(url, parametros);

					responseString = Encoding.Default.GetString(response);
				}
				catch
				{
					responseString = @"
						{
							""ok"": true,
							""result"": {
													""file_id"": ""AwACAgEAAxkBAAMQXyb_WnZoXb09ide8L9Mjk1X7v38AAsUAA2cYOEUUg4pNFqc51BoE"",
								""file_unique_id"": ""AgADwQADZxg4RQ"",
								""file_size"": 41236,
								""file_path"": ""voice/file_1.oga""

							}
						}";

				}
				return responseString;
				
			}
		}
		
		private static string SpeechToText(string tokenId, string fileId)
		{
			string postData = "file_id=" + Uri.EscapeDataString(fileId);

			NameValueCollection values = new NameValueCollection();
			values["file_id"] = fileId;

			string result = PostGetFileInstagram(URLTelegramGetFile, values);


			//string result = GetHttpClient("POST", URLTelegramGetFile , "application/x-www-form-urlencoded", postData);

			return result;
			//var objeto = JsonConvert.DeserializeObject(result);			
			//string voice_url = URLTelegramGetVoice + objeto.

		}

		private static void BotClient_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
		{
			if (e.Message.Type == MessageType.Voice || e.Message.Type == MessageType.Audio)
			{
				string retorno = SpeechToText(TokenId, e.Message.Voice.FileId);
				RetornoGetFileInstagram r = JsonConvert.DeserializeObject<RetornoGetFileInstagram>(retorno);
				Console.WriteLine(JsonConvert.SerializeObject(r));
				Console.WriteLine($"Audio: {e.Message.Voice.ToString()}");
			}
			if (e.Message.Type == MessageType.Text)
			{

				Console.WriteLine($"Mensagem: {e.Message.Text}");
				//Console.WriteLine(e.Message.Text);

				Dialogflow d = new Dialogflow();
				QueryResult retornoMensagem = d.intentText(e.Message.Text, "pt-br");


				if (e.Message.Text.ToUpper() == "OI Robot")
				{
					botClient.SendTextMessageAsync(
						e.Message.Chat.Id,
						$"Olá {e.Message.From.FirstName}, tudo bem! Seu ID é {e.Message.From.Id} "
						);

				}
				else
				{
					botClient.SendTextMessageAsync(
							e.Message.Chat.Id,
							retornoMensagem.FulfillmentText
							);

				}

			}

		}

		public void teste()
		{
			botClient.OnMessage += BotClient_OnMessage;


			botClient.StartReceiving();

			Thread.Sleep(Timeout.Infinite);
			botClient.StopReceiving();


		}
		static void Main(string[] args)
		{
			/*
			var speech = SpeechClient.Create();
			var config = new RecognitionConfig
			{
				Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
				SampleRateHertz = 16000,
				LanguageCode = LanguageCodes.English.UnitedStates
			};

			var audio = RecognitionAudio.FromStorageUri("gs://cloud-samples-tests/speech/brookly.flac");
			var response = speech.Recognize(config, audio);


			foreach (var result in response.Results)
			{
				foreach (var alternative in result.Alternatives)
				{
					Console.WriteLine(alternative.Transcript);
				}
			}
			*/
			botClient.OnMessage += BotClient_OnMessage;

			
			botClient.StartReceiving();
			
			Thread.Sleep(Timeout.Infinite);
			botClient.StopReceiving();
			
		}
	}
}
