namespace FinalProject.API
{
    public class DependencyInjection
    {
        private readonly WebApplicationBuilder builder;

        public DependencyInjection(WebApplicationBuilder builder)
        {
            this.builder = builder;
        }

        public void AddServices()
        {
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
