namespace HotelsApplication.Domain.Dtos;

public record GuestDto(
    string FullName, 
    DateTime DateOfBirth,
    string Gender,
    string DocumentType,
    string DocumentNumber,
    string Email,
    string PhoneNumber
);

