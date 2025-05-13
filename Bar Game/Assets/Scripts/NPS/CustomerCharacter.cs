using UnityEngine;

namespace BarGame.NPS {
    [RequireComponent(typeof(PathFindingLogic), typeof(CustomerStateHandler))]

    public class CustomerCharacter : MonoBehaviour {
        public CustomerStateHandler StateHandler;
        public Transform dialogPosition;


        protected void Awake()
        {
            StateHandler = GetComponent<CustomerStateHandler>();
        }

        protected void Update() { 
            StateHandler.HandleState();
        }

    }
}