using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;

namespace backend.Data.Configs
{
    public class CourseEntityConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.Property(_ => _.StartTime)
            .HasColumnType("datetime");

            entity.Property(_ => _.EndTime)
            .HasColumnType("datetime");
        }
    }
}