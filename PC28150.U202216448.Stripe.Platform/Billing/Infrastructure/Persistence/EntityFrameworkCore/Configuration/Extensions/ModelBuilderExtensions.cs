using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace PC28150.U202216448.Stripe.Platform.Billing.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

/// <summary>
///     Model builder extensions for the Billing bounded context.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public static class ModelBuilderExtensions
{
    /// <summary>
    ///     Applies the Billing bounded context persistence configuration.
    /// </summary>
    /// <param name="builder">
    ///     The model builder for the database context
    /// </param>
    public static void ApplyBillingConfiguration(this ModelBuilder builder)
    {
        // Billing Context

        builder.Entity<PaymentMandate>().HasKey(p => p.Id);
        builder.Entity<PaymentMandate>().Property(p => p.Id)
            .HasConversion(new StronglyTypedIdConverter<PaymentMandateId, int>(id => id.Value,
                value => new PaymentMandateId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Entity<PaymentMandate>().OwnsOne(p => p.MandateReference,
            m =>
            {
                m.WithOwner().HasForeignKey("Id");
                m.Property(r => r.Identifier).HasColumnName("MandateReference").IsRequired();
                m.HasIndex(r => r.Identifier).IsUnique();
            });
        builder.Entity<PaymentMandate>().Navigation(p => p.MandateReference).IsRequired();

        builder.Entity<PaymentMandate>().OwnsOne(p => p.MerchantId,
            m =>
            {
                m.WithOwner().HasForeignKey("Id");
                m.Property(a => a.Value).HasColumnName("MerchantId").IsRequired();
            });
        builder.Entity<PaymentMandate>().Navigation(p => p.MerchantId).IsRequired();

        builder.Entity<PaymentMandate>().OwnsOne(p => p.ValidityWindow,
            v =>
            {
                v.WithOwner().HasForeignKey("Id");
                v.Property(w => w.StartDate).HasColumnName("ValidityStartDate").IsRequired();
                v.Property(w => w.EndDate).HasColumnName("ValidityEndDate").IsRequired();
            });
        builder.Entity<PaymentMandate>().Navigation(p => p.ValidityWindow).IsRequired();

        builder.Entity<PaymentMandate>().OwnsOne(p => p.AuthorizedLimit,
            a =>
            {
                a.WithOwner().HasForeignKey("Id");
                a.Property(m => m.Value).HasColumnName("AuthorizedLimitValue").HasPrecision(18, 2).IsRequired();
                a.Property(m => m.Currency).HasColumnName("AuthorizedLimitCurrency").IsRequired();
            });
        builder.Entity<PaymentMandate>().Navigation(p => p.AuthorizedLimit).IsRequired();

        builder.Entity<PaymentMandate>().Property(p => p.Status).IsRequired();
        builder.Entity<PaymentMandate>().Property(p => p.Notes).IsRequired(false);
    }
}
