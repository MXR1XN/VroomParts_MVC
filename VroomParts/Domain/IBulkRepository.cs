namespace VroomParts.Domain
{
	public interface IBulkRepository<T> where T : class
	{
		void CreateRange(IEnumerable<T> entities);
		void DeleteRange(IEnumerable<T> entities);
	}
}
