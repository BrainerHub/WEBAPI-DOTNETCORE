using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SampleCodeModel.DbEntities;

public partial class SampleContex : DbContext
{
    public SampleContex(DbContextOptions<SampleContex> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasOne(d => d.Products).WithMany(p => p.ProductImages).HasConstraintName("FK_ProductImage_Products");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
