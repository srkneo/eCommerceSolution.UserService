using Dapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Infrastructure.DbContext;
using Npgsql;

namespace eCommerce.Infrastructure.Repositories;

internal class UsersRepository : IUsersRepository
{

    private readonly DapperDbContext _dbContext;

    public UsersRepository(DapperDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApplicationUser?> AddUser(ApplicationUser user)
    {

        //Generate a new unique user ID for the user
        user.UserID = Guid.NewGuid();

        // SQL Query to insert user data into the "Users" table.
        // Correct SQL Query to insert user data into the "Users" table.
        string query = "INSERT INTO public.\"Users\" (\"UserID\", \"Email\", \"PersonName\", \"Gender\", \"Password\") " +
                       "VALUES (@UserID, @Email, @PersonName, @Gender, @Password)";
        int rowCountAffected = await _dbContext.dbConnection.ExecuteAsync(query, user);
        if (rowCountAffected > 0)
        {
            return user;
        }
        
        return null;
    }

    public async Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password)
    {
        // SQL query to select a user by Email and Password
        string query = "SELECT * FROM public.\"Users\" WHERE \"Email\" = @Email AND \"Password\" = @Password";

        ApplicationUser? user = await _dbContext.dbConnection
            .QueryFirstOrDefaultAsync<ApplicationUser>(query, new { Email = email, Password = password });

        return user;

    }

    public async Task<ApplicationUser?> GetUserByUserID(Guid? userID)
    {
        string query = "SELECT * FROM public.\"Users\" WHERE \"UserID\" = @UserID";
        var parameters = new { UserID = userID };

        using var connection = _dbContext.dbConnection;
        return await connection.QueryFirstOrDefaultAsync(query,parameters);
    }
}
