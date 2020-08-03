using System;
using System.Collections.Generic;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System.IO;


namespace consoleAppDialogFlow
{
	class Dialogflow
	{

		public Dialogflow()
		{
		}


		public QueryResult intentVoice(byte[] voice, string linguagem)
		{

			var credential = GoogleCredential.FromFile(@"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");
			//var storage = StorageClient.Create(credential);
			System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");
			SessionsClient sessionsClient = SessionsClient.Create();

			// Initialize request argument(s)
			/*queryInput:
						{
						audioConfig:
							{
							audioEncoding: encoding,
				  sampleRateHertz: sampleRateHertz,
				  languageCode: languageCode,
				},*/
			AudioEncoding audioEncoding = AudioEncoding.OggOpus;
			int sampleRateHertz = 16000;

			QueryInput query = new QueryInput
			{
				AudioConfig = new InputAudioConfig
				{
					AudioEncoding = AudioEncoding.OggOpus,
					SampleRateHertz = 16000,
					LanguageCode = "pt-BR"
				}
			};


			byte[] inputAudio = voice;

			byte[] encodedAudio;

			DetectIntentRequest request = new DetectIntentRequest
			{
				SessionAsSessionName = SessionName.FromProjectSession("dio-bot-fnss", "1234567891"),
				QueryParams = new QueryParameters(),
				QueryInput = query,
				OutputAudioConfig = new OutputAudioConfig(),
				InputAudio = ByteString.Empty,
				OutputAudioConfigMask = new FieldMask(),
			};

			// Make the request
			DetectIntentResponse response = sessionsClient.DetectIntent(request);

			return response.QueryResult;
		}


		public QueryResult intentText(string mensagem, string linguagem)
		{
			
			var credential = GoogleCredential.FromFile(@"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");
			//var storage = StorageClient.Create(credential);
			System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");
			SessionsClient sessionsClient = SessionsClient.Create();
			
			// Initialize request argument(s)
			QueryInput query = new QueryInput
			{
				Text = new TextInput
				{
					Text = mensagem,//"Something you want to ask a DF agent",
					LanguageCode = linguagem//"en-us"
				}
			};

			DetectIntentRequest request = new DetectIntentRequest
			{
				SessionAsSessionName = SessionName.FromProjectSession("dio-bot-fnss", "1234567891"),
				QueryParams = new QueryParameters(),
				QueryInput = query,
				OutputAudioConfig = new OutputAudioConfig(),
				InputAudio = ByteString.Empty,
				OutputAudioConfigMask = new FieldMask(),
			};
		
			// Make the request
			DetectIntentResponse response = sessionsClient.DetectIntent(request);

			return response.QueryResult;
		}

	}
}
