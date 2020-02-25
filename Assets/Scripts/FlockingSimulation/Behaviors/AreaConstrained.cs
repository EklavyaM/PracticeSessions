using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates movement for flocking agent constrained in a circular area.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/AreaConstrained", fileName = "Area Constrained Behavior")]
    public class AreaConstrained : FlockingBehavior
    {
        #region EXPOSED-PRIVATE-VARIABLES

        /// <summary>
        /// Center position for the area.
        /// </summary>
        [SerializeField] private Vector2 _areaCenter;

        /// <summary>
        /// Radius for the area.
        /// </summary>
        [SerializeField, Range(0.001f, 1000)] private float _areaRadius = 1f;

        #endregion

        #region PRIVATE-VARIABLES

        /// <summary>
        /// Used to cache vector from center to agent.
        /// </summary>
        private Vector2 _centerToAgent;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Constrained movement for flock agent by pulling towards center if agent
        /// goes beyond radius.
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            _centerToAgent = _areaCenter - flockAgent.Position;

            if (_centerToAgent.sqrMagnitude <= _areaRadius * _areaRadius)
                return Vector2.zero;

            return _centerToAgent * _areaRadius * _areaRadius;
        }

        #endregion
    }
}
