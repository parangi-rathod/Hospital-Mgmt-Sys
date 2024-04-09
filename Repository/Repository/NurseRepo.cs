using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class NurseRepo : INurseRepo
    {
        #region props
        public readonly AppDbContext _context;

        #endregion

        #region ctor
        public NurseRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> isNurseExists(int nurseId)
        {
            bool isExists = await _context.Users.AnyAsync(u=>u.Id.Equals(nurseId) && u.Role.Equals(RoleType.Nurse));
            return isExists;
        }

        #endregion



    }
}
