using BootstrapLayout.Models;

namespace BootstrapLayout.Data
{
    public class StaffRepository : IStaffRepository
    {
        private readonly AppDbContext _context;

        public StaffRepository(AppDbContext context)
        {
            _context = context;
        }
        public Staff CreateStaff(Staff staff)
        {
            _context.StaffMembers.Add(staff);
            _context.SaveChanges();
            return staff;
        }

        public void DeleteStaff(int id)
        {
            var staffToDelete = _context.StaffMembers.FirstOrDefault(s => s.Id == id);
            if (staffToDelete != null)
            {
                _context.StaffMembers.Remove(staffToDelete);
                _context.SaveChanges();
            }
        }

        public Staff EditStaff(Staff staff)
        {
            var staffToUpdate = _context.StaffMembers.FirstOrDefault(s => s.Id == staff.Id);
            if (staffToUpdate != null)
            {
                staffToUpdate.StaffNumber = staff.StaffNumber;
                staffToUpdate.StaffName = staff.StaffName;
                staffToUpdate.StaffPhoto = staff.StaffPhoto;
                staffToUpdate.StaffEmail = staff.StaffEmail;
                staffToUpdate.Department = staff.Department;
                staffToUpdate.Salary = staff.Salary;

                _context.StaffMembers.Update(staffToUpdate);
                _context.SaveChanges();
                return staffToUpdate;
            }
            return staff;
        }

        public IEnumerable<Staff> GetAllStaff()
        {
            return _context.StaffMembers.ToList();
        }

        public Staff GetStaffById(int id)
        {
            return _context.StaffMembers.FirstOrDefault(s => s.Id == id);

        }
    }
}
