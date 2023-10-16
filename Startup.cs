using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLProject.Books;
using GraphQLProject.Data;
using GraphQLProject.DataLoader;
using GraphQLProject.Types;
using GraphQLProject.Authors;
using GraphQLProject.Publishers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GraphQLProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<ApplicationDbContext>(
                options => options.UseSqlite("Data Source=BookStore.db"));

            services
            .AddGraphQLServer()
            .AddQueryType(d => d.Name("Query"))
            .AddTypeExtension<PublisherQueries>()
             .AddTypeExtension<AuthorQueries>()
                .AddTypeExtension<BookQueries>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddTypeExtension<PublisherMutations>()
            .AddTypeExtension<AuthorMutations>()
                .AddTypeExtension<BookMutation>()
            .AddSubscriptionType<Subscription>()
            .AddType<AuthorType>()
            .AddType<BookType>()
            .AddType<PublisherType>()
                 .EnableRelaySupport()
           .AddFiltering()
           .AddSorting()
            .AddDataLoader<BookByIdDataLoader>()
            .AddDataLoader<AuthorByIdDataLoader>()
            .AddDataLoader<PublisherByIdDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

        }
    }
}