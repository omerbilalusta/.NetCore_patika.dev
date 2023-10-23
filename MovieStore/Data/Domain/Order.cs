using Base.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class Order : BaseModel
    {
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual List<Movie> Movie_Orders { get; set; }

    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.TotalAmount).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
        }
    }
}
