using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineGeneration
{
    public class Line : MonoBehaviour
    {
        public Vector2 Origin { set; get; }
        public Vector2 EndPoint { set; get; }
        public float DrawSpeed { set; get; }
        public bool IsFinished { set; get; }

        public Material SharedMaterial
        {
            set
            {
                _lineRenderer.sharedMaterial = value;
            }
            get
            {
                return _lineRenderer.sharedMaterial;
            }
        }
        public float Width
        {
            set
            {
                _lineRenderer.startWidth = _lineRenderer.endWidth = value;
            }
            get
            {
                return _lineRenderer.startWidth;
            }
        }

        public static float MIN_DISTANCE = 0.1f;

        private LineRenderer _lineRenderer;
        private Vector2 _currentEndPoint;

        private void Awake()
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
            _lineRenderer.enabled = false;
        }

        private void Start()
        {
            _lineRenderer.SetPosition(0, Origin);
            _currentEndPoint = Origin;
            IsFinished = false;
        }

        private void Update()
        {
            if (IsFinished)
                return;

            _lineRenderer.enabled = true;

            _currentEndPoint = Vector2.Lerp(_currentEndPoint, EndPoint, DrawSpeed * Time.deltaTime);
            _lineRenderer.SetPosition(1, _currentEndPoint);

            if (Vector2.Distance(_currentEndPoint, EndPoint) < MIN_DISTANCE)
                IsFinished = true;
        }
    }
}


