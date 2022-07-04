
using App.Application;
using App.Infrastructre;
using iNNOVATEQAssignment.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Add root folder to store files
builder.WebHost.UseWebRoot("wwwroot");


// Add file logging.

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("app.log", append: true);
});

// Add services to the container.
builder.Services.AddApplicationServices();
// add infrastructure to the services 
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add Middleware to app 
app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
// Prepare database by adding data from json
app.PrepareDatabase(System.IO.File.ReadAllText(builder.Environment.ContentRootPath + @"\Data\MOCK_DATA_USERS.json"))
                .GetAwaiter()
                .GetResult();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
