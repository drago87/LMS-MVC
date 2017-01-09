//    public class SharedRepository
//    {
//        //private ApplicationUserManager _userManager;

//        #region Users
//        public void UpdateUser(ApplicationUser user)
//        {
//            _ctx.Entry(user).State = EntityState.Modified;
//            _ctx.SaveChanges();
//        }
//        #endregion

//        #region Roles
//        public void AddUserToRole(ApplicationUser user, IdentityRole role)
//        {
//            var userStore = new UserStore<ApplicationUser>(_ctx);
//            var userManager = new UserManager<ApplicationUser>(userStore);

//            userManager.AddToRole(user.Id, role.Name);
//            _ctx.SaveChanges();
//        }
//        #endregion

//        #region Classes
//        public void AddUserToClass(ApplicationUser user, ClassUnit classunit)
//        {
//            var temp = _ctx.MyClassUnit.Single(b => b.ClassUnitID == classunit.ClassUnitID);//.Participants.Add(user);
//            var temp2 = _ctx.Users.Single(b => b.Id == user.Id);

//            temp.Participants.Add(temp2);
//        }

//        public void UpdateClassUnit(ClassUnit classunit)
//        {
//            _ctx.Entry(classunit).State = EntityState.Modified;
//            _ctx.SaveChanges();
//        }
//        #endregion

//        #region Subject
//        public Subject GetSubjectByName(string Name)
//        {
//            return _ctx.MySubjects.FirstOrDefault(b => b.SubjectName == Name);
//        }
//        #endregion

//        #region Files
//        public ICollection<Dossier> GetAllFilesInFolder(Folder Fold)
//        {
//            return _ctx.MyFiles.Where(b => b.Folder == Fold).ToList();
//        }
//        #endregion


//        public void edit(ApplicationUser user, string roleId, int classunitId)
//        {
//            var userStore = new UserStore<ApplicationUser>(_ctx);
//            var userManager = new UserManager<ApplicationUser>(userStore);
//            ApplicationUser theUser = _ctx.Users.SingleOrDefault(b => b.Id == user.Id);

//            if (roleId != "-1")
//            {
//                IdentityRole role = _ctx.Roles.SingleOrDefault(b => b.Id == roleId);
//                //AddRemoveFromRole
//                if (GetUserRolesNameAsList(theUser).Contains(role.Name))
//                {
//                    userManager.RemoveFromRole(theUser.Id, role.Name);
//                }
//                else
//                {
//                    userManager.AddToRole(theUser.Id, role.Name);
//                }
//            }

//            if (classunitId != -1)
//            {
//                ClassUnit classunit = _ctx.MyClassUnit.SingleOrDefault(b => b.ClassUnitID == classunitId);

//                //AddRemoveFromClassUnit
//                if (theUser.ClassUnits.Contains(classunit))
//                {
//                    theUser.ClassUnits.Remove(classunit);
//                    classunit.Participants.Remove(theUser);
//                }
//                else
//                {
//                    theUser.ClassUnits.Add(classunit);
//                    classunit.Participants.Add(theUser);
//                }
//            }

//            theUser.Email = user.Email;
//            theUser.UserName = user.UserName;
//            theUser.PhoneNumber = user.PhoneNumber;
//            _ctx.Entry(theUser).State = EntityState.Modified;
//            _ctx.SaveChanges();
//        }

//        public List<string> GetUserRolesNameAsList(ApplicationUser applicationUser)
//        {
//            List<string> tempRoleNames = new List<string>();

//            if (applicationUser.Roles != null && applicationUser.Roles.Count > 0)
//            {
//                foreach (var item in applicationUser.Roles)
//                {
//                    tempRoleNames.Add(GetRoleById(item.RoleId).Name);
//                }
//            }
//            else
//            {
//                tempRoleNames.Add("Not assigned a Role yet!");
//            }

//            return tempRoleNames;
//        }

//        public List<string> GetUserClassUnitsNameAsList(ApplicationUser applicationUser)
//        {
//            List<string> tempClassNames = new List<string>();


//            if (applicationUser.ClassUnits != null && applicationUser.ClassUnits.Count > 0)
//            {
//                foreach (var item in applicationUser.ClassUnits)
//                {
//                    tempClassNames.Add(item.ClassName);
//                }
//            }
//            else
//            {
//                tempClassNames.Add("Not assigned a Class unit yet!");
//            }

//            return tempClassNames;
//        }

//    }
//}