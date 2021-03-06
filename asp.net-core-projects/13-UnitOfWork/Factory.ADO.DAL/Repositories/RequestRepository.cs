﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;
using System.Data;

namespace Factory.ADO.DAL.Repositories
{
    public class RequestRepository:IRepository<Request>
    {
        private readonly ADONetContext context;

        private const string GetRequestsCommand = "SELECT * FROM Requests";
        private const string GetRequestByIdCommand = "SELECT * FROM Requests WHERE RequestId = @id";
        private const string InsertRequestCommand = "INSERT INTO Requests (RequestCreatorId, RequestHandlerId, DateOfCreate, MachineId, RequestStatusId, InnerRequestId) " +
            "Values (@creatorId, @handlerId, @date, @machineId, 1, @innerRequestId)";
        private const string UpdateRequestCommand = "UPDATE Requests SET RequestCreatorId = @creatorId, RequestHandlerId = @handlerId, " +
            "DateOfCreate = @date, MachineId = @machineId, RequestStatusId = @status, InnerRequestId = @innerRequestId WHERE RequestId = @id";
        private const string DeleteRequestCommand = "DELETE FROM Requests WHERE RequestId = @id";

        public RequestRepository(ADONetContext context)
        {
            this.context = context;
        }

        public IEnumerable<Request> GetAll()
        {
            List<Request> requests = new List<Request>();
            try
            {
                SqlCommand command = context.CreateCommand();
                command.CommandText = GetRequestsCommand;
                command.CommandType = CommandType.Text;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Request request = new Request();
                        request.RequestId = Convert.ToInt32(reader["RequestId"]);
                        request.RequestCreatorId = (int)reader["RequestCreatorId"];

                        if(reader["RequestHandlerId"] != DBNull.Value)
                        {
                            request.RequestHandlerId = (int)reader["RequestHandlerId"];
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
                reader.Close();
            }
            catch { }
            
            return requests;
        }

        public Request GetById(int id)
        {
            if (id != 0)
            {
                Request request = new Request();
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = GetRequestByIdCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        request.RequestId = Convert.ToInt32(reader["RequestId"]);
                        request.RequestCreatorId = (int)reader["RequestCreatorId"];

                        if (reader["RequestHandlerId"] != DBNull.Value)
                        {
                            request.RequestHandlerId = (int)reader["RequestHandlerId"];
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
                    reader.Close();
                }
                catch { }

                return request;
            }
            return null;            
        }

        public void Create(Request item)
        {
            if (item != null)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = InsertRequestCommand;
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

                    command.ExecuteNonQuery();
                }
                catch { }
            }                        
        }

        public void Update(Request item)
        {
            if (item != null)
            {
                try
                {
                    SqlCommand command = context.CreateCommand();
                    command.CommandText = UpdateRequestCommand;
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
                    command.CommandText = DeleteRequestCommand;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
                catch { }
            }                        
        }
    }
}
