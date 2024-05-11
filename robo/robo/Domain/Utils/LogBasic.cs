using System;
using System.IO;

namespace Robo.Domain.Utils
{
    public static class LogBasic
    {
        private static readonly string LogDirectory = "C:/MeuLog";
        private static readonly string LogFileName = "PlataformaSolicita.TransferExe.log";

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogError(string message, Exception ex)
        {
            Log("ERROR", $"{message}: {ex}");
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        private static void Log(string level, string message)
        {
            try
            {
                string logFilePath = Path.Combine(LogDirectory, LogFileName);
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                using (StreamWriter writer = File.AppendText(logFilePath))
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception)
            {
                // Se houver algum erro ao gravar no log, ignoramos para evitar loops infinitos ou falhas adicionais.
            }
        }

        internal static void LogError(string v)
        {
            throw new NotImplementedException();
        }
    }
}
