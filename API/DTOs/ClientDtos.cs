namespace MinimalAPICrud;

public record ClientCreateDto(string Name, string PhoneNumber);
public record ClientUpdateDto(string Name, string PhoneNumber);
public record ClientResponseDto(int Id, string Name, string PhoneNumber);
