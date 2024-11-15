using System;
using System.Threading.Tasks;
using System.Timers;

namespace Jetwin_Sales_and_Inventory.Utility_Class
{
    internal class DataRefreshManager : IDisposable
    {
        //NOTES FOR FUTURE SELF: IF CONCURRENT ACCESS BUG IS FOUND THEN USE SEMAPHORESLIM
        private readonly Timer refreshTimer;
        private DateTime lastCheckTime;

        public event Action OnDataUpdated;

        public DataRefreshManager(int intervalMilliseconds = 5000)
        {
            refreshTimer = new Timer(intervalMilliseconds);
            refreshTimer.Elapsed += CheckForUpdates;
            lastCheckTime = DateTime.MinValue;
        }

        public void Start() => refreshTimer.Start();

        public void Stop() => refreshTimer.Stop();

        private void CheckForUpdates(object sender, ElapsedEventArgs e)
        {
            //CHECK INVENTORY IF THERE IS ANY ADDED OR UPDATED PRODUCTS
            const string checkQuery = "SELECT MAX(LastUpdated) FROM Inventory";
            DateTime latestUpdate = (DateTime?)DatabaseHelper.ExecuteScalar(checkQuery, null) ?? DateTime.MinValue;

            //UPDATE WHEN THERE IS NEW/UPDATED PRODUCT
            if (latestUpdate > lastCheckTime)
            {
                lastCheckTime = latestUpdate;
                OnDataUpdated?.Invoke();
            }
        }

        public void Dispose()
        {
            Stop();
            refreshTimer.Dispose();
        }
    }
}
