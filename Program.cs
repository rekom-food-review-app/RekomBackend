using RekomBackend.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigServiceCollection(builder.Configuration);

var app = builder.Build();

// if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
   try
   {
      await next(context);
   }
   catch (Exception e)
   {
      context.Response.StatusCode = 500;
   }
});

app.MapControllers();

app.Run();