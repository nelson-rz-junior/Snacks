using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Snacks.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da categoria")]
        [Display(Name = "Nome")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe a descrição da categoria")]
        [Display(Name = "Descrição")]
        [StringLength(200)]
        public string Description { get; set; }

        public List<Snack> Snacks { get; set; }
    }
}