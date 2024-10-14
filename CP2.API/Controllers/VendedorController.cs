using CP2.Application.Dtos;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorApplicationService _applicationService;

        // construtor que recebe a instância do serviço de vendedor via injeção de dependência
        public VendedorController(IVendedorApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// obtém todos os vendedores cadastrados.
        /// </summary>
        /// <returns>retorna a lista de vendedores ou uma mensagem de erro.</returns>
        [HttpGet]
        [Produces<IEnumerable<VendedorEntity>>]
        public IActionResult Get()
        {
            var vendedor = _applicationService.ObterTodosVendedores();

            if (vendedor is not null)
                return Ok(vendedor); // retorna os vendedores com status 200 (ok)

            return BadRequest("não foi possível obter os dados"); // retorna erro 400 (bad request) se houver falha
        }

        /// <summary>
        /// busca um vendedor específico pelo seu id.
        /// </summary>
        /// <param name="id">identificador único do vendedor.</param>
        /// <returns>retorna os dados do vendedor ou uma mensagem de erro.</returns>
        [HttpGet("{id}")]
        [Produces<VendedorEntity>]
        public IActionResult GetPorId(int id)
        {
            var vendedor = _applicationService.ObterVendedorPorId(id);

            if (vendedor is not null)
                return Ok(vendedor); // retorna os dados do vendedor com status 200 (ok)

            return BadRequest("não foi possível obter os dados"); // retorna erro 400 (bad request) se não encontrar o vendedor
        }

        /// <summary>
        /// cadastra um novo vendedor.
        /// </summary>
        /// <param name="entity">dados do vendedor a serem cadastrados.</param>
        /// <returns>retorna o vendedor cadastrado ou uma mensagem de erro.</returns>
        [HttpPost]
        [Produces<VendedorEntity>]
        public IActionResult Post([FromBody] VendedorDto entity)
        {
            try
            {
                var vendedor = _applicationService.SalvarDadosVendedor(entity);

                if (vendedor is not null)
                    return Ok(vendedor); // retorna o vendedor cadastrado com status 200 (ok)

                return BadRequest("não foi possível salvar os dados"); // retorna erro 400 (bad request) se falhar ao salvar
            }
            catch (Exception ex)
            {
                // retorna um erro detalhado em caso de exceção, incluindo a mensagem e status 400 (bad request)
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// atualiza as informações de um vendedor existente.
        /// </summary>
        /// <param name="id">identificador único do vendedor a ser atualizado.</param>
        /// <param name="entity">novos dados do vendedor para atualização.</param>
        /// <returns>retorna o vendedor atualizado ou uma mensagem de erro.</returns>
        [HttpPut("{id}")]
        [Produces<VendedorEntity>]
        public IActionResult Put(int id, [FromBody] VendedorDto entity)
        {
            try
            {
                var vendedor = _applicationService.EditarDadosVendedor(id, entity);

                if (vendedor is not null)
                    return Ok(vendedor); // retorna os dados do vendedor atualizado com status 200 (ok)

                return BadRequest("não foi possível editar os dados"); // retorna erro 400 (bad request) se a atualização falhar
            }
            catch (Exception ex)
            {
                // retorna um erro detalhado em caso de exceção, incluindo a mensagem e status 400 (bad request)
                return BadRequest(new
                {
                    Error = ex.Message,
                    status = HttpStatusCode.BadRequest,
                });
            }
        }

        /// <summary>
        /// exclui um vendedor do sistema com base no id informado.
        /// </summary>
        /// <param name="id">identificador único do vendedor a ser removido.</param>
        /// <returns>retorna uma mensagem de sucesso ou erro dependendo do resultado.</returns>
        [HttpDelete("{id}")]
        [Produces<VendedorEntity>]
        public IActionResult Delete(int id)
        {
            var objModel = _applicationService.DeletarDadosVendedor(id);

            if (objModel is not null)
                return Ok(objModel); // retorna status 200 (ok) com os dados do vendedor excluído

            return BadRequest("não foi possível deletar os dados"); // retorna erro 400 (bad request) se a exclusão falhar
        }
    }
}
