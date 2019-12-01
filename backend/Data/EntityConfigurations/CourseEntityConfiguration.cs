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
            entity.Property(_ => _.Level)
            .HasConversion(new EnumToStringConverter<TopicLevel>());

            entity.Property(_ => _.SoftDeleted)
            .HasDefaultValue(false);
        }
    }
}