using System.Net.Mime;
using PC28150.U202216448.Stripe.Platform.Billing.Application.CommandServices;
using PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Resources;
using PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest.Transform;
using PC28150.U202216448.Stripe.Platform.Resources.Errors;
using PC28150.U202216448.Stripe.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace PC28150.U202216448.Stripe.Platform.Billing.Interfaces.Rest;

/// <summary>
///     REST controller that exposes the payment mandate endpoints.
/// </summary>
/// <remarks>
///     Author: Gustavo Alonso Olivares Lao
/// </remarks>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Payment Mandate Endpoints.")]
public class PaymentMandatesController(
    IPaymentMandateCommandService paymentMandateCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;

    /// <summary>
    ///     Creates a new payment mandate
    /// </summary>
    /// <param name="resource">
    ///     The <see cref="CreatePaymentMandateResource" /> with the payment mandate information
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The created <see cref="PaymentMandateResource" /> payment mandate, including its generated id
    /// </returns>
    [HttpPost]
    [EndpointSummary("Create a new payment mandate")]
    [EndpointDescription("Create a new payment mandate with the provided information")]
    [ProducesResponseType(typeof(PaymentMandateResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreatePaymentMandate([FromBody] CreatePaymentMandateResource resource,
        CancellationToken cancellationToken)
    {
        var createPaymentMandateCommand =
            CreatePaymentMandateCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await paymentMandateCommandService.Handle(createPaymentMandateCommand, cancellationToken);

        return BillingActionResultAssembler.ToActionResultFromCreatePaymentMandateResult(
            this,
            result,
            _errorLocalizer,
            _problemDetailsFactory,
            createdPaymentMandate => StatusCode(StatusCodes.Status201Created,
                PaymentMandateResourceFromEntityAssembler.ToResourceFromEntity(createdPaymentMandate))
        );
    }
}
