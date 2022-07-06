using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class TVShowConfiguration : IEntityTypeConfiguration<TVShow> {
    public void Configure(EntityTypeBuilder<TVShow> builder) {
        builder.Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
        builder.Property(p => p.ImageUrl).IsRequired();
        builder.Property(p => p.Year).IsRequired();
        builder.HasOne(g => g.TVShowGenre).WithMany().HasForeignKey(p => p.TVShowGenreId);
    }
}
