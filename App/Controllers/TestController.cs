using Bogus;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using NpgsqlTypes;
using RekomBackend.App.Common.Enums;
using RekomBackend.App.Entities;
using RekomBackend.Database;

namespace RekomBackend.App.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
   private readonly RekomContext _context;
   private readonly HttpClient _httpClient;

   public TestController(RekomContext context, HttpClient httpClient)
   {
      _context = context;
      _httpClient = httpClient;
   }

   private async Task<string> GetRandomFoodPhotoUrlAsync(string key)
   {
      var response = await _httpClient.GetAsync($"https://api.unsplash.com/photos/random?query={key}&orientation=landscape&client_id=95aC00Gt2ddgOEb8GMLmZGYwetm9ZRwUyDJjWu0jurI");
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync();
      dynamic result = JsonConvert.DeserializeObject(json)!;
      return result.urls.regular;
   }
   
   [HttpPost("fake-restaurants")]
   // [Obsolete("Obsolete")]
   public async Task<IActionResult> Fake()
   {
      var faker = new Faker("en");
      
      for (var i = 0; i <= 1000; i++)
      {
         var account = new Account()
         {
            Username = faker.Internet.UserName(),
            Email = faker.Internet.Email(),
            PasswordHash = "123456",
            Role = Role.Restaurant,
            IsConfirmed = true
         };

         account.Restaurant = new Restaurant()
         {
            Name = faker.Company.CompanyName(),
            CoverImageUrl = faker.Image.PicsumUrl(),
            Address = faker.Address.FullAddress(),
            Location = new Point(faker.Address.Latitude(), faker.Address.Longitude()),
            Description = string.Join(" ", faker.Lorem.Words(30))
         };

         // account.Restaurant.FullTextSearch = NpgsqlTsVector.Parse($"{account.Restaurant.Description} {account.Restaurant.Address} {account.Restaurant.Name}");
         
         var foodList = new List<Food>();

         for (var j = 0; j < 2; j++)
         {
            var fod = new Food
            {
               Name = faker.Lorem.Letter(4),
               Price = (float)faker.Random.Decimal(1, 100),
               ImageUrl = faker.Image.PicsumUrl(),
               Description = string.Join(" ", faker.Lorem.Words(30))
            };
            
            // fod.FullTextSearch = NpgsqlTsVector.Parse($"{fod.Name} {fod.Description} {account.Restaurant.Name} {account.Restaurant.Description}");
            
            var imageList = new List<FoodImage>();
            for (var k = 0; k < 2; k++)
            {
               var img = new FoodImage { ImageUrl = faker.Image.PicsumUrl() };
               imageList.Add(img);
            }
            fod.Images = imageList;
            
            foodList.Add(fod);
         }

         account.Restaurant.Menu = foodList;
         
         _context.Accounts.Add(account);
         await _context.SaveChangesAsync();
      }
      
      return Ok();
   }
}