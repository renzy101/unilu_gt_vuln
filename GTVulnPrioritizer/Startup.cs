using GTVulnPrioritizer;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        //services.AddSingleton(new OpenAIClient(""));  // Replace with your OpenAI API key
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Vulnerability}/{action=Index}");
        });
      


    }

   /* public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }*/
}