using System.Globalization;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Resources;

namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="PaymentMandate" /> domain aggregate into a
///     <see cref="PaymentMandateResource" /> for REST representation.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public static class PaymentMandateResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts a <see cref="PaymentMandate" /> domain aggregate to its <see cref="PaymentMandateResource" />
    ///     representation.
    /// </summary>
    /// <param name="entity">
    ///     The <see cref="PaymentMandate" /> aggregate to convert. Must not be null.
    /// </param>
    /// <returns>
    ///     A <see cref="PaymentMandateResource" /> object representing the provided payment mandate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="entity" /> is null.</exception>
    public static PaymentMandateResource ToResourceFromEntity(PaymentMandate entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "PaymentMandate entity cannot be null when converting to resource.");
        return new PaymentMandateResource(
            entity.Id,
            entity.MandateReference.ToString(),
            entity.MerchantId.ToString(),
            entity.ValidityWindow.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            entity.ValidityWindow.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            entity.AuthorizedLimit.Value,
            entity.AuthorizedLimit.Currency);
    }
}
