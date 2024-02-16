using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using redditAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Rest of your code


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get Firebase configuration settings from appsettings.json
var configuration = builder.Configuration;
var projectId = configuration["Firebase:ProjectId"];
var serviceAccountKeyFilePath = configuration["Firebase:ServiceAccountKeyFilePath"];

//Firebase initialisation 
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(serviceAccountKeyFilePath)
});

builder.Services.AddSingleton<FirestoreService>(_ =>
    new FirestoreService(projectId, serviceAccountKeyFilePath));

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
