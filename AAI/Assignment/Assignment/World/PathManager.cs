using Assignment.Movement.Planning;
using System;
using System.Collections.Generic;

namespace Assignment.World
{
    class PathManager
    {
        private static PathManager _instance = null;
        private HashSet<PathPlanner> SearchRequests;
        private int NumSearchCyclesPerUpdate;

        public static PathManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PathManager();
                }
                return _instance;
            }
        }

        public PathManager()
        {
            SearchRequests = new HashSet<PathPlanner>();
            NumSearchCyclesPerUpdate = Settings.Instance.PathManagerCyclesPerUpdate;
        }

        public void RegisterSearch(PathPlanner request)
        {
            SearchRequests.Add(request);
        }

        public void UpdateSearches()
        {
            throw new NotImplementedException();
        }

        public void Unregister(PathPlanner planner)
        {
            SearchRequests.Remove(planner);
        }
    }
}
