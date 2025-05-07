using VroomParts.Domain.TrackViews;

namespace VroomParts.Data.Repository.TrackViews
{
    public class ViewedCarPartRepository : Repository<ViewedCarPart>, IViewedCarPatrsRepository
    {
        public ViewedCarPartRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
