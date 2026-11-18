using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Resources.Errors;
using PC28150.U202216448.Stripe.Platform.Shared.Application.Model;
using PC28150.U202216448.Stripe.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming Billing results into HTTP action results.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public static class BillingActionResultAssembler
{
    // --- Helper for transforming BillingError to StatusCode ---
    private static int ToStatusCodeFromBillingError(BillingError error)
    {
        return error switch
        {
            BillingError.PaymentMandateNotFound => StatusCodes.Status404NotFound,
            BillingError.DuplicateMandateReference => StatusCodes.Status409Conflict,
            BillingError.InvalidMandateReference => StatusCodes.Status400BadRequest,
            BillingError.InvalidMerchantId => StatusCodes.Status400BadRequest,
            BillingError.InvalidEffectiveWindow => StatusCodes.Status400BadRequest,
            BillingError.InvalidMonetaryAmountValue => StatusCodes.Status400BadRequest,
            BillingError.InvalidMonetaryAmountCurrency => StatusCodes.Status400BadRequest,
            BillingError.InvalidMandateStatus => StatusCodes.Status400BadRequest,
            BillingError.OperationCancelled => StatusCodes.Status409Conflict,
            BillingError.DatabaseError => StatusCodes.Status500InternalServerError,
            BillingError.InternalServerError => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status400BadRequest // Default
        };
    }

    // --- Specific Assembler Methods ---

    /// <summary>
    ///     Converts the result of the create payment mandate command into an action result.
    /// </summary>
    /// <param name="controller">The controller that handles the request.</param>
    /// <param name="result">The <see cref="Result{T}" /> of the create payment mandate command.</param>
    /// <param name="errorLocalizer">The localizer for error messages.</param>
    /// <param name="problemDetailsFactory">The factory to create problem details responses.</param>
    /// <param name="successAction">The action to execute when the result is successful.</param>
    /// <returns>The <see cref="IActionResult" /> for the request.</returns>
    public static IActionResult ToActionResultFromCreatePaymentMandateResult(
        ControllerBase controller,
        Result<PaymentMandate> result,
        IStringLocalizer<ErrorMessages> errorLocalizer,
        ProblemDetailsFactory problemDetailsFactory,
        Func<PaymentMandate, IActionResult> successAction)
    {
        if (result.IsSuccess) return successAction(result.Value!);

        var error = (BillingError)result.Error!;
        var statusCode = ToStatusCodeFromBillingError(error);
        // Localized message according to the Accept-Language header
        var localizedMessage = errorLocalizer[$"{nameof(BillingError)}.{error}"];
        return problemDetailsFactory.CreateProblemDetails(controller, statusCode, result.Error, localizedMessage);
    }
}
