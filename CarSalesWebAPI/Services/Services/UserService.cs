using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;
using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Services.SecurityServices.TokenService;
using CarSalesWebAPI.Services.SecurityServices.CryptographyService;
using AutoMapper;
using System.Net;
using CarSalesWebAPI.Services.Helpers;
using CarSalesWebAPI.Services.Validations.UserValidator;

namespace CarSalesWebAPI.Services.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IUnityOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;
        private readonly ICryptography _criptograph;

        public UserService(IUnityOfWork uow, IMapper mapper, ITokenService token, ICryptography criptograph)
        {
            _uow = uow;
            _mapper = mapper;
            _token = token;
            _criptograph = criptograph;
        }

        public async Task<ResponseService<UserDto>> CreateAdm(CreateUserAdmDto userDto, CancellationToken cancellationToken)
        {
            var userEmail = await _uow.UserRepository.GetById(u=> u.Email == userDto.Email, cancellationToken);

            if (userEmail != null)
            {
                if (userDto.Password != userDto.ComfirmPassword)
                {
                    return GenerateErroResponse<UserDto>("Senhas diferentes", HttpStatusCode.BadRequest);
                }
                userEmail.IsAdmin = true;
                _uow.UserRepository.UpdateEntity(userEmail);
                await _uow.Commit(cancellationToken);
                var userdto3 = _mapper.Map<UserDto>(userEmail);
                return GenerateSuccessResponse(userdto3, HttpStatusCode.Created);
            }

            if (userDto.Password != userDto.ComfirmPassword)
            {
                return GenerateErroResponse<UserDto>("Senhas diferentes", HttpStatusCode.BadRequest);
            }
            
            var user = _mapper.Map<User>(userDto);
            user.Password = _criptograph.EncryptPassword(userDto.Password);
            _uow.UserRepository.Add(user);
            await _uow.Commit(cancellationToken);
            var userdto2 = _mapper.Map<UserDto>(user);
            return GenerateSuccessResponse(userdto2, HttpStatusCode.Created);
        }

        public async Task<ResponseService<UserDto>> CreateCommonUser(CreateCommonUserDto userDto, CancellationToken cancellationToken)
        {
            var userEmail = await _uow.UserRepository.GetById(u => u.Email == userDto.Email, cancellationToken);
            
            if (userEmail != null)
            {
                return GenerateErroResponse<UserDto>("Email inválido", HttpStatusCode.BadRequest);
            }

            if (userDto.Password != userDto.ComfirmPassword)
            {
                return GenerateErroResponse<UserDto>("Senhas diferentes", HttpStatusCode.BadRequest);
            }

            var user = _mapper.Map<User>(userDto);
            user.Password = _criptograph.EncryptPassword(userDto.Password);
            _uow.UserRepository.Add(user);
            await _uow.Commit(cancellationToken);
            var userdto2 = _mapper.Map<UserDto>(user);
            return GenerateSuccessResponse(userdto2, HttpStatusCode.Created);
        }

        public async Task<ResponseService> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(user => user.Id == id, cancellationToken);
            
            if(user is null)
            {
                return GenerateErroResponse("Usuário não encontrado", HttpStatusCode.NotFound);
            }

            _uow.UserRepository.SoftDelete(user);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessResponse("Usuário deletado com sucesso", HttpStatusCode.NoContent);
        }

        public async Task<ResponseService<UserDto>> GetUserById(int id, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(u => u.Id == id && u.IsDeleted == false, cancellationToken);

            if(user is null)
            {
                return GenerateErroResponse<UserDto>("Usuário não encontrado", HttpStatusCode.NotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return GenerateSuccessResponse(userDto, HttpStatusCode.OK);
        }
        public async Task<ResponseService<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellation)
        {
            var users = await _uow.UserRepository.GetUserOrderName(cancellation);

            if(!users.Any())
            {
                return GenerateErroResponse<IEnumerable<UserDto>>("Nenhum usuário cadastrado", HttpStatusCode.NotFound);
            }

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users); 
            return GenerateSuccessResponse(userDto, HttpStatusCode.OK);
        }

        public async Task<ResponseService<IEnumerable<UserDto>>> GetAllCommonUsersDisable(CancellationToken cancellation)
        {
            var users = await _uow.UserRepository.GetUsersDisable(cancellation);

            if (!users.Any())
            {
                return GenerateErroResponse<IEnumerable<UserDto>>("Nenhum usuário Excluído", HttpStatusCode.NotFound);
            }

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return GenerateSuccessResponse(userDto, HttpStatusCode.OK);
        }

        public async Task<ResponseService> UpdateUser(int id, UpdateUserDto upUserDto, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(u => u.Id == id && u.IsDeleted == false, cancellationToken);
            
            if (user is null)
            {
                return GenerateErroResponse("Usuário não encontrado", HttpStatusCode.NotFound);
            }
            if(id != upUserDto.Id)
            {
                return GenerateErroResponse("Não pode alterar o id", HttpStatusCode.BadRequest);
            }

            var userUp = _mapper.Map(upUserDto, user);
            _uow.UserRepository.UpdateEntity(userUp);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessResponse("Usuário atualizado com sucesso", HttpStatusCode.NoContent);
        }

        public async Task<ResponseService<LoginOutUserDto>> Login(LoginUserDto loginUser, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(u => u.Email == loginUser.Email 
                                                        && u.IsDeleted == false, cancellationToken);

            if (user is null)
            {
                return GenerateErroResponse<LoginOutUserDto>("Login inválido...", HttpStatusCode.BadRequest);
            }

            if (_criptograph.VerifyPassword(loginUser.Password, user.Password))
            {
                var userDto = _mapper.Map<UserTokenDto>(user);
                var createToken = await _token.GenerateToken(userDto);

                var result = new LoginOutUserDto()
                {
                    Authenticated = true,
                    Token = createToken,
                    Message = "Token jwt ok"
                };

                return GenerateSuccessResponse(result, HttpStatusCode.OK);
            }

            return GenerateErroResponse<LoginOutUserDto>("Erro ao fazer login", HttpStatusCode.BadRequest);
        }
    }
}
