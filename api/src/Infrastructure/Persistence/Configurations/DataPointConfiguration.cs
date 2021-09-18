using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volue.Domain.Entities;

namespace Volue.Infrastructure.Persistence.Configurations
{
    public class DataPointConfiguration : IEntityTypeConfiguration<DataPoint>
    {
        public void Configure(EntityTypeBuilder<DataPoint> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.HasKey(t => t.TimeStamp);
            builder.HasIndex(t => t.Name);
            builder.Property(t => t.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}