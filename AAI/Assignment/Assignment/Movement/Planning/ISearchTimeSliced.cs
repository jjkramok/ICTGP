﻿using System.Collections.Generic;

namespace Assignment.Movement.Planning
{
    interface ISearchTimeSliced
    {
        /// <summary>
        /// Executes one cycle of the used pathfinding algorithm
        /// </summary>
        /// <returns></returns>
        SearchStatus CycleOnce();

        /// <summary>
        /// Returns all edges that have been examined already
        /// </summary>
        /// <returns></returns>
        List<World.Graph.Edge> GetSPT();

        double CostToTarget();

        /// <summary>
        /// Returns final path to the goal when <see="CycleOnce()"/> returns a success
        /// </summary>
        /// <returns></returns>
        List<World.Location> GetPath();
    }
}
