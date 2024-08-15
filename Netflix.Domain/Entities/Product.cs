namespace Netflix.Domain;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public virtual Film? Film { get; set; }

    public virtual ICollection<OperationLog> OperationLogs { get; set; } = new List<OperationLog>();

    public virtual Series? Series { get; set; }
}
