using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.CreateDTO
{
    public class UpdateRoleDTO
    {
        public string RoleId { get; set; }
        public string NewRoleName { get; set; }

        public UpdateRoleDTO(string roleId, string newRoleName)
        {
            RoleId = roleId;
            NewRoleName = newRoleName;
        }
    }
}
