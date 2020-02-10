using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snacks.Models
{
    public class Snack
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do produto")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Descrição resumida")]
        [Required(ErrorMessage = "Informe a descrição resumida do produto")]
        [StringLength(100)]
        public string SummaryDescription { get; set; }

        [Display(Name = "Descrição completa")]
        [Required(ErrorMessage = "Informe a descrição completa do produto")]
        [StringLength(255)]
        public string FullDescription { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o preço do produto")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Produto favorito")]
        public bool IsFavoriteSnack { get; set; }
        
        [Display(Name = "Em estoque")]
        public bool InStock { get; set; }

        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [NotMapped]
        public string ThumbImageUrl { get; set; }

        [NotMapped]
        public string StorefrontImageUrl { get; set; }

        [NotMapped]
        public string DetailImageUrl { get; set; }

        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }
    }
}
