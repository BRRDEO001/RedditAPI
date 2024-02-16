using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "fbServiceAccountKey.json");

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000");

// Rest of your code

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
var projectId = configuration["Firebase:ProjectId"];
var serviceAccountKeyFilePath = configuration["Firebase:ServiceAccountKeyFilePath"];

//Firebase initialization 
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("fbServiceAccountKey.json")
}) ;

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
