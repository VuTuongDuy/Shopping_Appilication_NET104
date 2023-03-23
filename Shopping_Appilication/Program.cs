using Shopping_Appilication.IServices;
using Shopping_Appilication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
//builder.Services.AddSingleton<IProductServices, ProductServices>();
//builder.Services.AddScoped<IProductServices, ProductServices>();
/*
 * Singleton: Services s? ch? ???c t?o m?t l?n trong su?t lifetime c?a ?ng d?ng. Ph� h?p v�i c�c services c� t�nh to�n c?c v� kh�ng thay ??i
 * Scope: M?i request s? kh?i t?o l?i services 1 l?n. D�ng cho c�c services c� t�nh ch? ??c th� n�o ?�.
 * Transient: ?c kh?i t?o m?i khi c� y�u c?u, m?i request s? ?c nh?n 1 services kh�c nhau, v� ?c s? d?ng v?i services c� nhi?u http
 * */
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//test
//hjhjhjhj
