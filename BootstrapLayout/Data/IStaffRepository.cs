using BootstrapLayout.Models;

namespace BootstrapLayout.Data
{
    public interface IStaffRepository
    {
        Staff CreateStaff(Staff staff);
        Staff EditStaff(Staff staff);
        Staff GetStaffById(int id);
        IEnumerable<Staff> GetAllStaff();
        void DeleteStaff(int id);
    }
}
