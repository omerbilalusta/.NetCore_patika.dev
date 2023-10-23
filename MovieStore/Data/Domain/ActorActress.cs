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
    public class ActorActress : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<Movie_ActorActress> Movie_ActorActress { get; set; }
    }

    public class ActorActressConfiguration : IEntityTypeConfiguration<ActorActress>
    {
        public void Configure(EntityTypeBuilder<ActorActress> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);

            builder.HasMany(x => x.Movie_ActorActress)
                .WithOne(x => x.ActorActress)
                .HasForeignKey(x => x.ActorActressId)
                .IsRequired(false);

        }
    }
}
