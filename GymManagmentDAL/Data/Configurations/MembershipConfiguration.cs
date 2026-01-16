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
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(x => x.CreatedAt).HasColumnName("StartDate").HasDefaultValueSql("GETDATE()");

            //Many to Many Realtionship
            builder.HasKey(x => new { x.MemberId, x.PlanId });

            //added
            //builder.HasOne(x => x.Member).WithMany(y => y.Memberships).HasForeignKey(x => x.MemberId);
            //builder.HasOne(x => x.Plan).WithMany(y => y.PlanMember).HasForeignKey(x => x.PlanId);
            builder.Ignore(x => x.Id);
        }
    }
}
