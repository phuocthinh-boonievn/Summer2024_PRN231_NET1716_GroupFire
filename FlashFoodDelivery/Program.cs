using API;
using Business_Layer.Services.VNPay;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// VNPay setting 
builder.Services.AddSingleton<VNPayHelper>();
builder.Services.Configure<VNPaySettings>(configuration.GetSection("VNPaySettings"));

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, builder.Environment);
