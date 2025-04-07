using AuthWebServer.Config;
using AuthWebServer.Config.Extensions;
using AuthWebServer.Config.Filter;
using AuthWebServer.Config.Options;
using ClassLibrary;
using ClassLibrary.Services;
using Microsoft.EntityFrameworkCore;
using Model;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers(options => {
    options.Filters.Add<GlobalExceptionFilter>();
})
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
    });

builder.Services.AddAutoMapper(typeof(AuthWebServer.MapProfile));
// 配置数据库链接字符串
builder.Services.AddDbContext<MyDbContext>(options => {
    options.UseSqlite("Data Source=mydb.db");
});

builder.Services.AddMemoryCache();
// 添加Jwt
builder.Services.AddJwtService(builder.Configuration);

builder.Services.AddOptions<JwtConfigOption>()
    .Bind(builder.Configuration.GetSection(JwtConfigOption.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
