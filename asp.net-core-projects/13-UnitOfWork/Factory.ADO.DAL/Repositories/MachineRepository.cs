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
            List<Machine> machines = new List<Machine>();
            try
            {
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
                reader.Close();
            }
            catch { }
            
            return machines;
        }

        public Machine GetById(int id)
        {
            if (id != 0)
            {
                Machine machine = new Machine();
                try
                {
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
                    reader.Close();
                }
                catch { }

                return machine;
            }
            return null;
        }

        public void Create(Machine item)
        {
            if (item != null)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = InsertMachineCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.MachineName);
                    command.Parameters.AddWithValue("price", item.Price);
                    command.Parameters.AddWithValue("date", item.DateOfDelivery);
                    command.Parameters.AddWithValue("delivererId", item.DelivererId);

                    command.ExecuteNonQuery();
                }
                catch { }
            }                        
        }

        public void Update(Machine item)
        {
            if (item != null)
            {
                try
                {
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
                catch { }
            }                        
        }

        public void Delete(int id)
        {
            if (id != 0)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = DeleteMachineCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
                catch { }
            }                        
        }
    }
}
