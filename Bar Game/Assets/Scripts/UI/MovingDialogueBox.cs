using System.Collections;
using UnityEngine;

namespace BarGame.UI {
    public class MovingDialogueBox : MonoBehaviour {

        private Transform _customer; // Ссылка на объект посетителя
        public Vector3 offset; // Смещение для позиционирования облака

        private RectTransform rectTransform;

        protected void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetCustomer(Transform customer)
        {
            _customer = customer;
        }

        protected void Update()
        {
            if (_customer != null)
            {
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(_customer.position);

                rectTransform.position = screenPosition + offset;
            }
        }


    }
}