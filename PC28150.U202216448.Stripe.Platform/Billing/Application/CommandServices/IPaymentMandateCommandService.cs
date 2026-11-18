using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Aggregates;
using PC28150.U202216448.Stripe.Platform.Billing.Domain.Model.Commands;
using PC28150.U202216448.Stripe.Platform.Shared.Application.Model;

namespace PC28150.U202216448.Stripe.Platform.Billing.Application.CommandServices;

/// <summary>
///     Represents the payment mandate command service in the Stripe Platform.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
public interface IPaymentMandateCommandService
{
    /// <summary>
    ///     Handles the create payment mandate command in the Stripe Platform.
    /// </summary>
    /// <param name="command">
    ///     The <see cref="CreatePaymentMandateCommand" /> command to handle.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A <see cref="Result{T}" /> with the created <see cref="PaymentMandate" /> or the error found.
    /// </returns>
    Task<Result<PaymentMandate>> Handle(CreatePaymentMandateCommand command, CancellationToken cancellationToken);
}
