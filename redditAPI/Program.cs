using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Firebase initialisation 
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("reddit-api-3ef2f-firebase-adminsdk-k7wei-68e730d850.json")
});

builder.Services.AddSingleton<FirestoreService>(_ =>
    new FirestoreService("reddit-api-3ef2f", "reddit-api-3ef2f-firebase-adminsdk-k7wei-68e730d850.json"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
