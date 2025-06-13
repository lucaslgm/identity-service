namespace Identity.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes made in this context to the database as a single transaction.
        /// </summary>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}