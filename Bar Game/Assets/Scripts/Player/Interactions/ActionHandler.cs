using BarGame.Items;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BarGame.Player.Interactions {
    public class ActionHandler : MonoBehaviour {
        public bool canMove = true;
        public ProgressBarController progressBarController;

        public static event Action OnCompletingAction;

        private StateHandler _stateHandler;

        private Shaker shaker;
        private Glass glass;
        private float _timerInSec = 0f;
        private float _timerMax = 3f;
        private bool _isPouring = false;
        private List<KeyCode> playerInput = new List<KeyCode>();

        protected void Awake()
        {
            _stateHandler = GetComponent<StateHandler>();
        }

        public void Initialize(Shaker shaker, Glass glass)
        {
            this.shaker = shaker;
            this.glass = glass;
        }

        public void SetCurrentShaker(Shaker otherShaker)
        {
            shaker = otherShaker;
        }

        public void SetCurrentGlass(Glass otherGlass)
        {
            glass = otherGlass;
        }

        public void PerformAction(List<KeyCode> sequence)
        {
            canMove = false;
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        playerInput.Add(key);
                        Debug.Log($"Pressed: {key}");

                        if (playerInput.Count > sequence.Count)
                            playerInput.RemoveAt(0);

                        CheckSequence(sequence);
                    }
                }
            }
        }
        public void Filling()
        {
            if (shaker == null)
                _stateHandler.SetState(StateHandler.State.Basic);
            else if (Input.GetKey(KeyCode.H))
            {
                if (!_isPouring)
                {
                    _isPouring = true;
                    canMove = false;
  
                    _timerInSec = 0;
                    progressBarController.StartProgress(_timerMax);

                }
                _timerInSec += Time.deltaTime;
                if (_timerInSec >= _timerMax)
                {
                    if (gameObject.GetComponent<PlayerCharacter>().PickUpHandler.PickUp.tag != TagUtils.ShakerTagName)
                        gameObject.GetComponent<PlayerCharacter>().PickUpHandler.DestroyCurrentPickUp();
                    Debug.Log("Poured!");
                    _timerInSec = 0;
                    _isPouring = false;
                    canMove = true;

                    // Adding things to lists
                    if (_stateHandler.CurrentFillingObject == "Glass")
                        shaker.AddToGlass(glass.RecipeToMatch);
                    else if (_stateHandler.CurrentFillingObject == "Shaker")
                        shaker.ShakingActions.Add(_stateHandler.CurrentAction);
                    OnCompletingAction?.Invoke();

                    _stateHandler.SetState(StateHandler.State.Basic);
                }
            }
        }
        private void CheckSequence(List<KeyCode> sequence)
        {
            if (playerInput.Count == sequence.Count)
            {
                bool isMatch = true;
                for (int i = 0; i < playerInput.Count; i++)
                    if (playerInput[i] != sequence[i])
                    {
                        isMatch = false;
                        break;
                    }

                if (isMatch)
                {
                    if (shaker != null && _stateHandler.IsState(StateHandler.State.Shaking))
                    {
                        shaker.ChangeSprite();
                        shaker.ShakingActions.Add(_stateHandler.CurrentAction); // adding everything from shaker to the glass
                    }
                    if (glass != null && _stateHandler.IsState(StateHandler.State.Stirring))
                    {
                        glass.ChangeSprite();
                        // Adding action to the list of this particular glass
                        glass.RecipeToMatch.Add(_stateHandler.CurrentAction);
                    }

                    Debug.Log("Done!");

                    OnCompletingAction?.Invoke();

                    playerInput.Clear();
                    _stateHandler.SetState(StateHandler.State.Basic);
                    canMove = true;
                }
            }
        }
    }
}