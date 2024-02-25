using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
    [Table("Ctaegorias")]
    public class Categoria
    {
        //é uma boa prtica inicializar a coleção
        public Categoria()
        {
            Produtos = new Collection<Produto>();
        }
        [Key]
        public int CategoriaId { get; set; }
        //chave Primaria 

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }
        //este atributo é Not Null
        //tamanho maximo de 80 caracteres

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }
        //este atributo é Not Null
        //tamanho maximo de 300 caracteres
        public ICollection<Produto>? Produtos {get; set; }
        //propriedade de navegação
        //nao entra na tabela

    }
}
