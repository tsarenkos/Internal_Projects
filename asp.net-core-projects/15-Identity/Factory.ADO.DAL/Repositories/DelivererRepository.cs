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
            try
            {
                List<Deliverer> deliverers = new List<Deliverer>();

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
                else
                {
                    return null;
                }
                reader.Close();
                return deliverers;
            }
            catch (Exception exc)
            {
                throw exc;
            }            
        }

        public Deliverer GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }

                Deliverer deliverer = new Deliverer();
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
                else
                {
                    return null;
                }
                reader.Close();
                return deliverer;
            }
            catch (Exception exc)
            {
                throw exc;
            }                       
        }

        public void Create(Deliverer item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = InsertDelivererCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.DelivererName);

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }                      
        }

        public void Update(Deliverer item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = UpdateDelivererCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.DelivererName);
                command.Parameters.AddWithValue("id", item.DelivererId);

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
                command.CommandText = DeleteDelivererCommand;
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
