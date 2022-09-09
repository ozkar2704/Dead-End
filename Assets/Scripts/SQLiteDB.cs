using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Collections.Generic;
using Assets.Scripts;

/// <summary>
/// Clase para conectar y realizar consultas con la base de datos
/// </summary>
public class SQLiteDB : MonoBehaviour
{
    public static SQLiteDB instance;
    private string dbName = "URI=file:DataBase.db";
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Borra, crea y inicializa las tablas para resetear todos los datos
    /// </summary>
    public void ResetearTodo()
    {
        DeleteTables();
        CreateTables();
        InsertData();
    }

    /// <summary>
    /// Datos iniciales para la base de datos
    /// </summary>
    private void InsertData()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                //Creación items por nivel
                string sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (0, 'gold');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (0, 'manzanas');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (0, 'hachas');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (1, 'noConoceAlaio');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (1, 'noConoceTrama');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "INSERT INTO inventaries (key, value) VALUES (1, 'caeBien');";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Borrar las tablas
    /// </summary>
    private void DeleteTables()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string sqlcreation = "DROP TABLE IF EXISTS partidas;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "DROP TABLE IF EXISTS niveles;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "DROP TABLE IF EXISTS items;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "DROP TABLE IF EXISTS conoce;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "DROP TABLE IF EXISTS collect;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "DROP TABLE IF EXISTS inventaries;";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Crear las tablas
    /// </summary>
    private void CreateTables()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS partidas(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "nombre     VARCHAR(50) NOT NULL,";
                sqlcreation += "escena     VARCHAR(50) NOT NULL,";
                sqlcreation += "puntos     INTEGER NOT NULL,";
                sqlcreation += "deadEnds     INTEGER NOT NULL";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS niveles(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "partidaId     INTEGER NOT NULL,";
                sqlcreation += "nombre     VARCHAR(50) NOT NULL,";
                sqlcreation += "FOREIGN KEY(partidaId) REFERENCES partidas(id)";
                sqlcreation += "ON DELETE CASCADE";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS items(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "key     VARCHAR(50) NOT NULL,";
                sqlcreation += "value     INTEGER NOT NULL,";
                sqlcreation += "nivelId     INTEGER NOT NULL,";
                sqlcreation += "FOREIGN KEY(nivelId) REFERENCES niveles(id)";
                sqlcreation += "ON DELETE CASCADE";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS conoce(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "key     VARCHAR(50) NOT NULL,";
                sqlcreation += "value     INTEGER NOT NULL,";
                sqlcreation += "nivelId     INTEGER NOT NULL,";
                sqlcreation += "FOREIGN KEY(nivelId) REFERENCES niveles(id)";
                sqlcreation += "ON DELETE CASCADE";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS collect(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "key     VARCHAR(50) NOT NULL,";
                sqlcreation += "collected     BIT NOT NULL,";
                sqlcreation += "scene     VARHAR(50) NOT NULL,";
                sqlcreation += "partidaId     INTEGER NOT NULL,";
                sqlcreation += "FOREIGN KEY(partidaId) REFERENCES partidas(id)";
                sqlcreation += "ON DELETE CASCADE";
                sqlcreation += ");";

                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();

                sqlcreation = "";

                sqlcreation += "CREATE TABLE IF NOT EXISTS inventaries(";
                sqlcreation += "id INTEGER NOT NULL ";
                sqlcreation += "PRIMARY KEY AUTOINCREMENT,";
                sqlcreation += "key     INTEGER NOT NULL,";
                sqlcreation += "value     VARCHAR(50) NOT NULL";
                sqlcreation += ");";
                command.CommandText = sqlcreation;
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    /// <summary>
    /// Obtener el id de una partida a partir de su nombre
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetPartidaId(string name)
    {
        int result = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM partidas WHERE nombre = '" + name + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["id"]))
                    {
                        result = Convert.ToInt32(reader["id"]);
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Obtener el id de un nivel
    /// </summary>
    /// <param name="name"></param>
    /// <param name="partidaId"></param>
    /// <returns></returns>
    public int GetNivelId(string name, int partidaId)
    {
        int result = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM niveles WHERE nombre = '" + name + "' AND partidaId = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["id"]))
                    {
                        result = Convert.ToInt32(reader["id"]);
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Obtener partida
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Partida GetPartida(string name)
    {
        Partida result = null;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM partidas WHERE nombre = '" + name + "' AND escena != 'End';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["id"]))
                    {
                        result = new Partida();
                        result.Id = Convert.ToInt32(reader["id"]);
                        result.Nombre = reader["nombre"].ToString();
                        result.Escena = reader["escena"].ToString();
                        result.Puntos = Convert.ToInt32(reader["puntos"]);
                        result.DeadEnds = Convert.ToInt32(reader["deadEnds"]);
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Obtener partida
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Partida GetPartidaById(int id)
    {
        Partida result = null;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM partidas WHERE id = '" + id + "' AND escena != 'End';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["id"]))
                    {
                        result = new Partida();
                        result.Id = Convert.ToInt32(reader["id"]);
                        result.Nombre = reader["nombre"].ToString();
                        result.Escena = reader["escena"].ToString();
                        result.Puntos = Convert.ToInt32(reader["puntos"]);
                        result.DeadEnds = Convert.ToInt32(reader["deadEnds"]);
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Comprobar si un item o cofre ya se ha cogido
    /// </summary>
    /// <param name="key"></param>
    /// <param name="scene"></param>
    /// <param name="partidaId"></param>
    /// <returns></returns>
    public bool IsCollected(string key, string scene, int partidaId)
    {
        bool result = false;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM collect WHERE key = '" + key + "' AND scene = '" + scene + "' AND partidaId = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["collected"]))
                    {
                        result = true;
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Establecer como recogido un item o cofre
    /// </summary>
    /// <param name="key"></param>
    /// <param name="scene"></param>
    /// <param name="partidaId"></param>
    public void SetCollected(string key, string scene, int partidaId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO collect (key, collected, scene, partidaId) VALUES ('" + key + "', " + 1 + ", '" + scene + "', '" + partidaId + "');";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {

                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Devuelve cuantos items de la partida se han recogido
    /// </summary>
    /// <param name="partidaId"></param>
    /// <returns></returns>
    public int SelectCollected(int partidaId)
    {
        int result = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT id FROM collect WHERE  partidaId = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result++;
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Select general
    /// </summary>
    /// <param name="table"></param>
    /// <param name="key"></param>
    /// <param name="nivelId"></param>
    /// <returns></returns>
    public int Select(string table, string key, int nivelId)
    {
        int result = 0;
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM " + table + " WHERE nivelId = '" + nivelId + "' AND key = '" + key + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    if (!Convert.IsDBNull(reader["value"]))
                    {
                        result = Convert.ToInt32(reader["value"]);
                    }
                }
            }

            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Seleccionar items en el inventario
    /// </summary>
    /// <param name="key">0 = item, 1 = conoce</param>
    /// <returns></returns>
    public List<string> SelectInventary(int key)
    {
        List<string> result = new List<string>();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT value FROM inventaries WHERE key = '" + key + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    int contador = 0;
                    while (reader.Read())
                    {
                        result.Add(reader.GetValue(contador).ToString());
                    }
                }
            }
            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Seleccionar partidas activas
    /// </summary>
    /// <returns></returns>
    public List<int> SelectPartidas()
    {
        List<int> result = new List<int>();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM partidas;";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        result.Add(Convert.ToInt32(reader["id"]));
                    }
                }
            }
            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Seleccionar las puntuaciones de las partidas finalizadas
    /// </summary>
    /// <returns></returns>
    public List<int> SelectPartidasFinalizadas()
    {
        List<int> result = new List<int>();
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT puntos FROM partidas WHERE escena = 'End';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToInt32(reader["puntos"]));
                    }
                }
            }
            connection.Close();
        }
        return result;
    }

    /// <summary>
    /// Insertar una partida nueva
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="escena"></param>
    /// <param name="puntos"></param>
    /// <param name="deadEnds"></param>
    public void InsertPartida(string nombre, string escena, int puntos, int deadEnds)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO partidas (nombre, escena, puntos, deadEnds) VALUES ('" + nombre + "', '" + escena + "', '" + puntos + "', '" + deadEnds + "');";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Actualizar una partida
    /// </summary>
    /// <param name="partidaId"></param>
    /// <param name="escena"></param>
    /// <param name="puntos"></param>
    /// <param name="deadEnds"></param>
    public void UpdatePartida(int partidaId, string escena, int puntos, int deadEnds)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE partidas SET escena = '" + escena + "', puntos = '" + puntos + "', deadEnds = '" + deadEnds + "' WHERE id = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Insertar un nivel nuevo
    /// </summary>
    /// <param name="partidaId"></param>
    /// <param name="nombre"></param>
    public void InsertNivel(int partidaId, string nombre)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO niveles (partidaId, nombre) VALUES ('" + partidaId + "', '" + nombre + "');";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Insertar item
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="nivelId"></param>
    public void InsertItems(string key, int value, int nivelId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO items (key, value, nivelId) VALUES ('" + key + "', '" + value + "', '" + nivelId + "');";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                        

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Insertar valor conoce de un NPC
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="nivelId"></param>
    public void InsertConoce(string key, int value, int nivelId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO conoce (key, value, nivelId) VALUES ('" + key + "', '" + value + "', '" + nivelId + "');";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Borrar partida
    /// </summary>
    /// <param name="partidaId"></param>
    public void DeletePartida(int partidaId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM partidas WHERE id = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Borrar nivel
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="partidaId"></param>
    public void DeleteNivel(string nombre, int partidaId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM niveles WHERE nombre = '" + nombre + "' AND partidaId = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Borrar items de un nivel
    /// </summary>
    /// <param name="nivelId"></param>
    public void DeleteItems(int nivelId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM items WHERE nivelId = '" + nivelId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Borrar valores conoce de un nivel
    /// </summary>
    /// <param name="nivelId"></param>
    public void DeleteConoce(int nivelId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM conoce WHERE nivelId = '" + nivelId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }

    /// <summary>
    /// Borrar valores de recogido de una partida
    /// </summary>
    /// <param name="partidaId"></param>
    public void DeleteCollect(int partidaId)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM collect WHERE partidaId = '" + partidaId + "';";
                Debug.Log(command.CommandText);
                using (IDataReader reader = command.ExecuteReader())
                {
                    //while (reader.Read())
                    //{
                    //    Debug.Log("name: " + reader["name"] + " password: " + reader["password"]);

                    //}
                }
            }

            connection.Close();
        }
    }
}