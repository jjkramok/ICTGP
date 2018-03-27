using Assignment.Entity;
using Assignment.World;
using System.Collections.Generic;

namespace Assignment.Movement.Planning
{
    class PathPlanner
    {
        private ISearchTimeSliced currentSearch;
        private Location Goal;
        private PathFollowingTimeSliced Reference;

        /// <summary>
        /// Enlists this PathPlanner to the PathManager that distributes resources for pathplanning.
        /// </summary>
        private void RequestSearch()
        {
            PathManager.Instance.RegisterSearch(this);
        }

        /// <summary>
        /// Initiates path planning for an agent.
        /// </summary>
        /// <param name="goal">The goal location the agent is trying to reach.</param>
        /// <returns>Succes</returns>
        public bool RequestPathToLocation(Location start, Location goal)
        {
            Goal = goal;
            Graph.Vertex startVertex = GameWorld.Instance.NavGraph.NearestVertexFromLocation(start);
            Graph.Vertex goalVertex = GameWorld.Instance.NavGraph.NearestVertexFromLocation(goal);
            currentSearch = new AStarTimeSliced(startVertex, goalVertex);
            RequestSearch();
            return true;
        }

        /// <summary>
        /// Performs one cycle, one iteration of the used ISearchTimeSliced Pathfinding algorithm.
        /// </summary>
        /// <returns>Status of the search</returns>
        public SearchStatus CycleOnce()
        {
            SearchStatus status = currentSearch.CycleOnce();

            // TODO by default return value will be ignored by the Manager, we have to inform the agent ourselves.
            switch (status)
            {
                case SearchStatus.TARGET_FOUND:
                    // Notify the behaviour that a path was found and ready to retrieve.
                    Reference.NotifyOfSearchStatus(status);
                    break;
                case SearchStatus.TARGET_NOT_FOUND:
                    break;
                case SearchStatus.SEARCH_INCOMPLETED:
                    // Notify the behaviour that no path was found and the search is discontinued.
                    Reference.NotifyOfSearchStatus(status);
                    break;
            }
            return status;
        }

        public List<Location> GetPath()
        {
            List<Location> pathWithoutEndLocation = currentSearch.GetPath();
            pathWithoutEndLocation.Add(Goal);
            return pathWithoutEndLocation;
        }

        public PathPlanner(PathFollowingTimeSliced reference)
        {
            Reference = reference;
        }
    }
}
