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
    public class Movie : BaseModel
    {
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }

        public virtual List<Movie_ActorActress> Movie_ActorActress { get; set; }
        public virtual List<Movie_Order> Movie_Orders { get; set; }
    }

    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);
            builder.Property(x => x.PublishDate).IsRequired();

            builder.HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId)
                .IsRequired(true);

            builder.HasMany(x=>x.Movie_ActorActress)
                .WithOne(x=>x.Movie)
                .HasForeignKey(x=>x.MovieId)
                .IsRequired(true);

            builder.HasMany(x => x.Movie_Orders)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .IsRequired(false);
        }
    }

}
