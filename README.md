# 📡 SMS_API

This project was developed during my internship and it's a robust SMS sending API built using **ASP.NET Core**, supporting three different external providers: **Twilio**, **SMSApi**, and **SMSBulk**. It follows a modular and layered architecture, ensuring easy scalability, testing, and clean separation of concerns. It also includes logging of all sent messages and a background task to manage dynamic operations such as cost updates.

---

## 🛠 Technologies Used

- **ASP.NET Core 6.0**
- **Entity Framework Core** (for database access and migrations)
- **Twilio API**
- **SmsApi API**
- **SMSBulk API**
- **Razor Pages** (for web frontend)
- **SQL Server** (used with EF Core)

---

## 🧱 Solution Structure

The solution is divided into three main projects:

### `SMSAPI` (Main Backend API)

| Layer            | Description |
|------------------|-------------|
| **Controllers**   | Handle HTTP requests. Each controller targets a specific provider (`SmsTwillioController`, `SmsBulkController`, etc.). |
| **Services**      | Encapsulate logic to interact with each SMS provider (`SmsTwillio`, `SmsApi`, `SmsBulk`) and track sent messages via `MessageTracking`. |
| **Models**        | DTOs for data transfer between frontend/backend and service layers. E.g., `SmsMessageModel`, `SmsServiceMessageResultModel`. |
| **Entities**      | Contains the `SmsLog` entity used to persist data in the database. |
| **Contexts**      | `SmsContext` represents the EF Core database context and handles migrations. |
| **Middleware**    | `TimerUpdateCostServices` is a background service that executes periodic tasks such as cost updates. |
| **BL** (Business Layer) | Provides business logic interfaces and implementations like `IMessages` and `Messages`. |
| **Migrations**    | EF Core database schema migrations. |

### `SMSAPIWEB` (Web Frontend)

- A lightweight web interface built with Razor Pages (`Index.cshtml`, `Messages.cshtml`, etc.).
- Allows users to send messages and view logs.
- Uses basic models like `MessageInfo` and `SmsLog` for binding with the backend.

### `SmsUI` (Desktop App)

- WinForms-based GUI to send SMS from a Windows application.
- Uses `SmsTwillioMessageModel` for message structure.
- `Form1.cs` and `Form2.cs` manage the interface and send logic.

---

## 🧠 Architecture & Design Patterns

### **MVC-Inspired Design**

The project loosely follows the **Model-View-Controller** pattern:
- **Controllers** handle routing and input.
- **Services** handle logic and external API interaction.
- **Models/Entities** represent data structures for processing and storage.

### **Dependency Injection & Interfaces**

All services (`ISmsTwillio`, `ISmsApi`, etc.) are injected via interfaces, adhering to the **Dependency Inversion Principle (SOLID-D)**. This makes the code easily testable and maintainable.

### **Strategy Pattern**

Each SMS provider implements a different strategy via its own service class. This is an example of the **Strategy Pattern**, where the behavior (sending SMS) is encapsulated and interchangeable.

### **Layered Architecture**

The solution cleanly separates:
- **Presentation (Web/Desktop)**
- **Application Logic (Controllers & Services)**
- **Domain Logic (BL/Entities)**
- **Data Access (EF Core)**

---

## 🔄 How the API Works

1. A client sends an HTTP request to one of the endpoints (`/api/twillio/send`, `/api/bulk/send`, etc.).
2. The respective controller receives the request and validates input.
3. The appropriate service (`ISmsTwillio`, `ISmsBulk`, etc.) is injected and called.
4. The service communicates with the external SMS provider.
5. The result is returned as a `SmsServiceMessageResultModel`.
6. The message is logged to the database via EF Core.
7. `TimerUpdateCostServices` may periodically update balance or stats in the background.

---

## 🧪 API Endpoints (Examples)

```http
POST /api/twillio/send
POST /api/bulk/send
GET  /api/smslog
