using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;

namespace FactoryApp.DAL.Repositories
{
    public class MachineRepository : IRepository<MachineEntity>
    {
        private readonly string connectionString;

        private const string GetMachinesCommand = "SELECT * FROM Machines";
        private const string GetMachineByIdCommand = "SELECT * FROM Machines WHERE MachineId = @id";
        private const string InsertMachineCommand = "INSERT INTO Machines (MachineName, Price, DateOfDelivery, DelivererId) " +
            "Values (@name, @price, @date, @delivererId)";
        private const string UpdateMachineCommand = "UPDATE Machines SET MachineName = @name, Price = @price, DateOfDelivery = @date," +
            "DelivererId = @delivererId WHERE MachineId = @id";
        private const string DeleteMachineCommand = "DELETE FROM Machines WHERE MachineId = @id";

        public MachineRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<MachineEntity> GetAll()
        {
            List<MachineEntity> machines = new List<MachineEntity>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(GetMachinesCommand, connection);                
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MachineEntity machine = new MachineEntity();
                        machine.MachineId = Convert.ToInt32(reader["MachineId"]);
                        machine.MachineName = reader["MachineName"].ToString();
                        machine.Price = (float)reader["Price"];
                        machine.DateOfDelivery = (DateTime)reader["DateOfDelivery"];
                        machine.DelivererId = (int)(reader["DelivererId"]);
                        machines.Add(machine);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
            }            
            return machines;
        }
        public MachineEntity GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                MachineEntity machine = new MachineEntity();
                SqlCommand command = new SqlCommand(GetMachineByIdCommand, connection);                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    machine.MachineId = Convert.ToInt32(reader["MachineId"]);
                    machine.MachineName = reader["MachineName"].ToString();
                    machine.Price = (float)reader["Price"];
                    machine.DateOfDelivery = (DateTime)reader["DateOfDelivery"];
                    machine.DelivererId = (int)reader["DelivererId"];
                }
                else
                {
                    return null;
                }
                reader.Close();
                return machine;
            }                
        }
        public void Create(MachineEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(InsertMachineCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.MachineName);
                    command.Parameters.AddWithValue("price", item.Price);
                    command.Parameters.AddWithValue("date", item.DateOfDelivery);
                    command.Parameters.AddWithValue("delivererId", item.DelivererId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }                    
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void Update(MachineEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(UpdateMachineCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.MachineName);
                    command.Parameters.AddWithValue("price", item.Price);
                    command.Parameters.AddWithValue("date", item.DateOfDelivery);
                    command.Parameters.AddWithValue("delivererId", item.DelivererId);
                    command.Parameters.AddWithValue("id", item.MachineId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }                    
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(DeleteMachineCommand, connection);                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }              
        }
    }
}
