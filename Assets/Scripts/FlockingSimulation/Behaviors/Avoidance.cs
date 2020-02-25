using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates movement for flocking agent for avoidance.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/Avoidance", fileName = "Avoidance Behavior")]
    public class Avoidance : FlockingBehavior
    {
        #region PRIVATE-VARIABLES

        /// <summary>
        /// Calculated avoidance move.
        /// </summary>
        private Vector2 _avoidanceMove;

        private Vector2 _agentToNeighbor;
        private int _neighborsAvoided;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Avoidance movement for flock agent by moving away from its neighbors' average positions
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            if (contextCount == 0)
                return Vector2.zero;

            _avoidanceMove = Vector2.zero;
            _neighborsAvoided = 0;

            for (int i = 0; i < contextCount; ++i)
            {
                _agentToNeighbor = (Vector2)context[i].position - flockAgent.Position;

                if (_agentToNeighbor.sqrMagnitude < 4 * flock.AgentsAvoidanceRadiusSquared)
                {
                    _avoidanceMove -= _agentToNeighbor;
                    _neighborsAvoided++;
                }
            }

            if(_neighborsAvoided > 0)
                _avoidanceMove /= _neighborsAvoided;

            return _avoidanceMove;
        }

        #endregion
    }
}
