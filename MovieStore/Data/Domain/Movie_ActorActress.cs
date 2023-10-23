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
    public class Movie_ActorActress : BaseModel
    {
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int ActorActressId { get; set; }
        public virtual ActorActress ActorActress { get; set; }
    }

    public class Movie_ActorActressConfiguration : IEntityTypeConfiguration<Movie_ActorActress>
    {
        public void Configure(EntityTypeBuilder<Movie_ActorActress> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.MovieId).IsRequired();
            builder.Property(x => x.ActorActressId).IsRequired();
        }
    }

}
