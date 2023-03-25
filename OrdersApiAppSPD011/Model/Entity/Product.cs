namespace OrdersApiAppSPD011.Model.Entity
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public long Article { get; set; }

        public double Price { get; set; }        
    }
}
