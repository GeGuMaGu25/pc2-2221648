# Stripe Platform

## Description

The **Stripe Platform** is a RESTful API that supports the operations of Stripe, the global financial infrastructure
platform that allows businesses of all sizes to accept payments, manage subscriptions, issue cards, operate connected
accounts and automate revenue flows. This release exposes the endpoint to register **Payment Mandates** in the
`Billing` bounded context.

The application is built with **.NET 10**, **C# 14** and **ASP.NET Core**, following **Domain-Driven Design (DDD)**,
a layered architecture (Domain, Application, Infrastructure and Interfaces) and the **CQRS** pattern.

## Bounded Contexts

* **Billing**: PaymentMandate aggregate, its value objects (`StripeReference`, `AccountId`, `MandateStatus`) and the
  `/api/v1/payment-mandates` endpoint.
* **Shared**: Reusable base elements (aggregate root, result pattern, errors, repositories, unit of work, EF Core
  interceptors, problem details) and the `EffectiveWindow` and `MonetaryAmount` value objects.

## Endpoints

| Method | Route                      | Description                 |
|--------|----------------------------|-----------------------------|
| POST   | `/api/v1/payment-mandates` | Creates a payment mandate.  |

Error responses follow RFC 9457 Problem Details, localized in English (`en`, `en-US`) or Spanish (`es`, `es-PE`)
according to the `Accept-Language` header.

## Technologies

* .NET 10 / C# 14 / ASP.NET Core
* Entity Framework Core 10 with MySQL (`MySql.EntityFrameworkCore`), schema `stripe_ledger`
* Cortex.Mediator (domain events)
* Humanizer (naming conventions)
* OpenAPI and Scalar API Reference

## Getting Started

1. Update the `DefaultConnection` connection string in `appsettings.json`.
2. Run the application: `dotnet run --project PC28150.U202216448.Stripe.Platform`.
3. Pending migrations are applied on startup.
4. Open the API reference at `/scalar/v1` (Development environment).

## Author

* Gustavo Alonso Olivares Lao (U202216448)
