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

        private float _holdTime = 0.4f;
        private float _curTime = 0.0f;
        private bool _isHolding = false;

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
                    CheckCurrentState(PickUpHandler, actionHandler);
                    break;
            }
        }

        private void CheckCurrentState(PickUpHandler pickUpHandler, ActionHandler actionHandler)
        {
            bool IsHold = pickUpHandler.IsHold;
            var _nearObject = pickUpHandler.GetPickUp();
            var _pickUp = pickUpHandler.PickUp;
            if (_nearObject != null)
            {
                if (_currentState == State.Basic && IsHold && TagUtils.IsSpoon(_pickUp) && TagUtils.IsGlass(_nearObject) && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    actionHandler.playerInput.Add(KeyCode.LeftArrow);
                    _currentState = State.Stirring;
                }
                else if (_currentState == State.Basic && IsHold && TagUtils.IsShaker(_pickUp) && TagUtils.IsGlass(_nearObject) && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    CurrentFillingObject = TagUtils.GlassTagName;
                    _currentState = State.Filling;
                }
                else if (_currentState == State.Basic && IsHold && TagUtils.IsIngredient(_pickUp) && TagUtils.IsShaker(_nearObject) && Input.GetKeyDown(KeyCode.DownArrow))
                {
                    CurrentFillingObject = TagUtils.ShakerTagName;
                    _currentState = State.Filling;
                }
            }
            if (_currentState == State.Basic && IsHold && TagUtils.IsShaker(_pickUp))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (!_isHolding)
                    {
                        _isHolding = true;
                        _curTime = 0.0f;
                    }
                    else
                    {
                        _curTime += Time.deltaTime;
                        Debug.Log(_curTime);
                        if (_curTime >= _holdTime)
                        {
                            actionHandler.playerInput.Add(KeyCode.UpArrow);
                            _currentState = State.Shaking;
                            _isHolding = false;
                        }
                    }
                }
                else if (_isHolding)
                {
                    _isHolding = false;
                }
            }
        }

        public void SetState(State state)
        {
            _currentState = state;
        }

        public bool IsState(State state) => _currentState == state;
    }
}