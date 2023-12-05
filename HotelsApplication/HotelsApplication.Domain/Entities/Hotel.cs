namespace HotelsApplication.Domain.Entities
{
    public  class Hotel : DomainEntity
    {
        public Hotel(string hotelCod, string name, string address, int numberOfRooms, double rating, bool isAvailable) 
        {
            HotelCod = hotelCod;
            Name = name;
            Address = address;
            NumberOfRooms = numberOfRooms;
            Rating = rating;
            IsAvailable = isAvailable;
        }

        public string HotelCod { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int NumberOfRooms { get; private set; }
        public double Rating { get; private set; }
        public bool IsAvailable { get; private set; }

        public void UpdateHotel(string hotelCod, string name, string address, int numberOfRooms, double rating, bool isAvailable)
        {
            HotelCod = hotelCod;
            Name = name;
            Address = address;
            NumberOfRooms = numberOfRooms;
            Rating = rating;
            IsAvailable = isAvailable;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;            
        }

    }
}
