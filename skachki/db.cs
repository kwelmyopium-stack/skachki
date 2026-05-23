using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using skachki.Models;

namespace skachki
{
    public class DatabaseHelper
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SkachkiDB"].ConnectionString;

        // ==================== ЛОШАДИ ====================

        public List<Horse> GetHorses()
        {
            var list = new List<Horse>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT id, name, breed, age, color FROM Horses", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Horse
                        {
                            HorseID = (int)r["id"],
                            Name    = r["name"].ToString(),
                            Breed   = r["breed"].ToString(),
                            Age     = (int)r["age"],
                            Color   = r["color"].ToString()
                        });
            }
            return list;
        }

        public void AddHorse(Horse h)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Horses (name, breed, age, color) VALUES (@Name, @Breed, @Age, @Color)", conn);
                cmd.Parameters.AddWithValue("@Name",  h.Name);
                cmd.Parameters.AddWithValue("@Breed", h.Breed);
                cmd.Parameters.AddWithValue("@Age",   h.Age);
                cmd.Parameters.AddWithValue("@Color", h.Color);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateHorse(Horse h)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Horses SET name=@Name, breed=@Breed, age=@Age, color=@Color WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Name",  h.Name);
                cmd.Parameters.AddWithValue("@Breed", h.Breed);
                cmd.Parameters.AddWithValue("@Age",   h.Age);
                cmd.Parameters.AddWithValue("@Color", h.Color);
                cmd.Parameters.AddWithValue("@Id",    h.HorseID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteHorse(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Horses WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // ==================== ЖОКЕИ ====================

        public List<Jockey> GetJockeys()
        {
            var list = new List<Jockey>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT id, first_name, last_name, age, country FROM Jockeys", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Jockey
                        {
                            JockeyID  = (int)r["id"],
                            FirstName = r["first_name"].ToString(),
                            LastName  = r["last_name"].ToString(),
                            Age       = (int)r["age"],
                            Country   = r["country"].ToString()
                        });
            }
            return list;
        }

        public void AddJockey(Jockey j)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Jockeys (first_name, last_name, age, country) VALUES (@First, @Last, @Age, @Country)", conn);
                cmd.Parameters.AddWithValue("@First",   j.FirstName);
                cmd.Parameters.AddWithValue("@Last",    j.LastName);
                cmd.Parameters.AddWithValue("@Age",     j.Age);
                cmd.Parameters.AddWithValue("@Country", j.Country);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateJockey(Jockey j)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Jockeys SET first_name=@First, last_name=@Last, age=@Age, country=@Country WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@First",   j.FirstName);
                cmd.Parameters.AddWithValue("@Last",    j.LastName);
                cmd.Parameters.AddWithValue("@Age",     j.Age);
                cmd.Parameters.AddWithValue("@Country", j.Country);
                cmd.Parameters.AddWithValue("@Id",      j.JockeyID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteJockey(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Jockeys WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // ==================== ЗАЕЗДЫ ====================

        public List<Race> GetRaces()
        {
            var list = new List<Race>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT id, race_date, location, horse_id, jockey_id, place, distance FROM Races", conn);
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        list.Add(new Race
                        {
                            RaceID   = (int)r["id"],
                            RaceDate = (DateTime)r["race_date"],
                            Location = r["location"].ToString(),
                            HorseID  = (int)r["horse_id"],
                            JockeyID = (int)r["jockey_id"],
                            Place    = r["place"]    == DBNull.Value ? (int?)null : (int)r["place"],
                            Distance = r["distance"] == DBNull.Value ? (int?)null : (int)r["distance"]
                        });
            }
            return list;
        }

        public void AddRace(Race race)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO Races (race_date, location, horse_id, jockey_id, place, distance) " +
                    "VALUES (@Date, @Location, @HId, @JId, @Place, @Distance)", conn);
                cmd.Parameters.AddWithValue("@Date",     race.RaceDate);
                cmd.Parameters.AddWithValue("@Location", race.Location);
                cmd.Parameters.AddWithValue("@HId",      race.HorseID);
                cmd.Parameters.AddWithValue("@JId",      race.JockeyID);
                cmd.Parameters.AddWithValue("@Place",    (object)race.Place    ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Distance", (object)race.Distance ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRace(Race race)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Races SET race_date=@Date, location=@Location, horse_id=@HId, " +
                    "jockey_id=@JId, place=@Place, distance=@Distance WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Date",     race.RaceDate);
                cmd.Parameters.AddWithValue("@Location", race.Location);
                cmd.Parameters.AddWithValue("@HId",      race.HorseID);
                cmd.Parameters.AddWithValue("@JId",      race.JockeyID);
                cmd.Parameters.AddWithValue("@Place",    (object)race.Place    ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Distance", (object)race.Distance ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id",       race.RaceID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRace(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Races WHERE id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
