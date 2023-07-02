using SqlSugar;
using vueClassWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		builder =>
		{
			builder.WithOrigins("http://localhost:8080").AllowAnyHeader();
		});
});

builder.Services.AddSingleton<ISqlSugarClient>(config =>
{
	var client = new SqlSugarClient(new ConnectionConfig()
	{
		ConnectionString = builder.Configuration.GetConnectionString("SqlConnection"),
		DbType = DbType.SqlServer,
		IsAutoCloseConnection = true,
		InitKeyType = InitKeyType.Attribute
	});
	return client;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var sqlSugar = app.Services.CreateScope().ServiceProvider.GetRequiredService<ISqlSugarClient>();

sqlSugar.DbMaintenance.CreateDatabase();

sqlSugar.CodeFirst.InitTables(typeof(Student));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();