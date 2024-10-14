using CP2.Application.Dtos;
using CP2.Domain.Entities;
using CP2.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorApplicationService _applicationService;

        // construtor que recebe a instância do serviço de fornecedo via injeção de dependência
        public FornecedorController(IFornecedorApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// obtém todos os fornecedores cadastrados.
        /// </summary>
        /// <returns>retorna a lista de fornecedores ou uma mensagem de erro.</returns>
        [HttpGet]
        [Produces<IEnumerable<FornecedorEntity>>]
        public IActionResult Get()
        {
            var fornecedor = _applicationService.ObterTodosFornecedores();

            if (fornecedor is not null)
                return Ok(fornecedor); // retorna os fornecedores com status 200 (ok)

            return BadRequest("não foi possível obter os dados"); // retorna erro 400 (bad request) se houver falha
        }

        /// <summary>
        /// busca um fornecedor específico pelo seu id.
        /// </summary>
        /// <param name="id">identificador único do fornecedor.</param>
        /// <returns>retorna os dados do fornecedor ou uma mensagem de erro.</returns>
        [HttpGet("{id}")]
        [Produces<FornecedorEntity>]
        public IActionResult GetPorId(int id)
        {
            var fornecedor = _applicationService.ObterFornecedorPorId(id);

            if (fornecedor is not null)
                return Ok(fornecedor); // retorna os dados do fornecedor com status 200 (ok)

            return BadRequest("não foi possível obter os dados"); // retorna erro 400 (bad request) se não encontrar o fornecedor
        }

        /// <summary>
        /// cadastra um novo fornecedor.
        /// </summary>
        /// <param name="entity">dados do fornecedor a serem cadastrados.</param>
        /// <returns>retorna o fornecedor cadastrado ou uma mensagem de erro.</returns>
        [HttpPost]
        [Produces<FornecedorEntity>]
        public IActionResult Post([FromBody] FornecedorDto entity)
        {
            try
            {
                var fornecedor = _applicationService.SalvarDadosFornecedor(entity);

                if (fornecedor is not null)
                    return Ok(fornecedor); // retorna o fornecedor cadastrado com status 200 (ok)

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
        /// atualiza as informações de um fornecedor existente.
        /// </summary>
        /// <param name="id">identificador único do fornecedor a ser atualizado.</param>
        /// <param name="entity">novos dados do fornecedor para atualização.</param>
        /// <returns>retorna o fornecedor atualizado ou uma mensagem de erro.</returns>
        [HttpPut("{id}")]
        [Produces<FornecedorEntity>]
        public IActionResult Put(int id, [FromBody] FornecedorDto entity)
        {
            try
            {
                var fornecedor = _applicationService.EditarDadosFornecedor(id, entity);

                if (fornecedor is not null)
                    return Ok(fornecedor); // retorna os dados do fornecedor atualizado com status 200 (ok)

                return BadRequest("não foi possível salvar os dados"); // retorna erro 400 (bad request) se a atualização falhar
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
        /// exclui um fornecedor do sistema com base no id informado.
        /// </summary>
        /// <param name="id">identificador único do fornecedor a ser removido.</param>
        /// <returns>retorna uma mensagem de sucesso ou erro dependendo do resultado.</returns>
        [HttpDelete("{id}")]
        [Produces<FornecedorEntity>]
        public IActionResult Delete(int id)
        {
            var fornecedor = _applicationService.DeletarDadosFornecedor(id);

            if (fornecedor is not null)
                return Ok(fornecedor); // retorna status 200 (tudo certo) com os dados do fornecedor excluído

            return BadRequest("não foi possível deletar os dados"); // retorna erro 400 (bad request) se a exclusão falhar
        }
    }
}
