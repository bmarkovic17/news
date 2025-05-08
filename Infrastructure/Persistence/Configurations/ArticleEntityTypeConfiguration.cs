using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewsApp.Core.Domain.Entities.ArticleEntity;

namespace NewsApp.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the Entity Framework Core mapping for the Article entity.
/// Defines database schema, constraints, and relationships.
/// </summary>
internal sealed class ArticleEntityTypeConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("article", "news");

        builder
            .Property(article => article.Id)
            .UseHiLo("article_id_sequence", "news");

        builder
            .Property<int>("UserId")
            .IsRequired();

        builder.ComplexProperty(
            article => article.Title,
            propertyBuilder => propertyBuilder
                .Property(title => title.Value)
                .HasColumnName("title")
                .HasMaxLength(50));

        builder.ComplexProperty(
            article => article.Content,
            propertyBuilder => propertyBuilder
                .Property(content => content.Value)
                .HasColumnName("content"));
    }
}
