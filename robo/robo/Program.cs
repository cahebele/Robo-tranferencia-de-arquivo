using System;
using System.Configuration;
using System.IO;
using System.Threading;
using Robo.Domain.Utils;

namespace Robo
{
    public class Program
    {
        private void PrintRobotName()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("=== Robozinho Maroto ===");
            Console.WriteLine("=======================================");
        }
        private void Start()
        {
            try
            {
                PrintRobotName();
                Console.WriteLine("Robo iniciando ...");
                ScheduleLogging();
                Movelog();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                LogBasic.LogError(ex.Message, ex);
            }
            Console.WriteLine("Ciclo finalizado com sucesso.");
        }

        private void ScheduleLogging()
        {
            Timer timer = new Timer(LogMemory, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
        }

        private void LogMemory(object state)
        {
            DateTime now = DateTime.Now;
            string year = now.ToString("yyyy");
            string month = now.ToString("MM");
            string day = now.ToString("dd");
            string logFileName = $"{year}-{month}-{day}_Robozinho_Maroto.log";
            string logDirectory = Path.Combine(ConfigurationManager.AppSettings["LogDirectory"], year, month, day);
            string logFile = Path.Combine(logDirectory, logFileName);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            using (var logWriter = File.AppendText(logFile))
            {
                string timeExecucao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                logWriter.WriteLine($"[{timeExecucao}] Funcionouuuuuuu!!!");
            }
        }
        private void Movelog()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            string year = yesterday.ToString("yyyy");
            string month = yesterday.ToString("MM");
            string day = yesterday.ToString("dd");
            string sourceDirectory = Path.Combine(ConfigurationManager.AppSettings["LogDirectory"], year, month, day);
            string destinationDirectory = Path.Combine(ConfigurationManager.AppSettings["DestinationDirectory"], year, month, day);
            7
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            string logFileName = $"{year}-{month}-{day}_Robozinho_Maroto.log";
            string sourceFile = Path.Combine("C:/MeuLog", year, month, day, logFileName);
            string destinationFile = Path.Combine(destinationDirectory, logFileName);

            try
            {
                File.Copy(sourceFile, destinationFile);
                Console.WriteLine($"Arquivo copiado para {destinationFile}");
                File.Delete(sourceFile);
                Console.WriteLine($"Arquivo de origem {sourceFile} excluído");

                if (Directory.GetFiles(sourceDirectory).Length == 0)
                {
                    Directory.Delete(sourceDirectory);
                    Console.WriteLine($"Diretório de origem {sourceDirectory} excluído");
                }
                else
                {
                    Console.WriteLine($"O diretório de origem {sourceDirectory} não está vazio, não pode ser excluído");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro crítico na execução do robô!");
                LogBasic.LogError(ex.Message, ex);
            }
        }
        public static void Main()
        {
            try
            {
                var program = new Program();
                program.Start();
                Console.WriteLine("Pressione qualquer tecla para encerrar o programa...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                HandleGlobalException(ex);
            }
        }
        private static void HandleGlobalException(Exception ex)
        {
            // Lógica para lidar com exceções globalmente
        }
    }
}
