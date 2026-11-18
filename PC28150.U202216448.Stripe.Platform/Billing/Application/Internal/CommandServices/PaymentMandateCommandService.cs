using PC28150.U202216448.Stripe.Platform.Billing.Application.CommandServices;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Commands;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Errors;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.ValueObjects;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Repositories;
using PC28150.U202216448.Stripe.Platform.Shared.Application.Model;
using PC28150.U202216448.Stripe.Platform.Shared.Domain.Repositories;

namespace PC28150.U202216448.Stripe.Platform.Billing.Application.Internal.CommandServices;

/// <summary>
///     Represents the payment mandate command service in the Stripe Platform.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
/// <param name="paymentMandateRepository">
///     The <see cref="IPaymentMandateRepository" /> to use.
/// </param>
/// <param name="unitOfWork">
///     The <see cref="IUnitOfWork" /> to use.
/// </param>
public class PaymentMandateCommandService(
    IPaymentMandateRepository paymentMandateRepository,
    IUnitOfWork unitOfWork)
    : IPaymentMandateCommandService
{
    /// <inheritdoc />
    public async Task<Result<PaymentMandate>> Handle(CreatePaymentMandateCommand command,
        CancellationToken cancellationToken)
    {
        if (!TryParseStatus(command.Status, out var status))
            return Result<PaymentMandate>.Failure(BillingErrors.InvalidMandateStatus,
                BillingError.InvalidMandateStatus);

        PaymentMandate paymentMandate;
        try
        {
            paymentMandate = new PaymentMandate(command.MandateReference, command.MerchantId,
                command.ValidityStartDate, command.ValidityEndDate, command.AuthorizedLimitValue,
                command.AuthorizedLimitCurrency, status, command.Notes);
        }
        catch (ArgumentOutOfRangeException)
        {
            return Result<PaymentMandate>.Failure(BillingErrors.InvalidMonetaryAmountValue,
                BillingError.InvalidMonetaryAmountValue);
        }
        catch (ArgumentNullException)
        {
            return Result<PaymentMandate>.Failure(BillingErrors.InvalidMonetaryAmountCurrency,
                BillingError.InvalidMonetaryAmountCurrency);
        }
        catch (ArgumentException exception)
        {
            return exception.ParamName switch
            {
                "identifier" => Result<PaymentMandate>.Failure(BillingErrors.InvalidMandateReference,
                    BillingError.InvalidMandateReference),
                "value" => Result<PaymentMandate>.Failure(BillingErrors.InvalidMerchantId,
                    BillingError.InvalidMerchantId),
                _ => Result<PaymentMandate>.Failure(BillingErrors.InvalidEffectiveWindow,
                    BillingError.InvalidEffectiveWindow)
            };
        }

        if (await paymentMandateRepository.ExistsByMandateReferenceAsync(paymentMandate.MandateReference,
                cancellationToken))
            return Result<PaymentMandate>.Failure(
                BillingErrors.DuplicateMandateReferenceWithReference(command.MandateReference),
                BillingError.DuplicateMandateReference);

        await paymentMandateRepository.AddAsync(paymentMandate, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Return the created payment mandate
        return Result<PaymentMandate>.Success(paymentMandate);
    }

    /// <summary>
    ///     Converts the status text into a <see cref="MandateStatus" />, using Pending when it is not specified.
    /// </summary>
    /// <param name="statusText">The status text to convert.</param>
    /// <param name="status">The resulting <see cref="MandateStatus" />.</param>
    /// <returns><c>true</c> if the status text is valid or not specified; otherwise, <c>false</c>.</returns>
    private static bool TryParseStatus(string? statusText, out MandateStatus status)
    {
        status = MandateStatus.Pending;
        if (string.IsNullOrWhiteSpace(statusText)) return true;
        var statusName = Enum.GetNames<MandateStatus>()
            .FirstOrDefault(name => name.Equals(statusText.Trim(), StringComparison.OrdinalIgnoreCase));
        return statusName is not null && Enum.TryParse(statusName, out status);
    }
}
