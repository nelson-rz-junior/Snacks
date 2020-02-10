using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snacks.Models
{
    public class OrderItem
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Guid BasketId { get; set; }

        public int SnackId { get; set; }

        public int BasketItemId { get; set; }

        [Display(Name = "Produto")]
        public string SnackName { get; set; }

        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        [Display(Name = "Preço Unitário")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Snack Snack { get; set; }

        public virtual Order Order { get; set; }
    }
}