using PC28150.U202216448.Stripe.Platform.Shared.Domain.Model.Errors;

namespace PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Errors;

/// <summary>
///     Catalog of domain errors of the Billing bounded context.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public static class BillingErrors
{
    public static readonly Error PaymentMandateNotFound =
        Error.NotFound("Billing.PaymentMandateNotFound", "The specified payment mandate was not found.");

    public static readonly Error DuplicateMandateReference =
        Error.Conflict("Billing.DuplicateMandateReference",
            "A payment mandate with the same mandate reference already exists.");

    public static readonly Error InvalidMandateReference =
        Error.Validation("Billing.InvalidMandateReference",
            "The mandate reference must be a non-empty GUID.");

    public static readonly Error InvalidMerchantId =
        Error.Validation("Billing.InvalidMerchantId",
            "The merchant id must be a non-empty GUID.");

    public static readonly Error InvalidEffectiveWindow =
        Error.Validation("Billing.InvalidEffectiveWindow",
            "The validity end date must be later than the validity start date.");

    public static readonly Error InvalidMonetaryAmountValue =
        Error.Validation("Billing.InvalidMonetaryAmountValue",
            "The authorized limit value must be greater than or equal to zero.");

    public static readonly Error InvalidMonetaryAmountCurrency =
        Error.Validation("Billing.InvalidMonetaryAmountCurrency",
            "The authorized limit currency must not be null or blank.");

    public static readonly Error InvalidMandateStatus =
        Error.Validation("Billing.InvalidMandateStatus",
            "The mandate status is not valid. Allowed values are Pending, Active or Expired.");

    public static Error DuplicateMandateReferenceWithReference(Guid mandateReference) =>
        Error.Conflict("Billing.DuplicateMandateReference",
            $"A payment mandate with the mandate reference '{mandateReference}' already exists.");
}
