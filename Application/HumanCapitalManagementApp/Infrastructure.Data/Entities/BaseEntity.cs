namespace Infrastructure.Data
{
    public abstract class BaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
