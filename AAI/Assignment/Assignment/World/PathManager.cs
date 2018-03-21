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
            NumSearchCyclesPerUpdate = Settings.Instance.MaxPathfindingCyclesPerTick;
        }

        public void RegisterSearch(PathPlanner request)
        {
            SearchRequests.Add(request);
        }

        public void UpdateSearches()
        {
            if (SearchRequests.Count < 1)
            {
                return;
            }

            if (SearchRequests.Count == 1) {
                Math.Sin(0);
            }

            int NoOfCyclesPerRequest = (int) Math.Max(Math.Floor((double) (NumSearchCyclesPerUpdate / SearchRequests.Count)), 1d);

            HashSet<PathPlanner> CompletedRequests = new HashSet<PathPlanner>();
            foreach (var searchRequest in SearchRequests)
            {
                SearchStatus status = SearchStatus.NO_STATUS;
                for (int cycles = 0; cycles < NoOfCyclesPerRequest; cycles++)
                {
                    status = searchRequest.CycleOnce();
                    if (status == SearchStatus.TARGET_FOUND || status == SearchStatus.SEARCH_INCOMPLETED)
                    {
                        CompletedRequests.Add(searchRequest);
                    }
                }
            }

            // Remove all requests that are done.
            foreach (var completedRequest in CompletedRequests)
            {
                SearchRequests.Remove(completedRequest);
            }
        }

        public void Unregister(PathPlanner planner)
        {
            SearchRequests.Remove(planner);
        }
    }
}
