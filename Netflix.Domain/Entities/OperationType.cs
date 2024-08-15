namespace Netflix.Domain;

public partial class OperationType
{
    public Guid Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<OperationLog> OperationLogs { get; set; } = new List<OperationLog>();
}
