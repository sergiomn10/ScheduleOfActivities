using Microsoft.EntityFrameworkCore;
using ScheduleOfActivities.Business;
using ScheduleOfActivities.DataAccess;
using ScheduleOfActivities.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<ActivitiesDataContext>(
        //o => o.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"))
        o => o.UseNpgsql("Host=localhost;Port=5432;Pooling=true;Database=ActivitiesDB;User Id=postgres;Password=qwerty12345;")
    );
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ActivityProxy>();
builder.Services.AddTransient<PropertyRepository>();
builder.Services.AddTransient<ActivityRepository>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/",() => @"Objetivo
                        Implementar un CRUD con Net Core Web Api 
                        en el cual se puedan Agregar nuevas actividades, 
                        Re - agendar, Cancelar y Listar las actividades.");
app.MapGet("/api/propertyList", async (ActivityProxy proxy) => await proxy.GetAllProperty());

app.MapPost("/api/createActivity", async (ActivityModel model, ActivityProxy proxy ) => 
{
    bool result = await proxy.CreateActivity(model);
    if (result)
    {
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
    
});

app.MapPut("/api/rescheduleActivity", async (ActivityModel model, ActivityProxy proxy) =>
{
    bool result = await proxy.RescheduleActivity(model);
    if (result)
    {
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});

app.MapDelete("/api/cancelActivity", async (int activity_id, ActivityProxy proxy) =>
{
    bool result = await proxy.CancelActivity(activity_id);
    if (result)
    {
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});



app.Run();
