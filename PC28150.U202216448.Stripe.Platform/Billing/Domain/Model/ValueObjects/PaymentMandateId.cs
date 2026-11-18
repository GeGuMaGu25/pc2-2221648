using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.ValueObjects;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a payment mandate in the Billing bounded context.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct PaymentMandateId(int Value) : IEntityId<int>
{
    public static implicit operator int(PaymentMandateId id) => id.Value;
    public static implicit operator PaymentMandateId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
