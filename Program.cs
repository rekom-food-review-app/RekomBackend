using Microsoft.AspNetCore.HttpOverrides;
using RekomBackend.App.Hubs.RekomerSideHubs;
using RekomBackend.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigServiceCollection(builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// app.Use(async (context, next) =>
// {
//    try
//    {
//       await next(context);
//    }
//    catch (Exception e)
//    {
//       context.Response.StatusCode = 500;
//    }
// });

app.MapControllers();

app.UseCors("CorsPolicy");

app.MapHub<RekomerCommentHub>("/rekomer-side/ws/comment-hub");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
   ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();