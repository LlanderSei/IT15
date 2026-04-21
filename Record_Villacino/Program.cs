using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Record_Villacino.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
  .AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseMigrationsEndPoint();
}
else {
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}")
  .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope()) {
  var services = scope.ServiceProvider;
  var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
  var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

  const string adminRole = "Admin";
  var adminEmail = builder.Configuration["SeedAdmin:Email"] ?? "admin@record.local";
  var adminPassword = builder.Configuration["SeedAdmin:Password"] ?? "Admin123!";

  if (!await roleManager.RoleExistsAsync(adminRole)) {
    await roleManager.CreateAsync(new IdentityRole(adminRole));
  }

  var adminUser = await userManager.FindByEmailAsync(adminEmail);
  if (adminUser == null) {
    adminUser = new IdentityUser {
      UserName = adminEmail,
      Email = adminEmail,
      EmailConfirmed = true
    };

    var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
    if (!createUserResult.Succeeded) {
      var errors = string.Join("; ", createUserResult.Errors.Select(error => error.Description));
      throw new InvalidOperationException($"Failed to seed admin user: {errors}");
    }
  }

  if (!await userManager.IsInRoleAsync(adminUser, adminRole)) {
    await userManager.AddToRoleAsync(adminUser, adminRole);
  }
}

app.Run();
