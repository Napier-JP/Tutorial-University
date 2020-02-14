using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TutorialUniversity.Data;
using TutorialUniversity.Hubs;

namespace TutorialUniversity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // DIコンテナに登録する内容
        // AddTransient<インタフェースISomething, 具象クラスSomeImpl>とすることで、
        // コンストラクタの引数になっているISomething someに対してSomeImplのインスタンスがどこからともなく飛んでくる
        // 結果、コンストラクタの中でnew SomeImpl()しないのでSomeImpl()の実装ができていなくても単体でのテストができる
        // また、使いたい具象クラスの切り替えはここでやる
        // Models/DependencyInjectionSample/*を参照せよ
        //
        // AddDbContextは自動生成だとSqlServer　適宜修正
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            // AutoMapperの設定を参照させ、Mapperを一つだけ動かす
            services.AddAutoMapper(typeof(AutoMapperProfileConfiguration));
            // IMapperが必要とされたタイミングでシングルトン（アプリケーション全体で一つ不変のものでよいので）としてMapperクラスインスタンスを供給させる
            services.AddSingleton<IMapper, Mapper>();

            // SchoolContextという設定をSQliteとの対応関係として認識させ、Lazy(On-Demand) Loadingの対象とする
            // またSchoolContextが必要とされたタイミングでSchoolContextインスタンスを供給する
            services.AddDbContext<SchoolContext>(options => options.UseLazyLoadingProxies().UseSqlite(Configuration.GetConnectionString("SchoolDBContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ComputationHub>("/compute");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
