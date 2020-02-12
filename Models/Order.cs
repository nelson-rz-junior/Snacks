using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Snacks.Models
{
    public class Order
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [BindNever]
        [Display(Name = "Cesta")]
        public Guid BasketId { get; set; }

        [StringLength(36)]
        public string UserId { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o tipo do endereço")]
        [Display(Name = "Tipo do endereço")]
        [StringLength(50)]
        public string AddressType { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        [Display(Name = "Endereço")]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(50)]
        public string Complement { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [Display(Name = "CEP")]
        [DataType(DataType.PostalCode)]
        [StringLength(10, MinimumLength = 8, ErrorMessage = "O CEP deve possuir entre 8 e 10 caracteres")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(50)]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [Required(ErrorMessage = "Informe o estado")]
        [StringLength(2)]
        [Display(Name = "Estado")]
        public string State { get; set; }
        
        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(13)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }

        [Display(Name = "Total")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOrder { get; set; }

        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Entregue")]
        public bool IsDelivery { get; set; }

        [Display(Name = "Data de entrega")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Pago")]
        public bool IsPaid { get; set; }

        [Display(Name = "Data de pagamento")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }

        public virtual Basket Basket { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
