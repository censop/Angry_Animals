using Godot;
using System;

public partial class Animal : RigidBody2D
{
	[Export] private Label _label;
	[Export] private AudioStreamPlayer2D _stretchSound;
	[Export] private AudioStreamPlayer2D _launchSound;
	[Export] private AudioStreamPlayer2D _kickSound;

		public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		UpdateDebug();
	}

	private void UpdateDebug()
	{
		string ds = $"SL: {Sleeping} FR: {Freeze}";
		_label.Text = ds;
	}
}
