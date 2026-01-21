### **Mini Simulation Showcase**

##### **Overview**

**Mini Simulation Showcase** is a Unity project designed to demonstrate software engineering principles applied to real-time simulations.  
The focus is not on gameplay, but on **clean architecture**, **object lifecycle management**, and **performance-conscious design** in Unity.

---

##### **Project Goals**

- Demonstrate clean, maintainable architecture in Unity
- Avoid script interference and tight coupling
- Show understanding of object lifecycle management
- Compare different GameObject instantiation strategies

---

##### **Scenes**

###### Start Scene

The entry point of the project.

- Explains the purpose of the simulation
- Allows selection between two simulation modes:
  - **Static Instantiation Simulation**
  - **Dynamic Lifecycle Simulation**

Designed to give immediate context to the viewer.

---

###### Static Simulation Scene

**Concept:** Objects are instantiated once and reused.

**Characteristics:**

- Objects are spawned a single time
- Random positions within a bounded grid
- No overlapping instantiation
- Grid capacity defines the maximum object count
- No runtime destruction

**What this demonstrates:**

- Predictable memory usage
- Stable performance
- Controlled object reuse

---

###### Dynamic Simulation Scene

**Concept:** Objects are continuously instantiated with lifecycle control.

**Characteristics:**

- Objects spawn continuously over time
- A maximum object limit is enforced
- Oldest objects are destroyed when the limit is reached
- Uses the same grid constraints as the static simulation

**What this demonstrates:**

- Runtime allocation control
- Safe cleanup strategies
- Awareness of performance costs in dynamic systems

---

###### Architecture Overview

The project follows a clear separation of responsibilities:

- **Simulation Controllers** handle high-level logic
- **Grid System** manages spatial constraints and validation
- **SpawnedObject** represents individual entities
- **UI Layer** is isolated from simulation logic
- **Configuration objects** centralize tunable parameters

Systems communicate via **interfaces** to reduce coupling and improve scalability.

---

###### UI Design Philosophy

- Neutral, professional color palette
- Minimal visual effects
- Clear hierarchy and readability
- UI designed to resemble a software tool, not a game menu

---

###### Technologies Used

- Unity 2021 LTS (Built-in Render Pipeline)
- C#
- TextMeshPro

---

###### How to Run

1. Open the project in **Unity 2021 LTS**
2. Open the **StartScene**
3. Press Play
4. Choose a simulation mode

---

###### Notes for Reviewers

This project intentionally avoids complex visuals and gameplay systems.

The focus is on:

- Code organization
- Separation of concerns
- Object lifecycle awareness
- Performance-oriented thinking

---

###### Author

**Vinicius Marcondes Bacca**  
Unity Developer / Software Engineer
