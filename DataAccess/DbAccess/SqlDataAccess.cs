using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.DbAccess;
public class SqlDataAccess : ISqlDataAccess
{
	private readonly IConfiguration _config; //data from appsettings.json, secrets.json, appsettings.production or dev.json, environment variables, key vault, etc

	public SqlDataAccess(IConfiguration config)
	{
		_config = config;
	}

	//all async methods should return a Task
	//T is a generic meaning its whatever type we want to return
	//i.e. we're returning an IEnumerable<UserModel> from the database call which is a set of UserModels
	//if we need an Id, pass it in as a param
	//connectionId is the connection string (from appsettings.json)
	public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure,
												  U parameters,
												  string connectionId = "Default")
	{
		//uses the using keyword so that at the end of scope (method), the connection is shut down properly
		using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)); //search appsettings or any other config for the connection string name ("default")

		//asynchronously talk to the database returning type T which is the set of rows returned from the database
		//the QueryAsync method is implemented through Dapper
		return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}

	//generic identifiers can be any letter you want, but with naming conventions typically T comes first
	public async Task SaveData<T>(string storedProcedure,
								T parameters,
								string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId)); //search appsettings or any other config for the connection string name ("default")

		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
