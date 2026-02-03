var builder = WebApplication.CreateBuilder(args);

//Carter - minimal api endpoints
builder.Services.AddCarter();

//MediatR - CQRS pattern 
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

//Marten - ORM for document DB
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
