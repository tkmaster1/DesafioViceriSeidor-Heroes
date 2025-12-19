namespace Heroes.Common.Util.Entities;

public abstract class Entity
{
    public int? Code { get; set; }

    public string Name { get; set; }

    public DateTime DateCreate { get; set; }

    public DateTime? DateChange { get; set; }

    public bool Status { get; set; }
}