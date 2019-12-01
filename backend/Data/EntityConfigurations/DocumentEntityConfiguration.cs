using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Data.Enums;

namespace backend.Data.Configs
{
    public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> entity)
        {
            entity.Property(_ => _.DocType)
            .HasConversion(new EnumToStringConverter<DocumentType>());
        }
    }
}