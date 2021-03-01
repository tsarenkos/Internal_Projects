using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;

namespace FactoryApp.DAL.Repositories
{
    public class DelivererRepository : IRepository<DelivererEntity>
    {
        private readonly string connectionString;

        private const string GetDeliverersCommand = "SELECT * FROM Deliverers";
        private const string GetDelivererByIdCommand = "SELECT * FROM Deliverers WHERE DelivererId = @id";
        private const string InsertDelivererCommand = "INSERT INTO Deliverers (DelivererName) Values (@name)";
        private const string UpdateDelivererCommand = "UPDATE Deliverers SET DelivererName = @name WHERE DelivererId = @id";
        private const string DeleteDelivererCommand = "DELETE FROM Deliverers WHERE DelivererId = @id";

        public DelivererRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<DelivererEntity> GetAll()
        {
            List<DelivererEntity> deliverers = new List<DelivererEntity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(GetDeliverersCommand, connection);
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DelivererEntity deliverer = new DelivererEntity();
                        deliverer.DelivererId = Convert.ToInt32(reader["DelivererId"]);
                        deliverer.DelivererName = reader["DelivererName"].ToString();
                        deliverers.Add(deliverer);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
                return deliverers;
            }
        }

        public DelivererEntity GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DelivererEntity deliverer = new DelivererEntity();
                SqlCommand command = new SqlCommand(GetDelivererByIdCommand, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    deliverer.DelivererId = (int)reader["DelivererId"];
                    deliverer.DelivererName = (string)reader["DelivererName"];
                }
                else
                {
                    return null;
                }
                reader.Close();
                return deliverer;
            }
        }

        public void Create(DelivererEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(InsertDelivererCommand, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.DelivererName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }                
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(DelivererEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(UpdateDelivererCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.DelivererName);
                    command.Parameters.AddWithValue("id", item.DelivererId);
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
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(DeleteDelivererCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }                    
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
