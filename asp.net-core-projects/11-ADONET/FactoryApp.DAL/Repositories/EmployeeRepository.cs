using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FactoryApp.DAL.Entities;
using FactoryApp.DAL.Interfaces;

namespace FactoryApp.DAL.Repositories
{
    public class EmployeeRepository : IRepository<EmployeeEntity>
    {
        private readonly string connectionString;

        private const string GetEmployeesCommand = "SELECT * FROM Employees";
        private const string GetEmployeeByIdCommand = "SELECT * FROM Employees WHERE EmployeeId = @id";
        private const string InsertEmployeeCommand = "INSERT INTO Employees (Name, Position) Values (@name, @position)";
        private const string UpdateEmployeeCommand = "UPDATE Employees SET Name = @name, Position = @position WHERE EmployeeId = @id";
        private const string DeleteEmployeeCommand = "DELETE FROM Employees WHERE EmployeeId = @id";

        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }      

        public IEnumerable<EmployeeEntity> GetAll()
        {
            List<EmployeeEntity> employees = new List<EmployeeEntity>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(GetEmployeesCommand, connection);                
                command.CommandType = CommandType.Text;
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EmployeeEntity emp = new EmployeeEntity();
                        emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                        emp.Name = reader["Name"].ToString();
                        emp.Position = reader["Position"].ToString();
                        employees.Add(emp);
                    }
                }
                else
                {
                    return null;
                }
                reader.Close();
            }            
            return employees;
        }

        public EmployeeEntity GetById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                EmployeeEntity emp = new EmployeeEntity();

                SqlCommand command = new SqlCommand(GetEmployeeByIdCommand, connection);                
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);
                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.Name = reader["Name"].ToString();
                    emp.Position = reader["Position"].ToString();
                }
                else
                {
                    return null;
                }
                reader.Close();
                return emp;
            }
        }

        public void Create(EmployeeEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(InsertEmployeeCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.Name);
                    command.Parameters.AddWithValue("position", item.Position);
                    connection.Open();
                    command.ExecuteNonQuery();
                }                    
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(EmployeeEntity item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(UpdateEmployeeCommand, connection);                    
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("name", item.Name);
                    command.Parameters.AddWithValue("position", item.Position);
                    command.Parameters.AddWithValue("id", item.EmployeeId);
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
                    SqlCommand command = new SqlCommand(DeleteEmployeeCommand, connection);                   
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
