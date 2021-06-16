using System.Collections.Generic;
using GraphiQl;
using GraphQL;
using GraphQL.Server.Ui.Altair;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TriviaGameRestEFAPI.BLL;
using TriviaGameRestEFAPI.GraphQL;
using TriviaGameRestEFAPI.GraphQL.Types;

namespace TriviaGameRestEFAPI
{
    public class Startup
    {
        //readonly List<string> _origins = new List<string>()
        //{
        //    "*"
        //};

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            TriviaGameRestEFAPIBLL.InitialiseConnectionString(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<DocumentExecuter>();
            //services.AddTransient<Schema>();

            //services.AddTransient<Query>();
            //services.AddTransient<Mutation>();
            //services.AddTransient<AnswerQuestionInputType>();
            //services.AddTransient<AnswerType>();
            //services.AddTransient<ChoiceType>();
            //services.AddTransient<GameResultType>();
            //services.AddTransient<GameType>();
            //services.AddTransient<GenreType>();
            //services.AddTransient<QuestionType>();

            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.WithOrigins(_origins.ToArray()).
            //        AllowAnyHeader().
            //        AllowAnyMethod();
            //        //WithMethods("PUT", "DELETE", "GET");
            //    });
            //});

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseCors();




            app.UseGraphQLAltair();
            app.UseGraphQLAltair(new AltairOptions()
            {
                //Path = new PathString("/myaltair"),
                GraphQLEndPoint = new PathString("/graphql")
            });

            //app.UseGraphiQl("/graphiql", "/graphql");
            //app.UseGraphiQLServer(new GraphiQLOptions
            //{
            //    Path = "/mygraphiql",
            //    GraphQLEndPoint = "/graphql"
            //});
            //app.UseGraphiQLServer();

            app.UseGraphQLPlayground(new PlaygroundOptions
            {
                //Path = new PathString("/myplayground"),
                GraphQLEndPoint = new PathString("/graphql")
            });
            //app.UseGraphQLPlayground();





            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
