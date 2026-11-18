using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.Events;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Events;

/// <summary>
///     Domain event raised when a new payment mandate is created.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="MandateReference">The mandate reference of the created payment mandate.</param>
/// <param name="MerchantId">The merchant account identifier of the created payment mandate.</param>
public record PaymentMandateCreatedEvent(StripeReference MandateReference, AccountId MerchantId) : IEvent;
