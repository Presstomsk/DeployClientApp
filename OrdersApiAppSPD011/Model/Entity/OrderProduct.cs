namespace OrdersApiAppSPD011.Model.Entity
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int OrderId { get; set; }

        public Order? Order { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
