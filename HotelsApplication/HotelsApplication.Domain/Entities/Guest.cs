namespace HotelsApplication.Domain.Entities
{
    public class Guest : DomainEntity
    {
        public Guest(string guestId, string fullName, DateTime dateOfBirth, string gender,
                     string documentType, string documentNumber, string email, string phoneNumber)
        {
            GuestId = guestId;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string GuestId { get; init; }
        public string FullName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Gender { get; init; }
        public string DocumentType { get; init; }
        public string DocumentNumber { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string ReservationId { get; init; }
        public Reservation Reservation { get; init; }
    }
}
