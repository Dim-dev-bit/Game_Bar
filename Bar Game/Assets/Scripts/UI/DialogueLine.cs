using UnityEngine;

namespace BarGame.UI {

    [System.Serializable] /* The reason this class is made serializable is so that it is visible later in the Unity Editor when we need to visualize and edit any objects of the DialogueLine class. */
    public class DialogueLine {
        [TextArea] public string dialogue;
    }
}