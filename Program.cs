using JitCars.Data;
using JitCars.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JitCars.Services.Identity;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentity<Funcionario, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddErrorDescriber<ErrosCustomizados>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Funcionario/Login");
    options.LogoutPath = new PathString("/Funcionario/Deslogar");
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Funcionario", "Gerente" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

    }

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Funcionario>>();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    string usuarioGerentePadrao = "jitcars.admin";
    string senhaPadrao = "JitCarsAdmin@2024";

    if (await userManager.FindByNameAsync(usuarioGerentePadrao) == null)
    {

        Cargo cargo = new()
        {
            Titulo = "Gerente",
            Date = DateTime.UtcNow,
            Salario = 5000,
        };

        Endereco endereco = new()
        {
            Cep = "00000000",
            Estado = "N/A",
            Cidade = "N/A",
            Bairro = "N/A",
            Rua = "N/A",
            Numero = 0
        };

        var cargoExistente = await context.Cargos.FirstOrDefaultAsync(e => e.Titulo == cargo.Titulo);
        var cargoId = 0;

        if (cargoExistente == null)
        {
            context.Cargos.Add(cargo);
        } else
        {
            cargoId = cargoExistente.Id;  
        }

        context.Enderecos.Add(endereco);
        await context.SaveChangesAsync();

        cargoId = cargoId == 0 ? cargo.Id : cargoId;

        Funcionario gerente = new()
        {
            PrimeiroNome = "Gerente",
            Sobrenome = "JitCars",
            UserName = usuarioGerentePadrao,
            Cpf = "00000000000",
            EnderecoId = endereco.Id,
            CargoId = cargo.Id,
        };

        await userManager.CreateAsync(gerente, senhaPadrao);
        await userManager.AddToRoleAsync(gerente, "Gerente");
    }


}

app.Run();
