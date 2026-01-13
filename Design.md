# American Football League (AFL) - Design Document

## 1. Project Overview
A text-based simulation management game starting in the year 1950. The user acts as the "Commissioner" and "General Manager," overseeing the league, simulating games, and managing rosters.

**Core Pillars:**
**Historical Immersion** Starting in 1950 with appropriate names, lack of modern tech, and era-specific rules.
**Statistical Depth:** Play outcomes are determined by player attributes (0=100 scale) + logic + RNG.
**Progression:** Players age, gain experience, suffer injuries, and eventually retire.

##2. Technical Stack
**Language:** C# (.NET 8.0)
**Interface:** Console Application (Phase 1) -> Desktop/Web UI (Phase 2).
**Data Storage:** SQLite (local .db. file) or JSON serialization for early prototyping.

##3. Data Structures (The "Classes")

### A. Player
* **Bio:** ID, FirstName, LastName, Age, Position (QB, RB, WR, OL, DL, LB, DB, K/P).
* **Attributes (0-100):**
    * *Physical:* Speed, Strength, Agility, Durability
    * *Technical:* ThrowingAccuracy, Catching, Blocking, Tackling, Kicking.
    * *Mental:* Awareness, Clutch, Morale.
    * **Distribution Rule:** Rookie ratings follow a weighted curve.
        * Cap: Raw rookie cannot exceed 85 OVR.
        * Rarity: 80-85 is "Generational" (<1% chance).
        * Average: Most rookies land between 60-70.
        * Progression: 90+ ratings are earned through "Experience" and "Playtime."
* **Status:** CurrentTeamID, IsInjured, WeeksInjured.

### B. Team
* **Identity:** Name, City, Mascot, Conference (East, West).
* **Roster:** List<Player>.
* **Strategy:** Run/Pass Ratio (e.g., 1950s teams run 70% of the time).
* **Stats:** Wins, Losses, PointsFor, PointsAgainst.

### C. League
* **State:** CurrentYear (Starts 1950), CurrentWeek (1-12).
* **History:** List of past Champions (Hall of Champions).

## 4. The Simulation Engine (The "Brain")

### The Game Loop
1. **Pre-Snap:** Determine posession, down, distance, and field position.
2. **Play Call:**
    * Offense Picks `PlayType` (Run Inside, Run Outside, Short Pass, Long Pass, Punt, Field Goal).
    * Defense Picks `DefensiveSet` (4-3 Base, Blitz, Dime).
3.  ** Resolution (The Math):**
    * Calculate `OffensiveAdvantage` vs `DefensiveAdvantage` basd on player ratings involved in the play.
    * Apply `RNG` (Random Number Generator) variance.
4.  **Result:** Update down, Distance, Field Position, Score, and Time.

## 5. Roadmap
* **v0.1:** Create Classes and a "One Play" simulator in console
* **v.0.2:** Simulate a full Drive (series of plays).
* **v.0.3:** Simulate a full Game (4 quarters).
* **v.0.4:** Full Season Loop wiht 16 teams and draft

## 6. TODO
* ** Player Names need to be more diverse, they should be unique.
* ** Logic should weigh the Coach, and the Players Ratings for plays and defense calls

