using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        //injetar uma instancia da classe AppDbContext para acessar o banco de dados
        // depois colocar injeção de dependencia no construtor 

        //readonly = apenas leitura, nao pe possivel alterá-la
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //os verbos GET, POST, PUT, DELETE são metodos Actions
        //usados com Http atende o request, pois é uma WebAPI
        //ActionResult faz com que seja retornado ou uma lista de produtos
        //ou um StatusCode

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.Take(10).ToList();
            //vai retornar até 10 registros.

            if (produtos is null)
            {
                return NotFound();//retornará o codigo de erro caso a lista for vazia
                //o código de erro neste caso é o 400
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name ="ObterProduto")]
        //função para retornar um unico Produto pelo ID
        public ActionResult<Produto> Get(int id)
        {
            //nesse metodo, foi usado o FirstOrDefault para caso não ache, voltar um valor null
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto == null)
            {
                return NotFound("Produto não encontrado!");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {

            if (produto is null)
                return BadRequest();//statusCode 400


            //vai criar um produto na memoria 
            _context.Produtos.Add(produto);

            //vai salvar esse produto na tabela
            _context.SaveChanges();

            //vai adicionar um cabeçalho na resposta da requisição
            //vai retornar o código 200 em caso de sucesso nesse cabeçalho, StatusCode
            //cria uma URI(rota) "ObterProduto" para este produto que está sendo incluido
            //armezenar esse novo produto no proximo ID da tabela
            return new CreatedAtRouteResult("ObterProduto",
                new {id = produto.ProdutoId}, produto);
        }


        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();//statusCode 400
            }

            //esse método avisa que o status do produto foi modificado
            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();//salvando no banco de dados
        
            return Ok(produto);//retorna o codigo 200 com o body da resposta
           // return NoContent() retorna o 204, sucesso mas sem o body da resposta
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
                return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);//OK retorna o StatusCode 200, + o produto
        }
        

    }
}
