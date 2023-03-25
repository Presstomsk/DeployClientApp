namespace OrdersApiAppSPD011.Model.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        public int ClientId { get; set; }       
        
        public Client? Client { get; set; }
    }
}
