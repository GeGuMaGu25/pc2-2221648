namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Resources;

/// <summary>
///     Create payment mandate resource for REST API
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="MandateReference">
///     The unique identifier (GUID) of the payment mandate
/// </param>
/// <param name="MerchantId">
///     The unique identifier (GUID) of the merchant account
/// </param>
/// <param name="ValidityStartDate">
///     The start date of the validity window
/// </param>
/// <param name="ValidityEndDate">
///     The end date of the validity window
/// </param>
/// <param name="AuthorizedLimitValue">
///     The value of the authorized limit
/// </param>
/// <param name="AuthorizedLimitCurrency">
///     The currency of the authorized limit
/// </param>
/// <param name="Status">
///     The optional status of the payment mandate (Pending, Active or Expired). Pending by default
/// </param>
/// <param name="Notes">
///     The optional notes of the payment mandate
/// </param>
public record CreatePaymentMandateResource(
    Guid MandateReference,
    Guid MerchantId,
    DateOnly ValidityStartDate,
    DateOnly ValidityEndDate,
    decimal AuthorizedLimitValue,
    string? AuthorizedLimitCurrency,
    string? Status,
    string? Notes);
