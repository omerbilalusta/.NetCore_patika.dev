using Base.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Domain
{
    public class Genre : BaseModel
    {
        public string Title { get; set; }

        public virtual List<Genre_Customer> Genre_Customers { get; set; }
    }

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Genre_Customers)
                .WithOne(x => x.Genre)
                .HasForeignKey(x=>x.GenreId)
                .IsRequired(false);
        }
    }
}
