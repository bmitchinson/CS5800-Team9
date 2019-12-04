using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;

namespace backend.Data.Configs
{
    public class InstructorEntityConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> entity)
        {
            entity.Property(_ => _.BirthDate)
                .HasColumnType("date");
        }
    }
}