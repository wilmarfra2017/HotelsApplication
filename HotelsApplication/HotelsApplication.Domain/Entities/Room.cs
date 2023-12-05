using System.ComponentModel.DataAnnotations.Schema;

namespace HotelsApplication.Domain.Entities
{
    public class Room : DomainEntity
    {
        public Room(string roomCod, string type, double price, bool isAvailable, List<string> features, int capacity, Guid hotelId, double baseCost, double taxRate, string location)
        {
            RoomCod = roomCod;
            Type = type;
            Price = price;
            IsAvailable = isAvailable;
            Features = features;
            Capacity = capacity;
            HotelId = hotelId;
            BaseCost = baseCost;
            TaxRate = taxRate;
            Location = location;
        }
        
        public string RoomCod { get; private set; }
        public string Type { get; private set; }
        public double Price { get; private set; }
        public bool IsAvailable { get; private set; }
        public List<string> Features { get; private set; }
        public int Capacity { get; private set; }

        public Guid HotelId { get; private set; }

        public double BaseCost { get; private set; }
        public double TaxRate { get; private set; }
        public string Location { get; private set; }

        public virtual Hotel Hotel { get; private set; }


        public void UpdateRoom(string roomCod, string type, double price, bool isAvailable, List<string> features, int capacity, Guid hotelId, double baseCost, double taxRate, string location)
        {
            RoomCod = roomCod;
            Type = type;
            Price = price;
            IsAvailable = isAvailable;
            Features = features ?? new List<string>();
            Capacity = capacity;
            HotelId = hotelId;
            BaseCost = baseCost;
            TaxRate = taxRate;
            Location = location;
        }

        public void ChangeAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }


    }
}
