namespace WebApi.Models.Common
{
    public sealed class ErrorDto
    {
        public string Message { get; set; } = default!;
        public string? Details { get; set; }
    }
}
