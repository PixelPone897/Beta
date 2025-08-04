using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class BattleLogger : ILoggerService
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
