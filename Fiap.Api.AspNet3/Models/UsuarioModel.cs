using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fiap.Api.AspNet3.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioId { get; set; }
        [Required]
        [MaxLength(30)]
        public string NomeUsuario { get; set; }
        [Required]
        [MaxLength(30)]
        public string Senha { get; set; }
        [Required]
        [MaxLength(30)]
        public string Regra { get; set; }

        /*---------------------------------------------------------- Ctor´s*/
        public UsuarioModel()
        {
        }
        public UsuarioModel(int usuarioId, string nomeUsuario, string senha, string regra)
        {
            UsuarioId = usuarioId;
            NomeUsuario = nomeUsuario;
            Senha = senha;
            Regra = regra;
        }
    }
}