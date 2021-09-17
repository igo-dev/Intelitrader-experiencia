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

namespace Intelitrader_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
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
            var result = _mapper.Map<IEnumerable<GetUserDto>>(await _userRepository.ReadAll());
            return Ok(result);
        }


        /// <summary>
        /// Obter usuario por id.
        /// </summary>
        /// <response code="200">Usuario obtido com sucesso.</response>
        /// <response code="404">Usuario não encontrado.</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDto>> GetUser(Guid id)
        {
            var result = _mapper.Map<GetUserDto>(await _userRepository.Read(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }
