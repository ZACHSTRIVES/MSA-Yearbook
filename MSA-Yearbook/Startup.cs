using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSAYearbook.Data;
using MSAYearbook.GraphQL.Comments;
using MSAYearbook.GraphQL.Projects;
using MSAYearbook.GraphQL.Students;

namespace MSA_Yearbook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                        .AddTypeExtension<ProjectQueries>()
                        .AddTypeExtension<StudentQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                        .AddTypeExtension<StudentMutations>()
                        .AddTypeExtension<ProjectMutations>()
                        .AddTypeExtension<CommentMutations>()
                .AddType<ProjectType>()
                .AddType<StudentType>()
                .AddType<CommentType>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGraphQL();
                });
        }
    }
}
