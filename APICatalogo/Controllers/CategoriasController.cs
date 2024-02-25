using APICatalogo.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        //isso é uma rota para acessar esse endPoint 
        //a rota é quem manda na API para acessar os endpoints
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            //include permite carregar entidades relacionadas entre elas
            return _context.Categorias.Include(p => p.Produtos).Where
                (c => c.CategoriaId <= 5).ToList();
            //vai retornar todos as categorias com id menor ou igual a 5
            //e seus respectivos produtos.

        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _context.Categorias.Take(10).ToList();

                return categorias;
            }
            catch(Exception)
            {
                //StatusCodes possibilita que esconda o erro interno
                //mostre apenas a mensagem que for necessária
                //isso ajuda para não expor informações exageradas
                //podendo colocar a segurança da aplicação em risco
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }

        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria>Get(int id)
        {
            try
            {                
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound("Esta categoria não existe");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema no sistema ao tratar a sua solicitação.");
            }
            
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {

            try
            {
                if (categoria is null)
                {
                    return BadRequest();
                }

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria",
                    new
                    {
                        id = categoria.CategoriaId
                    }, categoria);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema no sistema ao tratar a sua solicitação.");
            }
           
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {

            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest($"Não foi possível realizar a atualização da categoria com id{id}");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema no sistema ao tratar a sua solicitação.");
            }
           
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound("Esta categoria não existe");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema no sistema ao tratar a sua solicitação.");
            }
           

        }
    }


}
