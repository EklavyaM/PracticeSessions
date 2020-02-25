using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates steered movement for flocking agent for cohesion.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/Steered Cohesion", fileName = "Steered Cohesion Behavior")]
    public class SteeredCohesion : Cohesion
    {
        #region EXPOSED-VARIABLES

        [SerializeField, Range(0, 10)] private float _smoothTime = 1f;

        #endregion

        #region PRIVATE-VARIABLES

        /// <summary>
        /// Used to cache velocity for smooth dampening
        /// </summary>
        private Vector2 _currentVelocity;

        /// <summary>
        /// Used to cache movement calculated by base class.
        /// </summary>
        private Vector2 _cohesionMove;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Steered Cohesive movement for flock agent by averaging the distances between it.
        /// and its neighbors.
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            _cohesionMove = base.CalculateMove(flockAgent, context, contextCount, flock);
            _cohesionMove = Vector2.SmoothDamp(flockAgent.Up, _cohesionMove, ref _currentVelocity, _smoothTime);

            return _cohesionMove;
        }

        #endregion
    }
}
