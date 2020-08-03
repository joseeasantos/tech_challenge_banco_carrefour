using System;
using System.IO;

namespace ProjetoDeComunicacaoTelegram
{
    class Program
    {
        
        static string tokenIdTelegram = "1364969608:AAF2KahU8xSsMcFvtQlhNgUIVv1OCXzkEtY";
        static string projectIdDialogflow = "dio-bot-fnss";
        static string localChaveDialogflow = @"C:\key\key.json";
        //static string localChaveDialogflow = @"C:\Users\Jose\source\repos\consoleAppDialogFlow\consoleAppDialogFlow\key\key.json";
        static void Main(string[] args)
        {
            
            StreamReader streamReader = File.OpenText(localChaveDialogflow);
            string json = streamReader.ReadToEnd();

            ProjetoDialogflow.Dialogflow df = new ProjetoDialogflow.Dialogflow(projectIdDialogflow, localChaveDialogflow);

            ProjetoTelegram.JoseExampleBot bot = new ProjetoTelegram.JoseExampleBot(tokenIdTelegram, df);
            bot.IniciarTelegram();
        }
    }
}
