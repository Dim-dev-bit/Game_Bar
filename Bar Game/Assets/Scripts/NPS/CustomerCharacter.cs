using UnityEngine;

namespace BarGame.NPS {
    [RequireComponent(typeof(PathFindingLogic), typeof(CustomerStateHandler))]

    public class CustomerCharacter : MonoBehaviour {
        private CustomerStateHandler _stateHandler;
        public Transform dialogPosition;


        protected void Awake()
        {
            _stateHandler = GetComponent<CustomerStateHandler>();
        }

        protected void Update() { 
            _stateHandler.HandleState();
        }

    }
}