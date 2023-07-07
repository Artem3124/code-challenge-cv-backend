namespace Shared.Core.Compilers
{
    public interface IInternalAssemblyPool
    {
        Guid Push(IInternalAssembly assembly);

        IInternalAssembly Pop(Guid uuid);
    }
}
