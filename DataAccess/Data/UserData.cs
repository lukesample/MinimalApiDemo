using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class UserData : IUserData
{
	private readonly ISqlDataAccess _db;

	public UserData(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<UserModel>> GetUsers() =>
		//dynamic means we can pass in any type parameters with values (i.e. the new { } which is an anonymous object)
		_db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

	public async Task<UserModel?> GetUser(int id)
	{
		var results = await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id });

		return results.FirstOrDefault(); //returns the first record in the IEnumerable or the default value for the UserModel which is null
	}

	public Task InsertUser(UserModel user) =>
		_db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

	public Task UpdateUser(UserModel user) =>
		_db.SaveData("dbo.spUser_Update", user); //pass in the whole model since we'll use everything here to update

	public Task DeleteUser(int id) =>
		_db.SaveData("dbo.spUser_Delete", new { Id = id });
}
