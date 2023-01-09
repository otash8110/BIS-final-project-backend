using FinalProject.API;

var builder = WebApplication.CreateBuilder(args);

var dependencyService = new DependencyInjection(builder);
dependencyService.AddServices();

var app = builder.Build();

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
