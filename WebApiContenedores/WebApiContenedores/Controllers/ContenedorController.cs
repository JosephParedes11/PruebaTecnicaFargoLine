using CrudBasico.Datos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiContenedores.Datos;
using WebApiContenedores.Models;
using WebApiContenedores.Utils;

namespace WebApiContenedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContenedorController : ControllerBase
    {

        ContenedorDatos contenedorDatos = new ContenedorDatos();

        [HttpGet("GetContenedores")]
        [Authorize]
        public async Task<List<ContenedorModel>> GetAllContenedores()
        {
            return await contenedorDatos.GetAllContenedores(); ;
        }


        [HttpGet("GetTiposContenedor")]
        [Authorize]
        public async Task<List<TipoContenedorModel>> GetTiposContenedor()
        {
            return await contenedorDatos.GetAllTiposContenedor(); ;
        }

        [HttpPost("CreateContenedor")]
        [Authorize]
        public async Task<ContenedorModel> CreateContenedor([FromBody] ContenedorModel model)
        {
            ContenedorModel response = new ContenedorModel();

            if (model == null)
            {
                return (ContenedorModel)Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            int insertId = await contenedorDatos.CreateContenedor(model);
            if (insertId > 0)
            {
                response.in_id = insertId;
                response.in_numero = model.in_numero;
                response.in_idTipo = model.in_idTipo;
                response.in_size = model.in_size;
                response.dc_peso = model.dc_peso;
                response.dc_tara = model.dc_tara;
            }
            else
            {
                return (ContenedorModel)Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        [HttpPut("UpdateContenedor")]
        [Authorize]
        public async Task<ContenedorModel> UpdateContenedor([FromBody] ContenedorModel model)
        {
            ContenedorModel response = new ContenedorModel();

            if (model.in_id == 0)
            {
                return (ContenedorModel)Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            bool result = await contenedorDatos.GetContenedoreById(model.in_id);

            bool estado = false;


            if (result)
            {
                estado = await contenedorDatos.UpdateContenedor(model);
            }

            if (estado)
            {
                response = new ContenedorModel()
                {
                    in_id = model.in_id,
                    in_numero = model.in_numero,
                    in_idTipo = model.in_idTipo,
                    in_size = model.in_size,
                    dc_peso = model.dc_peso,
                    dc_tara = model.dc_tara
                };
            }
            else
            {
                return (ContenedorModel)Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return response;
        }

        [HttpDelete("DeleteContenedor/{idContenedor}")]
        [Authorize]
        public async Task<int> DeleteContenedor(int idContenedor)
        {
            ContenedorModel response = new ContenedorModel();

            if (idContenedor == 0)
            {
                return -1;
            }

            bool result = await contenedorDatos.GetContenedoreById(idContenedor);

            bool estado = false;


            if (result)
            {
                estado = await contenedorDatos.DeleteContenedor(idContenedor);
            }

            if (estado)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
