# MvcUser

```bash
dotnet new mvc --auth individual -o MvcUser
```

Per creare le pagine di registrazione e login

```bash
dotnet add package Microsoft.AspNetCore.Identity.UI
```

## Creazione di un utente che estende IdentityUser

Nella cartella models creo il modell AppUser

```csharp
public class AppUser : IdentityUser
{
    public string Score
}
```

nella cartella Data aggiorno ApplicationDbContext per utilizzare il nuovo modello utente esteso invece di IdentityUser

```csharp
public class ApplicationDbContext : IdentityDbContext

public class ApplicationDbContext : IdentityDbContext<AppUser>
```

Modifiche al Program.cs

```csharp
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()// Aggiunto da noi!
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();


app.MapRazorPages();//dopo questo

using(var scope = app.Services.CreateScope()){  
    var service = scope.ServiceProvider;
    try{
        var userManager = service.GetRequiredService<UserManager<AppUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData.InitializeAsync(userManager, roleManager);
    }catch(Exception ex){
    }
}

app.Run();//prima di questo
```

dotnet ef migrations add InitialCreate

dotnet ef database update    

