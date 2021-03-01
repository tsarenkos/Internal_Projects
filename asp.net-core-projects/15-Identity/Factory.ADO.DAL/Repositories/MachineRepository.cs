using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System.Data;


namespace Factory.ADO.DAL.Repositories
{
    public class MachineRepository:IRepository<Machine>
    {
        private readonly ADONetContext context;

        private const string GetMachinesCommand = "SELECT * FROM Machines";
        private const string GetMachineByIdCommand = "SELECT * FROM Machines WHERE MachineId = @id";
        private const string InsertMachineCommand = "INSERT INTO Machines (MachineName, Price, DateOfDelivery, DelivererId) " +
            "Values (@name, @price, @date, @delivererId)";
        private const string UpdateMachineCommand = "UPDATE Machines SET MachineName = @name, Price = @price, DateOfDelivery = @date," +
            "DelivererId = @delivererId WHERE MachineId = @id";
        private const string DeleteMachineCommand = "DELETE FROM Machines WHERE MachineId = @id";

        public MachineRepository(ADONetContext context)
        {
            this.context = context;
        }
        public IEnumerable<Machine> GetAll()
        {            
            try
            {
                List<Machine> machines = new List<Machine>();

                SqlCommand command = context.CreateCommand();
                command.CommandText = GetMachinesCommand;
                command.CommandType = CommandType.Text;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Machine machine = new Machine();
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
                return machines;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Machine GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }

                Machine machine = new Machine();
                SqlCommand command = context.CreateCommand();
                command.CommandText = GetMachineByIdCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);

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
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Create(Machine item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = InsertMachineCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.MachineName);
                command.Parameters.AddWithValue("price", item.Price);
                command.Parameters.AddWithValue("date", item.DateOfDelivery);
                command.Parameters.AddWithValue("delivererId", item.DelivererId);

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }                       
        }

        public void Update(Machine item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = UpdateMachineCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.MachineName);
                command.Parameters.AddWithValue("price", item.Price);
                command.Parameters.AddWithValue("date", item.DateOfDelivery);
                command.Parameters.AddWithValue("delivererId", item.DelivererId);
                command.Parameters.AddWithValue("id", item.MachineId);

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }                
        }

        public void Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = DeleteMachineCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }                              
        }
    }
}
