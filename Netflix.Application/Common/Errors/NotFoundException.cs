namespace Netflix.Application.Common.Errors
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName,string property, string key)
            : base($"{entityName} with {property} '{key}' was not found.")
        {
        }
    }
}
