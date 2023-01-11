using FinalProject.Infrastructure;
using FinalProject.Application;
using FinalProject.Infrastructure.Context;
using FinalProject.API;

var builder = WebApplication.CreateBuilder(args);

//Add api services (extension)
builder.Services.AddAPIServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.SeedDatabase();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<SignalHub>("/demoHub");

app.Run();
