using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Factory.ADO.DAL.Repositories
{
    public class DelivererRepository:IRepository<Deliverer>
    {
        private readonly ADONetContext context;

        private const string GetDeliverersCommand = "SELECT * FROM Deliverers";
        private const string GetDelivererByIdCommand = "SELECT * FROM Deliverers WHERE DelivererId = @id";
        private const string InsertDelivererCommand = "INSERT INTO Deliverers (DelivererName) Values (@name)";
        private const string UpdateDelivererCommand = "UPDATE Deliverers SET DelivererName = @name WHERE DelivererId = @id";
        private const string DeleteDelivererCommand = "DELETE FROM Deliverers WHERE DelivererId = @id";

        public DelivererRepository(ADONetContext context)
        {
            this.context = context;
        }

        public IEnumerable<Deliverer> GetAll()
        {
            List<Deliverer> deliverers = new List<Deliverer>();
            try
            {
                SqlCommand command = context.CreateCommand();
                command.CommandText = GetDeliverersCommand;
                command.CommandType = CommandType.Text;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Deliverer deliverer = new Deliverer();
                        deliverer.DelivererId = Convert.ToInt32(reader["DelivererId"]);
                        deliverer.DelivererName = reader["DelivererName"].ToString();
                        deliverers.Add(deliverer);
                    }
                }
                reader.Close();
            }
            catch { }            

            return deliverers;
        }

        public Deliverer GetById(int id)
        {
            if (id != 0)
            {
                Deliverer deliverer = new Deliverer();
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = GetDelivererByIdCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        deliverer.DelivererId = (int)reader["DelivererId"];
                        deliverer.DelivererName = (string)reader["DelivererName"];
                    }
                    reader.Close();
                }
                catch { }

                return deliverer;
            }
            return null;            
        }

        public void Create(Deliverer item)
        {
            if (item != null)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = InsertDelivererCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.DelivererName);

                    command.ExecuteNonQuery();
                }
                catch { }
            }                       
        }

        public void Update(Deliverer item)
        {
            if (item != null)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = UpdateDelivererCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.DelivererName);
                    command.Parameters.AddWithValue("id", item.DelivererId);

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
                    command.CommandText = DeleteDelivererCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
                catch { }
            }                        
        }       
    }
}
