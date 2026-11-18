namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Commands;

/// <summary>
///     Command to create a payment mandate.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="MandateReference">
///     The GUID that uniquely identifies the payment mandate.
/// </param>
/// <param name="MerchantId">
///     The GUID of the merchant account.
/// </param>
/// <param name="ValidityStartDate">
///     The start date of the validity window.
/// </param>
/// <param name="ValidityEndDate">
///     The end date of the validity window.
/// </param>
/// <param name="AuthorizedLimitValue">
///     The value of the authorized limit.
/// </param>
/// <param name="AuthorizedLimitCurrency">
///     The currency of the authorized limit.
/// </param>
/// <param name="Status">
///     The status of the payment mandate. When it is not specified, Pending is used.
/// </param>
/// <param name="Notes">
///     The optional notes of the payment mandate.
/// </param>
public record CreatePaymentMandateCommand(
    Guid MandateReference,
    Guid MerchantId,
    DateOnly ValidityStartDate,
    DateOnly ValidityEndDate,
    decimal AuthorizedLimitValue,
    string? AuthorizedLimitCurrency,
    string? Status,
    string? Notes);
