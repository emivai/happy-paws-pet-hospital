﻿using HappyPaws.Core.Entities;
using HappyPaws.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HappyPaws.Infrastructure.Persistence.Configurations
{
    public class AppointmentEntityTypeConfiguration : BaseEntityTypeConfiguration<Appointment>
    {
        public override void Configure(EntityTypeBuilder<Appointment> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Price).HasColumnName("price");

            builder.Property(p => p.Status).HasColumnName("status").HasConversion<int>();

            builder.Property(p => p.PetId).HasColumnName("pet_id");

            builder.Property(p => p.TimeSlotId).HasColumnName("time_slot_id");

            builder.HasOne(e => e.Pet).WithMany().HasForeignKey(e => e.PetId);

            builder
                .HasOne(e => e.TimeSlot)
                .WithOne()
                .HasForeignKey<TimeSlot>(e => e.Id)
                .IsRequired();
        }
    }
}