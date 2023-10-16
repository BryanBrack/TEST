using API_TEST_NEORIS.Extensions;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Hosting.Server;
using TEST.CORE.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
c.OperationFilter<AddRequiredHeaderParameter>()
); 


builder.Services.AddMultiTenant<TenantInfo>()
    .WithHeaderStrategy("Client")
    .WithInMemoryStore(options =>
    {
        options.Tenants.Add(new TenantInfo { Id = "1", Identifier = "test_neoris", Name = "BDConnection1", ConnectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog = BD_TEST; Server = DESKTOP-3SRA8UR" });
    });

builder.Services.configureTrasient();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMultiTenant();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
