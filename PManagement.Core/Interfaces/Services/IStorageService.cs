using PManagement.Entity.Authentication;
using PManagement.Entity.UserProfile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PManagement.Core.Interfaces.Services
{
    public interface IStorageService
    {
        void List();
        string Updoad(string fileName, System.IO.Stream fileStream);
    }
}