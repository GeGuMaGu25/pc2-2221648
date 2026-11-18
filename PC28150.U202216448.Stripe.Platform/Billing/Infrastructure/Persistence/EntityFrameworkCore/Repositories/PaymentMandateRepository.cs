using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Repositories;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using PC28150.U202216448.Stripe.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PC28150.U202216448.Stripe.Platform.Billing.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the payment mandate repository in the Stripe Platform.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class PaymentMandateRepository(AppDbContext context)
    : BaseRepository<PaymentMandate>(context), IPaymentMandateRepository
{
    /// <inheritdoc />
    public async Task<bool> ExistsByMandateReferenceAsync(StripeReference mandateReference,
        CancellationToken cancellationToken)
    {
        return await Context.Set<PaymentMandate>().AnyAsync(
            paymentMandate => paymentMandate.MandateReference.Identifier == mandateReference.Identifier,
            cancellationToken);
    }
}
