using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Api.Domain.Entities;

namespace Portfolio.Api.Infra.Data.Mappings
{
    public class ProjectImagesMapping : IEntityTypeConfiguration<ProjectImages>
    {
        public void Configure(EntityTypeBuilder<ProjectImages> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("PKtbprojectimages");
            
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("bigint")
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.Property(x => x.ProjectId)
                .HasColumnName("project_id")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(x => x.ImagePath)
                .HasColumnName("image_path")
                .HasColumnType("nvarchar(max)")
                .IsRequired();
            
            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectImages)
                .HasForeignKey(x => x.ProjectId);
            
            builder.ToTable("tb_project_images", "ptf");
        }
    }
}