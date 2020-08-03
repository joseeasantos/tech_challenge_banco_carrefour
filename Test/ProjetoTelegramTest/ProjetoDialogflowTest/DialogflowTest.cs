using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;
using System;
using Xunit;

namespace ProjetoDialogflowTest
{
	public class DialogflowTest
	{
		[Fact]
		public void Dialogflow()
		{
			var projetoId = "dio-bot-fnss"; 
			ProjetoDialogflow.Dialogflow df = new ProjetoDialogflow.Dialogflow(projetoId, 
				                                                               @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");
			Assert.Equal(projetoId, df.ProjectId);
		}

		[Fact]
		public void IntentText()
		{
			var projetoId = "dio-bot-fnss";
			ProjetoDialogflow.Dialogflow df = new ProjetoDialogflow.Dialogflow(projetoId,
																			   @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json");

			string retorno = df.IntentText("oi", "pt-BR", "123");

			Assert.Equal("Ol�!", retorno);
		}


		[Fact]
		public void IntentVoice()
		{
			//n�o terminado devido a complica��es na chamada de eventos
		}
	}
}
