using Actor;

public class CardLayoutField:BasicLayoutField<CardSystem.Card>
{
    public override bool Place(CardSystem.Card obj)
    {
        SetSize(obj.thumbnail.bounds.size);
        return base.Place(obj);
    }
}