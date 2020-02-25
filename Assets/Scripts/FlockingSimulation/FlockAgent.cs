using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlockingSimulation
{
    /// <summary>
    /// Default Flocking Agent
    /// </summary>
    public class FlockAgent : MonoBehaviour
    {
        #region EXPOSED-PRIVATE-VARIABLES
        /// <summary>
        /// Sprite Renderer assigned to the agent;
        /// </summary>
        [SerializeField] private SpriteRenderer _agentSprite;

        /// <summary>
        /// Collider assigned to the agent;
        /// </summary>
        [SerializeField] private Collider2D _agentCollider;

        #endregion

        #region PRIVATE-VARIABLES

        /// <summary>
        /// Cached transform for agent;
        /// </summary>
        private Transform _agentTransform;

        #endregion

        #region PUBLIC-PROPERTIES

        /// <summary>
        /// Sets color of the sprite;
        /// </summary>
        public Color AgentColor { set { _agentSprite.color = value; } }

        /// <summary>
        /// Gets collider assigned to the agent;
        /// </summary>
        public Collider2D AgentCollider { get { return _agentCollider; } }

        /// <summary>
        /// Gets world space position of agent.
        /// </summary>
        public Vector2 Position { get { return _agentTransform.position; } }

        /// <summary>
        /// Gets direction facing forward
        /// </summary>
        public Vector2 Up { get { return _agentTransform.up; } }

        /// <summary>
        /// Sets name of agent.
        /// </summary>
        public string Name { set { _agentTransform.name = value; } }

        #endregion

        #region MONOBEHAVIOR

        private void Awake()
        {
            _agentTransform = transform;
        }

        #endregion

        #region PUBLIC-METHODS
        
        /// <summary>
        /// Moves in direction of velocity.
        /// </summary>
        /// <param name="velocity"></param>
        public void MoveAgent(Vector2 velocity)
        {
            _agentTransform.up = velocity;
            _agentTransform.position += (Vector3)velocity * Time.deltaTime;
        }

        #endregion
    }
}
