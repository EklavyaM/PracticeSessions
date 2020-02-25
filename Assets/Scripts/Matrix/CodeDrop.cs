using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Global;

namespace Matrix
{
    public class CodeDrop : MonoBehaviour
    {
        [SerializeField] private int _maxSpawnedVoodoos;

        [SerializeField] private Camera _screen;
        [SerializeField] private Vector2 _screenOffsets;
        [SerializeField] private Vector2 _startingPosition;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private bool _canMove;
        [SerializeField] private bool _canSpawn;

        [SerializeField] [Range(0.001f, 10)] private float _spawnDelay;
        [SerializeField] [Range(0.001f, 1000)] private float _speed;

        private float _timeCounter = 0;

        private RectTransform _rectTransform;

        public Camera Screen { set { _screen = value; } }
        public Vector2 ScreenOffsets { set { _screenOffsets = value; } }
        public Vector2 StartPosition { set { _startingPosition = value; } }
        public Vector2 Direction { set { _direction = value; OnValidate(); } }
        public float Speed { set { _speed = value; } get { return _speed; } }
        public float SpawnDelay { set { _spawnDelay = value; OnValidate(); } }
        public bool CanMove { get { return _canMove; } set { _canMove = value; } }
        public bool CanSpawn { get { return _canSpawn; } set { _canSpawn = value; } }
        public int MaxSpawnedVoodoos { set { _maxSpawnedVoodoos = value; } }

        public static RectTransform Canvas { get; set; }
        public static ObjectPool VoodooPool { get; set; }

        private void OnValidate()
        {
            _direction.x = Mathf.Clamp(_direction.x, -1, 1);
            _direction.y = Mathf.Clamp(_direction.y, -1, 1);

            _spawnDelay = Mathf.Clamp(_spawnDelay, 0.001f, 10);
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            _rectTransform.position = new Vector2(Canvas.rect.width/2, Canvas.rect.height);
            //Spawn();
        }

        private void Update()
        {
            _timeCounter += Time.deltaTime;

            Move();
            // CheckForScreen();

/*            if (SpawnTimerExpired())
                Spawn();*/
        }

        private void Move()
        {
            if (!_canMove)
                return;

            _rectTransform.Translate(_speed * _direction * Time.deltaTime);
        }

        private void CheckForScreen()
        {
            if (!Canvas.rect.Contains(_rectTransform.position))
                _rectTransform.position = _startingPosition;

           /* if (_screen == null || _screenOffsets == null)
                return;

            Vector3 bottomLeft = _screen.ViewportToWorldPoint(new Vector3(0, 0, _screen.nearClipPlane));
            Vector3 topRight = _screen.ViewportToWorldPoint(new Vector3(1, 1, _screen.nearClipPlane));

            bottomLeft.x -= _screenOffsets.x;
            bottomLeft.y -= _screenOffsets.y;
            topRight.x += _screenOffsets.x;
            topRight.y += _screenOffsets.y;

            if (transform.position.x > topRight.x || transform.position.y > topRight.y
                || transform.position.x < bottomLeft.x || transform.position.y < bottomLeft.y)
            {
                transform.position = _startingPosition;
            }*/
        }

        private bool SpawnTimerExpired()
        {
            if (!_canSpawn)
            {
                _timeCounter = 0;
                return false;
            }
        
            if (_timeCounter >= _spawnDelay)
            {
                _timeCounter = 0;
                return true;
            }
            return false;
        }

        private void Spawn()
        {
            GameObject[] voodooObjects;
            
            if(VoodooPool.RetrievePoolables(1, out voodooObjects))
            {
                IPoolable poolable = voodooObjects[0].GetComponent<IPoolable>();

                Vector2 canvasPos;
                Vector2 screenPoint = _screen.WorldToScreenPoint(transform.position);

                RectTransformUtility.ScreenPointToLocalPointInRectangle(Canvas, screenPoint, null, out canvasPos);

                poolable.SetOrientation(canvasPos, Vector3.zero, Vector3.one);
                poolable.SetAlive(true);
            }
        }
    }
}
