using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Factory.Storage.Core.Entities;
using Factory.Storage.Core.Interfaces;

namespace Factory.ADO.DAL.Repositories
{
    public class EmployeeRepository:IRepository<Employee>
    {
        private readonly ADONetContext context;

        private const string GetEmployeesCommand = "SELECT * FROM Employees";
        private const string GetEmployeeByIdCommand = "SELECT * FROM Employees WHERE EmployeeId = @id";
        private const string InsertEmployeeCommand = "INSERT INTO Employees (Name, Position) Values (@name, @position)";
        private const string UpdateEmployeeCommand = "UPDATE Employees SET Name = @name, Position = @position WHERE EmployeeId = @id";
        private const string DeleteEmployeeCommand = "DELETE FROM Employees WHERE EmployeeId = @id";

        public EmployeeRepository(ADONetContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetAll()
        {           
            try
            {
                List<Employee> employees = new List<Employee>();

                SqlCommand command = context.CreateCommand();
                command.CommandText = GetEmployeesCommand;
                command.CommandType = CommandType.Text;
                
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee emp = new Employee();
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
                return employees;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public Employee GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return null;
                }

                Employee emp = new Employee();
                SqlCommand command = context.CreateCommand();
                command.CommandText = GetEmployeeByIdCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("id", id);

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
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Create(Employee item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = InsertEmployeeCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.Name);
                command.Parameters.AddWithValue("position", item.Position);

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void Update(Employee item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }

                SqlCommand command = context.CreateCommand();
                command.CommandText = UpdateEmployeeCommand;
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("name", item.Name);
                command.Parameters.AddWithValue("position", item.Position);
                command.Parameters.AddWithValue("id", item.EmployeeId);

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
                command.CommandText = DeleteEmployeeCommand;
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
