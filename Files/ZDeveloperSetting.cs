using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using ZEROWORLD.Files.Interfaces;

namespace ZEROWORLD.Files
{
    class ZDeveloperSetting : ILoadBase, ILanguageBase
    {
        public enum MessageType
        {
            Info,
            Error,
            Warn
        }

        private static StreamWriter _logger;
        private static readonly string saveDirectoryName = "_ZEROWORLD";
        private static readonly string saveDirectoryPath = Path.Combine(Main.SavePath, saveDirectoryName);
        private static readonly string logFileName = "_ZWLog";
        private static readonly string logFileExtension = ".log";
        private static readonly string logFilePath = Path.Combine(saveDirectoryPath, logFileName, logFileExtension);

        public void LanguageLoad(List<(string, string, string[])> Texts)
        {
            Texts.AddRange(new (string, string, string[])[]
            {
                ("InfoMessage.Info", "Info", new string[]
                {
                    "[Info]",
                    "[信息]"
                }),
                ("InfoMessage.Error", "Error", new string[]
                {
                    "[Error]",
                    "[错误]"
                }),
                ("InfoMessage.Warn", "Warn", new string[]
                {
                    "[Warning]",
                    "[警告]"
                })
            });
        }

        public void Load()
        {
            new Action(delegate
            {
                Directory.CreateDirectory(saveDirectoryPath);
                File.Delete(logFilePath);
            }).TryAction();
            _logger = File.CreateText(logFilePath);
        }

        public void Unload()
        {
            _logger = null;
        }

        internal static void Write(string message, params object[] format)
        {
            using (_logger)
            {
                _logger.WriteLine(string.Format(message, format));
            }
        }

        internal static void Write(MessageType messageType, string message, params object[] format)
        {
            string preString;
            switch (messageType)
            {
                case MessageType.Info:
                    preString = "Info";
                    break;
                case MessageType.Error:
                    preString = "Error";
                    break;
                case MessageType.Warn:
                    preString = "Warn";
                    break;
                default:
                    throw new ArgumentException("messageType unsuccessfully format.");
            }
            using (_logger)
            {
                _logger.WriteLine($"{ZLanguage.Get(preString, true)}{string.Format(message, format)}");
            }
        }
    }
}