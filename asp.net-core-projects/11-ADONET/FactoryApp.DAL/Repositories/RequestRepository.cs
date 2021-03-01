using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;

namespace FactoryApp.DAL.Repositories
{
    public class RequestRepository : IRepository<RequestEntity>
    {
        private readonly string connectionString;

        private const string GetRequestsCommand = "SELECT * FROM Requests";
        private const string GetRequestByIdCommand = "SELECT * FROM Requests WHERE RequestId = @id";
        private const string InsertRequestCommand = "INSERT INTO Requests (RequestCreatorId, RequestHadlerId, DateOfCreate, MachineId, RequestStatusId, InnerRequestId) " +
            "Values (@creatorId, @handlerId, @date, @machineId, 1, @innerRequestId)";
        private const string UpdateRequestCommand = "UPDATE Requests SET RequestCreatorId = @creatorId, RequestHadlerId = @handlerId, " +
            "DateOfCreate = @date, MachineId = @machineId, RequestStatusId = @status, InnerRequestId = @innerRequestId WHERE RequestId = @id";
        private const string DeleteRequestCommand = "DELETE FROM Requests WHERE RequestId = @id";

        public RequestRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IEnumerable<RequestEntity> GetAll()
        {
            List<RequestEntity> requests = new List<RequestEntity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(GetRequestsCommand, connection);                
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RequestEntity request = new RequestEntity();
                        request.RequestId = Convert.ToInt32(reader["RequestId"]);
                        request.RequestCreatorId = (int)reader["RequestCreatorId"];

                        if (reader["RequestHadlerId"] != DBNull.Value)
                        {
                            request.RequestHandlerId = (int)reader["RequestHadlerId"];
                        }
                        else
                        {
                            request.RequestHandlerId = null;
                        }

                        request.DateOfCreate = (DateTime)reader["DateOfCreate"];
                        request.MachineId = (int)(reader["MachineId"]);
                        request.RequestStatusId = (int)(reader["RequestStatusId"]);

                        if (reader["InnerRequestId"] != DBNull.Value)
                        {
                            request.InnerRequestId = (int)(reader["InnerRequestId"]);
                        }
                        else
                        {
                            request.InnerRequestId = null;
                        }
                        requests.Add(request);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
            }
            return requests;
        }

        public RequestEntity GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                RequestEntity request = new RequestEntity();
                SqlCommand command = new SqlCommand(GetRequestByIdCommand, connection);                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    request.RequestId = Convert.ToInt32(reader["RequestId"]);
                    request.RequestCreatorId = (int)reader["RequestCreatorId"];

                    if (reader["RequestHadlerId"] != DBNull.Value)
                    {
                        request.RequestHandlerId = (int)reader["RequestHadlerId"];
                    }
                    else
                    {
                        request.RequestHandlerId = null;
                    }

                    request.DateOfCreate = (DateTime)reader["DateOfCreate"];
                    request.MachineId = (int)reader["MachineId"];
                    request.RequestStatusId = (int)reader["RequestStatusId"];

                    if (reader["InnerRequestId"] != DBNull.Value)
                    {
                        request.InnerRequestId = (int)(reader["InnerRequestId"]);
                    }
                    else
                    {
                        request.InnerRequestId = null;
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
                return request;
            }
        }

        public void Create(RequestEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(InsertRequestCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("creatorId", item.RequestCreatorId);                    

                    if (item.RequestHandlerId != null)
                    {
                        command.Parameters.AddWithValue("handlerId", item.RequestHandlerId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("handlerId", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("date", item.DateOfCreate);
                    command.Parameters.AddWithValue("machineId", item.MachineId);

                    if (item.InnerRequestId != null)
                    {
                        command.Parameters.AddWithValue("innerRequestId", item.InnerRequestId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("innerRequestId", DBNull.Value);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                    
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(RequestEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(UpdateRequestCommand, connection);
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("creatorId", item.RequestCreatorId);

                    if (item.RequestHandlerId != null)
                    {
                        command.Parameters.AddWithValue("handlerId", item.RequestHandlerId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("handlerId", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("date", item.DateOfCreate);
                    command.Parameters.AddWithValue("machineId", item.MachineId);
                    command.Parameters.AddWithValue("status", item.RequestStatusId);

                    if (item.InnerRequestId != null)
                    {
                        command.Parameters.AddWithValue("innerRequestId", item.InnerRequestId);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("innerRequestId", DBNull.Value);
                    }

                    command.Parameters.AddWithValue("id", item.RequestId);
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
                    SqlCommand command = new SqlCommand(DeleteRequestCommand, connection);                    
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
