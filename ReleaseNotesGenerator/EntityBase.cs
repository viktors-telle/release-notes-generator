namespace ReleaseNotesGenerator
{
    public class EntityBase<T> where T : struct
    {
        public T Id { get; set; }
    }
}
