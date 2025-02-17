using Inv_back.Data;
using Microsoft.EntityFrameworkCore;

var builder =WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryContext>(options =>
//Has to match the one in app settings.json!!
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();
builder.Services.AddCors(
    options=>
    //which frontend can call backend
    options.AddPolicy("AllowAllOrigins",
    builder =>{
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    })
);
var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.UseDeveloperExceptionsPage();
}
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();