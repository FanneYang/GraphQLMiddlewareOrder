using System;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FacebookPost
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FacebookContext>();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }



        private static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<FacebookContext>();
                if (context.Database.EnsureCreated())
                {
                    var author = new User { UserId = 1, LastName = "Yang", FirstName = "Fan", JoinDate = new DateTime(2015, 11, 27) };

                    context.Posts.Add(new Post
                    {
                        PostId = 1,
                        AuthorId = 1,
                        Author = author,
                        Content = "This is my first post! I'm glad to use this as part of the Demo!",
                        PostTime = new DateTime(2020, 11, 11)
                    }); 

                    context.SaveChangesAsync();
                }
            }
        }
    }
}
