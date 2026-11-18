using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Events;
using PC28150.U202216448.Stripe.Platform.Shared.Application.Internal.EventHandlers;

namespace PC28150.U202216448.Stripe.Platform.Billing.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="PaymentMandateCreatedEvent" />.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public class PaymentMandateCreatedEventHandler(ILogger<PaymentMandateCreatedEventHandler> logger)
    : IEventHandler<PaymentMandateCreatedEvent>
{
    /// <inheritdoc />
    public Task Handle(PaymentMandateCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Payment mandate created: Reference {MandateReference} (MerchantID: {MerchantId})",
            domainEvent.MandateReference.Identifier, domainEvent.MerchantId.Value);

        return Task.CompletedTask;
    }
}
