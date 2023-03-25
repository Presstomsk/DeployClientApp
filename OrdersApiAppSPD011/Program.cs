
using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Data;
using OrdersApiAppSPD011.Model.Entity;
using OrdersApiAppSPD011.Service.ClientService;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IDao<Client>, DaoClient>();
builder.Services.AddTransient<IDao<Product>, DaoProduct>();
builder.Services.AddTransient<IDao<Order>, DaoOrder>();
builder.Services.AddTransient<IDao<OrderProduct>, DaoOrderProduct>();

var app = builder.Build();

app.MapGet("/ping", () => new { Time = DateTime.Now, Message = "pong" });


app.MapGet("/client/all", async (IDao<Client> daoClient) => await daoClient.GetAllAsync());

app.MapPost("/client/add", async (Client client, IDao<Client> daoClient) => await daoClient.AddAsync(client));

app.MapPost("/client/update", async (Client client, IDao<Client> daoClient) => await daoClient.UpdateAsync(client));

app.MapPost("/client/delete", async (DTO dto, IDao<Client> daoClient) => await daoClient.DeleteAsync(dto.Id));

app.MapGet("/client/get/{id:int}", async (int id, IDao<Client> daoClient) => await daoClient.GetAsync(id));



app.MapGet("/order/all", async (IDao<Order> daoOrder) => await daoOrder.GetAllAsync());

app.MapPost("/order/add", async (Order order, IDao<Order> daoOrder) => await daoOrder.AddAsync(order));

app.MapPost("/order/update", async (Order order, IDao<Order> daoOrder) => await daoOrder.UpdateAsync(order));

app.MapPost("/order/delete", async (DTO dto, IDao<Order> daoOrder) => await daoOrder.DeleteAsync(dto.Id));

app.MapGet("/order/get/{id:int}", async (int id, IDao<Order> daoOrder) => await daoOrder.GetAsync(id));


app.MapGet("/product/all", async (IDao<Product> daoProduct) => await daoProduct.GetAllAsync());

app.MapPost("/product/add", async (Product product, IDao<Product> daoProduct) => await daoProduct.AddAsync(product));

app.MapPost("/product/update", async (Product product, IDao<Product> daoProduct) => await daoProduct.UpdateAsync(product));

app.MapPost("/product/delete", async (DTO dto, IDao<Product> daoProduct) => await daoProduct.DeleteAsync(dto.Id));

app.MapGet("/product/get/{id:int}", async (int id, IDao<Product> daoProduct) => await daoProduct.GetAsync(id));



app.MapGet("/order_product/all", async (IDao<OrderProduct> daoOrderProduct) => await daoOrderProduct.GetAllAsync());

app.MapPost("/order_product/add", async (OrderProduct orderProduct, IDao<OrderProduct> daoOrderProduct) => 
                                                                    await daoOrderProduct.AddAsync(orderProduct));

app.MapPost("/order_product/update", async (OrderProduct orderProduct, IDao<OrderProduct> daoOrderProduct) => 
                                                                    await daoOrderProduct.UpdateAsync(orderProduct));

app.MapPost("/order_product/delete", async (DTO dto, IDao<OrderProduct> daoOrderProduct) => await daoOrderProduct.DeleteAsync(dto.Id));

app.MapGet("/order_product/get/{id:int}", async (int id, IDao<OrderProduct> daoOrderProduct) => await daoOrderProduct.GetAsync(id));


app.MapGet("/check/{id:int}", async (int id, IDao<OrderProduct> daoOrderProducts) =>
{
    var orderProducts = await daoOrderProducts.GetAllAsync();
    var needOrderProducts = orderProducts.Where(x => x.OrderId == id).Select(x =>
                                                new { x.Product?.Name, Check = x.Product?.Price * x.Count }).ToList();
    double price = default;
    List<string> names = new List<string>();
    needOrderProducts.ForEach(x =>
    {
        if (x.Name != null)
        { 
            names.Add(x.Name);
        }
        if (x.Check != null)
        {
            price += (double)x.Check;
        }
    });

    return new { Products = string.Join(",",names.Distinct()), Price = price };
});


app.MapGet("/info/{id:int}", async (int id, IDao<OrderProduct> daoOrderProducts) =>
{
    var orderProducts = await daoOrderProducts.GetAllAsync();
    var needOrderProducts = orderProducts.Where(x => x.OrderId == id).Select(x =>
                                                new { x.Product?.Name, x.Count }).ToList();  
    
    Dictionary<string,int> infos = new Dictionary<string, int>();

    needOrderProducts.ForEach(x =>
    {        
        if (x.Name != null && !infos.TryAdd(x.Name, x.Count))
        {
            infos[x.Name] += x.Count;
        }
    });

    List<string> answer = new List<string>();

    foreach (var item in infos)
    {
        answer.Add($"{item.Key} - {item.Value} רע.");
    }

    return new { OrderInfo = answer};
});

app.Run();