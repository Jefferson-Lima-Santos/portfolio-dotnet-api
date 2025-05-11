using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Api.Domain.Entities;

namespace Portfolio.Api.Infra.Data.Mappings
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PKtbproject");
            
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(x => x.ProjectUId)
                .HasColumnName("ProjectUId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();
            
            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.Subtitle)
                .HasColumnName("subtitle")
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar")
                .HasMaxLength(1000)
                .IsRequired();
            
            builder.Property(x => x.GitHubURL)
                .HasColumnName("github_url")
                .HasColumnType("nvarchar")
                .HasMaxLength(1000)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .HasColumnName("is_deleted")
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();
            
            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("datetimeoffset")
                .HasDefaultValueSql("(getdate())")
                .IsRequired();
            
            builder.Property(x => x.DeletedAt)
                .HasColumnName("deleted_at")
                .HasColumnType("datetimeoffset")
                .HasDefaultValueSql("(getdate())")
                .IsRequired(false);

            builder.ToTable("tb_project", "ptf");
        }
    }
}