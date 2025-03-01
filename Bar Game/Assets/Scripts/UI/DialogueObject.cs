using UnityEngine;

namespace BarGame.UI {
    //The following line allows you to create a dialogue object from the Unity Editor itself
    [CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
    public class DialogueObject : ScriptableObject {
        public DialogueLine[] dialogueLines;
       
    }
}