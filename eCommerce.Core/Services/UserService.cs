using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UserService(IUsersRepository usersRepository, IMapper mapper )
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> LoginRequest(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.password);

        if (user == null)
        {
            return null;
        }

        return _mapper.Map<AuthenticationResponse>(user) with {         
            Success = true,Token = "token"
        } ;

    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {

        // Use AutoMapper to map RegisterRequest to ApplicationUser
        ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);

        ApplicationUser? registeredUser = await _usersRepository.AddUser(user);

        if (registeredUser == null)
        {
            return null;
        }

        //Return Success Response
        
        return _mapper.Map<AuthenticationResponse>(registeredUser) with
        {
            Success = true,
            Token = "token"
        };
    }
}
