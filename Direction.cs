public class Direction {
    public Location targetLoc;
    public float angle;

    public Direction(float angle, Location target)
    {
        this.angle = angle;
        this.targetLoc = target;
    }

    public void GoToTarget()
    {
        targetLoc.Display();
    }
}
