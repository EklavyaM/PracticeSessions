using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates movement for flocking agent for alignment.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/Alignment", fileName = "Alignment Behavior")]
    public class Alignment : FlockingBehavior
    {
        #region PRIVATE-VARIABLES

        /// <summary>
        /// Calculated alignment move.
        /// </summary>
        private Vector2 _alignmentMove;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Alignment movement for flock agent by averaging the forward direction of each neighbor.
        /// is facing.
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            if (contextCount == 0)
                return flockAgent.Up;

            _alignmentMove = Vector2.zero;

            for (int i = 0; i < contextCount; ++i)
                _alignmentMove += (Vector2) context[i].up;

            _alignmentMove /= contextCount;

            return _alignmentMove;
        }

        #endregion
    }
}
