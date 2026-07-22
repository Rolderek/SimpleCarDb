using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SimpleCarDb.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleCarDb.SwaggerFilters
{
    public class BrandUpdateSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Megvizsgáljuk, hogy éppen a Brand modellre vonatkozik-e a generálás
            if (context.Type == typeof(Brand))
            {
                // Átírjuk a Swaggerben látható példa (Example) adatait a kívánt formátumra
                schema.Example = new OpenApiObject
                {
                    ["id"] = new OpenApiInteger(1),
                    ["name"] = new OpenApiString("Mitsubishi Motors")
                };

                // Ha a Cars mezőt teljesen el akarod tüntetni a példából vagy a sémából:
                if (schema.Properties.ContainsKey("cars"))
                {
                    schema.Properties.Remove("cars");
                }
            }
        }
    }
}