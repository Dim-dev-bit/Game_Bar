using BarGame.Player;
using System.Collections.Generic;
using UnityEngine;

namespace BarGame.Items {
    public class Shaker : MonoBehaviour {
        public Sprite newSprite;

        public List<string> ShakingActions;

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.ActionHandler.SetCurrentShaker(this);
            }
        }
        public void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagUtils.PlayerTagName))
            {
                PlayerCharacter player = other.GetComponent<PlayerCharacter>();
                if (player != null)
                    player.ActionHandler.SetCurrentShaker(null);
            }
        }

        public void AddToGlass(List<string> glassList)
        {
            if (ShakingActions.Count == 1)
            {
                ShakingActions.RemoveAt(0);
                return;
            }
            glassList.Add("shaking");
            foreach (string action in ShakingActions)
            {
                Debug.Log(action);
                glassList.Add(action);
            }

            ShakingActions.Clear();
        }

    }
}