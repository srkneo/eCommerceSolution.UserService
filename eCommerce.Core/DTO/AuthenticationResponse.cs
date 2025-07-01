namespace eCommerce.Core.DTO;
public record AuthenticationResponse
{
    public Guid UserID { get; init; }
    public string? Email { get; init; }
    public string? PersonName { get; init; }
    public string? Gender { get; init; }
    public string? Token { get; init; }
    public bool Success { get; init; }
}