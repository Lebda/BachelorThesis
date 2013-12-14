
namespace XEP_Prism.Infrastructure
{
    public interface XEP_IResolver<out T>
    {
        T Resolve();
    }
}
