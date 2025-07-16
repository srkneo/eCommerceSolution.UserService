using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts;

/// <summary>
/// Contract for users service that contains use cases for users
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Method to handle user login use case and returns an AuthenticationReponse object that 
    /// contains status of login
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> LoginRequest(LoginRequest loginRequest);

    /// <summary>
    /// Methods to handle user registration use case and return an object
    /// of AuthenticationResponse type that represents status of user registration
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>

    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);


    /// <summary>
    /// Method to retrieve user by their UserID
    /// </summary>
    /// <param name="UserID">User ID to search</param>
    /// <returns>UserDTO object based on the matching UserID</returns>
    Task<UserDTO> GetUserByUserID(Guid UserID);
}
