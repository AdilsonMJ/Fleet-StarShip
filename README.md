# Aircraft Control Project

## Description
This project is an starship control application where you can add, edit, and remove aircraft from your fleet. 
It also allows you to create missions involving multiple starships. 
Each starship maintains a record of its missions, and each mission keeps a log of the starships involved.

## Technologies Used
- **Database:** MySQL
- **Table Relationship:** Many-to-Many
- **API Consumption:** [The Star Wars API](https://swapi.dev/about) for retrieving initial spacecraft and additional information
- **API Consumption Library:** Refit
- **Framework:** .NET 7

## Features
1. **Aircraft Management:**
   - Add, edit, and remove aircraft from the fleet.

2. **Mission Creation:**
   - Create missions involving multiple starships.

3. **Starship Records:**
   - Each starship maintains a record of its missions.

4. **Mission Records:**
   - Each mission keeps a log of the starships involved.

## Setup
1. Create a database on your machine.
2. Configure database access.
3. Run migrations and update the database.
