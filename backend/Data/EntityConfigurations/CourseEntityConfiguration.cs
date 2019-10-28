using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data.Enums;

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

            entity.Property(_ => _.level)
            .HasConversion(new EnumToStringConverter<TopicLevel>());
        }
    }
}