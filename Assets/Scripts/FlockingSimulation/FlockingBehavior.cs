using UnityEngine;
using System.Collections;

namespace FlockingSimulation
{
    /// <summary>
    /// Scriptable object used to hold flocking behaviors.
    /// </summary>
    public abstract class FlockingBehavior : ScriptableObject
    {
        /// <summary>
        /// Calculates movement for flocking agents. 
        /// </summary>
        /// <param name="flockAgent"></param>
        /// <param name="context"></param>
        /// <param name="flock"></param>
        /// <returns></returns>
        public abstract Vector2 CalculateMove(FlockAgent flockAgent, Transform[] context, int contextCount, Flock flock);
    }
}
