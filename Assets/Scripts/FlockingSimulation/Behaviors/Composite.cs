using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Calculates movement by combining multiple behaviors.
    /// </summary>
    [CreateAssetMenu(menuName = "FlockingSimulation/Behaviors/Composite", fileName = "Composite Behavior")]
    public class Composite : FlockingBehavior
    {
        #region DATA-STRUCTURES

        [System.Serializable]
        private struct WeightedBehavior
        {
            public FlockingBehavior Behavior;

            [Range(0, 10)] public float Weight;
        }

        #endregion

        #region EXPOSED-PRIVATE-VARIABLES

        /// <summary>
        /// Behaviors with weight assigned to them.
        /// </summary>
        [SerializeField] private WeightedBehavior[] _weightedBehaviors;

        #endregion

        #region PRIVATE-VARIABLES

        private Vector2 _compositeMove;
        private Vector2 _partialMove;

        #endregion

        /// <summary>
        /// Calculates Movement by combining multiple behaviors.
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="contextCount"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public override Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock)
        {
            _compositeMove = Vector2.zero;

            for(int i =0; i<_weightedBehaviors.Length;++i)
            {
                _partialMove = _weightedBehaviors[i].Behavior.CalculateMove(flockAgent, context, contextCount, flock);
                
                if (_partialMove == Vector2.zero)
                    continue;

                _partialMove *= _weightedBehaviors[i].Weight;

                if(_partialMove.sqrMagnitude > _weightedBehaviors[i].Weight * _weightedBehaviors[i].Weight)
                {
                    _partialMove = _partialMove.normalized * _weightedBehaviors[i].Weight;
                }

                _compositeMove += _partialMove;
            }

            return _compositeMove;
        }
    }

}
