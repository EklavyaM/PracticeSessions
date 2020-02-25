using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Manages the entire flock including initialization and movement.
    /// </summary>
    public class Flock : MonoBehaviour
    {
        #region EXPOSED-PRIVATE-VARIABLES

        /// <summary>
        /// Flock agent prefab.
        /// </summary>
        [SerializeField] private FlockAgent _agentPrefab;

        /// <summary>
        /// Flocking Agent Behavior
        /// </summary>
        [SerializeField] private FlockingBehavior _agentBehavior;

        /// <summary>
        /// Number of agents instantiated on start.
        /// </summary>
        [SerializeField, Range(2, 10000)] private int _numberOfAgents = 100;

        /// <summary>
        /// Density of agents.
        /// </summary>
        [SerializeField, Range(0.001f, 1000)] private float _agentsDensity = 0.08f;

        /// <summary>
        /// Drive factor for the agent to move in the calculated direction.
        /// </summary>
        [SerializeField, Range(0, 1000)] private float _agentsAcceleration = 1f;

        /// <summary>
        /// Maximum velocity that can be achieved by an agent.
        /// </summary>
        [SerializeField, Range(0, 1000)] private float _agentsMaxVelocity = 10f;

        /// <summary>
        /// Radius in which neighors to an agent may be found.
        /// </summary>
        [SerializeField, Range(0, 100)] private float _agentsContextRadius = 2f;

        /// <summary>
        /// Radius in which agent must avoid the neighbors found.
        /// </summary>
        [SerializeField, Range(0, 100)] private float _agentsAvoidanceRadius = 0.1f;

        /// <summary>
        /// Maximum number of agents to consider as neighbors while calculating movement.
        /// </summary>
        [SerializeField, Range(0, 100)] private int _maxAgentsContextCount = 5;

        #endregion

        #region PRIVATE-VARIABLES

        /// <summary>
        /// Used to cache Flock Transform.
        /// </summary>
        private Transform _transform;

        /// <summary>
        /// Used to cache a flocking agent.
        /// </summary>
        private FlockAgent _tempFlockAgent;

        /// <summary>
        /// Used to cache position of a flocking agent.
        /// </summary>
        private Vector2 _tempAgentPosition;

        /// <summary>
        /// Used to cache rotation of a flocking agent.
        /// </summary>
        private Quaternion _tempAgentRotation;

        /// <summary>
        /// Used to cache movement of an agent calculated by Flocking Behavior
        /// </summary>
        private Vector2 _tempAgentMove;

        /// <summary>
        /// Used to cache neighboring colliders of an agent.
        /// </summary>
        private Collider2D[] _tempAgentContextColliders;

        /// <summary>
        /// Used to cache neighboring agents to an agent.
        /// </summary>
        private Transform[] _tempAgentContext;

        /// <summary>
        /// Used to cache number of neighbors retrieved.
        /// </summary>
        private int _tempAgentContextCount;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Used to cache square of agent avoidance radius.
        /// </summary>
        public float AgentsAvoidanceRadiusSquared { private set; get; }

        /// <summary>
        /// List of initialized agents.
        /// </summary>
        public FlockAgent[] Agents { private set; get; }

        /// <summary>
        /// Used to cache Inverse of agent density.
        /// </summary>
        public float AgentsDensityInverse { private set; get; }

        /// <summary>
        /// Used to cache square of agent max velocity.
        /// </summary>
        public float AgentsMaxVelocitySquared { private set; get; }

        #endregion

        #region MONOBEHAVIOR

        private void Start()
        {
            InstantiateCaches();
            InstantiateAgents();
        }

        private void Update()
        {
            MoveAgents();
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < _numberOfAgents; ++i)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(Agents[i].Position, _agentsContextRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(Agents[i].Position, _agentsAvoidanceRadius);
            }
        }

        #endregion

        #region PRIVATE-METHODS

        /// <summary>
        /// Instantiates all cache variables.
        /// </summary>
        private void InstantiateCaches()
        {
            _transform = transform;

            AgentsDensityInverse = (1.0f / _agentsDensity);

            AgentsMaxVelocitySquared = _agentsMaxVelocity * _agentsMaxVelocity;
            AgentsAvoidanceRadiusSquared = _agentsAvoidanceRadius * _agentsAvoidanceRadius;

            _tempAgentContextColliders = new Collider2D[_maxAgentsContextCount];
            _tempAgentContext = new Transform[_maxAgentsContextCount];
        }

        /// <summary>
        /// Instantiates all flocking agents and populates the agents list.
        /// </summary>
        private void InstantiateAgents()
        {
            Agents = new FlockAgent[_numberOfAgents];

            for(int i = 0; i<_numberOfAgents;++i)
            {
                _tempAgentPosition = Random.insideUnitSphere * _numberOfAgents * AgentsDensityInverse;
                _tempAgentRotation = Quaternion.Euler(transform.forward * Random.Range(0, 360));

                _tempFlockAgent = Instantiate(
                        _agentPrefab.gameObject,
                        _tempAgentPosition,
                        _tempAgentRotation,
                        _transform
                    ).GetComponent<FlockAgent>();

                _tempFlockAgent.Name = "Agent " + i;

                Agents[i] = _tempFlockAgent;
            }
        }

        /// <summary>
        /// Calculates movement vector of each agent and move it accordingly.
        /// </summary>
        private void MoveAgents()
        {
            for(int i=0; i<_numberOfAgents;++i)
            {
                _tempAgentContextCount = GetAgentContext(Agents[i]);

                _tempAgentMove = _agentBehavior.CalculateMove(Agents[i], _tempAgentContext, _tempAgentContextCount, this);
                _tempAgentMove *= _agentsAcceleration;

                if (_tempAgentMove.sqrMagnitude >= AgentsMaxVelocitySquared)
                    _tempAgentMove = _tempAgentMove.normalized * _agentsMaxVelocity;

                Agents[i].MoveAgent(_tempAgentMove);
            }
        }

        /// <summary>
        /// Retrieves all agents in the neighborhood of the agent and returns the count.
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        private int GetAgentContext(FlockAgent agent)
        {
            int contextCount = Physics2D.OverlapCircleNonAlloc(agent.Position, _agentsContextRadius, _tempAgentContextColliders);
            int fixedContextCount = 0;

            for(int i = 0; i< contextCount; ++i)
            {
                if (_tempAgentContextColliders[i] == agent.AgentCollider)
                    continue;

                _tempAgentContext[fixedContextCount++] = _tempAgentContextColliders[i].transform;
            }

            return fixedContextCount;
        }

        #endregion
    }
}
