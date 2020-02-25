using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates steered movement for flocking agent for avoidance.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/Steered Avoidance", fileName = "Steered Avoidance Behavior")]
    public class SteeredAvoidance : Avoidance
    {
        #region EXPOSED-PRIVATE-VARIABLES

        [SerializeField, Range(0, 10)] private float _smoothTime = 1f;

        #endregion

        #region PRIVATE-VARIABLES

        /// <summary>
        /// Calculated avoidance move in base class.
        /// </summary>
        private Vector2 _avoidanceMove;

        /// <summary>
        /// Used to cache velocity for smooth dampening.
        /// </summary>
        private Vector2 _currentVelocity;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Steered Avoidance movement for flock agent by moving away from its neighbors' average positions.
        /// is facing.
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            _avoidanceMove = base.CalculateMove(flockAgent, context, contextCount, flock);
            _avoidanceMove = Vector2.SmoothDamp(flockAgent.Up, _avoidanceMove, ref _currentVelocity, _smoothTime);

            return _avoidanceMove;
        }

        #endregion
    }
}
