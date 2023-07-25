using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleCodeModel.DbEntities;

public partial class Product
{
    [Key]
    public int ProductId { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Price { get; set; }

    public long? Quantity { get; set; }

    public int? StatusId { get; set; }

    [InverseProperty("Products")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
