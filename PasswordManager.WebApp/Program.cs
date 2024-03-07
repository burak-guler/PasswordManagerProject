using Microsoft.AspNetCore.Builder;
using PasswordManager.WebApp.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

//httpclient container 
builder.Services.AddHttpServices();
//httpclient için yapýlandýrma
builder.Services.AddHttpClient("WebApiService", client =>
{
    client.BaseAddress = new Uri("https://localhost:7014/");
    // Diðer yapýlandýrma seçenekleri
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}");

app.Run();
