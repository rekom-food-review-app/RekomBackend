using RekomBackend.Configuration;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

builder.Services.ConfigServiceCollection(builder.Configuration);

// if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();