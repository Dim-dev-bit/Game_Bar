using BarGame;
using UnityEngine;

namespace BarGame {
    public class test : MonoBehaviour {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("EEE");
            }
        }
    }
}