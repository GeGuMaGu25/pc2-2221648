using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Events;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.Aggregates.Internal;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;

/// <summary>
///     Represents the PaymentMandate aggregate root within the Billing Bounded Context.
/// </summary>
/// <remarks>
///     This class encapsulates the core data and behavior of a payment mandate. Author: Gustavo Alonso Olivares Lao
/// </remarks>
public partial class PaymentMandate : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PaymentMandate" /> aggregate with default values.
    ///     This constructor is primarily for persistence mechanisms.
    /// </summary>
    public PaymentMandate()
    {
        MandateReference = null!; // Materialized by the persistence mechanism
        MerchantId = null!; // Materialized by the persistence mechanism
        ValidityWindow = null!; // Materialized by the persistence mechanism
        AuthorizedLimit = null!; // Materialized by the persistence mechanism
        Status = MandateStatus.Pending;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PaymentMandate" /> aggregate.
    /// </summary>
    /// <param name="mandateReference">
    ///     The <see cref="StripeReference" /> that uniquely identifies the payment mandate.
    /// </param>
    /// <param name="merchantId">
    ///     The <see cref="AccountId" /> of the merchant.
    /// </param>
    /// <param name="validityWindow">
    ///     The <see cref="EffectiveWindow" /> validity window of the payment mandate.
    /// </param>
    /// <param name="authorizedLimit">
    ///     The <see cref="MonetaryAmount" /> authorized limit of the payment mandate.
    /// </param>
    /// <param name="status">
    ///     The <see cref="MandateStatus" /> of the payment mandate.
    /// </param>
    /// <param name="notes">
    ///     The optional notes of the payment mandate.
    /// </param>
    public PaymentMandate(StripeReference mandateReference, AccountId merchantId, EffectiveWindow validityWindow,
        MonetaryAmount authorizedLimit, MandateStatus status, string? notes)
    {
        ArgumentNullException.ThrowIfNull(mandateReference);
        ArgumentNullException.ThrowIfNull(merchantId);
        ArgumentNullException.ThrowIfNull(validityWindow);
        ArgumentNullException.ThrowIfNull(authorizedLimit);
        MandateReference = mandateReference;
        MerchantId = merchantId;
        ValidityWindow = validityWindow;
        AuthorizedLimit = authorizedLimit;
        Status = status;
        Notes = notes;
        AddDomainEvent(new PaymentMandateCreatedEvent(MandateReference, MerchantId));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PaymentMandate" /> aggregate using primitive values.
    /// </summary>
    /// <param name="mandateReference">The GUID that uniquely identifies the payment mandate.</param>
    /// <param name="merchantId">The GUID of the merchant account.</param>
    /// <param name="validityStartDate">The start date of the validity window.</param>
    /// <param name="validityEndDate">The end date of the validity window.</param>
    /// <param name="authorizedLimitValue">The value of the authorized limit.</param>
    /// <param name="authorizedLimitCurrency">The currency of the authorized limit.</param>
    /// <param name="status">The <see cref="MandateStatus" /> of the payment mandate.</param>
    /// <param name="notes">The optional notes of the payment mandate.</param>
    /// <exception cref="ArgumentException">Thrown when a reference, an id or the validity window is not valid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the authorized limit value is less than zero.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the authorized limit currency is null or blank.</exception>
    public PaymentMandate(Guid mandateReference, Guid merchantId, DateOnly validityStartDate,
        DateOnly validityEndDate, decimal authorizedLimitValue, string? authorizedLimitCurrency,
        MandateStatus status, string? notes)
        : this(new StripeReference(mandateReference), new AccountId(merchantId),
            new EffectiveWindow(validityStartDate, validityEndDate),
            new MonetaryAmount(authorizedLimitValue, authorizedLimitCurrency), status, notes)
    {
    }

    /// <summary>
    ///     Gets the unique identifier of the payment mandate.
    /// </summary>
    public PaymentMandateId Id { get; }

    /// <summary>
    ///     Gets the reference that uniquely identifies the payment mandate.
    /// </summary>
    public StripeReference MandateReference { get; private set; }

    /// <summary>
    ///     Gets the account identifier of the merchant.
    /// </summary>
    public AccountId MerchantId { get; private set; }

    /// <summary>
    ///     Gets the validity window of the payment mandate.
    /// </summary>
    public EffectiveWindow ValidityWindow { get; private set; }

    /// <summary>
    ///     Gets the authorized limit of the payment mandate.
    /// </summary>
    public MonetaryAmount AuthorizedLimit { get; private set; }

    /// <summary>
    ///     Gets the status of the payment mandate.
    /// </summary>
    public MandateStatus Status { get; private set; }

    /// <summary>
    ///     Gets the optional notes of the payment mandate.
    /// </summary>
    public string? Notes { get; private set; }
}
