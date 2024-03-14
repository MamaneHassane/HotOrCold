namespace HotOrCold.Dtos.Definitions;

public record CustomerAuthenticationDto(
    string Username,
    string Password
);
public record CreateCustomerDto (
    // Pas besoin pour l'instant
);

public record ReadCustomerDto (
    // Pas besoin pour l'instant
);

public record UpdateCustomerDto (
    // Pas besoin pour l'instant
);