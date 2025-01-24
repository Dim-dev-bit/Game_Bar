using UnityEngine;
namespace BarGame.NPS {
    public class CustomerSpawner : MonoBehaviour {
        [SerializeField]
        private GameObject _nps;
        [SerializeField]
        private float _maxTimeInSecInterval = 2f;
        private float _timeInSecTillNextNPS;

        protected void Start()
        {
            _timeInSecTillNextNPS = _maxTimeInSecInterval;
        }
        protected void Update()
        {
            _timeInSecTillNextNPS -= Time.deltaTime;
            if (_timeInSecTillNextNPS <= 0)
            {
                Instantiate(_nps, transform);
                _timeInSecTillNextNPS = _maxTimeInSecInterval;
            }
        }
    }
}