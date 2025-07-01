using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository )
    {
        _usersRepository = usersRepository;
    }

    public async Task<AuthenticationResponse?> LoginRequest(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.password);

        if (user == null)
        {
            return null;
        }

        return new AuthenticationResponse(
            user.UserID, 
            user.Email,
            user.PersonName,
            user.Gender,
            "token",
            Success:true
         );
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {

        //Create a new Application object from RegisterRequest

        ApplicationUser user = new ApplicationUser() { 
        
            PersonName = registerRequest.PersonName,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
            Gender = registerRequest.Gender.ToString(),

        };

        ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

        if (registeredUser == null)
        {
            return null;
        }

        //Return Success Response
        return new AuthenticationResponse(

            registeredUser.UserID,
            registeredUser.Email,
            registeredUser.PersonName,
            registeredUser.Gender,
            "token",
            Success:true
        );
    }
}
