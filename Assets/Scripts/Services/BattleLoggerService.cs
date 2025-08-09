using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services
{
    [Serializable]
    public class BattleLoggerService : ILoggerService
    {
        [SerializeField]
        public TMP_Text loggingText;

        public void EnableLogging()
        {
            Debug.Log("Enabled Logging!");
        }

        public void Log(string message)
        {
            loggingText.text = message;
        }

        public void DisableLogging()
        {
            Debug.Log("Disable Logging!");
        }
    }
}
