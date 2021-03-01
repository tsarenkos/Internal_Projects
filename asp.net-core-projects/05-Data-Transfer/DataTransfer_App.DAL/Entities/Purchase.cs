using System;

namespace DataTransfer_App.DAL.Entities
{
    public class Purchase
    {
        public int Id { get; }
        public string Name { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateEdit { get; set; }

        public Purchase(int id, string name, DateTime dateCreate)
        {
            this.Id = id;
            this.Name = name;
            this.DateCreate = dateCreate;
            this.DateEdit = dateCreate;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return ((Purchase)obj).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
