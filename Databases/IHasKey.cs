namespace Databases
{
    public interface IHasKey<T>
    {
        T Key { get; set; }
    }
}
