using UnityEngine;
using UnityEngine.AI;
namespace BarGame.NPS {
    public class PathFindingLogic : MonoBehaviour {
        public bool IsStopped = false;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        public Transform _exit;


        private float _lookRadius = 100f;

        private float _currentDirection;

        private GameObject _target;
        private NavMeshAgent _agent;
        private Collider2D[] _colliders = new Collider2D[2];



        private float eps = 0.5f;


        protected void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            if (_spriteRenderer == null) _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void FindingSeat()
        {
            _currentDirection = _agent.velocity.x;
            if (_currentDirection > 0.1f)
                _spriteRenderer.flipX = false;
            else if (_currentDirection < -0.1f)
                _spriteRenderer.flipX = true;

            if (_target == null)
            {
                _target = GetTarget();
            }
            if (_target != null)
            {
                _agent.SetDestination(_target.transform.position);
                if (Vector2.Distance(_agent.transform.position, _target.transform.position) < eps)
                {
                    _spriteRenderer.flipX = true;
                    IsStopped = true;
                }
            }   
        }

        public void ExitingBar(bool matched)
        {
            if (_exit != null)
            {
                _currentDirection = _agent.velocity.x;
                if (_currentDirection > 0.1f)
                    _spriteRenderer.flipX = false;
                else if (_currentDirection < -0.1f)
                    _spriteRenderer.flipX = true;

                _agent.SetDestination(_exit.position);
                if (Vector2.Distance(_agent.transform.position, _exit.position) < eps)
                {
                    Destroy(gameObject);
                }
            }
        }


        private GameObject GetTarget()
        {
            GameObject target = null;
            var position = transform.position;
            var radius = _lookRadius / 3;
            var mask = LayerUtils.FreeChairLayer;
            var size = Physics2D.OverlapCircleNonAlloc(position, radius, _colliders, mask);
            for (int i = 1; i < 3; i++)
            {
                if (size > 0)
                    break;
                size = Physics2D.OverlapCircleNonAlloc(position, radius * (i + 1), _colliders, mask);

            }
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject && _colliders[i].gameObject.layer != LayerUtils.BusyChairLayerNum)
                    {
                        target = _colliders[i].gameObject;
                        target.layer = LayerUtils.BusyChairLayerNum;
                        break;
                    }
                }
            }

            return target;
        }
    }
}