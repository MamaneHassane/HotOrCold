using HotOrCold.Entities;

namespace HotOrCold.Security.Models;

public class TokenGenerationResponseForAdmin
{
    public Administrator? Administrator { get; set; }
    public string? AccessToken { get; set; }
}