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
        /// <response code="204">Lista obtida com sucesso porem vazia.</response>
        /// <response code="400">Solicitação não reconhecida pelo servidor.</response>
        /// <response code="500">Ocorreu um erro interno no servidor.</response>


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDto>>> Get()
        {
            _logger.LogInformation("Searching for users!");

            try
            {
                IEnumerable<UserModel> users = await _userRepository.ReadAll();

                if (users == null) return NoContent();

                IEnumerable<GetUserDto> mappedDto = _mapper.Map<IEnumerable<GetUserDto>>(users);

                return Ok(mappedDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for users!");
                return StatusCode(500);
            }

        }


        /// <summary>
        /// Obter usuario por id.
        /// </summary>
        /// <response code="200">Usuario obtido com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>
        /// <response code="400">Solicitação não reconhecida pelo servidor.</response>
        /// <response code="500">Erro interno.</response>
        /// <param name="id">Id do usuario.</param>

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetById(Guid id)
        {
            _logger.LogInformation("Searching for user id {0}", id);

            try
            {
                UserModel user = await _userRepository.Read(id);

                if (user == null) return NotFound();

                GetUserDto result = _mapper.Map<GetUserDto>(user);
            return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for user id {0}", id);
                return StatusCode(500);
            }

        }


        /// <summary>
        /// Atualizar usuario por id.
        /// </summary>
        /// <response code="200">Usuario atualizado com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>
        /// <response code="400">Solicitação não reconhecida pelo servidor.</response>
        /// <response code="500">Erro interno.</response>
        /// <param name="id">Id do usuario a editar.</param>
        /// <param in="body" name="updateUserDto">Dados para atualização.</param>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpdate(Guid id, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Put updating user id {0}", id);

            try
            {
                if (!await _userRepository.UserExists(id)) return NotFound();

            UserModel userModel = _mapper.Map<UserModel>(updateUserDto);
            userModel.Id = id;
            
                await _userRepository.PutUpdate(userModel);

                await _userRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while put updating user id {0}", id);
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Atualizar parcialmente usuario por id.
        /// </summary>
        /// <response code="200">Usuario atualizado com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>
        /// <response code="400">Solicitação não reconhecida pelo servidor.</response>
        /// <response code="500">Erro interno.</response>
        /// <param name="id">Id do usuario a editar.</param>
        /// <param name="updateUserDto">Dados para atualização.</param>

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUpdate(Guid id, JsonPatchDocument<UpdateUserDto> updateUserDto)
        {
            _logger.LogInformation("Patch updating user id {0}", id);

            try
            {
                UserModel entity = await _userRepository.Read(id);

                if (entity == null)
                    return NotFound();

                JsonPatchDocument<UserModel> mappedUser = _mapper.Map<JsonPatchDocument<UserModel>>(updateUserDto);

                try
                {
                    mappedUser.ApplyTo(entity);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                if (!TryValidateModel(entity)) return BadRequest();

                await _userRepository.SaveChangesAsync();
            return Ok();
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while patch updating user id {0}", id);
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Cadastrar usuario.
        /// </summary>
        /// <response code="201">Usuario cadastrado com sucesso.</response>
        /// <response code="400">Requisição mal formada.</response>
        /// <response code="500">Erro interno.</response>

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            _logger.LogInformation("Creating new user");

            try
        {
            UserModel userModel = _mapper.Map<UserModel>(createUserDto);

            await _userRepository.Create(userModel);
                await _userRepository.SaveChangesAsync();
                GetUserDto createdUser = _mapper.Map<GetUserDto>(userModel);

                return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new user");
                return StatusCode(500);
            }

        }


        /// <summary>
        /// Deletar usuario por id.
        /// </summary>
        /// <response code="200">Usuario deletado com sucesso.</response>
        /// <response code="404">Usuario nao encontrado.</response>
        /// <response code="400">Solicitação não reconhecida pelo servidor.</response>
        /// <response code="500">Erro interno.</response>
        /// <param name="id">Id do usuario a deletar.</param>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation("Deleting user id {0}", id);

            try
            {
                bool userExists = await _userRepository.UserExists(id);

                if (!userExists) return NotFound();

            await _userRepository.Delete(id);
                await _userRepository.SaveChangesAsync();
            return Ok();
        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user id {0}", id);
                return StatusCode(500);
            }
        }
    }
}
