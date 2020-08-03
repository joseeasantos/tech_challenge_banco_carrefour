using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDialogflow
{
    public class Dialogflow
    {

		private SessionsClient sessionClient;
		private string localChaveDeSeguranca;
		
		public string ProjectId { get; set; }


		public Dialogflow(string projectId, string localDaChaveDeSeguranca)
		{
			this.ProjectId = projectId;
			this.localChaveDeSeguranca = localDaChaveDeSeguranca;

			var credential = GoogleCredential.FromFile(this.localChaveDeSeguranca);
			//var storage = StorageClient.Create(credential);
			System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", this.localChaveDeSeguranca); 
		
			this.sessionClient = SessionsClient.Create();
		}



		public string IntentText(string mensagem, string linguagem, string sessionId)
		{			
			// Initialize request argument(s)
			QueryInput query = new QueryInput
			{
				Text = new TextInput
				{
					Text = mensagem,
					LanguageCode = linguagem
				}
			};

			DetectIntentRequest request = new DetectIntentRequest
			{
				SessionAsSessionName = SessionName.FromProjectSession(this.ProjectId, sessionId),
				//QueryParams = new QueryParameters(),
				QueryInput = query,
				//OutputAudioConfig = new OutputAudioConfig(),
				//InputAudio = ByteString.Empty,
				//OutputAudioConfigMask = new FieldMask(),
			};

			// Make the request
			DetectIntentResponse response = sessionClient.DetectIntent(request);

			return response.QueryResult?.FulfillmentText;
		}


		public QueryResult IntentVoice(ByteString voice, string linguagem, string sessionId)
		{
			QueryInput query = new QueryInput
			{

				AudioConfig = new InputAudioConfig
				{
					AudioEncoding = AudioEncoding.OggOpus,
					SampleRateHertz = 16000,
					LanguageCode = "pt-BR"
				}
			};


			InputAudioConfig inputAudioConfig = new InputAudioConfig
			{
				AudioEncoding = AudioEncoding.OggOpus,
				SampleRateHertz = 16000,
				LanguageCode = "pt-BR"
			};

			OutputAudioConfig outputAudioConfig = new OutputAudioConfig
			{
				AudioEncoding = OutputAudioEncoding.OggOpus,
				SampleRateHertz = 16000
			};

			DetectIntentRequest request = new DetectIntentRequest
			{
				SessionAsSessionName = SessionName.FromProjectSession(this.ProjectId, sessionId),
				QueryParams = new QueryParameters(),
				QueryInput = query,
				OutputAudioConfig = outputAudioConfig,
				InputAudio = voice,
				OutputAudioConfigMask = new FieldMask(),
			};

			// Make the request
			DetectIntentResponse response = sessionClient.DetectIntent(request);

			return response.QueryResult;
		}


	}
}
