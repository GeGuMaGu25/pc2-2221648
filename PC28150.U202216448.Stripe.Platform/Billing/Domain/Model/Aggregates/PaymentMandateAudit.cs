using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.Entities;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;

/// <summary>
///     Partial class for the <see cref="PaymentMandate" /> aggregate, focusing on audit timestamps.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public partial class PaymentMandate : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
