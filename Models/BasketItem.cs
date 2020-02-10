using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snacks.Models
{
    public class BasketItem
    {
        public int Id { get; set; }

        public Guid BasketId { get; set; }

        public int SnackId { get; set; }

        public int Quantity { get; set; }

        public string SnackName { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Snack Snack { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
