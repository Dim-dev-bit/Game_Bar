using System.Collections;
using TMPro;
using UnityEngine;

namespace BarGame.UI {
    public class DialogueDisplayer : MonoBehaviour {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text dialogueText;
        public DialogueObject currentDialogue;
        public bool dialogueStarted = false;
        public bool dialogueFinished;
        public string orderPhrase;

       
        private int _numberOfChoices = 2;

        protected void Start()
        {
            dialogueBox.SetActive(false);
        }
        public void StartingDialogue()
        {
            dialogueFinished = false;
            dialogueStarted = true;
            dialogueBox.SetActive(true);
            DisplayDialogue(currentDialogue);
        }
        public void DisplayDialogue(DialogueObject dialogueObject)
        {
            StartCoroutine(MoveThroughDialogue(dialogueObject));
        }
        private IEnumerator MoveThroughDialogue(DialogueObject dialogueObject)
        {
            for (int i = 0; i < dialogueObject.dialogueLines.Length; i++)
            {
                if (i == 2) // This is the last line before variety of drinks  
                {
                    dialogueText.text = dialogueObject.dialogueLines[Random.Range(i, i + _numberOfChoices)].dialogue;
                    orderPhrase = dialogueText.text;
                    i += _numberOfChoices - 1;
                }
                else
                {
                    dialogueText.text = dialogueObject.dialogueLines[i].dialogue;
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow));

                yield return null;
            }

            dialogueStarted = false;
            dialogueFinished = true;
            dialogueBox.SetActive(false);
           

            yield return null;
        }
    }
}