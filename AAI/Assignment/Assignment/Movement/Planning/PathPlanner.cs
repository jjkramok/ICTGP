using Assignment.Entity;
using Assignment.World;
using System.Collections.Generic;

namespace Assignment.Movement.Planning
{
    class PathPlanner
    {
        private readonly BaseEntity Entity;
        private ISearchTimeSliced currentSearch;

        public void RequestPath()
        {
            PathManager.Instance.RegisterSearch(this);
        }

        public bool RequestPathToLocation(Location goal)
        {
            return false; // TODO
        }

        public SearchStatus CycleOnce()
        {
            SearchStatus status = currentSearch.CycleOnce();
            switch (status)
            {
                case SearchStatus.TARGET_FOUND:
                    // TODO notify entity that the path has been found
                    break;
                case SearchStatus.TARGET_NOT_FOUND:
                    // TODO What would we like to do with this? Pass it to the entity?
                    break;
            }
            return status;
        }

        public List<Location> GetPath()
        {
            // TODO add goal to the path?
            return currentSearch.GetPath();
        }

        public PathPlanner(BaseEntity entity)
        {
            Entity = entity;
        }
    }
}
