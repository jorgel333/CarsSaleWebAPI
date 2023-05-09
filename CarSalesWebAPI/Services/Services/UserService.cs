using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Services.Interfaces.Services;
using CarSalesWebAPI.Domain.Dtos.UserDtos;
using AutoMapper;

namespace CarSalesWebAPI.Services.Services
{
    public class UserService : Service, IUserService
    {
        private readonly IUnityOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnityOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ResponseService> CreateAdm(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            var userEmail = await _uow.UserRepository.GetById(u=> u.Email == userDto.Email, cancellationToken);

            if (userEmail != null)
            {
                if (userDto.Password != userDto.ComfirmPassword)
                {
                    return GenerateErrorResponse("Senhas diferentes");
                }
                userEmail.IsAdmin = true;
                _uow.UserRepository.UpdateEntity(userEmail);
                await _uow.Commit(cancellationToken);
                return GenerateSuccessfullResponse("Usuario promovio a adm com sucesso");
            }

            if (userDto.Password != userDto.ComfirmPassword)
            {
                return GenerateErrorResponse("Senhas diferentes");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Birthday = userDto.Birthday,
                IsDeleted = false,
                IsAdmin = true
            };

            _uow.UserRepository.Add(user);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Usuário Adm criado com sucesso");
        }

        public async Task<ResponseService> CreateCommonUser(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            var userEmail = await _uow.UserRepository.GetById(u => u.Email == userDto.Email, cancellationToken);

            if(userEmail != null) //especiifcar no banco de dados que essa informação não pode ser nula
            {
                return GenerateErrorResponse("Email já em uso");
            }

            if(userDto.Password != userDto.ComfirmPassword)
            {
                return GenerateErrorResponse("Senhas diferentes");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Birthday = userDto.Birthday,
                IsDeleted = false,
                IsAdmin = false
            };

            _uow.UserRepository.Add(user);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Usuário cadastrado com sucesso");
        }

        public async Task<ResponseService> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(user => user.Id == id, cancellationToken);
            
            if(user is null)
            {
                return GenerateErrorResponse("Usuário não encontrado");
            }

            _uow.UserRepository.SoftDelete(user);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Usuário deletado com sucesso");
        }

        public async Task<ResponseService<IEnumerable<UserDto>>> GetAllUsers(CancellationToken cancellation)
        {
            var users = await _uow.UserRepository.GetUserOrderName(cancellation);

            if(!users.Any())
            {
                return GenerateErroResponse<IEnumerable<UserDto>>("Nenhum usuário cadastrado");
            }

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users); 
            return GenerateSuccessfullResponse(userDto);
        }

        public async Task<ResponseService<IEnumerable<UserDto>>> GetAllCommonUsersDisable(CancellationToken cancellation)
        {
            var users = await _uow.UserRepository.GetUsersDisable(cancellation);

            if (!users.Any())
            {
                return GenerateErroResponse<IEnumerable<UserDto>>("Nenhum usuário Excluído");
            }

            var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return GenerateSuccessfullResponse(userDto);
        }

        public async Task<ResponseService> UpdateUser(int id, UpdateUserDto upUserDto, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetById(u => u.Id == id && u.IsDeleted == false, cancellationToken);
            
            if (user is null)
            {
                return GenerateErrorResponse("Usuário não encontrado");
            }
            if(id != upUserDto.Id)
            {
                return GenerateErrorResponse("Não pode alterar o id");
            }

            var userUp = _mapper.Map(upUserDto, user);
            _uow.UserRepository.UpdateEntity(userUp);
            await _uow.Commit(cancellationToken);
            return GenerateSuccessfullResponse("Usuário atualizado com sucesso");
        }
    }
}
