**English** | [Русский](README.md)
# Practical Work No. 4: "Template Method" Pattern

## Work Information
- **Topic:** Implementing the behavioral "Template Method" pattern in C#
- **Platform:** Console application (.NET)

## Objective of the Work
To consolidate theoretical knowledge on the topic "Behavioral Design Patterns" and to gain practical skills in implementing the **"Template Method"** pattern in a console application in C#.

## Project Description
A console application **"Order Processing System"** demonstrating the operation of the "Template Method" pattern. The system allows processing two types of orders — **regular** and **premium** — through a single algorithm, while individual steps of the algorithm are overridden in subclasses.

## Project Structure

### 1. `OrderProcessor` (abstract class — template class)
Contains the **template method** `ProcessOrder(decimal amount)`, which defines the skeleton of the algorithm:

1. `ValidateOrder(amount)` — order validation
2. `CalculateDiscount(amount)` — discount calculation
3. `ProcessPayment(amount - discount)` — payment processing
4. `SendNotification()` — notification sending
5. `LogOrder()` — order logging (common method)

**Abstract methods** (implemented in subclasses):
- `ValidateOrder(decimal amount)`
- `CalculateDiscount(decimal amount)`
- `ProcessPayment(decimal amount)`
- `SendNotification()`
- `DisplayName` (property)

**Concrete method** (common to all):
- `LogOrder()` — displays a message about the completion of processing.

### 2. Derived Classes

| Class | Features |
|-------|-------------|
| `RegularOrderProcessor` | Regular order. Discount — 0%. Standard validation and payment. |
| `PremiumOrderProcessor` | Premium order. Discount — 15%. Extended validation, priority payment. |

### 3. `OrderManager` (order manager)
- Stores the list of completed orders (`List<string>`).
- `ProcessOrder(OrderProcessor processor, decimal amount)` — processes an order through the passed processor and saves the information.
- `ShowAllOrders()` — displays all completed orders.
- `ClearAllOrders()` — clears the list.
- `Count` — the current number of orders.

### 4. `Program` (entry point)
- Displays information about the authors and the work.
- Runs the **pattern demonstration** (`DemonstrateTemplateMethod`).
- Implements an **interactive menu** of 5 items.

## Functional Capabilities

### Main Screen
On startup, the following is displayed:
- Last name and first name of the students.
- Group name.
- Name of the practical work.
- Brief description.

### Interactive Menu

| No. | Item | Action |
|---|-------|----------|
| 1 | Place a regular order | Creates a `RegularOrderProcessor`, prompts for the amount, processes the order |
| 2 | Place a premium order | Creates a `PremiumOrderProcessor`, prompts for the amount, processes the order |
| 3 | Show all orders | Calls `ShowAllOrders()` |
| 4 | Clear the order list | Calls `ClearAllOrders()` and displays the current count |
| 5 | Exit | Terminates the application |

### Action Confirmation
After each operation, a message with the current number of orders is displayed, for example:
```
Order of 1500 RUB processed as REGULAR. Total orders: 3
```

### Pattern Demonstration
In the `Main` method, a separate `DemonstrateTemplateMethod()` block:
- Creates instances of `RegularOrderProcessor` and `PremiumOrderProcessor`.
- Through the base class `OrderProcessor`, performs one order with each processor.
- Shows that the client code works identically with different order types thanks to the template method.

## Example of Operation

```
Practical Work No. 4: Implementing the behavioral "Template Method" pattern in C#

----- DEMONSTRATION OF THE PATTERN'S OPERATION -----
The client code works with different order types identically:

=> Processing a REGULAR order through the base class:
  [Regular] Validating order for the amount of 1000 RUB ...
  [Regular] Discount: 0%
  [Regular] Payment of 1000 RUB completed.
  [Regular] Notification: order accepted for processing.
Order processing completed successfully.

=> Processing a PREMIUM order through the base class:
  [Premium] Extended validation of order for the amount of 1000 RUB ...
  [Premium] Discount 15%: 150.00 RUB
  [Premium] Payment of 850.00 RUB completed with priority.
  [Premium] Notification: premium order accepted for processing.
Order processing completed successfully.

=> Conclusion: the same ProcessOrder() call — different behavior of the steps.
----- END OF DEMONSTRATION -----

----- MENU -----
1. Place a regular order
2. Place a premium order
3. Show all orders
4. Clear the order list
5. Exit
Choose an item: 
```

## Compliance with the Assignment Requirements

| Requirement | Status | Implementation |
|------------|--------|------------|
| **No. 1.** "Template Method" pattern | ✅ | Abstract `OrderProcessor` with the template method `ProcessOrder`, two subclasses, common method `LogOrder()` |
| **No. 2.** Business logic (`OrderManager`) | ✅ | List storage, `ProcessOrder`, `ShowAllOrders`, `ClearAllOrders` |
| **No. 3.** Handling of all menu items | ✅ | 5 items, each uses the template method |
| **No. 4.** Action confirmation | ✅ | Message with the current number of orders after processing/clearing |
| **No. 5.** Pattern demonstration | ✅ | `DemonstrateTemplateMethod()` block in `Main` |


**This work corresponds to a grade of "Excellent" (5).**

## How to Run
1. Open the project in Visual Studio / Rider / VS Code.
2. Make sure the .NET SDK is installed.
3. Build and run the project:
   ```bash
   dotnet run
   ```
4. Follow the instructions in the console menu.

## Technologies Used
- **Language:** C#
- **Platform:** .NET (Console Application)
- **Pattern:** Template Method (behavioral)
- **OOP principles:** abstraction, inheritance, polymorphism, encapsulation
