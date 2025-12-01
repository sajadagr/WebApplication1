using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapGet("/secret", () =>
{
    string password = "MySecret123"; // Noncompliant
    return $"Password is {password}";
});

// ⚠️ 2. Empty catch block (Bug)
app.MapGet("/error", () =>
{
    try
    {
        int x = 10 / 1;
    }
    catch (Exception)
    {
        // Noncompliant: Empty catch block
    }
    return "Handled exception badly!";
});

// 🐢 3. Bad performance practice
app.MapGet("/slow", () =>
{
    System.Threading.Thread.Sleep(5000); // Noncompliant
    return "Slow response simulated!";
});

// 💡 4. Unused variable
app.MapGet("/unused", () =>
{
    int unused = 42; // Noncompliant
    return "Unused variable example.";
});


app.Run();
