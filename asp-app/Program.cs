
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/time", () => DateTime.Now.Hour +" : " +  DateTime.Now.Minute
+" : " +  DateTime.Now.Second);

app.MapGet("/whoami", async context=>await context.Response.WriteAsync(
    context.Request.Headers["User-Agent"].ToString()));

app.MapGet("/error", async context=>{
    if (!context.Request.Query.ContainsKey("status")){
        await context.Response.WriteAsync($"Your code: 200");
        return;
    }
    var status = context.Request.Query["status"];
        await context.Response.WriteAsync($"Your code:{status}");
});
app.Run();
