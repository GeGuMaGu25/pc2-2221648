namespace PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Value object that represents an effective window delimited by a start date and an end date.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public record EffectiveWindow
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EffectiveWindow" /> value object.
    /// </summary>
    /// <param name="startDate">
    ///     The date when the window starts.
    /// </param>
    /// <param name="endDate">
    ///     The date when the window ends. It must be later than <paramref name="startDate" />.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="endDate" /> is less than or equal to <paramref name="startDate" />.
    /// </exception>
    public EffectiveWindow(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException(
                $"The end date ({endDate:yyyy-MM-dd}) must be later than the start date ({startDate:yyyy-MM-dd}).",
                nameof(endDate));
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    ///     Gets the date when the window starts.
    /// </summary>
    public DateOnly StartDate { get; }

    /// <summary>
    ///     Gets the date when the window ends.
    /// </summary>
    public DateOnly EndDate { get; }

    /// <summary>
    ///     Checks whether the window is expired at a given date.
    /// </summary>
    /// <param name="checkDate">
    ///     The date to check against the end date of the window.
    /// </param>
    /// <returns>
    ///     <c>true</c> if <paramref name="checkDate" /> is later than <see cref="EndDate" />; otherwise, <c>false</c>.
    /// </returns>
    public bool IsExpired(DateOnly checkDate)
    {
        return checkDate > EndDate;
    }
}
