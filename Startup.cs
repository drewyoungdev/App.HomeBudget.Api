using HouseBudgetApi.BusinessLogic;
using HouseBudgetApi.BusinessLogic.Interfaces;
using HouseBudgetApi.Configs;
using HouseBudgetApi.DatabaseContext;
using HouseBudgetApi.DatabaseContext.Interfaces;
using HouseBudgetApi.Repositories;
using HouseBudgetApi.Repositories.Interfaces;
using HouseBudgetApi.Services;
using HouseBudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseBudgetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // https://www.qappdesign.com/using-mongodb-with-net-core-webapi/
            // {
            //     "income": {
            //         "grossAnnualSalary": 70155.12,
            //         "estimatedMonthlyTaxes": 1242.06,
            //         "estimatedMonthlyDeductions": 186,
            //         "percentageTo401K": 15
            //     },
            //     "variableCosts": {
            //         {
            //            "Vehicle Payment": 348,
            //            "Vehicle Insurance": 154
            //         }
            //     }
            //     "loanSettings": {
            //         "termInMonths": 360,
            //         "rate": 4.5,
            //         "percentageForDownPayment": 20
            //     },
            //     "listings": [
            //         {
            //             "address": "131 S Dorchester Ave, Royal Oak, MI 48067",
            //             "price": 259000
            //         },
            //         {
            //             "address": "364 E Maplehurst St, Ferndale, MI 48220",
            //             "price": 289890
            //         }
            //     ]
            // }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAmortizationCalculator, AmortizationCalculator>();
            services.AddTransient<IHousingBudgetService, HousingBudgetService>();
            services.AddTransient<IListingsEvaluator, ListingsEvaluator>();
            services.AddTransient<IBudgetEvaluator, BudgetEvaluator>();

            services.AddTransient<IBudgetPreferencesRepository, BudgetPreferencesRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                });

            services.AddTransient<IHouseBudgetContext, HouseBudgetContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else
            // {
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
