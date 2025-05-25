using System;
using Npgsql;

public class ClienteRepository
{
    private string connectionString = "Host=localhost;Username=postgres;Password=sua_senha;Database=loja_virtual";

    public void CriarCliente(string nome, string email, string telefone)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "INSERT INTO clientes (nome, email, telefone) VALUES (@nome, @email, @telefone)";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("nome", nome);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("telefone", telefone);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Cliente criado com sucesso!");
    }

    public void BuscarClientePorId(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "SELECT * FROM clientes WHERE id = @id";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("id", id);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Console.WriteLine($"ID: {reader["id"]}, Nome: {reader["nome"]}, Email: {reader["email"]}, Telefone: {reader["telefone"]}");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
    }

    public void AtualizarCliente(int id, string nome, string email, string telefone)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        var sql = "UPDATE clientes SET nome = @nome, email = @email, telefone = @telefone WHERE id = @id";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("nome", nome);
        cmd.Parameters.AddWithValue("email", email);
        cmd.Parameters.AddWithValue("telefone", telefone);

        int rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            Console.WriteLine("Cliente atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Cliente não encontrado.");
        }
    }
}
