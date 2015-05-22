namespace uPubDash.DependencyInjection
{
    public interface IIocContainer
    {
        T Get<T>();
    }
}