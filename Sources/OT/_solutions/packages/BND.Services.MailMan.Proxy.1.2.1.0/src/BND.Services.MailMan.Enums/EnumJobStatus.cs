namespace BND.Services.MailMan.Enums
{
    /// <summary>
    /// The enum job status.
    /// </summary>
    public enum EnumJobStatus
    {
        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The in que.
        /// </summary>
        InQue = 1,

        /// <summary>
        /// The task initial.
        /// </summary>
        TaskInitial = 100,

        /// <summary>
        /// The task created.
        /// </summary>
        TaskCreated = 110,

        /// <summary>
        /// The task parallel starting.
        /// </summary>
        TaskParallelStarting = 120,

        /// <summary>
        /// The task sync starting.
        /// </summary>
        TaskSyncStarting = 121,

        /// <summary>
        /// The task started.
        /// </summary>
        TaskStarted = 129,

        /// <summary>
        /// The task completed.
        /// </summary>
        TaskCompleted = 190,

        /// <summary>
        /// The done.
        /// </summary>
        Done = 800,

        /// <summary>
        /// The error.
        /// </summary>
        Error = 999
    }
}
