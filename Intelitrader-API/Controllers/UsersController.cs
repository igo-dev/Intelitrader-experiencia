using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Intelitrader_API.Data;
using Intelitrader_API.Models;
using Intelitrader_API.Interfaces;
using Intelitrader_API.Dtos;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Intelitrader_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsersController(IUserRepository userRepository, IMapper mapper, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Obter lista de usuarios.
        /// </summary>
        /// <response code="200">Lista obtida com sucesso.</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers()
        {
            _logger.LogInformation("Searching for users!");
        }


        /// <summary>
        /// Obter usuario por id.
        /// </summary>
        /// <response code="200">Usuario obtido com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser(Guid id)
        {
            _logger.LogInformation("Searching for user id {0}", id);

            if (result == null)
                return NotFound();

            return Ok(result);
                _logger.LogError(ex, "Error while searching for user id {0}", id);
        }


        /// <summary>
        /// Editar usuario por id.
        /// </summary>
        /// <response code="200">Usuario editado com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Put updating user id {0}", id);

            UserModel userModel = _mapper.Map<UserModel>(updateUserDto);
            userModel.Id = id;
            
                _logger.LogError(ex, "Error while put updating user id {0}", id);

            return Ok();
        }
                _logger.LogError(ex, "Error while patch updating user id {0}", id);


        /// <summary>
        /// Cadastrar usuario.
        /// </summary>
        /// <response code="201">Usuario cadastrado com sucesso.</response>

        [HttpPost]
            _logger.LogInformation("Creating new user");
        {
            UserModel userModel = _mapper.Map<UserModel>(createUserDto);
            await _userRepository.Create(userModel);
                _logger.LogError(ex, "Error while creating new user");

            return Ok();
        }


        /// <summary>
        /// Deletar usuario por id.
        /// </summary>
        /// <response code="200">Usuario deletado com sucesso.</response>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            _logger.LogInformation("Deleting user id {0}", id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.Delete(id);
            await _userRepository.SaveChanges();

            return Ok();
        }
                _logger.LogError(ex, "Error while deleting user id {0}", id);
    }
}
