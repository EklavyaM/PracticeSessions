using System.Collections.Generic;
using UnityEngine;

namespace LineGeneration
{
    public class LineGenerator : MonoBehaviour
    {
        public enum Direction
        {
            Left, 
            Right, 
            Up
        }

        public Vector2 CurrentEndPoint
        {
            get
            {
                if (_lines.Count == 0)
                    return _origin;
                return _lines[_lines.Count - 1].EndPoint;
            }
        }
        public Direction CurrentDirection
        {
            get
            {
                return lastDirection;
            }
        }
        public float CurrentDistanceToCover
        {
            get
            {
                return lastDistanceToCover;
            }
        }

        [SerializeField] private Vector2 _origin;
        [SerializeField] [Range(0.01f, 10)] private float _drawSpeed;
        [SerializeField] [Range(0.01f, 10)] private float _width;
        [SerializeField] [Range(0.01f, 10)] private float minDistance;
        [SerializeField] [Range(0.01f, 10)] private float maxDistance;
        [SerializeField] private Material _sharedMaterial;

        [SerializeField] private LineGenerator _copyGenerator;
        [SerializeField] private bool _shouldCopy;


        private Direction lastDirection;
        private float lastDistanceToCover;
        private Transform _linesHolder;
        private List<Line> _lines = new List<Line>();

        private void Awake()
        {
            InitializeLinesHolder(out _linesHolder, "LinesHolder");
        }

        private void Update()
        {
            UpdateLineParameters();
            CreateNewLine();   
        }

        private void CreateNewLine()
        {
            if (_lines.Count != 0 && !_lines[_lines.Count - 1].IsFinished)
                return;

            Direction newDirection = _shouldCopy ? _copyGenerator.lastDirection : GetNewDirection(lastDirection);
            float distanceToCover = _shouldCopy ? _copyGenerator.lastDistanceToCover : Random.Range(minDistance, maxDistance);

            Vector2 newOrigin = _lines.Count == 0 ? _origin : _lines[_lines.Count-1].EndPoint;
            Vector2 newEndPoint = GetNewEndPoint(newOrigin, newDirection, distanceToCover);

            // Debug.Log("New Origin:  " + newOrigin + " New EndPoint " + newEndPoint);

            InitializeLine(_linesHolder, newOrigin, newEndPoint, _sharedMaterial, _width, _drawSpeed, _lines);

            lastDirection = newDirection;
            lastDistanceToCover = distanceToCover;
        }

        private Vector2 GetNewEndPoint(Vector2 origin, Direction direction, float distance)
        {
            Vector2 endPoint = origin;

            switch(direction)
            {
                case Direction.Left:
                    endPoint.x -= distance;
                    break;
                case Direction.Right:
                    endPoint.x += distance;
                    break;
                case Direction.Up:
                    endPoint.y += distance;
                    break;
            }

            return endPoint;
        }

        private Direction GetNewDirection(Direction lastDirection)
        {
            Direction newDirection;

            switch (lastDirection)
            {
                case Direction.Up:
                    newDirection = (Direction)Random.Range(0, 2);
                    break;
                default:
                    newDirection = Direction.Up;
                    break;
            }

            return newDirection;
        }

        private void UpdateLineParameters()
        {
            _lines.ForEach(line => { line.DrawSpeed = _drawSpeed; line.Width = _width; });
        }

        private void InitializeLinesHolder(out Transform holder, string name)
        {
            holder = new GameObject(name).transform;
            holder.parent = transform;
        }

        private void InitializeLine(Transform holder, Vector2 origin, Vector2 endPoint, 
                                    Material sharedMaterial, float width, float drawSpeed, 
                                    List<Line> lines)
        {
            GameObject lineObject = new GameObject();
            lineObject.transform.parent = holder;

            Line line = lineObject.AddComponent<Line>();
            line.Origin = origin;
            line.EndPoint = endPoint;
            line.SharedMaterial = sharedMaterial;
            line.Width = width;
            line.DrawSpeed = drawSpeed;

            lines.Add(line);

            lineObject.transform.name = "Line " + lines.Count;
        }
    }

}
