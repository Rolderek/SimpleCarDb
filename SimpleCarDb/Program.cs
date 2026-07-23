using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleCarDb.Data;

var builder = WebApplication.CreateBuilder(args);

//ide jön az eslő kommentelt rész

builder.Services.AddControllers();

//swagger szűrő a megjelenítéshez:
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<SimpleCarDb.SwaggerFilters.BrandUpdateSchemaFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=.;Database=Automobilista;Trusted_Connection=True;TrustServerCertificate=True;"
        ));

var app = builder.Build();

//swagger beállítások:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleCarDb API");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();

/*
//ezzel lehet figyelmenkívül hagyni a körkörös hivatkozásokat és akkor nem kell a "[JsonIgnore]" sehová
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
*/

//adatbázis frissítés: --var app = builder.Build();-- sor után kell berakni
/*
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}
*/