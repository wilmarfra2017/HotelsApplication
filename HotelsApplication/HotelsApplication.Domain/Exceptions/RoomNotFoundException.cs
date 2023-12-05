namespace HotelsApplication.Domain.Exceptions
{
    public class RoomNotFoundException : Exception
    {
        public RoomNotFoundException(string message) : base(message)
        {
        }

    }
}
