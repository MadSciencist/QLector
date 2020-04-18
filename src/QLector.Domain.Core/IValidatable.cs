namespace QLector.Domain.Core
{
    /// <summary>
    /// Provides validation
    /// </summary>
    public interface IValidatable
    {
        /// <summary>
        /// Validate the object
        /// </summary>
        void Validate();
    }
}
