using Godot;
using System;

public partial class LevelBase : Node
{
	[Export] private Marker2D _marker;
	[Export] private PackedScene _animalScene;
	
	public override void _Ready()
	{
		SignalHub.Instance.OnAnimalDied += SpawnAnimal;
		SpawnAnimal();
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnAnimalDied -= SpawnAnimal;
    }


	private void SpawnAnimal()
	{
		Animal newAnimal = _animalScene.Instantiate<Animal>();
		newAnimal.GlobalPosition = _marker.GlobalPosition;
		CallDeferred(MethodName.AddChild, newAnimal);
	}
}
