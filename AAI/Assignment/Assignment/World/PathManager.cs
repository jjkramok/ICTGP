using Assignment.Movement.Planning;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Assignment.World
{
    class PathManager
    {
        private static PathManager _instance = null;
        private HashSet<PathPlanner> SearchRequests;
        private HashSet<PathPlanner> CompletedRequests = new HashSet<PathPlanner>();
        private int NumSearchCyclesPerUpdate;
        private int MaxRequestsHandledPerUpdate = 5;

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

        public static void Delete()
        {
            _instance = null;
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

            int NoOfCyclesPerRequest = (int) Math.Max(Math.Floor((double) (NumSearchCyclesPerUpdate / Math.Min(SearchRequests.Count, MaxRequestsHandledPerUpdate))), 1d);

            int requestsHandled = 0;
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
                if (++requestsHandled > MaxRequestsHandledPerUpdate)
                {
                    break;
                }
            }

            // Remove all requests that are done.
            foreach (var completedRequest in CompletedRequests)
            {
                SearchRequests.Remove(completedRequest);
            }
            CompletedRequests.Clear();
        }

        private void Unregister(PathPlanner planner)
        {
            SearchRequests.Remove(planner);
        }

        public void RequestUnregister(PathPlanner planner)
        {
            CompletedRequests.Add(planner);
        }

        public void Render(Graphics g)
        {
            string reqInfo = "";
            if (SearchRequests.Count > 0)
            {
                reqInfo += String.Format("amount of requests / max handled: {0} : {1}\n", SearchRequests.Count, MaxRequestsHandledPerUpdate);
                reqInfo += String.Format("cycles per request: {0}\n", (int)Math.Max(Math.Floor((double)(NumSearchCyclesPerUpdate / Math.Min(SearchRequests.Count, MaxRequestsHandledPerUpdate))), 1d));
            }
            foreach (var request in SearchRequests)
            {
                reqInfo += String.Format("{0}\n", request.ToString());
            }
            g.DrawString(reqInfo, new Font(FontFamily.GenericSansSerif, 10), Brushes.Black, 50, 50);
        }
    }
}
