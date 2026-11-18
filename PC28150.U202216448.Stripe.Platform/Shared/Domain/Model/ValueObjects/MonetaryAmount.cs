namespace PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Value object that represents a monetary amount expressed in a specific currency.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public record MonetaryAmount
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MonetaryAmount" /> value object.
    /// </summary>
    /// <param name="value">
    ///     The amount value. It must be greater than or equal to zero.
    /// </param>
    /// <param name="currency">
    ///     The currency of the amount. It must not be null or blank.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown when <paramref name="value" /> is less than zero.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when <paramref name="currency" /> is null or blank.
    /// </exception>
    public MonetaryAmount(decimal value, string? currency)
    {
        if (value < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(value), value,
                "The monetary amount value must be greater than or equal to zero.");
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentNullException(nameof(currency),
                "The monetary amount currency must not be null or blank.");
        Value = value;
        Currency = currency;
    }

    /// <summary>
    ///     Gets the amount value.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    ///     Gets the currency of the amount.
    /// </summary>
    public string Currency { get; }

    /// <summary>
    ///     Multiplies the amount value by a factor.
    /// </summary>
    /// <param name="factor">
    ///     The factor to multiply the amount value by.
    /// </param>
    /// <returns>
    ///     A new <see cref="MonetaryAmount" /> with the resulting value and the same currency.
    /// </returns>
    public MonetaryAmount Multiply(decimal factor)
    {
        return new MonetaryAmount(Value * factor, Currency);
    }
}
