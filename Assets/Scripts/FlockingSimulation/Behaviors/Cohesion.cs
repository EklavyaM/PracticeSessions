using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates movement for flocking agent for cohesion.
    /// </summary>
    [CreateAssetMenu(menuName ="FlockingSimulation/Behaviors/Cohesion", fileName = "Cohesion Behavior")]
    public class Cohesion : FlockingBehavior
    {
        #region PRIVATE-VARIABLES

        /// <summary>
        /// Calculated cohesion move.
        /// </summary>
        private Vector2 _cohesionMove;

        #endregion

        #region PUBLIC-METHODS

        /// <summary>
        /// Calculates Cohesive movement for flock agent by averaging the distances between it
        /// and its neighbors.
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

            _cohesionMove = Vector2.zero;

            for(int i = 0; i<contextCount;++i) 
                _cohesionMove += (Vector2) context[i].position;

            _cohesionMove /= contextCount;
            _cohesionMove -= flockAgent.Position;

            return _cohesionMove;
        }

        #endregion
    }
}
