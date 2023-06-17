namespace Domain.Entities.Base
{
    /// <summary>
    /// Represents an EntityBase in the system
    /// </summary>
    /// <typeparam name="IdType">Type of Id column of entity</typeparam>
    public abstract class BaseEntity<IdType>
    {
        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public IdType Id { get; set; } = default!;
    }
}