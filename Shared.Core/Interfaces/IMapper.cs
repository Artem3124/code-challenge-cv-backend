namespace Shared.Core.Interfaces
{
    public interface IMapper<S, T>
    {
        public T Map(S entity);

        public List<T> Map(List<S> entity);
    }
}
