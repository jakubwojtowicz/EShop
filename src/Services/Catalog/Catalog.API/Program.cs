var builder = WebApplication.CreateBuilder(args);

// ############## Add services to the container ##############

var assembly = typeof(Program).Assembly;

//MediatR - CQRS pattern 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

//Fluent Validation
builder.Services.AddValidatorsFromAssembly(assembly);

//Carter - minimal api endpoints
builder.Services.AddCarter();

//Marten - ORM for document DB
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// ############## Configure the HTTP request pipeline ##############

app.MapCarter();

app.UseExceptionHandler(options => {});

app.Run();
