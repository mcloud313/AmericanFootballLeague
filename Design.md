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

## Phase 2: The Simulation Core (Fidelity)
* [ ] **v0.4:** **The Narrative Engine & Stats.**
    * **Play-by-Play:** "Unitas passes to Berry for 12 yards" (requires Roster lookup).
    * **Box Score:** Track individual Game Stats (Passing/Rushing/Receiving yards).
    * **Turnovers:** Implement Interceptions, Fumbles, and Sacks.
    * **Overtime:** Logic for 5th Quarter if tied.
* [ ] **v0.5:** **The Depth Chart.**
    * **Substitution Logic:** Starters play most snaps; backups come in for blowouts or injuries.
    * **Specialists:** Designate specific KR/PR (Kick/Punt Returners) and K/P.
* [ ] **v0.6:** **Advanced Game Logic.**
    * **Penalties:** False starts, holding (kills drives), pass interference (extends drives).
    * **Fatigue:** Players perform worse if they play 10 snaps in a row without a break.
    * **Home Field Advantage:** Slight boost to Home team, especially in loud stadiums.

## Phase 3: The Season Loop (The Grind)
* [ ] **v0.7:** **Schedule Generator.**
    * Round-robin scheduling for 16 teams.
    * Division logic (Play division rivals twice).
* [ ] **v0.8:** **The Weekly Workflow.**
    * Simulating other games in the background.
    * **News Ticker:** "Breaking News: NY's Star QB out for season!"
    * **Injury System:** "Probable," "Questionable," "Doubtful," "IR."
* [ ] **v0.9:** **Standings & Playoffs.**
    * Track W-L-T records.
    * Tie-breaker logic for playoff seeding.
    * The "AFL Championship Game."

## Phase 4: Franchise Management (The OOTP Layer)
* [ ] **v1.0:** **The Human Element.**
    * **Coaching Staff:** Hire OC/DC with specific schemes (e.g., "Vertical Passing" vs "Wishbone").
    * **Scouting:** Fog of War. You don't see "Overall 85," you see "Scout Grade: A-" (and scouts can be wrong!).
    * **Morale:** Players get angry if they lose or don't get touches.
* [ ] **v1.1:** **The Front Office.**
    * **Financials:** Ticket prices, stadium maintenance, player contracts/salaries.
    * **The Draft:** 12 Rounds of rookies. Logic for AI teams to draft based on "Best Player Available" vs "Team Need."
    * **Trades:** AI logic to offer/accept trades based on player value.

## Phase 5: Historical & Immersion (The "Flavor")
* [ ] **v1.2:** **Historical Database.**
    * Import real rookie classes by year (e.g., 1957 draft class has Jim Brown).
    * "Era Settings": 1950s (Run heavy) vs 1980s (Pass heavy) rule changes.
* [ ] **v1.3:** **Records & Awards.**
    * Season Awards (MVP, Rookie of the Year).
    * Hall of Fame induction logic.
    * League Record Book (Single Season Rushing Record, etc.).

## Phase 6: User Interface (The Look)
* [ ] **v2.0:** **UI Overhaul.**
    * Move from Console to a Desktop App (WPF or MAUI) or Web App (Blazor).
    * Clickable hyperlinks (Click a player name to see their card).
    * HTML Reports: Export "End of Season Report" to a webpage.