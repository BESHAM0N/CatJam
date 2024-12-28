using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class CatView : MonoBehaviour
    {
        private Cat _cat;
        private Vector3? _targetPosition; 
        private float _hideTimer;       
        private bool _isHiding;
        
        private const float SPEED = 5f;
        private const float TARGET_DISTANCE  = 0.01f;
        private const float HIDE_DELAY = 0.5f;

        [Inject]
        public void Construct(Cat cat)
        {
            _cat = cat ?? throw new ArgumentNullException(nameof(cat));
        }

       private void Start()
        {
            InitializeCatEventHandlers();
            OnPositionChanged(_cat.Position);
        }

        private void OnDisable()
        {
            UnsubscribeCatEventHandlers();
        }

        private void Update()
        {
            HandleMovement();
            HandleHiding();
        }

        private void InitializeCatEventHandlers()
        {
            _cat.OnPositionChanged += OnPositionChanged;
            _cat.OnReachedEnd += StartHideCatSequence;
        }

        private void UnsubscribeCatEventHandlers()
        {
            _cat.OnPositionChanged -= OnPositionChanged;
            _cat.OnReachedEnd -= StartHideCatSequence;
        }

        private void HandleMovement()
        {
            if (_targetPosition.HasValue)
            {
                MoveTowardsTarget();
            }
        }

        private void HandleHiding()
        {
            if (!_isHiding) return;

            _hideTimer -= Time.deltaTime;
            if (_hideTimer <= 0f)
            {
                HideCat();
            }
        }

        private void OnPositionChanged(Vector2Int position)
        {
            _targetPosition = new Vector3(position.x, position.y, 0);
        }

        private void MoveTowardsTarget()
        {
            if (!_targetPosition.HasValue) return;

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition.Value, Time.deltaTime * SPEED);

            if (Vector3.Distance(transform.position, _targetPosition.Value) <= TARGET_DISTANCE)
            {
                CompleteMovement();
            }
        }

        private void CompleteMovement()
        {
            if (_targetPosition.HasValue)
            {
                transform.position = _targetPosition.Value;
                _targetPosition = null;
            }
        }

        private void StartHideCatSequence()
        {
            _hideTimer = HIDE_DELAY;
            _isHiding = true;
        }

        private void HideCat()
        {
            _isHiding = false;
            gameObject.SetActive(false);
        }
    }
}
