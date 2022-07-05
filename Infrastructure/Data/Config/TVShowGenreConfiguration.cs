using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class TVShowGenreConfiguration : IEntityTypeConfiguration<TVShowGenre>
{
    public void Configure(EntityTypeBuilder<TVShowGenre> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
    }
}
