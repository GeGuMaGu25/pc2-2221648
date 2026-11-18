using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace PC28150.U202216448.Stripe.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}