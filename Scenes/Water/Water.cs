using Godot;
using System;

public partial class Water : Area2D
{
	[Export] AudioStreamPlayer2D _splash;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
    {
		_splash.GlobalPosition = body.GlobalPosition;
        _splash.Play();

		if (body is Animal animal)
		{
			animal.Die();
		}
    }

    public override void _Process(double delta)
	{
	}

}
