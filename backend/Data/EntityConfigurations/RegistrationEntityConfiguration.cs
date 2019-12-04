using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;

namespace backend.Data.Configs
{
    public class RegistrationEntityConfiguration : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {

            entity.Property(_ => _.StartTime)
            .HasColumnType("datetime");

            entity.Property(_ => _.EndTime)
            .HasColumnType("datetime");
            
        }
    }
}