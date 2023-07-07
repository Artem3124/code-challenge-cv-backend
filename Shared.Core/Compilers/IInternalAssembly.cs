namespace Shared.Core.Compilers
{
    public interface IInternalAssembly : IDisposable
    {
        int Execute();
    }
}
