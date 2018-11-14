namespace Geek.Project.Entity.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
