using Shopping_Appilication.IServices;
using Shopping_Appilication.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IProductServices, ProductServices>();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddSingleton<IProductServices, ProductServices>();
//builder.Services.AddScoped<IProductServices, ProductServices>();
/*
 * Singleton: Services s? ch? ???c t?o m?t l?n trong su?t lifetime c?a ?ng d?ng. Phù h?p vói các services có tính toàn c?c và không thay ??i
 * Scope: M?i request s? kh?i t?o l?i services 1 l?n. Dùng cho các services có tính ch? ??c thù nào ?ó.
 * Transient: ?c kh?i t?o m?i khi có yêu c?u, m?i request s? ?c nh?n 1 services khác nhau, và ?c s? d?ng v?i services có nhi?u http
 * */
//Khai báo sử dụng Ssesion với thời gian timeout là 30s
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(120);
});
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
//test
//xinchao
