﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Core.Entity
{
    public class Role
    {
        public enum UserRole
        {
            PasswordAcces = 1,
            CreatePassword=2,
            UpdatePassword=3,
            RemovePassword=4,
        }

        public static readonly UserRole[] AllRoles = (UserRole[])Enum.GetValues(typeof(UserRole));
    }
}
