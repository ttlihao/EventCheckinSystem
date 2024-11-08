using EventCheckinSystem.Repo.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.ResponseDTO
{

    public class UserEventResponse
    {
        public int Id { get; set; } 
        public string UserID { get; set; }
        public int EventID { get; set; }   
    }
}