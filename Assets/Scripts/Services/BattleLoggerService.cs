using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class BattleLoggerService : MonoBehaviour, ILoggerService
    {
        [SerializeField]
        private float charDelay = 0.05f;
        [SerializeField]
        private float linePause = 0.25f;
        private bool canLog;

        private TMP_Text guiText;
        private Queue<string> printingQueue;
        private List<string> currentLines;
        private Coroutine currentTyping;
        

        private void Start()
        {
            guiText = GetComponent<TMP_Text>();
            canLog = false;
            printingQueue = new Queue<string>();
            currentLines = new List<string>();
        }


        public IEnumerator ProcessQueue()
        {
            while(printingQueue.Count > 0)
            {
                string newMessage = printingQueue.Dequeue();
                currentLines.Add(newMessage + "\n");

                yield return StartCoroutine(Typewriter(newMessage));
                yield return new WaitForSeconds(linePause);
            }

            currentTyping = null;
        }

        public IEnumerator Typewriter(string message)
        {
            guiText.text += "\n"; // add a new line for each message

            foreach (char c in message)
            {
                guiText.text += c;
                guiText.ForceMeshUpdate();

                while (guiText.isTextOverflowing)
                {
                    string currentLogString = guiText.text;
                    string removeLog = currentLines[0];
                    currentLines.RemoveAt(0);

                    guiText.text = currentLogString.Substring(removeLog.Length);
                    guiText.ForceMeshUpdate();
                }

                yield return new WaitForSeconds(charDelay);
            }
        }

        public void EnableLogging() => canLog = true;

        public void Log(string message)
        {
            printingQueue.Enqueue(message);

            if(canLog && currentTyping == null)
            {
                currentTyping = StartCoroutine(ProcessQueue());
            }
        }

        public void DisableLogging() => canLog = false;
    }
}
