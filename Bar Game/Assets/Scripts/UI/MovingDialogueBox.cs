using BarGame.NPS;
using UnityEngine;

namespace BarGame.UI {
    public class MovingDialogueBox : MonoBehaviour {

        private Transform _customer; // Ссылка на объект посетителя
        private Transform _child;

        private RectTransform rectTransform;

        protected void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetCustomer(Transform customer)
        {
            _customer = customer;
            _child = _customer.GetChild(0);
        }

        protected void Update()
        {
            if (_customer != null && _child != null)
                // This Camera.main.WorldToScreenPoint is necessary for converting coords in real world to canvas coords
                rectTransform.position = Camera.main.WorldToScreenPoint(_child.position);
        }
    }
}