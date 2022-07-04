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
    internal sealed class MediaConfirguration : IEntityTypeConfiguration<Media>
    {
        /// <summary>
        /// Configuration Table Design
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable(nameof(Media));
            builder.HasKey(media => media.Id);
        }
    }
}
