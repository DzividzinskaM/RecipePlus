using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.interfaces
{
    public interface IAddRequest
    {
        public IEnumerable<AddRequest> GetAll();

    //    public IEnumerable<AddRequest> GetRequestByType(int requestTypeId);

        //public IEnumerable<Request> GetAllNew();

        public AddRequest Get(int id);

        public void Create(AddRequest request);

        public void AcceptRequest(int id);

        public void NotAcceptRequest(int id);

        public void DeleteRequest(int id);
    }
}
