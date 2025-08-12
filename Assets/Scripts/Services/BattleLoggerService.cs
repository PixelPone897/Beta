using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services
{
    [Serializable]
    public class BattleLoggerService : ILoggerService
    {
        [SerializeField]
        private TextMeshProUGUI loggingText;
        private BattleLogAutoFit autoFit;

        private bool canLog;

        public BattleLoggerService()
        {
            canLog = false;
        }

        public void EnableLogging()
        {
            Debug.Log("Enabled Logging!");
            canLog = true;
            autoFit = loggingText.GetComponent<BattleLogAutoFit>();
        }

        public void Log(string message)
        {
            if(canLog)
            {
                autoFit.AddLog(message);
            }
                
        }

        public void DisableLogging()
        {
            Debug.Log("Disable Logging!");
            canLog = false;
        }
    }
}
