using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//convenções do EF Core são um conjunto de rregras usadas
//para criar e atualizar o esquema de banco de dados 
//EX: O atributo "Nome" foi criado como longText, pois o tipo string
//nao tem tamanho definido, entao a regra é ele ser criado
//com o tamanho maximo 

//Porém, é possivel sobrescrever essas convenções com as 
//Data Annotations 

namespace APICatalogo.Models
{
    //referencia para a tabela Categorias (nao precisaria, pq ja esta relacionado)
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId {get; set; }

        [JsonIgnore]//ignorar essa referencia na serialização e disserialização
        public Categoria? Categoria {get; set; }
        //cada produto vai ter uma categoria, por isso o CategoriaId
        //propriedade de navegação
        
    }
}
 