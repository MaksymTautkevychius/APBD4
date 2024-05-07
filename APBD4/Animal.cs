namespace APBD4;

public class Animal
{
    private int _id;
    private float _weight;
    private String _name,_category,_furColor;
    private List<Visits> _VisitsList;
    
    public Animal(int id, float weight, string name, string category, string furColor)
    {
        _id = id;
        _weight = weight;
        _name = name;
        _category = category;
        _furColor = furColor;
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public float Weight
    {
        get => _weight;
        set => _weight = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Category
    {
        get => _category;
        set => _category = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FurColor
    {
        get => _furColor;
        set => _furColor = value ?? throw new ArgumentNullException(nameof(value));
    }
}