using Ecommerce.Application.Models.Authorization;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ecommerce.Infrastructure.Persistence;

public class EcommerceDbContextData
{
    public static async Task LoadDataAsync(EcommerceDbContext context, UserManager<Usuario> usuarioManager, 
        RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Role.ADMIN));
                await roleManager.CreateAsync(new IdentityRole(Role.USER));
            }

            if (!usuarioManager.Users.Any())
            {
                var usuarioAdmin = new Usuario
                {
                    Nombre = "Jorge",
                    Apellido = "Medina",
                    Email = "medicode.developer@gmail.com",
                    UserName = "jorge.medina",
                    Telefono = "985644644",
                    AvatarUrl = "https://gravatar.com/medidrones",
                };

                await usuarioManager.CreateAsync(usuarioAdmin, "PasswordMedina123$");
                await usuarioManager.AddToRoleAsync(usuarioAdmin, Role.ADMIN);

                var usuario = new Usuario
                {
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "juan.perez@gmail.com",
                    UserName = "juan.perez",
                    Telefono = "98563434534",
                    AvatarUrl = "https://gravatar.com/medidrones",
                };

                await usuarioManager.CreateAsync(usuario, "PasswordJuanPerez123$");
                await usuarioManager.AddToRoleAsync(usuario, Role.USER);
            }

            if (!context.Categories!.Any())
            {
                var categoryData = File.ReadAllText("../Infrastructure/Data/category.json");
                var categories = JsonConvert.DeserializeObject<List<Category>>(categoryData);
                await context.Categories!.AddRangeAsync(categories!);
                await context.SaveChangesAsync();
            }

            if (!context.Products!.Any())
            {
                var productData = File.ReadAllText("../Infrastructure/Data/product.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                await context.Products!.AddRangeAsync(products!);
                await context.SaveChangesAsync();
            }

            if (!context.Images!.Any())
            {
                var imageData = File.ReadAllText("../Infrastructure/Data/image.json");
                var imagenes = JsonConvert.DeserializeObject<List<Image>>(imageData);
                await context.Images!.AddRangeAsync(imagenes!);
                await context.SaveChangesAsync();
            }

            if (!context.Reviews!.Any())
            {
                var reviewData = File.ReadAllText("../Infrastructure/Data/review.json");
                var reviews = JsonConvert.DeserializeObject<List<Review>>(reviewData);
                await context.Reviews!.AddRangeAsync(reviews!);
                await context.SaveChangesAsync();
            }

            if (!context.Countries!.Any())
            {
                var countryData = File.ReadAllText("../Infrastructure/Data/countries.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(countryData);
                await context.Countries!.AddRangeAsync(countries!);
                await context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            var logger = loggerFactory.CreateLogger<EcommerceDbContextData>();
            logger.LogError(e.Message);
        }
    }
}
