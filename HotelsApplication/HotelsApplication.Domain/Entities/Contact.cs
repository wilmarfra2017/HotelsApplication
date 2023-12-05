namespace HotelsApplication.Domain.Entities
{
    public class Contact : DomainEntity
    {
        public Contact(string contactId, string fullName, string phoneNumber)
        {
            ContactId = contactId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }

        public string ContactId { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
    }
}
