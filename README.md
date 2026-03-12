# Functional Safety Distance Calculator (ISO 13855 & ISO 13857)

A robust C# .NET 8 Windows Forms application designed for Functional Safety Engineers to calculate the minimum safety distance ($S$) for safeguards (like light curtains and laser scanners) and validate reach-over risks according to international machinery safety standards.

## 🚀 Features

* **ISO 13855 Calculations:** Supports Perpendicular, Parallel, and Angular approach calculations based on the standard formulas ($S = (K \times T) + C$).
* **ISO 13857 Reach-Over Validation:** Implements Table 2 (High Risk) logic to verify if the calculated safety distance is sufficient to prevent reaching the hazard over the protective structure.
* **Normative Constraints:** Built-in *Poka-yoke* validations that throw custom `SafetyDistanceException` alerts if input parameters (like guard heights below 1000mm) fall outside the normative scope.
* **Clean Architecture:** Implements the **Strategy Design Pattern** for seamless scalability and separation of concerns between different approach types.

## 🛠️ Tech Stack

* **Language:** C#
* **Framework:** .NET 8.0 (LTS)
* **UI:** Windows Forms (WinForms)
* **Architecture:** Strategy Pattern

## ⚙️ How to Run

1. Clone this repository to your local machine.
2. Open the `.sln` file using Visual Studio 2022.
3. Build the solution to restore any dependencies.
4. Run the application (`F5`).

## ⚠️ Disclaimer

This software is an engineering tool developed for educational and preliminary calculation purposes. It does not replace a formal risk assessment. Always verify the results against the official, latest versions of the ISO standards and local regulations before implementing safety measures in real-world industrial environments.
