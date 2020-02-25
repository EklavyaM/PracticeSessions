using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matrix
{
    public class OffscreenLocationGenerator : MonoBehaviour
    {
        [SerializeField] private Camera _screen;
        [SerializeField] private float _startYOffset = -1f;
        [SerializeField] private float _locationSpacing = 1f;

        private List<Vector3> _spawnPositionsTop;
        private List<Vector3> _spawnPositionsBottom;

        public List<Vector3> SpawnPositionsTop { get { return _spawnPositionsTop; } }
        public List<Vector3> SpawnPositionsBottom { get { return _spawnPositionsBottom; } }
        public float LocationSpacing { get { return _locationSpacing; } }

        private void Awake()
        {
            GenerateLocations();
        }

        public void GenerateLocations()
        {
            if (_locationSpacing == 0 || _screen == null)
                return;

            _spawnPositionsTop = _spawnPositionsTop ?? new List<Vector3>();
            _spawnPositionsBottom = _spawnPositionsBottom ?? new List<Vector3>();
            _spawnPositionsTop.Clear();
            _spawnPositionsBottom.Clear();

            Vector3 topLeft = _screen.ViewportToWorldPoint(new Vector3(0, 1, _screen.nearClipPlane));
            Vector3 bottomLeft = _screen.ViewportToWorldPoint(new Vector3(0, 0, _screen.nearClipPlane));
            Vector3 topRight = _screen.ViewportToWorldPoint(new Vector3(1, 1, _screen.nearClipPlane));

            int locationsCount = Mathf.CeilToInt((topRight.x - topLeft.x) / _locationSpacing);

            for (int i = 0; i < locationsCount; i++)
            {
                Vector2 topPosition = new Vector2(topLeft.x + i * _locationSpacing, topLeft.y + _startYOffset);
                Vector2 bottomPosition = new Vector2(bottomLeft.x + i * _locationSpacing, bottomLeft.y - _startYOffset);
                _spawnPositionsTop.Add(topPosition);
                _spawnPositionsBottom.Add(bottomPosition);
            }
        }
    }
}

