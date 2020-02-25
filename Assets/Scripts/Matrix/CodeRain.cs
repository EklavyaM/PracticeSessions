using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

namespace Matrix
{
    public class CodeRain : MonoBehaviour
    {
        [SerializeField] private OffscreenLocationGenerator _locationGenerator;
        [SerializeField] private GameObject _codeDropPrefab;
        [SerializeField] private GameObject _voodooCharacterPrefab;

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private RectTransform _canvas;

        [SerializeField] private int _maxCharacterCount = 50;

        [SerializeField] private bool _biDirectional;

        private Transform _codeDropsHolder;
        private ObjectPool _voodooCharacterPool;

        private void Awake()    
        {
            _codeDropsHolder = new GameObject("Code Drops").transform;
            _codeDropsHolder.parent = transform;

            CodeDrop.Canvas = _canvas;

            // InitializeVoodooCharacterPool();
        }

        private void Start()
        {
          /*  if(_biDirectional)
                InitializeBidirectionalMovingSpawners(_locationGenerator.SpawnPositionsTop, _locationGenerator.SpawnPositionsBottom);
            else
                InitializeSingleDirectionMovingSpawners(_locationGenerator.SpawnPositionsTop);*/
        }
/*
        private void Update()
        {
            if (Input.GetButton("Fire1"))
                VoodooCharacter.ResetMidColor();
        }*/

        private void InitializeVoodooCharacterPool()
        {
            GameObject holder = new GameObject("Voodoo Character Holder");
            _voodooCharacterPool = holder.AddComponent<ObjectPool>();

            _voodooCharacterPool.InitializePool(_voodooCharacterPrefab, _maxCharacterCount, true, _canvas);
        }

        private void InitializeSingleDirectionMovingSpawners(List<Vector3> positions)
        {
            if (positions == null)
                return;

            CodeDrop.Canvas = _canvas;
            CodeDrop.VoodooPool = _voodooCharacterPool;

            foreach (Vector3 position in positions)
            {
                GameObject codeDropObject = Instantiate(_codeDropPrefab, position, Quaternion.identity, _codeDropsHolder);

                CodeDrop codeDrop = codeDropObject.GetComponent<CodeDrop>();

                codeDrop.Screen = _mainCamera;
                codeDrop.ScreenOffsets = Vector2.zero;
                codeDrop.Direction = Vector2.down;
                codeDrop.StartPosition = position;
                codeDrop.Speed = Random.Range(1.5f, 4);
                codeDrop.SpawnDelay = (_locationGenerator.LocationSpacing / codeDrop.Speed);
                codeDrop.CanMove = true;
                codeDrop.CanSpawn = true;
                codeDrop.MaxSpawnedVoodoos = 30;
            }
        }

        private void InitializeBidirectionalMovingSpawners(List<Vector3> topPositions, List<Vector3> bottomPositions)
        {
            if (topPositions == null || bottomPositions == null)
                return;

            if (topPositions.Count != bottomPositions.Count)
                return;


            CodeDrop.Canvas = _canvas;
            CodeDrop.VoodooPool = _voodooCharacterPool;

            for (int i=0; i<topPositions.Count;i++)
            {
                Vector2 direction = Random.Range(0, 2) < 1 ? Vector2.down : Vector2.up;
                Vector3 spawnPosition = direction == Vector2.down ? topPositions[i] : bottomPositions[i];

                GameObject codeDropObject = Instantiate(_codeDropPrefab, spawnPosition, Quaternion.identity, _codeDropsHolder);

                CodeDrop codeDrop = codeDropObject.GetComponent<CodeDrop>();

                codeDrop.Screen = _mainCamera;
                codeDrop.ScreenOffsets = Vector2.zero;
                codeDrop.Direction = direction;
                codeDrop.StartPosition = spawnPosition;
                codeDrop.Speed = Random.Range(2, 5);
                codeDrop.SpawnDelay = (_locationGenerator.LocationSpacing / codeDrop.Speed);
                codeDrop.CanMove = true;
                codeDrop.CanSpawn = true;
                codeDrop.MaxSpawnedVoodoos = 30;
            }
        }
    }
}
