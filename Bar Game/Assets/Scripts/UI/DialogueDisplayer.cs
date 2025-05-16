using BarGame.NPS;
using BarGame.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BarGame.UI {
    public class DialogueDisplayer : MonoBehaviour {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private Image dialogueImage;
        public DialogueObject currentDialogue;
        public bool dialogueStarted = false;
        public bool dialogueFinished;
        public string orderPhrase;
        private PlayerCharacter _player;

        private int _greetingDialogueLength;
        private int _leavingDialogueLength = 2;

       
        private int _numberOfChoices = 2;

        protected void Start()
        {
            dialogueBox.SetActive(false);
            _greetingDialogueLength = currentDialogue.dialogueLines.Length - _leavingDialogueLength;
        }
        public void SetPlayer(PlayerCharacter player)
        {
            _player = player;
        }
        public void StartingDialogue()
        {
            dialogueFinished = false;
            dialogueStarted = true;
            DisplayDialogue(currentDialogue);
        }

        public void EndingPhrase(bool isMatched)
        {
            dialogueImage.sprite = isMatched ? currentDialogue.dialogueLines[_greetingDialogueLength + 1].image :
                 currentDialogue.dialogueLines[_greetingDialogueLength + 0].image;
        }
        public void DisplayDialogue(DialogueObject dialogueObject)
        {
            StartCoroutine(MoveThroughDialogue(dialogueObject));
        }
        private IEnumerator MoveThroughDialogue(DialogueObject dialogueObject)
        {
            dialogueImage.sprite = dialogueObject.dialogueLines[0].image;
            dialogueBox.SetActive(true);
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.UpArrow) && _player != null));
            _player.ActionHandler.canMove = !dialogueStarted;
            if (_player != null && Input.GetKeyDown(KeyCode.UpArrow))
            {
                int ind = Random.Range(2, 2 + _numberOfChoices); // One is the num of windows before choice of drinks
                for (int i = 1; i < _greetingDialogueLength; i++)
                {
                    if (i == 2) // This is the last line before variety of drinks  
                    {
                        dialogueImage.sprite = dialogueObject.dialogueLines[ind].image;
                        orderPhrase = dialogueObject.dialogueLines[ind].dialogue;
                        i += _numberOfChoices - 1;
                    }
                    else
                    {
                        // So that in the end the choice of a customer will be shown
                        if (i != _greetingDialogueLength - 1)
                            dialogueImage.sprite = dialogueObject.dialogueLines[i].image;
                        else
                            dialogueImage.sprite = dialogueObject.dialogueLines[ind].image;
                    }

                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow));
                    yield return null;
                }

                dialogueStarted = false;
                dialogueFinished = true;
            }

            _player.ActionHandler.canMove = true;
            yield return null;
        }
    }
}