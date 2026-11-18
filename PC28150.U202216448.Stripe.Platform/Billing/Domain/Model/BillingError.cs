namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model;

/// <summary>
///     Represents the errors of the Billing bounded context.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public enum BillingError
{
    None,
    PaymentMandateNotFound,
    DuplicateMandateReference,
    InvalidMandateReference,
    InvalidMerchantId,
    InvalidEffectiveWindow,
    InvalidMonetaryAmountValue,
    InvalidMonetaryAmountCurrency,
    InvalidMandateStatus,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
