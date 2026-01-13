# American Football League (AFL) - Design Document

## 1. Project Overview
A text-based simulation management game starting in the year 1950. The user acts as the "Commissioner" and "General Manager," overseeing the league, simulating games, and managing rosters.

**Core Pillars:**
* **Historical Immersion:** Starting in 1950 with appropriate names, lack of modern tech, and era-specific rules.
* **Statistical Depth:** Play outcomes are determined by individual player attributes (0-100 scale) + tactical logic + RNG.
* **Progression:** Players age, gain experience, suffer injuries, and eventually retire.
* **Living History:** The game tracks historical stats, records, and Hall of Fame inductions (OOTP style).

## 2. Technical Stack
* **Language:** C# (.NET 8.0)
* **Interface:** Console Application (Phase 1) -> Desktop/Web UI (Phase 2).
* **Data Storage:** SQLite (local .db file) or JSON serialization for early prototyping.

## 3. Data Structures (The "Classes")

### A. Player
* **Bio:** ID, FirstName, LastName, Age, Position (QB, RB, WR, OL, DL, LB, DB, K/P).
* **Attributes (0-100):**
    * *Physical:* Speed, Strength, Agility, Durability.
    * *Technical:* ThrowingAccuracy, Catching, Blocking, Tackling, Kicking.
    * *Mental:* Awareness, Clutch, Morale.
    * **Distribution Rule:** Rookie ratings follow a weighted curve.
        * Cap: Raw rookies cannot exceed 85 OVR.
        * Rarity: 80-85 is "Generational" (<1% chance).
        * Average: Most rookies land between 60-70.
        * Progression: 90+ ratings are earned through "Experience" and "Playtime."
* **Status:** CurrentTeamID, IsInjured, WeeksInjured.
* **History:** Career Stats (Yards, TDs, INTs), Awards.

### B. Team
* **Identity:** Name, City, Mascot, Conference (East/West).
* **Personnel:** * *Roster:* List<Player> (All signed players).
    * *Depth Chart:* Mapping of starters vs backups.
    * *Staff:* Head Coach (with traits like "Conservative," "Aggressive," "Guru").
* **Strategy:** Run/Pass Ratio (e.g., 1950s teams run 70% of the time).
* **Stats:** Wins, Losses, PointsFor, PointsAgainst.

### C. League
* **State:** CurrentYear (starts 1950), CurrentWeek (1-12).
* **History:** List of past champions (Hall of Champions), League Records.

## 4. The Simulation Engine (The "Brain")

### The Game Loop
1.  **Pre-Snap:** Determine possession, down, distance, and field position.
2.  **Play Call:**
    * Offense picks `PlayType` (Run Inside, Run Outside, Short Pass, Long Pass, Punt, Field Goal).
    * Defense picks `DefensiveSet` (4-3 Base, Blitz, Dime).
3.  **Resolution (The Math):**
    * **Matchup System:** Instead of Team Averages, calculate specific matchups (e.g., WR vs CB, OL vs DL).
    * **Ball Carrier Logic:** Determine *who* has the ball and use their specific stats for the result.
    * **Events:** Check for Turnovers (Fumble/INT), Penalties, and Injuries.
4.  **Result:** Update Down, Distance, Field Position, Score, Time, and Player Stats.

## 5. Roadmap & Status

### Phase 1: The Core Engine (Current)
* [x] **v0.1:** Create Classes and a "One Play" simulator in Console. (**DONE**)
* [x] **v0.2:** Simulate a full Drive (series of plays until score/punt). (**DONE**)
* [ ] **v0.3:** **The Clock & Game Loop.** Implement Time (Minutes/Seconds), Quarters, and Halftime. 
* [ ] **v0.4:** **The Box Score.** Track stats for individual players during the game (e.g., "Unitas: 12/20, 150 yds").

### Phase 2: The Season Loop
* [ ] **v0.5:** **Schedule Generator.** Create a round-robin schedule for 16 teams.
* [ ] **v0.6:** **Weekly Loop.** Simulate all games in a week, update Standings, handle Injuries.
* [ ] **v0.7:** **Playoffs & Championship.** Logic for the top teams to play for the trophy.

### Phase 3: The Franchise Mode (OOTP Depth)
* [ ] **v0.8:** **Offseason.** Player Aging, Retirement, and the Draft.
* [ ] **v0.9:** **Management.** Hiring Coaches, simple finances/contracts.
* [ ] **v1.0:** **Persistence.** Saving and Loading the league to a database/file.

## 6. Known Issues / TODOs
* **Naming:** Player names need to be more diverse and guaranteed unique across the league.
* **Logic Depth:** Engine currently uses "Team Average" rating. Needs to move to "Weighted Unit Ratings" (e.g., Pass Offense = QB + WRs + OL).
* **Injuries:** Engine calculates all players (even injured ones). Need to implement an "Active/Inactive" check.
* **Turnovers:** The engine currently assumes clean plays. Need to implement Fumbles, Interceptions, Sacks and Safeties.
* **AI Logic:** Coaches pick random plays. Need to implement "Situation Aware" logic (e.g., Don't throw a bomb when up by 3 with 1 minute left).