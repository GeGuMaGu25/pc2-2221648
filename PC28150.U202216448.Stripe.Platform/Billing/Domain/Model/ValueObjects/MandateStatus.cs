namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;

/// <summary>
///     Represents the status of a payment mandate in the Stripe Platform.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public enum MandateStatus
{
    Pending,
    Active,
    Expired
}
