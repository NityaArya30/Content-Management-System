using SignageBackend.DAL.Models;
using SignageBackend.BAL.Services;
using SignageBackend.BAL.ViewModels;
using SignageBackend.BAL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WSignageContext>();
//builder.Services.AddTransient<IUsers, UsersService>();
builder.Services.AddTransient<ICampaigns, CampaignsServices>();
builder.Services.AddTransient<IContent, ContentService>();
builder.Services.AddTransient<ILayout, LayoutServices>();
builder.Services.AddTransient<IUser, UserService>();
builder.Services.AddTransient<IPermission, PermissionServices>();
builder.Services.AddTransient<IResolution, ResolutionServices>();
builder.Services.AddTransient<IRole, RoleService>();
builder.Services.AddTransient<ISchedule, ScheduleServices>();
builder.Services.AddTransient<IScreen, ScreenServices>();
builder.Services.AddTransient<IGroup, GroupServices>();
builder.Services.AddTransient<IGroupBy, GroupByServices>();
builder.Services.AddTransient<IGroupCampaign, GroupCampaignServices>();
builder.Services.AddTransient<IGroupSchedule, GroupScheduleServices >();
builder.Services.AddTransient<IGroupScreen, GroupScreenServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);

app.Run();
