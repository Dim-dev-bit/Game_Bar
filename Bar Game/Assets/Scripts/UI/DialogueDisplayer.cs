using BarGame.UI;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI {
    public class DialogueDisplayer : MonoBehaviour {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text dialogueText;
        public DialogueObject currentDialogue;
        public bool dialogueStarted = false;
        public bool dialogueFinished;

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
                dialogueText.text = dialogueObject.dialogueLines[i].dialogue;
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