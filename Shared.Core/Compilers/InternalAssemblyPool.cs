namespace Shared.Core.Compilers
{
    public class InternalAssemblyPool : IInternalAssemblyPool
    {
        private readonly Dictionary<Guid, IInternalAssembly> _assemblies = new();

        public IInternalAssembly Pop(Guid uuid)
        {
            if (!_assemblies.TryGetValue(uuid, out var assembly))
            {
                throw new ArgumentException($"Internal assembly {uuid} does not exists.");
            }

            _assemblies.Remove(uuid);

            return assembly;
        }

        public Guid Push(IInternalAssembly assembly)
        {
            var uuid = Guid.NewGuid();

            _assemblies.Add(uuid, assembly);

            return uuid;
        }
    }
}
