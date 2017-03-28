using System;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    /// <summary>
    /// enumerable UpdateState is contains the state of eu updating process.
    /// </summary>
    public enum UpdateState
    {
        /// <summary>
        /// Checkin the publication date in global xml and database
        /// </summary>
        Checking,
        /// <summary>
        /// Remove temporary data if they still there in database (status is Updating).
        /// </summary>
        RemoveTemporary,
        /// <summary>
        /// Download global file to memory.
        /// </summary>
        DownloadXml,
        /// <summary>
        /// Parsing golbal xml to entity
        /// </summary>
        Parsing,
        /// <summary>
        /// Add entity to database.
        /// </summary>
        AddSanctionList,
        /// <summary>
        /// Change current sanctions status from Active to Removed.
        /// </summary>
        ChangeActiveToRemove,
        /// <summary>
        /// Change new sanctions status from Updating to Active.
        /// </summary>
        ChangeUpdateToActive,
        /// <summary>
        /// Remove old sanctions that have the status is Removed.
        /// </summary>
        RemoveSanctionlist,
        /// <summary>
        /// Finished.
        /// </summary>
        Finished
    }

    /// <summary>
    /// The EuUpdateProgress class provies methods to manange lasted status of updating process of Eu sancations list.
    /// </summary>
    public class EuUpdateProgress
    {
        #region [Fields]
        /// <summary>
        /// Current progress in percent.
        /// </summary>
        private decimal _progress = 0;
        private UpdateState _currentState;
        private AutoUpdateStatus _status;

        #endregion

        #region [Constructor]
        public EuUpdateProgress()
        {
            _status = new AutoUpdateStatus();
        }
        #endregion

        #region [Public Methods]

        public void Update(UpdateState state, decimal progress = 1)
        {
            _progress = progress;
            _currentState = state;
            _status.Progress = CalculateProgress();
            if (state == UpdateState.RemoveTemporary)
            {
                _status.StartUpdate = DateTime.Now;
            }
            else if (state == UpdateState.Finished)
            {
                _status.FinishedUpdate = DateTime.Now;
            }
        }
        public AutoUpdateStatus GetStatus()
        {
            return _status;
        }
        public void SetStatus(string message, string status, int responseCode = 200)
        {
            _status.Message = message;
            _status.ResponseCode = responseCode;
            _status.Status = status;
        }

        #endregion

        #region [Private Method]

        /// <summary>
        /// This method will calculate the progress for the overview of process.
        /// we split the process in 9 step each step have the difference ratio.
        /// </summary>
        /// <returns>System.Decimal.</returns>
        private decimal CalculateProgress()
        {
            decimal progress = 1;

            if (_currentState == UpdateState.Checking)
            {
                progress = ((_progress * 5) / 100);
            }
            else if (_currentState == UpdateState.RemoveTemporary)
            {
                progress = ((_progress * 5) / 100) + 5;
            }
            else if (_currentState == UpdateState.DownloadXml)
            {
                progress = ((_progress * 10) / 100) + 15;
            }
            else if (_currentState == UpdateState.Parsing)
            {
                progress = ((_progress * 10) / 100) + 25;
            }
            else if (_currentState == UpdateState.AddSanctionList)
            {
                progress = ((_progress * 55) / 100) + 35;
            }
            else if (_currentState == UpdateState.ChangeActiveToRemove)
            {
                progress = ((_progress * 5) / 100) + 85;
            }
            else if (_currentState == UpdateState.ChangeUpdateToActive)
            {
                progress = ((_progress * 5) / 100) + 90;
            }
            else if (_currentState == UpdateState.RemoveSanctionlist)
            {
                progress = ((_progress * 5) / 100) + 95;
            }
            else
            {
                progress = 100;
            }
            return Math.Truncate(progress);
        }

        #endregion
    }
}