using UnityEngine;

namespace BarGame.Player.Interactions {
    public class StateHandler : MonoBehaviour {
        public PickUpHandler PickUpHandler;
        
        // What is player doing with the corresponding glass
        public string CurrentAction;
        public string CurrentFillingObject;

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
                    CurrentAction = "shaking";
                    actionHandler.PerformAction(Combinations.shakingSequence);
                    break;
                case State.Filling:
                    CurrentAction = PickUpHandler.PickUp.tag;
                    actionHandler.Filling();
                    break;
                case State.Stirring:
                    CurrentAction = "stirring";
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
                if (_currentState == State.Basic && IsHold && TagUtils.IsSpoon(_pickUp) && TagUtils.IsGlass(_nearObject) && Input.GetKeyDown(KeyCode.F))
                    _currentState = State.Stirring;
                else if (_currentState == State.Basic && IsHold && TagUtils.IsShaker(_pickUp) && TagUtils.IsGlass(_nearObject) && Input.GetKeyDown(KeyCode.H))
                {
                    CurrentFillingObject = TagUtils.GlassTagName;
                    _currentState = State.Filling;
                }
                else if (_currentState == State.Basic && IsHold && TagUtils.IsIngredient(_pickUp) && TagUtils.IsShaker(_nearObject) && Input.GetKeyDown(KeyCode.H))
                {
                    CurrentFillingObject = TagUtils.ShakerTagName;
                    _currentState = State.Filling;
                }
            }
            if (_currentState == State.Basic && IsHold && TagUtils.IsShaker(_pickUp) && Input.GetKeyDown(KeyCode.G))
                _currentState = State.Shaking;
        }

        public void SetState(State state)
        {
            _currentState = state;
        }

        public bool IsState(State state) => _currentState == state;
    }
}