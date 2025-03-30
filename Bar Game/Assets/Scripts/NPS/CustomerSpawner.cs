using UnityEngine;
namespace BarGame.NPS {
    public class CustomerSpawner : MonoBehaviour {
        [SerializeField]
        private GameObject _nps;
        [SerializeField]
        private float _maxTimeInSecInterval = 2f;
        [SerializeField]
        private int _maxNumberOfCustomers = 8;
        private int _currentNumberOfCustomers = 0;
        private float _timeInSecTillNextNPS;

        protected void Start()
        {
            _timeInSecTillNextNPS = _maxTimeInSecInterval;
        }
        protected void Update()
        {
            if (_currentNumberOfCustomers < _maxNumberOfCustomers)
            {
                _timeInSecTillNextNPS -= Time.deltaTime;
                if (_timeInSecTillNextNPS <= 0)
                {
                    Instantiate(_nps, transform);
                    _currentNumberOfCustomers++;
                    _timeInSecTillNextNPS = _maxTimeInSecInterval;
                }
            }
        }
    }
}