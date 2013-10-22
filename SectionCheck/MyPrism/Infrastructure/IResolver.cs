
namespace MyPrism.Infrastructure
{
    public interface IResolver<out T>
    {
        T Resolve();
    }
}
