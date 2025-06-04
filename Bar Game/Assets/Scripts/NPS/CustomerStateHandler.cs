using BarGame.Furniture;
using BarGame.Items;
using BarGame.Player;
using BarGame.ProgressBar;
using BarGame.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BarGame.NPS {
    public class CustomerStateHandler : MonoBehaviour {

        public enum State {
            Searching,
            Phrasing,
            OrderWaiting,
            Leaving
        }

        private PlayerCharacter _player;

        private State _startingState;
        private State _currentState;
        private bool _recipeMatched;

        private PathFindingLogic _pathFindingLogic;
        private DialogueDisplayer _dialogueDisplayer;
        private MovingDialogueBox _movingDialogueBox;
        private RecipesManager _recipesManager;
        private Table _table;

        private Rigidbody2D _rb;

        private List<string> _order;
        private char[] delimiters = { ' ', ',', '.', '!', '?', ':', ';' };
        private Dictionary<string, List<string>> _recipes;
        private GameObject _givenGlassObj;
        private Glass _givenGlass;

        public ProgressBarForNPS progressBarController;
        
        private float _timerMM = 20f;
        private float _timerOW = 40f;
        private float _timerInSec = 0f;

        public int score = 0;
        protected void Awake()
        {
            _startingState = State.Searching;
            _currentState = _startingState;

            _pathFindingLogic = GetComponent<PathFindingLogic>();
            _dialogueDisplayer = GetComponentInChildren<DialogueDisplayer>(true);
            _movingDialogueBox = GetComponentInChildren<MovingDialogueBox>(true);
            _recipesManager = new RecipesManager();
            _recipes = _recipesManager.Recipes;

            _rb = GetComponent<Rigidbody2D>();

            _rb.gravityScale = 0;
        }
        public void HandleState()
        {
            switch (_currentState)
            {
                case State.Phrasing:
                    Phrasing();
                    CheckCurrentState();
                    break;
                case State.OrderWaiting:

                    
                    OrderWaiting();
                    break;
                case State.Leaving:
                    _pathFindingLogic.ExitingBar(_recipeMatched);
                    break;
                default:
                case State.Searching:
                    _pathFindingLogic.FindingSeat();
                    CheckCurrentState();
                    break;
            }
        }

        public void SetCurrentTable(Table table)
        {
            _table = table;
        }

        private void CheckCurrentState()
        {
            if (_currentState == State.Searching && _pathFindingLogic.IsStopped)
            {
                _movingDialogueBox.SetCustomer(transform);
                progressBarController.StartProgress(_timerMM);
                _currentState = State.Phrasing;
                _rb.velocity = new Vector2(0f, 0f);
            }
            else if (_currentState == State.Phrasing && _dialogueDisplayer.dialogueFinished && _player != null) 
            {
                _timerInSec = 0f;
                progressBarController.Renewed(_timerOW);
                _currentState = State.OrderWaiting;
                _order = GetRecipe(_dialogueDisplayer.orderPhrase);

                _dialogueDisplayer.dialogueFinished = false;
            }
        }

        private void Phrasing()
        {
            _timerInSec += Time.deltaTime;

            if (_timerInSec >= _timerMM)
            {
                _timerInSec = 0f;
                _recipeMatched = false;
                _dialogueDisplayer.EndingPhrase(_recipeMatched);
                _currentState = State.Leaving;
            }

            _dialogueDisplayer.SetPlayer(_player);
            if (!_dialogueDisplayer.dialogueStarted && !_dialogueDisplayer.dialogueFinished)
            {
                _timerInSec = 0f;
                _dialogueDisplayer.StartingDialogue();
                _movingDialogueBox.SetCustomer(transform);
            }
        }

        private void OrderWaiting()
        {
            _timerInSec += Time.deltaTime;

            if (_timerInSec >= _timerOW)
            {
                _timerInSec = 0f;
                _recipeMatched = false;
                _dialogueDisplayer.EndingPhrase(_recipeMatched);
                _currentState = State.Leaving;
            }

            var mask = LayerUtils.PickUpLayer;
            var rayDistance = 3f;
            if (_table != null)
            {
                float direction = (_table.transform.position - transform.position).normalized.x;
                Vector2 horizontalDirection = new Vector2(direction, 0);
                RaycastHit2D hit = Physics2D.Raycast(
                    transform.position,
                    horizontalDirection,
                    rayDistance,
                    mask
                );
                if (hit.collider != null)
                {
                    _givenGlassObj = hit.collider.gameObject;
                    if (_givenGlassObj != null && TagUtils.IsGlass(_givenGlassObj))
                    {
                        _givenGlass = _givenGlassObj.GetComponent<Glass>();
                        _recipeMatched = CompareRecipes(_givenGlass.RecipeToMatch);
                        Debug.Log(_recipeMatched);
                        _givenGlass.RecipeToMatch.Clear();
                        _dialogueDisplayer.EndingPhrase(_recipeMatched);
                        _timerInSec = 0f;
                        score += 1;
                        _currentState = State.Leaving;
                    }
                }
            }
        }
        private List<string> GetRecipe(string inputString)
        {
            string[] words = inputString.Split(delimiters, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (Array.Find(_recipes.Keys.ToArray(), w => w.Equals(word, StringComparison.OrdinalIgnoreCase)) != null)
                    return _recipes[word];

            }
            return null;
        }

        private bool CompareRecipes(List<string> otherRecipe)
        {
            if (_order.Count == otherRecipe.Count)
            {
                for (int i = 0; i < _order.Count; i++)
                {
                    if (string.Compare(otherRecipe[i], _order[i], StringComparison.OrdinalIgnoreCase) != 0)
                        return false;
                }
                return true;
            }
            return false;   
        }
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TagUtils.PlayerTagName)
            {
                _player = collision.GetComponent<PlayerCharacter>();
            }
        }

        protected void OnTriggerExit2D(Collider2D collision)
        {
            _player = null;
        }
    }
}