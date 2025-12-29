using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(x => x.Phone).HasColumnType("varchar").HasMaxLength(11);

            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymUserValidEmailCheck", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone like '01%' and phone not like '%[^0-9]%'");
            });

            //helps optimize database query performance for filtering and sorting operations.
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();


            builder.OwnsOne(x => x.Address, addressBuilder =>
            {
                addressBuilder.Property(x => x.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);
                addressBuilder.Property(x => x.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);
                addressBuilder.Property(x => x.BuildingNo).HasColumnName("BuildingNo");
            });
        }
    }
}
