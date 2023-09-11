using System;
using UnityEngine;

namespace Common.Utils
{
    public class ConsoleOutDebugLogUtil
    {
        private readonly string _tag;

        public ConsoleOutDebugLogUtil(string tag)
        {
            _tag = tag;
        }

        public void LogError(string text)
        {
            Log(text, LogType.Error);
        }
        
        public void LogWarning(string text)
        {
            Log(text, LogType.Warning);
        }

        public void Log(string text, LogType logType = LogType.Log)
        {
            var finalText = $"{_tag} -> {text}";
            switch (logType)
            {
                case LogType.Log:
                    Debug.Log(finalText);
                    break;
                case LogType.Error:
                    Debug.LogError(finalText);
                    break;
                case LogType.Assert:
                    Debug.Log(finalText);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(finalText);
                    break;
                case LogType.Exception:
                    Debug.LogError(finalText);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(logType),
                        logType,
                        null);
            }
        }
    }
}