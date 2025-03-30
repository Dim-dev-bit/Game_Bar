using UnityEngine;

namespace BarGame.Player.Interactions {
    public class StateHandler : MonoBehaviour {
        public PickUpHandler PickUpHandler;

        public enum State {
            Basic,
            Filling,
            Shaking,
            Stirring
        }

        private State _currentState;
        protected void Awake()
        {
            PickUpHandler = GetComponent<PickUpHandler>();
            _currentState = State.Basic;
        }

        public void HandleState(ActionHandler actionHandler, PickUpHandler pickUpHandler)
        {
            switch (_currentState)
            {
                case State.Shaking:
                    actionHandler.PerformAction(Combinations.shakingSequence);
                    break;
                case State.Filling:
                    actionHandler.Filling();
                    break;
                case State.Stirring:
                    actionHandler.PerformAction(Combinations.stirringSequence);
                    break;
                default:
                case State.Basic:
                    pickUpHandler.Basic();
                    CheckCurrentState(PickUpHandler);
                    break;
            }
        }

        private void CheckCurrentState(PickUpHandler pickUpHandler)
        {
            bool IsHold = pickUpHandler.IsHold;
            var _nearObject = pickUpHandler.GetPickUp();
            var _pickUp = pickUpHandler.PickUp;
            if (_nearObject != null)
            {
                if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.SpoonTagName) && _nearObject.CompareTag(TagUtils.GlassTagName) && Input.GetKeyDown(KeyCode.F))
                    _currentState = State.Stirring;
                else if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.BottleTagName) && _nearObject.CompareTag(TagUtils.GlassTagName) && Input.GetKeyDown(KeyCode.H))
                    _currentState = State.Filling;
            }
            if (_currentState == State.Basic && IsHold && _pickUp.CompareTag(TagUtils.ShakerTagName) && Input.GetKeyDown(KeyCode.G))
                _currentState = State.Shaking;
        }

        public void SetState(State state)
        {
            _currentState = state;
        }
    }
}