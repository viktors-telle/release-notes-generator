namespace ReleaseNotes.Generator.Domain
{
    public class EntityBase<T> where T : struct
    {
        public T Id { get; set; }
    }
}
