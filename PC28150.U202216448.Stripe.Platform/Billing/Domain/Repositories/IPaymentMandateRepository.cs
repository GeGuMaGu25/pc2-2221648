using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Repositories;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Repositories;

/// <summary>
///     Represents the payment mandate repository in the Stripe Platform.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public interface IPaymentMandateRepository : IBaseRepository<PaymentMandate>
{
    /// <summary>
    ///     Checks whether a payment mandate with the given mandate reference exists.
    /// </summary>
    /// <param name="mandateReference">
    ///     The <see cref="StripeReference" /> mandate reference to search for.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     <c>true</c> if a payment mandate with the mandate reference exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsByMandateReferenceAsync(StripeReference mandateReference,
        CancellationToken cancellationToken);
}
