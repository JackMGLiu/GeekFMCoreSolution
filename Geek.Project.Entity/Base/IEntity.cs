namespace Geek.Project.Entity.Base
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
