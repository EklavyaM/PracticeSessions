using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineGeneration
{
    public class LineFollower : MonoBehaviour
    {
        [SerializeField] LineGenerator lineGenerator;
        [SerializeField] [Range(0.01f, 100)] private float _moveSpeed;

        private void LateUpdate()
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, lineGenerator.CurrentEndPoint, _moveSpeed * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }
}
