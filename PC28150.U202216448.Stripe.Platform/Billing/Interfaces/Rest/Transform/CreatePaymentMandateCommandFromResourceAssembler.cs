using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Commands;
using PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Resources;

namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CreatePaymentMandateResource" /> into a
///     <see cref="CreatePaymentMandateCommand" />.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public static class CreatePaymentMandateCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts a <see cref="CreatePaymentMandateResource" /> to a <see cref="CreatePaymentMandateCommand" />.
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreatePaymentMandateResource" /> containing the data for creating a payment mandate.
    ///     Must not be null.
    /// </param>
    /// <returns>
    ///     A new <see cref="CreatePaymentMandateCommand" /> instance.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the input <paramref name="resource" /> is null.</exception>
    public static CreatePaymentMandateCommand ToCommandFromResource(CreatePaymentMandateResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreatePaymentMandateResource cannot be null when converting to command.");
        return new CreatePaymentMandateCommand(
            resource.MandateReference,
            resource.MerchantId,
            resource.ValidityStartDate,
            resource.ValidityEndDate,
            resource.AuthorizedLimitValue,
            resource.AuthorizedLimitCurrency,
            resource.Status,
            resource.Notes
        );
    }
}
