using App.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructre.EntitiesConfigurations
{
    internal sealed class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        /// <summary>
        /// Configuration Table Design
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable(nameof(Users));
            builder.HasKey(users => users.Id);
            builder.Property(users => users.JoiningDate).HasDefaultValueSql("getdate()");
        }
    }
}
