using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public interface IPoolable
    {
        bool IsAlive();
        void SetAlive(bool alive);
        void SetOrientation(Vector3 position, Vector3 rotation, Vector3 scale);
    }

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform _pooledObjectsHolder;
        [SerializeField] private GameObject _pooledObject;
        [SerializeField] private int _poolSize;

        [SerializeField] private bool _poolingUI;
        [SerializeField] private RectTransform _pooledUIHolder;

        private GameObject[] _pool;

        private void Awake()
        {
            if (_pooledObjectsHolder != null)
                return;

            _pooledObjectsHolder = new GameObject("Pooled Objects Holder").transform;
            _pooledObjectsHolder.parent = transform;
            _pooledObjectsHolder.localPosition = Vector3.zero;
        }

        private void Start()
        {
            if (_pooledObject == null)
                return;

            InitializePool(_pooledObject, _poolSize, _poolingUI, _pooledUIHolder);
        }

        private void ClearPool()
        {
            if (_pool == null)
                return;

            for (int i = _pool.Length - 1; i >= 0; i--)
            {
                _pool[i].GetComponent<IPoolable>().SetAlive(false);
                Destroy(_pool[i]);
            }
        }

        private void InstantiateObjects(int maxCount)
        {
            _pool = new GameObject[maxCount];
            _poolSize = maxCount;

            for (int i = 0; i < maxCount; i++)
            {
                GameObject obj = Instantiate(_pooledObject, _poolingUI ? _pooledUIHolder : _pooledObjectsHolder);

                IPoolable poolable = obj.GetComponent<IPoolable>();
                poolable.SetAlive(false);

                _pool[i] = obj;
            }
        }

        public bool RetrievePoolables(int count, out GameObject[] retrievedObjects)
        {
            if (_pool == null)
            {
                retrievedObjects = null;
                return false;
            }

            GameObject[] deadObjects = new GameObject[count];
            int retrievedCount = 0;

            for(int i=0; i<_pool.Length;i++)
            {
                if(!_pool[i].GetComponent<IPoolable>().IsAlive())
                {
                    deadObjects[retrievedCount++] = _pool[i];
                    if(retrievedCount == count)
                    {
                        retrievedObjects = deadObjects;
                        return true;
                    }
                }
            }

            retrievedObjects = null;
            return false;
        }

        public void InitializePool(GameObject obj, int maxCount, bool poolingUI = false, RectTransform pooledUIHolder = null)
        {
            if(obj == null)
            {
                throw new System.NullReferenceException("There ain't no object here to pool.");
            }

            if (obj.GetComponent<IPoolable>() == null)
            {
                throw new System.NullReferenceException("Implement the IPoolable interface ya bum.");
            }

            if (poolingUI && pooledUIHolder == null)
            {
                throw new System.NullReferenceException("Pass in a RectTransfrom to hold yer UI.");
            }

            if (maxCount < 0)
            {
                throw new System.ArithmeticException("Why the negetive maxCount?");
            }

            _pooledObject = obj;
            _poolingUI = poolingUI;
            _pooledUIHolder = pooledUIHolder;

            ClearPool();
            InstantiateObjects(maxCount);
        }
    }
}
