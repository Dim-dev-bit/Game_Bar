using Assets.Scripts.UI;
using UnityEngine;

namespace BarGame.NPS {
    [RequireComponent(typeof(PathFindingLogic), typeof(CustomerStateHandler))]

    public class CustomerCharacter : MonoBehaviour {
        private CustomerStateHandler _stateHandler;
        private DialogueDisplayer _dialogueDisplayer;


        protected void Awake()
        {
            _dialogueDisplayer = FindAnyObjectByType<DialogueDisplayer>();
            _stateHandler = GetComponent<CustomerStateHandler>();
        }

        protected void Update() { 
            _stateHandler.HandleState(_dialogueDisplayer);
        }
    }
}