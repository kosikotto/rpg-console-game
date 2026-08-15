# ⚔️ Console RPG – Shakes & Fidget Style

This project is a console-based role-playing game inspired by the popular Shakes & Fidget universe. Although the interface is minimalist, under the hood, a complex, enterprise-ready software architecture ensures smooth operation.

---

## 🏗️ Architectural Solutions

The main focus of the development was not only on gameplay but also on applying Clean Code principles and the separation of concerns:

*   **Layered Architecture:**
    *   **Domain:** Business logic and game entities.
    *   **Application:** Gameplay flows and services.
    *   **Infrastructure:** Data access and external connections.
    *   **UI (Presentation):** The layer responsible for the console display.
*   **Dependency Injection (DI):** Implemented to ensure loose coupling between components.
*   **Data Management:** Utilizing Entity Framework with a DB-First approach, backed by an MSSQL database.

---

## 🎮 Key Features

*   **Modular structure:** Logic, data access, and presentation are strictly separated, allowing any layer to be replaced without modifying the others.
*   **Versioned database:** Game state and character data are securely stored in an MSSQL database.
*   **Inspired gameplay:** Character progression, quests, and statistics based on classic RPG mechanics.

---

## ⚠️ Known Issues and Ongoing Development

The project is currently under active development. At the moment, the save and load functionality is limited:

*   **Cause of the issue:** Due to the lack of file system validation logic (checking for folder and file existence), the program fails to find the write path in certain environments.
*   **Status:** Fixing this bug and fine-tuning I/O handling is part of the next development cycle.

---

## 🛠️ Technologies

*   **Language:** C# / .NET
*   **Database:** MSSQL
*   **ORM:** Entity Framework
*   **Patterns:** Repository Pattern, Dependency Injection, Layered Architecture

---

## 💻 Running the Project

Running the project is extremely simple and requires no complex configuration:

1. Clone the repository to your local machine.
2. Open the solution file (e.g., in Visual Studio).
3. Press the "Play" (Run) button, and the console application will launch immediately.
