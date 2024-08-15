namespace Netflix.Application.Common.Errors
{
    public record ValidationError(string PropertyName, string ErrorMessage);
}
