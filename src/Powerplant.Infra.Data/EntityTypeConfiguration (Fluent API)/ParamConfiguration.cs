using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Powerplant.Core.Domain.Model;

namespace Powerplant.Infra.Data.EntityConfig
{
    public class ParamConfiguration : IEntityTypeConfiguration<ParamModel>
    {
        public void Configure(EntityTypeBuilder<ParamModel> builder)
        {
            builder.ToTable("Params");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Key)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
