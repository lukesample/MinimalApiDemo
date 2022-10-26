namespace MinimalApiDemo;

public static class Api
{
    /// <summary>
    /// Extension method used to map endpoints for the app
    /// </summary>
    /// <param name="app"></param>
    public static void ConfigureApi (this WebApplication app)
    {
        //all api endpoint mapping goes here
        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{id}", GetUser);
        app.MapPost("/Users", InsertUser);
        app.MapPut("/Users", UpdateUser);
        app.MapDelete("/Users", DeleteUser);
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <param name="data">dependency injection for UserData</param>
    /// <returns></returns>
    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    
    /// <summary>
    /// Gets a single user by id
    /// </summary>
    /// <param name="id">passed in from the user</param>
    /// <param name="data">dependency injection for UserData</param>
    /// <returns></returns>
    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var results = await data.GetUser(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Insert a single user to the database
    /// </summary>
    /// <param name="user">User model with data from the user contained in request body</param>
    /// <param name="data"></param>
    /// <returns></returns>
    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
