namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;

/// <summary>
///     Value object that uniquely identifies a payment mandate in the Stripe Platform.
/// </summary>
/// <remarks>
///     The reference is generated in another bounded context. Author: Gustavo Alonso Olivares Lao
/// </remarks>
public record StripeReference
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StripeReference" /> value object.
    /// </summary>
    /// <param name="identifier">
    ///     The GUID that uniquely identifies the payment mandate. It must not be empty.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="identifier" /> is an empty GUID.
    /// </exception>
    public StripeReference(Guid identifier)
    {
        if (identifier == Guid.Empty)
            throw new ArgumentException("The stripe reference must not be an empty GUID.", nameof(identifier));
        Identifier = identifier;
    }

    /// <summary>
    ///     Gets the GUID that uniquely identifies the payment mandate.
    /// </summary>
    public Guid Identifier { get; }

    public override string ToString() => Identifier.ToString();
}
