namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Resources;

/// <summary>
///     Payment mandate resource for REST API
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="Id">
///     The unique identifier of the payment mandate
/// </param>
/// <param name="MandateReference">
///     The reference that uniquely identifies the payment mandate
/// </param>
/// <param name="MerchantId">
///     The unique identifier of the merchant account
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
public record PaymentMandateResource(
    int Id,
    string MandateReference,
    string MerchantId,
    string ValidityStartDate,
    string ValidityEndDate,
    decimal AuthorizedLimitValue,
    string AuthorizedLimitCurrency);
