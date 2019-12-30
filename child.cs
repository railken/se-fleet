public void Main(string argument, UpdateType updateSource) 
{
    
    // var pos = Me.GetPosition();
    // Echo (pos.ToString());

    var actions = new List<Action>();
    actions.Add(new MoveToAction());

    foreach (var action in actions) {
      action.handle(this, argument);
    }
}

interface Action
{
  void handle(Program app, string argument);
}

public class MoveToAction: Action
{
  Program app;

  public void handle(Program app, string argument)
  {
    this.app = app;

    string[] coords = argument.Split(':');
    
    if (coords[0] != "moveTo") {
       return;
    }
    
    app.Echo ("Moving to... ");
    app.Echo("X: " + coords[1]);
    app.Echo("Y: " + coords[2]);
    app.Echo("Z: " + coords[3]);

    this.move(new Vector3D(
      Convert.ToDouble(coords[1]), 
      Convert.ToDouble(coords[2]), 
      Convert.ToDouble(coords[3])
    ));
  }

  public void move(Vector3D coord)
  {
    var remote = app.GridTerminalSystem.GetBlockWithName("Remote Control") as IMyRemoteControl;

    remote.SetAutoPilotEnabled(false);
    remote.ClearWaypoints();    
    remote.AddWaypoint(coord, "On my way");
    remote.SetAutoPilotEnabled(true);
  }
}

public Program()
{

}

public void Save()
{

}
