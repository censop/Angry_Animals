using Godot;
using System;

public partial class SignalHub : Node
{
	public static SignalHub Instance {get; private set;}


	[Signal] public delegate void OnAnimalDiedEventHandler();
	[Signal] public delegate void OnAttemptMadeEventHandler();

	public override void _Ready()
	{
		Instance = this;
	}

	public static void EmitOnAnimalDied()
	{
		Instance.EmitSignal(SignalName.OnAnimalDied);
	}

	public static void EmitOnAttemptMade()
	{
		Instance.EmitSignal(SignalName.OnAttemptMade);
	}

}
