namespace Shared.Core.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public string EntityUUID { get; init; }

        public string EntityName { get; init; }

        public ObjectNotFoundException(Guid entityUUID, string entityName) : this(entityUUID.ToString(), entityName)
        {

        }

        public ObjectNotFoundException(int entityId, string entityName) : this(entityId.ToString(), entityName)
        {

        }

        public ObjectNotFoundException(string entityId, string entityName)
            : base($"{entityName} with Id {entityId} was not found.")
        {
            EntityUUID = entityId;
            EntityName = entityName;
        }
    }
}
