using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IDeleteRequest
    {
        public IEnumerable<DeleteRequest> GetAll();


        public DeleteRequest Get(int id);

        public void Accept(int id);

        public void NotAccept(int id);

        public void Create(DeleteRequest request);
    }
}
