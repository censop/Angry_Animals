using Godot;
using System;

public partial class Cup : StaticBody2D
{
	[Export] AnimationPlayer _dieAnimation;
	public override void _Ready()
	{
		_dieAnimation.AnimationFinished += OnAnimationFinished;
	}

    private void OnAnimationFinished(StringName animName)
    {
        QueueFree();
    }

    public void Die()
	{
		_dieAnimation.Play("vanish");
	}
}
