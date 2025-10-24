namespace GameNamespace;

public interface IGameObject
{
    public void Move(float dt);
    public void Draw();
    public void Reset();
}