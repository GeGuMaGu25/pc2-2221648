namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;

/// <summary>
///     Value object that identifies a merchant account in the Stripe Platform.
/// </summary>
/// <remarks>
///     The identifier is originated in another bounded context. Author: Gustavo Alonso Olivares Lao
/// </remarks>
public record AccountId
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AccountId" /> value object.
    /// </summary>
    /// <param name="value">
    ///     The GUID value of the account. It must not be empty.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="value" /> is an empty GUID.
    /// </exception>
    public AccountId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("The account id must not be an empty GUID.", nameof(value));
        Value = value;
    }

    /// <summary>
    ///     Gets the GUID value of the account.
    /// </summary>
    public Guid Value { get; }

    public override string ToString() => Value.ToString();
}
