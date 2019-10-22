using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;

namespace backend.Data.Configs
{
    public class RegistrationEntityConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            // entity.HasKey(_ => _.RegistrationId);

            // entity
            //     .HasOne(_ => _.Student)
            //     .WithMany(_ => _.Registrations)
            //     .HasForeignKey(_ => _.StudentId);

            // entity
            //     .HasOne(_ => _.Instructor)
            //     .WithMany(_ => _.Registrations)
            //     .HasForeignKey(_ => _.InstructorId);

            // entity
            //     .HasOne(_ => _.Course)
            //     .WithMany(_ => _.Registrations)
            //     .HasForeignKey(_ => _.CourseId);
        }
    }
}